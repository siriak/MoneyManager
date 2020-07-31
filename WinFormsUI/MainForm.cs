using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Core;

namespace WinFormsUI
{
	public partial class MainForm : Form
	{
		const string categoriesFileName = "categories.json";
		const string transactionsFileName = "transactions.json";

		private Date startDate, endDate;
		private double smoothingRatio;

		public MainForm() => InitializeComponent();
		private event Action OnFilteringUpdated = () => { };

		private void MainForm_Load(object sender, EventArgs e)
		{
			OnFilteringUpdated += RefreshList;
			OnFilteringUpdated += RefreshChart;

			var cts = new CancellationTokenSource();
			clbCategories.ItemCheck += async (o, e) => 
			{
				// No lock here because this code only executes in
				// UI thread, which means critical section cannot
				// be executed in different threads simultaneously
				cts.Cancel();
				cts = new CancellationTokenSource();
				var ct = cts.Token;

				const int debounceDelayMs = 100;
				await Task.Delay(debounceDelayMs);
				if (ct.IsCancellationRequested)
				{
					return;
				}

				OnFilteringUpdated();
			};

			dateTimePickerStart.Value = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);
			dateTimePickerEnd.Value = DateTime.Now.Date;

			LoadCategories();
			LoadTransactions();

			File.WriteAllText(categoriesFileName, StateManager.SaveCategoriesToJson());
			var formattedRecords = State.Instance.Transactions.Select(DisplayManager.FormatLedgerRecord);
			File.WriteAllLines(transactionsFileName, formattedRecords);

			RefreshCategories();
			RefreshChart();
			RefreshList();
		}

		private void LoadCategories()
		{
			if (!File.Exists(categoriesFileName))
			{
				File.WriteAllText(categoriesFileName, StateManager.SaveCategoriesToJson());
			}
			StateManager.LoadCategories(File.ReadAllText(categoriesFileName));
		}

		private void LoadTransactions()
		{
			var currentDirecory = Directory.GetCurrentDirectory();
			var filesUsb = Directory.GetFiles(currentDirecory + "/usb").Select(f => ("usb", (Stream)File.OpenRead(f)));
			var filesPb = Directory.GetFiles(currentDirecory + "/pb").Select(f => ("pb", (Stream)File.OpenRead(f)));
			StateManager.LoadTransactions(filesUsb.Concat(filesPb));
		}

		private void RefreshCategories()
		{
			var isFirstLoad = clbCategories.Items.Count == 0;

			var selectedCategories = clbCategories.CheckedItems
												  .Cast<object>()
												  .Select(clbCategories.GetItemText)
												  .ToList();

			clbCategories.Items.Clear();
			clbCategories.Items.AddRange(State.Instance.Categories.Select(c => c.Name).OrderBy(c => c).ToArray());

			foreach (var c in selectedCategories)
			{
				clbCategories.SetItemChecked(clbCategories.FindStringExact(c), true);
			}

			if (isFirstLoad)
			{
				clbCategories.SetItemChecked(0, true);
			}
		}

		private void RefreshList()
		{
			lbTransactions.Items.Clear();

			lbTransactions.Items.AddRange(
				StateHelper.GetTransactionsUnion(
					      clbCategories.CheckedItems.Cast<object>().Select(clbCategories.GetItemText),
					      startDate,
					      endDate)
				     .Select(t => (object) DisplayManager.FormatLedgerRecord(t))
				     .Reverse()
				     .ToArray());
		}

		private void RefreshChart()
		{
			chartSeriesSmoothed.Series.Clear();
			chartSeriesCumulative.Series.Clear();

			var selectedCategories = clbCategories.CheckedItems
			                                      .Cast<object>()
			                                      .Select(clbCategories.GetItemText)
			                                      .ToList();

			var smoothedSeriesToRemove = chartSeriesSmoothed.Series.Where(s => selectedCategories.All(c => c != s.Name));
			var cumulativeSeriesToRemove = chartSeriesCumulative.Series.Where(s => selectedCategories.All(c => c != s.Name));

			foreach (var s in smoothedSeriesToRemove)
			{
				chartSeriesSmoothed.Series.Remove(s);
			}

			foreach (var s in cumulativeSeriesToRemove)
			{
				chartSeriesCumulative.Series.Remove(s);
			}

			foreach (var c in selectedCategories)
			{
				if (chartSeriesSmoothed.Series.FindByName(c) is null)
				{
					var newSeries = new Series
					{
						Name = c,
						ChartType = SeriesChartType.Line
					};
					chartSeriesSmoothed.Series.Add(newSeries);
				}

				if (chartSeriesCumulative.Series.FindByName(c) is null)
				{
					var newSeries = new Series
					{
						Name = c,
						ChartType = SeriesChartType.Line
					};
					chartSeriesCumulative.Series.Add(newSeries);
				}

				var smoothedSeries = chartSeriesSmoothed.Series.FindByName(c);
				smoothedSeries.Points.Clear();

				var cumulativeSeries = chartSeriesCumulative.Series.FindByName(c);
				cumulativeSeries.Points.Clear();

				var category = State.Instance.Categories.First(category => category.Name == c);
				var smoothedTimeSeries = StateHelper.GetSmoothedTimeSeries(c, smoothingRatio);
				var cumulativeTimeSeries = StateHelper.GetCumulativeTimeSeries(c, category.Increment, category.Capacity);

				for (var date = startDate; date <= endDate; date = date.AddDays(1))
				{
					_ = smoothedSeries.Points.AddXY(date.ToString(), smoothedTimeSeries[date]);
				}

				for (var date = startDate; date <= endDate; date = date.AddDays(1))
				{
					_ = cumulativeSeries.Points.AddXY(date.ToString(), cumulativeTimeSeries[date]);
				}
			}
		}

		private void dateTimePickerStart_ValueChanged(object sender, EventArgs e)
		{
			startDate = dateTimePickerStart.Value.ToDate();
			OnFilteringUpdated();
		}

		private void dateTimePickerEnd_ValueChanged(object sender, EventArgs e)
		{
			endDate = dateTimePickerEnd.Value.ToDate();
			OnFilteringUpdated();
		}

		private void txtboxSmoothingRatio_TextChanged(object sender, EventArgs e)
		{
			if (double.TryParse(txtboxSmoothingRatio.Text, out var newSmoothingRatio)
				&& newSmoothingRatio <= 1 && newSmoothingRatio >= 0)
			{
				smoothingRatio = newSmoothingRatio;
				OnFilteringUpdated();
			}
		}
	}
}

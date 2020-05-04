using System;
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
		//
		private Date startDate, endDate;

		public MainForm() => InitializeComponent();
		private event Action OnFilteringUpdated = () => { };

		private async void MainForm_Load(object sender, EventArgs e)
		{
			State.OnStateUpdated += RefreshList;
			State.OnStateUpdated += RefreshChart;
			State.OnStateUpdated += RefreshCategories;
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

			await State.Init();
		}

		private void RefreshCategories()
		{
			var isFirstLoad = clbCategories.Items.Count == 0;

			var selectedCategories = clbCategories.CheckedItems
												  .Cast<object>()
												  .Select(clbCategories.GetItemText)
												  .ToList();

			clbCategories.Items.Clear();
			clbCategories.Items.AddRange(State.Categories.OrderBy(c => c).ToArray());

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
				State.GetTransactionsUnion(
					      clbCategories.CheckedItems.Cast<object>().Select(clbCategories.GetItemText),
					      startDate,
					      endDate)
				     .Select(t => (object) $"{t.Amount.Amount} {t.Amount.Currency}: {t.Description}")
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

				const double smoothingRatio = 0.99;
				const int increment = 100;
				const int capacity = 10000;

				var smoothedTimeSeries = State.GetSmoothedTimeSeries(c,smoothingRatio);
				var cumulativeTimeSeries = State.GetCumulativeTimeSeries(c, increment, capacity);

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
	}
}

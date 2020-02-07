using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Core;

namespace WinFormsUI
{
	public partial class MainForm : Form
	{
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

			clbCategories.ItemCheck += async (o, e) => 
			{ 
				await Task.Delay(100).ConfigureAwait(false);
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
			clbCategories.Items.AddRange(State.Categories.ToArray());

			foreach (var c in selectedCategories)
			{
				clbCategories.SetItemChecked(clbCategories.FindStringExact(c), true);
			}

			if (isFirstLoad)
			{
				clbCategories.SetItemChecked(clbCategories.FindStringExact("All"), true);
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
			chartSeries.Series.Clear();

			var selectedCategories = clbCategories.CheckedItems
			                                      .Cast<object>()
			                                      .Select(clbCategories.GetItemText)
			                                      .ToList();
			var seriesToRemove = chartSeries.Series.Where(s => selectedCategories.All(c => c != s.Name)).ToList();

			foreach (var s in seriesToRemove)
			{
				chartSeries.Series.Remove(s);
			}

			foreach (var c in selectedCategories)
			{
				if (chartSeries.Series.FindByName(c) is null)
				{
					var newSeries = new Series
					{
						Name = c,
						ChartType = SeriesChartType.Line
					};
					chartSeries.Series.Add(newSeries);
				}

				var series = chartSeries.Series.FindByName(c);
				series.Points.Clear();
				var timeSeries = State.GetTimeSeries(c, 0.99);

				for (var date = startDate; date <= endDate; date = date.AddDays(1))
				{
					_ = series.Points.AddXY(date.ToString(), timeSeries[date]);
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

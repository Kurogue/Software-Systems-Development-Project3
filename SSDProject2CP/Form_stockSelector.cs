using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SSDProject2CP
{
    public partial class Form_stockSelector : Form
    {
        public Form_stockSelector()
        {
            InitializeComponent();
        }

        //This button loads the stock forms based on the stock and dates selected
        private void button_readTicker_Click(object sender, EventArgs e)
        {
            //Allows for the files to be filtered by month, day, week and all file times
            openFileDialog_stockSelect.Filter = "All Files (*.*)|*.*|Month Files (*-Month.csv)|*-Month.csv|Week Files (*-Week.csv)|*-Week.csv|Day Files (*-Day.csv)|*-Day.csv";
            //Gets the current path directory
            string path = Directory.GetCurrentDirectory();
            //Goes back in the directory to get the stock data folder
            string tempPath = Path.GetFullPath(Path.Combine(path, @"..\..\..\..\"));
            string directoryPath = tempPath + "Stock Data";
            //Opens the file dialog box in the stock data folder
            openFileDialog_stockSelect.InitialDirectory = directoryPath;
            
            //This opens the file dialog for the user to select the stock they want
            DialogResult result = openFileDialog_stockSelect.ShowDialog();
            //Stores the file names along with its path into an array of strings
            string[] fn = openFileDialog_stockSelect.FileNames;
            //Sets empty strings for the ticker and interval variables
            string ticker = "";
            string interval = "";

            //For Each file in the array create a candlestick list, fill two charts, and show the form
            foreach (string x in fn)
            {
                //creates a list of smart candlesticks
                var list = new List<smartCandleStick>();
                //Sets the starting point for reading the csv file from the 2nd line
                var lines = File.ReadAllLines(x).Skip(1);

                //Starts reading the csv file
                foreach (var line in lines)
                {
                    //Breaks the csv line based off of commas
                    var values = line.Split(',');
                    //Makes sure the values array has a length of 9 before proceeding
                    if(values.Length == 9)
                    {
                        //Gets the ticker value from the first element in values
                        ticker = values[0];
                        //Removes the double quotation marks from the ticker variable
                        ticker = ticker.Trim(new char[] { '\"' });
                        //Gets the interval from the second index in the values array
                        interval = values[1];
                        //Removes the double quotation marks from the interval variable
                        interval = interval.Trim(new char[] { '\"' });
                        //Grabs the date from the 3rd and 4th variable and combines them and stores them into a temp string
                        string temp = values[2] + values[3];
                        //This trims off the Quotes from the dates
                        temp = temp.Trim(new char[] { '\"' });
                        //Parses the date to be converted from a string to a datetime object
                        DateTime date = DateTime.Parse(temp).Date;

                        //Checks if the date is greater than the end date and if it is to continue reading lines
                        if (date > dateTimePicker_endDate.Value) { continue; }
                        //Checks if the date is less than the start date and if it is to break out of the loop
                        if (date < dateTimePicker_startDate.Value) { break; }

                        //Creates a new smart candlestick object from the data in the csv file
                        var candleStick = new smartCandleStick(
                            date: DateTime.Parse(temp),
                            open: Math.Round(Decimal.Parse(values[4]), 2),
                            close: Math.Round(Decimal.Parse(values[7]), 2),
                            high: Math.Round(Decimal.Parse(values[5]), 2),
                            low: Math.Round(Decimal.Parse(values[6]), 2),
                            volume: long.Parse(values[8])
                        );
                        //Adds the smart candlestick data into the List with the newest dates first
                        list.Add(candleStick);
                    }
                }

                //Creates a chart title variable from the ticker and the interval variable
                string chartTitle = ticker +" - "+interval;
                //creates a new form
                Form_stockCharts stockCharts = new Form_stockCharts();
                //Sets the Form name the path of the stock file
                stockCharts.Text = x;

                //Clears the chart from the new form
                stockCharts.chart_candleSticks.Series.Clear();
                //Sets the chart series name as the chart title
                Series stock = new Series(chartTitle);
                stockCharts.chart_candleSticks.Series.Add(stock);

                //Sets the chart type as candlestick
                stockCharts.chart_candleSticks.Series[chartTitle].ChartType = SeriesChartType.Candlestick;

                //Setup for the candlestick chart to show the openclose, setting the width, and the price colors
                stockCharts.chart_candleSticks.Series[chartTitle]["OpenCloseStyle"] = "Triangle";
                stockCharts.chart_candleSticks.Series[chartTitle]["ShowOpenClose"] = "Both";
                stockCharts.chart_candleSticks.Series[chartTitle]["PointWidth"] = "1.0";
                stockCharts.chart_candleSticks.Series[chartTitle]["PriceUpColor"] = "Green";
                stockCharts.chart_candleSticks.Series[chartTitle]["PriceDownColor"] = "Red";

                //Stores the highest and lowest stocks in the candlestick list as variables
                try
                {
                    decimal lowestStock = list.Min(candleStick => candleStick.low);
                    decimal highestStock = list.Max(candleStick => candleStick.high);

                    //Sets the maximum and minum for the Y axis of the chart so the candle sticks are easier to see
                    stockCharts.chart_candleSticks.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(lowestStock) - 1;
                    stockCharts.chart_candleSticks.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(highestStock) + 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Date out of range.");
                    Console.WriteLine(ex.Message);
                    break;
                }

                //Populate the chart with the candlesticks from the list
                stockCharts.chart_candleSticks.Series[chartTitle].XValueMember = "date";
                stockCharts.chart_candleSticks.Series[chartTitle].YValueMembers = "high, low, open, close";
                stockCharts.chart_candleSticks.DataManipulator.IsStartFromFirst = true;

                //Sets the data source for the candlestick chart as the candlestick list and binds the data
                stockCharts.chart_candleSticks.DataSource = list;
                stockCharts.chart_candleSticks.DataBind();

                //Sets the border color and color of each indivudual candle stick to green or red if the stock went up or down accordingly
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].close > list[i].open)
                    {
                        stock.Points[i].BorderColor = Color.Green;
                        stock.Points[i].Color = Color.Green;
                    }
                    else if (list[i].close < list[i].open)
                    {
                        stock.Points[i].BorderColor = Color.Red;
                        stock.Points[i].Color = Color.Red;
                    }
                }

                //Clears the volume chart and sets the title of the volume chart to the chart title
                stockCharts.chart_stockVolume.Series.Clear();
                Series volume = new Series(chartTitle);
                stockCharts.chart_stockVolume.Series.Add(volume);
                
                //Sets the chart type to column
                stockCharts.chart_stockVolume.Series[chartTitle].ChartType = SeriesChartType.Column;
                
                //Sets the width of the bars in the column chart to 1
                stockCharts.chart_stockVolume.Series[chartTitle]["PointWidth"] = "1.0";

                //Populate the chart's x and y axis with dates and volumes from the candlestick list
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    string temp = list[i].date.ToShortDateString();
                    volume.Points.AddXY(temp, list[i].volume);
                }

                //Sets the data source for the column chart as the candlestick list
                stockCharts.chart_stockVolume.DataManipulator.IsStartFromFirst = true;
                stockCharts.chart_stockVolume.DataSource = list;

                //Transfers the dates from the original form to the new forms so the user knows what dates they selected previously
                //This way they can update their charts accordingly
                stockCharts.dateTimePicker_startDateForm2.Value = dateTimePicker_startDate.Value;
                stockCharts.dateTimePicker_endDateForm2.Value = dateTimePicker_endDate.Value;

                //Shows the form for the charts
                stockCharts.Show();
            }
        }
    }
}

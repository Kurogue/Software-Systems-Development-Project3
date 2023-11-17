using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace SSDProject2CP
{
    public partial class Form_stockCharts : Form
    {
        //Form Local variable to store the candlesticks of the current form
        List<smartCandleStick> candleSticks = new List<smartCandleStick>();
        List<recognizer> patterns = new List<recognizer>();
        List<string> currentAnnotations = new List<string>();
        public Form_stockCharts()
        {
            InitializeComponent();
        }

        //When the form is initalized the current candlestick is stored in the form local list variable
        private void Form_stockCharts_Load(object sender, EventArgs e)
        {
            //Gets ready to read the stock file and skips the first line
            var lines = File.ReadAllLines(this.Text).Skip(1);

            //Reads all the lines in the csv file
            foreach (var line in lines)
            {
                //Reads the string until a comma is encountered
                var values = line.Split(',');
                //Checks to see the length of the line gathered and makes sure there is 8 lines to be read
                if (values.Length == 9)
                {
                    //Grabs the date from the csv file and stores it in a temp variable
                    string temp = values[2] + values[3];
                    //This trims off the Quotes from the dates
                    temp = temp.Trim(new char[] { '\"' });
                    //Parses the date to be converted from a string to a datetime object
                    DateTime date = DateTime.Parse(temp).Date;

                    //Checks if the date is greater than the end date and if it is to continue reading lines
                    if (date > dateTimePicker_endDateForm2.Value) { continue; }
                    //Checks if the date is less than the start date and if it is to break out of the loop
                    if (date < dateTimePicker_startDateForm2.Value) { break; }

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
                    candleSticks.Add(candleStick);
                }
            }
            initalizePatterns();
            comboBox_stockPatterns.Items.Clear();
            for (int i = 0; i < patterns.Count; i++)
            {
                comboBox_stockPatterns.Items.Add(patterns[i].name);
            }

        }

        //This fucntion updates the candlestick and volume charts when the dates are updated
        private void button_stockUpdate_Click(object sender, EventArgs e)
        {
            //Gets the current file path from the form title/text
            string selectedStock = this.Text;

            //Gets ready to read the stock file and skips the first line
            var lines = File.ReadAllLines(selectedStock).Skip(1);

            //Initalizies local and global varibales for updating the chart with
            string ticker = "";
            string interval = "";

            //Clears the old charts so it can be repopulated
            candleSticks.Clear();

            //Reads the lines from the csv file
            foreach (var line in lines)
            {
                //Splits the line  by commas and stores it into the variable
                var values = line.Split(',');
                //Checks to see if the values array is 9 and if it is, it proceeds
                if (values.Length == 9)
                {
                    //Sets the ticker name from the first index of the array
                    ticker = values[0];
                    //Trims the double quotations from the string so it can be properly displayed
                    ticker = ticker.Trim(new char[] { '\"' });
                    //Gets the Interval of the stock from the second index of the array and stores it for later
                    interval = values[1];
                    //Trims the double quotations from the string so it can be properly displayed
                    interval = interval.Trim(new char[] { '\"' });
                    //Gets the date variable and stores it into a string variable
                    string temp = values[2] + values[3];
                    //This trims off the Quotes from the dates
                    temp = temp.Trim(new char[] { '\"' });
                    //Parses the date to be converted from a string to a datetime object
                    DateTime date = DateTime.Parse(temp).Date;

                    //Checks if the date is greater than the end date and if it is to continue reading lines
                    if (date > dateTimePicker_endDateForm2.Value) { continue; }
                    //Checks if the date is less than the start date and if it is to break out of the loop
                    if (date < dateTimePicker_startDateForm2.Value) { break; }

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
                    candleSticks.Add(candleStick);
                }
            }
            //Sets a chart title variable to the ticker plus the interval
            string chartTitle = ticker + " - " + interval;
            
            //Clears the series from the current chart and sets it as the chart title variable
            chart_candleSticks.Series.Clear();
            Series stock = new Series(chartTitle);
            chart_candleSticks.Series.Add(stock);

            //Sets the chart style to candlesticks
            chart_candleSticks.Series[chartTitle].ChartType = SeriesChartType.Candlestick;

            //Setup for the candlestick chart to show the openclose, setting the width, and the price colors
            chart_candleSticks.Series[chartTitle]["OpenCloseStyle"] = "Triangle";
            chart_candleSticks.Series[chartTitle]["ShowOpenClose"] = "Both";
            chart_candleSticks.Series[chartTitle]["PointWidth"] = "1.0";
            chart_candleSticks.Series[chartTitle]["PriceUpColor"] = "Green";
            chart_candleSticks.Series[chartTitle]["PriceDownColor"] = "Red";

            //Stores the highest and lowest stocks in the candlestick list as variables
            try
            {
                decimal lowestStock = candleSticks.Min(candleStick => candleStick.low);
                decimal highestStock = candleSticks.Max(candleStick => candleStick.high);

                //Sets the maximum and minum for the Y axis of the chart so the candle sticks are easier to see
                chart_candleSticks.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(lowestStock) - 1;
                chart_candleSticks.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(highestStock) + 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Date out of range.");
                Console.WriteLine(ex.Message);
                return;
            }

            //Populate the chart with the candlesticks from the list
            chart_candleSticks.Series[chartTitle].XValueMember = "date";
            chart_candleSticks.Series[chartTitle].YValueMembers = "high, low, open, close";
            chart_candleSticks.DataManipulator.IsStartFromFirst = true;

            //Sets the data source for the candlestick chart as the candlestick list and binds the data
            chart_candleSticks.DataSource = candleSticks;
            chart_candleSticks.DataBind();

            //Sets the border color and color of each indivudual candle stick to green or red if the stock went up or down accordingly
            for (int i = 0; i < candleSticks.Count; i++)
            {
                if (candleSticks[i].close > candleSticks[i].open)
                {
                    stock.Points[i].BorderColor = Color.Green;
                    stock.Points[i].Color = Color.Green;
                }
                else if (candleSticks[i].close < candleSticks[i].open)
                {
                    stock.Points[i].BorderColor = Color.Red;
                    stock.Points[i].Color = Color.Red;
                }
            }

            //Clears the old stock series/name from the chart and sets the chart title to the series name
            chart_stockVolume.Series.Clear();
            Series volume = new Series(chartTitle);
            chart_stockVolume.Series.Add(volume);

            //Sets the chart type to column
            chart_stockVolume.Series[chartTitle].ChartType = SeriesChartType.Column;

            //Sets the width of the bars in the column chart to 1
            chart_stockVolume.Series[chartTitle]["PointWidth"] = "1.0";

            //Populate the chart's x and y axis with dates and volumes from the candlestick list
            for (int i = candleSticks.Count - 1; i >= 0; i--)
            {
                string temp = candleSticks[i].date.ToShortDateString();
                volume.Points.AddXY(temp, candleSticks[i].volume);
            }

            //Sets the data source for the column chart as the candlestick list
            chart_stockVolume.DataManipulator.IsStartFromFirst = true;
            chart_stockVolume.DataSource = candleSticks;
            //Calls the update arrows function to make sure the annotations are correctly updated when the dates are updated
            //Clears the current annotations if the None option is selected on the combobox
            for (int i = chart_candleSticks.Annotations.Count - 1; i >= 0; i--)
            {
                chart_candleSticks.Annotations.RemoveAt(i);
            }
            currentAnnotations.Clear();
            updateArrows();
        }

        //Combobox to show the selection of types of candlesticks to choose from
        private void comboBox_stockPatterns_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Calls the update arrow function to show the arrows on the chart
            updateArrows();
        }

        //This function updates the arrow annotations based on what is selected in the combobox
        private void updateArrows()
        {
            if(comboBox_stockPatterns.SelectedIndex == 0)
            {
                if (currentAnnotations.Contains(patterns[0].name))
                    return;
                List <int> bullishPatterns = patterns[0].recognize(candleSticks);
                currentAnnotations.Add(patterns[0].name);
                createAnnotations(bullishPatterns, patterns[0].name);
            }
            if(comboBox_stockPatterns.SelectedIndex == 1)
            {
                if (currentAnnotations.Contains(patterns[1].name))
                    return;
                List<int> bearishPatterns = patterns[1].recognize(candleSticks);
                currentAnnotations.Add(patterns[1].name);
                createAnnotations(bearishPatterns, patterns[1].name);

            }
            if (comboBox_stockPatterns.SelectedIndex == 2)
            {
                if (currentAnnotations.Contains(patterns[2].name))
                    return;
                List<int> neutralPatterns = patterns[2].recognize(candleSticks);
                currentAnnotations.Add(patterns[2].name);
                createAnnotations(neutralPatterns, patterns[2].name);

            }
            if (comboBox_stockPatterns.SelectedIndex == 3)
            {
                if (currentAnnotations.Contains(patterns[3].name))
                    return;
                List <int> marubozuPatterns = patterns[3].recognize(candleSticks);
                currentAnnotations.Add(patterns[3].name);
                createAnnotations(marubozuPatterns, patterns[3].name);

            }
            if (comboBox_stockPatterns.SelectedIndex == 4)
            {
                if (currentAnnotations.Contains(patterns[4].name))
                    return;
                List<int> dojiPatterns = patterns[4].recognize(candleSticks);
                currentAnnotations.Add(patterns[4].name);
                createAnnotations(dojiPatterns, patterns[4].name);
            }
            if (comboBox_stockPatterns.SelectedIndex == 5)
            {
                if (currentAnnotations.Contains(patterns[5].name))
                    return;
                List<int> dragonFlyDojiPatterns = patterns[5].recognize(candleSticks);
                currentAnnotations.Add(patterns[5].name);
                createAnnotations(dragonFlyDojiPatterns, patterns[5].name);
            }
            if (comboBox_stockPatterns.SelectedIndex == 6)
            {
                if (currentAnnotations.Contains(patterns[6].name))
                    return;
                List<int> gravestoneDojiPatterns = patterns[6].recognize(candleSticks);
                currentAnnotations.Add(patterns[6].name);
                createAnnotations(gravestoneDojiPatterns, patterns[6].name);
            }
            if (comboBox_stockPatterns.SelectedIndex == 7)
            {
                if (currentAnnotations.Contains(patterns[7].name))
                    return;
                List<int> hammerPatterns = patterns[7].recognize(candleSticks);
                currentAnnotations.Add(patterns[7].name);
                createAnnotations(hammerPatterns, patterns[7].name);
            }
            if (comboBox_stockPatterns.SelectedIndex == 8)
            {
                if (currentAnnotations.Contains(patterns[8].name))
                    return;
                List<int> invertedHammerPatterns = patterns[8].recognize(candleSticks);
                currentAnnotations.Add(patterns[8].name);
                createAnnotations(invertedHammerPatterns, patterns[8].name);
            }

            //Move back instead of forward from the index so the annotation is correct
            if (comboBox_stockPatterns.SelectedIndex == 9)
            {
                if (currentAnnotations.Contains(patterns[9].name))
                    return;
                List<int> engulfingPatterns = patterns[9].recognize(candleSticks);
                currentAnnotations.Add(patterns[9].name);
                createEngulfAnnotations(engulfingPatterns, patterns[9].name);
            }
            if (comboBox_stockPatterns.SelectedIndex == 10)
            {
                if (currentAnnotations.Contains(patterns[10].name))
                    return;
                List<int> engulfishBullishPatterns = patterns[10].recognize(candleSticks);
                currentAnnotations.Add(patterns[10].name);
                createEngulfAnnotations(engulfishBullishPatterns, patterns[10].name);
            }
            if (comboBox_stockPatterns.SelectedIndex == 11)
            {
                if (currentAnnotations.Contains(patterns[11].name))
                    return;
                List<int> engulfishBearishPatterns = patterns[11].recognize(candleSticks);
                currentAnnotations.Add(patterns[11].name);
                createEngulfAnnotations(engulfishBearishPatterns, patterns[11].name);
            }
            if (comboBox_stockPatterns.SelectedIndex == 12)
            {
                if (currentAnnotations.Contains(patterns[12].name))
                    return;
                List<int> haramisPatterns = patterns[12].recognize(candleSticks);
                currentAnnotations.Add(patterns[12].name);
                createEngulfAnnotations(haramisPatterns, patterns[12].name);
            }
            if (comboBox_stockPatterns.SelectedIndex == 13)
            {
                if (currentAnnotations.Contains(patterns[13].name))
                    return;
                List<int> haramisBullishPatterns = patterns[13].recognize(candleSticks);
                currentAnnotations.Add(patterns[13].name);
                createEngulfAnnotations(haramisBullishPatterns, patterns[13].name);
            }
            if (comboBox_stockPatterns.SelectedIndex == 14)
            {
                if (currentAnnotations.Contains(patterns[14].name))
                    return;
                List<int> haramisBearishPatterns = patterns[14].recognize(candleSticks);
                currentAnnotations.Add(patterns[14].name);
                createEngulfAnnotations(haramisBearishPatterns, patterns[14].name);
            }
            if (comboBox_stockPatterns.SelectedIndex == 15)
            {
                if (currentAnnotations.Contains(patterns[15].name))
                    return;
                List<int> peakPatterns = patterns[15].recognize(candleSticks);
                currentAnnotations.Add(patterns[15].name);
                createMountainAnnotations(peakPatterns, patterns[15].name); 
            }
            if (comboBox_stockPatterns.SelectedIndex == 16)
            {
                if (currentAnnotations.Contains(patterns[16].name))
                    return;
                List<int> valleyPatterns = patterns[16].recognize(candleSticks);
                currentAnnotations.Add(patterns[16  ].name);
                createMountainAnnotations(valleyPatterns, patterns[16].name);
            }
        }

        //Button to clear all the annotations from the chart
        private void button_clearAnnotations_Click(object sender, EventArgs e)
        {
            //Clears the current annotations if the None option is selected on the combobox
            for (int i = chart_candleSticks.Annotations.Count - 1; i >= 0; i--)
            {
                chart_candleSticks.Annotations.RemoveAt(i);
            }
            currentAnnotations.Clear();
        }
        private void initalizePatterns()
        {
            patterns.Add(new bullishRecognizer());
            patterns.Add(new bearishRecognizer());
            patterns.Add(new neutralRecognizer());
            patterns.Add(new marubozuRecognizer());
            patterns.Add(new dojiRecognizer());
            patterns.Add(new dragonFlyDojiRecognizer());
            patterns.Add(new gravestoneDojiRecognizer());
            patterns.Add(new hammerRecognizer());
            patterns.Add(new invertedHammerRecognizer());
            patterns.Add(new engulfingRecognizer());
            patterns.Add(new engulfingBullishRecognizer());
            patterns.Add(new engulfingBearishRecognizer());
            patterns.Add(new haramisRecognizer());
            patterns.Add(new haramisBullishRecognizer());
            patterns.Add(new haramisBearishRecognizer());
            patterns.Add(new peakRecognizer());
            patterns.Add(new valleyRecognizer());
        }

        private void createAnnotations(List<int> patternRecs, string name)
        {
            for (int i = 0; i < patternRecs.Count; i++)
            {
                RectangleAnnotation rectangleAnnotation = new RectangleAnnotation();
                rectangleAnnotation.AnchorDataPoint = chart_candleSticks.Series[0].Points[patternRecs[i]];

                double yLow = chart_candleSticks.ChartAreas[0].AxisY.ValueToPixelPosition((double)candleSticks[patternRecs[i]].low);
                double yHigh = chart_candleSticks.ChartAreas[0].AxisY.ValueToPixelPosition((double)candleSticks[patternRecs[i]].high);
                double height = Math.Max(yHigh, yLow) - Math.Min(yHigh, yLow);
                height /= 2.95;

                // Set the rectangle's coordinates to cover the candlestick
                rectangleAnnotation.AxisX = chart_candleSticks.ChartAreas[0].AxisX;
                rectangleAnnotation.Y = (double)candleSticks[patternRecs[i]].high + 0.5;
                rectangleAnnotation.Width = (chart_candleSticks.Series[0].Points[1].XValue - chart_candleSticks.Series[0].Points[0].XValue) * 0.6;
                rectangleAnnotation.Height = height;

                // Set appearance properties
                rectangleAnnotation.LineColor = Color.Gold; // Set the border color
                rectangleAnnotation.BackColor = Color.Transparent;

                // Add the annotation to the chart
                chart_candleSticks.Annotations.Add(rectangleAnnotation);

                TextAnnotation textAnnotation = new TextAnnotation();
                textAnnotation.Text = name; // Replace with the desired text
                textAnnotation.ForeColor = Color.Black;
                textAnnotation.AnchorDataPoint = chart_candleSticks.Series[0].Points[patternRecs[i]];
                textAnnotation.X = rectangleAnnotation.X;

                // Add the text annotation to the chart
                chart_candleSticks.Annotations.Add(textAnnotation);

            }
        }
        private void createEngulfAnnotations(List<int> patternRecs, string name)
        {
            for (int i = 0; i < patternRecs.Count; i++)
            {
                RectangleAnnotation rectangleAnnotation = new RectangleAnnotation();
                rectangleAnnotation.AnchorDataPoint = chart_candleSticks.Series[0].Points[patternRecs[i]];

                double yLow = chart_candleSticks.ChartAreas[0].AxisY.ValueToPixelPosition((double)candleSticks[patternRecs[i]].low);
                double yHigh = chart_candleSticks.ChartAreas[0].AxisY.ValueToPixelPosition((double)candleSticks[patternRecs[i]].high);
                double height = Math.Max(yHigh, yLow) - Math.Min(yHigh, yLow);
                height /= 2.95;

                // Set the rectangle's coordinates to cover the candlestick
                rectangleAnnotation.AxisX = chart_candleSticks.ChartAreas[0].AxisX;
                rectangleAnnotation.Y = (double)candleSticks[patternRecs[i]].high + 0.5;
                rectangleAnnotation.Width = (chart_candleSticks.Series[0].Points[1].XValue - chart_candleSticks.Series[0].Points[0].XValue) * 0.6;
                rectangleAnnotation.Height = height;

                // Set appearance properties
                rectangleAnnotation.LineColor = Color.Gold; // Set the border color
                rectangleAnnotation.BackColor = Color.Transparent;
                
                
                RectangleAnnotation rectangleAnnotation1 = new RectangleAnnotation();
                rectangleAnnotation1.AnchorDataPoint = chart_candleSticks.Series[0].Points[patternRecs[i]-1];

                double yLow1 = chart_candleSticks.ChartAreas[0].AxisY.ValueToPixelPosition((double)candleSticks[patternRecs[i]-1].low);
                double yHigh1 = chart_candleSticks.ChartAreas[0].AxisY.ValueToPixelPosition((double)candleSticks[patternRecs[i]-1].high);
                double height1 = Math.Max(yHigh1, yLow1) - Math.Min(yHigh1, yLow1);
                height1 /= 2.95;

                // Set the rectangle's coordinates to cover the candlestick
                rectangleAnnotation1.AxisX = chart_candleSticks.ChartAreas[0].AxisX;
                rectangleAnnotation1.Y = (double)candleSticks[patternRecs[i]-1].high + 0.5;
                rectangleAnnotation1.Width = (chart_candleSticks.Series[0].Points[1].XValue - chart_candleSticks.Series[0].Points[0].XValue) * 0.6;
                rectangleAnnotation1.Height = height1;

                // Set appearance properties
                rectangleAnnotation1.LineColor = Color.Gold; // Set the border color
                rectangleAnnotation1.BackColor = Color.Transparent;

                // Add the annotation to the chart
                chart_candleSticks.Annotations.Add(rectangleAnnotation);

                // Add the annotation to the chart
                chart_candleSticks.Annotations.Add(rectangleAnnotation1);


                TextAnnotation textAnnotation = new TextAnnotation();
                textAnnotation.Text = name; // Replace with the desired text
                textAnnotation.ForeColor = Color.Black;
                textAnnotation.AnchorDataPoint = chart_candleSticks.Series[0].Points[patternRecs[i]];
                textAnnotation.X = rectangleAnnotation.X;

                // Add the text annotation to the chart
                TextAnnotation textAnnotation1 = new TextAnnotation();
                textAnnotation1.Text = name; // Replace with the desired text
                textAnnotation1.ForeColor = Color.Black;
                textAnnotation1.AnchorDataPoint = chart_candleSticks.Series[0].Points[patternRecs[i]-1];
                textAnnotation1.X = rectangleAnnotation1.X;

                // Add the text annotation to the chart
                chart_candleSticks.Annotations.Add(textAnnotation);
                chart_candleSticks.Annotations.Add(textAnnotation1);

            }
        }

        private void createMountainAnnotations(List<int> patternRecs, string name)
        {
            for (int i = 0; i < patternRecs.Count; i++)
            {
                RectangleAnnotation rectangleAnnotation = new RectangleAnnotation();
                rectangleAnnotation.AnchorDataPoint = chart_candleSticks.Series[0].Points[patternRecs[i]];

                double yLow = chart_candleSticks.ChartAreas[0].AxisY.ValueToPixelPosition((double)candleSticks[patternRecs[i]].low);
                double yHigh = chart_candleSticks.ChartAreas[0].AxisY.ValueToPixelPosition((double)candleSticks[patternRecs[i]].high);
                double height = Math.Max(yHigh, yLow) - Math.Min(yHigh, yLow);
                height /= 2.95;

                // Set the rectangle's coordinates to cover the candlestick
                rectangleAnnotation.AxisX = chart_candleSticks.ChartAreas[0].AxisX;
                rectangleAnnotation.Y = (double)candleSticks[patternRecs[i]].high + 0.5;
                rectangleAnnotation.Width = (chart_candleSticks.Series[0].Points[1].XValue - chart_candleSticks.Series[0].Points[0].XValue) * 0.6;
                rectangleAnnotation.Height = height;

                // Set appearance properties
                rectangleAnnotation.LineColor = Color.Gold; // Set the border color
                rectangleAnnotation.BackColor = Color.Transparent;


                RectangleAnnotation rectangleAnnotation1 = new RectangleAnnotation();
                rectangleAnnotation1.AnchorDataPoint = chart_candleSticks.Series[0].Points[patternRecs[i] - 1];

                double yLow1 = chart_candleSticks.ChartAreas[0].AxisY.ValueToPixelPosition((double)candleSticks[patternRecs[i] - 1].low);
                double yHigh1 = chart_candleSticks.ChartAreas[0].AxisY.ValueToPixelPosition((double)candleSticks[patternRecs[i] - 1].high);
                double height1 = Math.Max(yHigh1, yLow1) - Math.Min(yHigh1, yLow1);
                height1 /= 2.95;

                // Set the rectangle's coordinates to cover the candlestick
                rectangleAnnotation1.AxisX = chart_candleSticks.ChartAreas[0].AxisX;
                rectangleAnnotation1.Y = (double)candleSticks[patternRecs[i] - 1].high + 0.5;
                rectangleAnnotation1.Width = (chart_candleSticks.Series[0].Points[1].XValue - chart_candleSticks.Series[0].Points[0].XValue) * 0.6;
                rectangleAnnotation1.Height = height1;

                // Set appearance properties
                rectangleAnnotation1.LineColor = Color.Gold; // Set the border color
                rectangleAnnotation1.BackColor = Color.Transparent;


                RectangleAnnotation rectangleAnnotation2 = new RectangleAnnotation();
                rectangleAnnotation2.AnchorDataPoint = chart_candleSticks.Series[0].Points[patternRecs[i] - 2];

                double yLow2 = chart_candleSticks.ChartAreas[0].AxisY.ValueToPixelPosition((double)candleSticks[patternRecs[i] - 2].low);
                double yHigh2 = chart_candleSticks.ChartAreas[0].AxisY.ValueToPixelPosition((double)candleSticks[patternRecs[i] - 2].high);
                double height2 = Math.Max(yHigh2, yLow2) - Math.Min(yHigh2, yLow2);
                height2 /= 2.95;

                // Set the rectangle's coordinates to cover the candlestick
                rectangleAnnotation2.AxisX = chart_candleSticks.ChartAreas[0].AxisX;
                rectangleAnnotation2.Y = (double)candleSticks[patternRecs[i] - 2].high + 0.5;
                rectangleAnnotation2.Width = (chart_candleSticks.Series[0].Points[1].XValue - chart_candleSticks.Series[0].Points[0].XValue) * 0.6;
                rectangleAnnotation2.Height = height2;

                // Set appearance properties
                rectangleAnnotation2.LineColor = Color.Gold; // Set the border color
                rectangleAnnotation2.BackColor = Color.Transparent;


                // Add the annotation to the chart
                chart_candleSticks.Annotations.Add(rectangleAnnotation);

                // Add the annotation to the chart
                chart_candleSticks.Annotations.Add(rectangleAnnotation1);

                // Add the annotation to the chart
                chart_candleSticks.Annotations.Add(rectangleAnnotation2);


                TextAnnotation textAnnotation = new TextAnnotation();
                textAnnotation.Text = name; 
                textAnnotation.ForeColor = Color.Black;
                textAnnotation.AnchorDataPoint = chart_candleSticks.Series[0].Points[patternRecs[i]];
                textAnnotation.X = rectangleAnnotation.X;

                // Add the text annotation to the chart
                TextAnnotation textAnnotation1 = new TextAnnotation();
                textAnnotation1.Text = name; 
                textAnnotation1.ForeColor = Color.Black;
                textAnnotation1.AnchorDataPoint = chart_candleSticks.Series[0].Points[patternRecs[i] - 1];
                textAnnotation1.X = rectangleAnnotation1.X;

                TextAnnotation textAnnotation2 = new TextAnnotation();
                textAnnotation2.Text = name;
                textAnnotation2.ForeColor = Color.Black;
                textAnnotation2.AnchorDataPoint = chart_candleSticks.Series[0].Points[patternRecs[i] - 2];
                textAnnotation2.X = rectangleAnnotation2.X;

                // Add the text annotation to the chart
                chart_candleSticks.Annotations.Add(textAnnotation);
                chart_candleSticks.Annotations.Add(textAnnotation1);
                chart_candleSticks.Annotations.Add(textAnnotation2);

            }
        }
    }
}

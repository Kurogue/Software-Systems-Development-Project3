using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSDProject2CP
{
    //Candlestick class so I can populate the datagrid view and candlestick and volume charts
    public class aCandleStick
    {
        //Variables for the candlestick class
        public Decimal open { get; set; }
        public Decimal close { get; set; }
        public Decimal high { get; set; }
        public Decimal low { get; set; }
        public long volume { get; set; }
        public DateTime date { get; set; }

        //Default Constructor
        public aCandleStick() { }

        //Constructor for the class
        public aCandleStick(DateTime date, decimal open, decimal close, Decimal high, Decimal low, long volume)
        {
            this.date = date;
            this.open = open;
            this.close = close;
            this.high = high;
            this.low = low;
            this.volume = volume;
        }
    }
    
    //Inherited Smart candlestick class to store the properies of the candlesticks
    public class smartCandleStick : aCandleStick
    {
        public Decimal range { get; set; }
        public Decimal bodyRange { get; set; }
        public Decimal topPrice { get; set; }
        public Decimal bottomPrice { get; set; }
        public Decimal topTail { get; set; }
        public Decimal bottomTail { get; set; }
        
        //Properties for the candlesticks
        public bool Bullish {  get; set; }
        public bool Bearish {  get; set; }
        public bool Neutral { get; set; }
        public bool Marubozu { get; set; }
        public bool Doji { get; set; }
        public bool DragonFlyDoji { get; set; }
        public bool GravestoneDoji {  get; set; }
        public bool Hammer { get; set; }
        public bool InvertedHammer { get; set; }

        //Smart candlestick constructor that inherites from the original candle stick constructor
        public smartCandleStick(DateTime date, decimal open, decimal close, Decimal high, Decimal low, long volume) : base(date, open, close, high, low, volume) 
        {
            //Methods to initalize the properties of the candlestick being created
            initHigherProperties();
            isProperties();
        }
        //Initalize properties for the smart candlestick variables
        public void initHigherProperties()
        {
            range = Math.Abs(high - low);
            bodyRange = Math.Abs(open - close);
            topPrice = Math.Max(open, close);
            bottomPrice = Math.Min(open, close);
            topTail = high - Math.Max(open, close);
            bottomTail = Math.Min(open, close) - low;
        }
        //Properties function that calls and initalizes each of the properties of the smart candlestick
        public void isProperties()
        {
            isBullish();
            isBearish();
            isNeutral();
            isMarubozu();
            isDoji();
            isDragonFlyDoji();
            isGravestoneDoji();
            isHammer();
            isInvertedHammer();
        }

        //Checks to see if the smart candlestick is bulliush candlestick
        public void isBullish()
        {
            if(close > open)
            {
                Bullish = true;
            }
            else
            {
                Bullish = false;
            }
        }
        //Checks to see if the smart candlestick is bearish candlestick
        public void isBearish()
        {
            if(close < open)
            {
                Bearish = true;
            }
            else
            {
                Bearish = false;
            }    
        }
        //Checks to see if the smart candle stick is neutral candlestick
        public void isNeutral()
        {
            if(open == close)
            {
                Neutral = true;
            }
            else
            {
                Neutral= false;
            }
        }
        //Checks to see if the smart candlestick is a marubozu candlestick
        public void isMarubozu()
        {
            if(open == high && close == low)
            {
                Marubozu = true;
                Bullish = false;
                Bearish = true;
                Neutral = false;
            }
            else if(open == low && close == high)
            {
                Marubozu = true;
                Bullish = true;
                Bearish = false;
                Neutral = false;
            }
            else
            {
                Marubozu = false;
            }
        }
        //Checks to see if the smart candlestic is a doji candlestick
        public void isDoji()
        {
            if(open == close && (open != high || open != low))
            {
                Doji = true;
                Neutral = true;
                Bullish = false;
                Bearish = false;
            }
            else
            {
                Doji = false;
            }
        }
        //Checks to see if the smart cnadlestick is a dragon fly doji candlestick
        public void isDragonFlyDoji()
        {
            if (open == close && open == high && close == high)
            {
                DragonFlyDoji = true;
                Neutral = false;
                Bullish = true;
                Bearish = false;
            }
            else
            {
                DragonFlyDoji = false;
            }
        }
        //Checks to see if the smart candlestick is a gravestone doji candlestick
        public void isGravestoneDoji()
        {
            if (open == close && open == low && close == low)
            {
                GravestoneDoji = true;
                Neutral = false;
                Bullish = false;
                Bearish = true;
            }
            else
            {
                GravestoneDoji = false;
            }
        }
        //Checks to see if the smart candlestick is a hammer candlestick
        public void isHammer()
        {
            if(bottomTail > bodyRange*2 && Convert.ToDouble(topTail) < (Convert.ToDouble(range) * .25))
            {
                Hammer = true;
                Bullish = true;
                Neutral = false;
                Bearish = false;
            }
            else
            {
                Hammer = false;
            }
        }
        //Checks to see if the smart candlestick is an inverted hammer candlestick
        public void isInvertedHammer()
        {
            if(topTail > bodyRange*2 && Convert.ToDouble(bottomTail) < (Convert.ToDouble(range) * .25))
            {
                InvertedHammer = true;
                Bullish = true;
                Bearish = false;
                Neutral = false;
            }
            else
            {
                InvertedHammer = false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace SSDProject2CP
{
    public abstract class recognizer
    {
        public string name { get; set; }
        public int patternSize {get; set; }

        public recognizer(string n, int size)
        {
            name = n;
            patternSize = size;
        }

        public List <int> recognize(List<smartCandleStick> LSCS) 
        {
            List<int> result = new List<int>();
            for (int i = patternSize-1; i < LSCS.Count; i++)
            {
                List <smartCandleStick> pattern = LSCS.GetRange(i-patternSize+1, patternSize);
                if (recognizePattern(pattern))
                    result.Add(i);
            }
            return result;
        }
        public abstract bool recognizePattern(List <smartCandleStick> lscs);
    }

    public class bullishRecognizer : recognizer
    {
        public bullishRecognizer() : base ("Bullish", 1) { }
        public override bool recognizePattern(List<smartCandleStick> lscs)
        {
            return lscs[0].Bullish;
        }
    }
    public class bearishRecognizer : recognizer
    {
        public bearishRecognizer() : base("Bearish", 1) { }
        public override bool recognizePattern(List<smartCandleStick> lscs)
        {
            return lscs[0].Bearish;
        }
    }
    public class neutralRecognizer : recognizer
    {
        public neutralRecognizer() : base("Neutral", 1) { }
        public override bool recognizePattern(List<smartCandleStick> lscs)
        {
            return lscs[0].Neutral;
        }
    }
    public class marubozuRecognizer : recognizer
    {
        public marubozuRecognizer() : base("Marubozu", 1) { }
        public override bool recognizePattern(List<smartCandleStick> lscs)
        {
            return lscs[0].Marubozu;
        }
    }

    public class dojiRecognizer : recognizer
    {
        public dojiRecognizer() : base("Doji", 1) { }
        public override bool recognizePattern(List<smartCandleStick> lscs)
        {
            return lscs[0].Doji;
        }
    }
    public class dragonFlyDojiRecognizer : recognizer
    {
        public dragonFlyDojiRecognizer() : base("Dragon Fly Recognizer", 1) { }
        public override bool recognizePattern(List<smartCandleStick> lscs)
        {
            return lscs[0].DragonFlyDoji;
        }
    }
    public class gravestoneDojiRecognizer : recognizer
    {
        public gravestoneDojiRecognizer() : base("Gravestone Doji", 1) { }
        public override bool recognizePattern(List<smartCandleStick> lscs)
        {
            return lscs[0].GravestoneDoji;
        }
    }
    public class hammerRecognizer : recognizer
    {
        public hammerRecognizer() : base("Hammer", 1) { }
        public override bool recognizePattern(List<smartCandleStick> lscs)
        {
            return lscs[0].Hammer;
        }
    }
    public class invertedHammerRecognizer : recognizer
    {
        public invertedHammerRecognizer() : base("Inverted Hammer", 1) { }
        public override bool recognizePattern(List<smartCandleStick> lscs)
        {
            return lscs[0].InvertedHammer;
        }
    }

    //Multiple candlestick Patterns
    public class engulfingRecognizer : recognizer
    {
        public engulfingRecognizer() : base("Engulfing", 2) { }
        public override bool recognizePattern(List<smartCandleStick> lscs)
        {
            if (((lscs[1].open <= lscs[0].close) && (lscs[1].close > lscs[0].open)) || ((lscs[1].open >= lscs[0].close) && (lscs[1].close < lscs[0].open)))
                return true;
            else
                return false;
        }
    }
    public class engulfingBullishRecognizer : recognizer
    {
        public engulfingBullishRecognizer() : base("Engulfing Bullish", 2) { }
        public override bool recognizePattern(List<smartCandleStick> lscs)
        {
            if ((lscs[1].open <= lscs[0].close) && (lscs[1].close > lscs[0].open))
                return true;
            else
                return false;
        }
    }
    public class engulfingBearishRecognizer : recognizer
    {
        public engulfingBearishRecognizer() : base("Engulfing Bearish", 2) { }
        public override bool recognizePattern(List<smartCandleStick> lscs)
        {
            if ((lscs[1].open >= lscs[0].close) && (lscs[1].close < lscs[0].open))
                return true;
            else
                return false;
        }
    }
    public class haramisRecognizer : recognizer
    {
        public haramisRecognizer() : base("Haramis", 2) { }
        public override bool recognizePattern(List<smartCandleStick> lscs)
        {
            if (((lscs[0].open > lscs[1].close) && (lscs[0].close > lscs[1].open)) || ((lscs[0].open < lscs[1].close) && (lscs[0].close > lscs[1].open)))
                return true;
            else
                return false;
        }
    }
    public class haramisBullishRecognizer : recognizer
    {
        public haramisBullishRecognizer() : base("Haramis Bullish", 2) { }
        public override bool recognizePattern(List<smartCandleStick> lscs)
        {
            if ((lscs[0].open > lscs[1].close) && (lscs[0].close > lscs[1].open))
                return true;
            else
                return false;
        }
    }
    public class haramisBearishRecognizer : recognizer
    {
        public haramisBearishRecognizer() : base("Haramis", 2) { }
        public override bool recognizePattern(List<smartCandleStick> lscs)
        {
            if ((lscs[0].open < lscs[1].close) && (lscs[0].close > lscs[1].open))
                return true;
            else
                return false;
        }
    }
    public class peakRecognizer : recognizer
    {
        public peakRecognizer() : base("Peak", 3) { }
        public override bool recognizePattern(List<smartCandleStick> lscs)
        {
            if ((lscs[1].high > lscs[0].high) && (lscs[1].high > lscs[2].high))
                return true;
            else
                return false;
        }
    }
    public class valleyRecognizer : recognizer
    {
        public valleyRecognizer() : base("Valley", 3) { }
        public override bool recognizePattern(List<smartCandleStick> lscs)
        {
            if ((lscs[1].low < lscs[0].low) && (lscs[1].low < lscs[2].low))
                return true;
            else
                return false;
        }
    }

}

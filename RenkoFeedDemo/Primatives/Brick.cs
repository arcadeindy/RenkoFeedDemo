#region Copyright and Author Details
// Copyright (C) 2016 SquidEyes, LLC.
// Written by Louis S. Berman <louis@squideyes.com>, 4/26/2016

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System;

namespace RenkoFeedDemo
{
    public class Brick
    {
        internal Brick(Decimals decimals, 
            DateTime? openOn, DateTime closeOn)
        {
            Decimals = decimals;
            OpenOn = openOn.Value;
            CloseOn = closeOn;
        }

        public Decimals Decimals { get; }
        public DateTime OpenOn { get; }
        public DateTime CloseOn { get;}

        public double OpenRate { get; internal set; }
        public double HighRate { get; internal set; }
        public double LowRate { get; internal set; }
        public double CloseRate { get; internal set; }

        public Trend Trend => (OpenRate < CloseRate) ?
            Trend.Rising : Trend.Falling;

        public override string ToString()
        {
            return string.Format(
                "{0} to {1} on {2}: {3}, {4}, {5}, {6}",
                OpenOn.ToTimeString(), 
                CloseOn.ToTimeString(),
                OpenOn.ToDateString(),
                OpenRate.ToRateString(Decimals),
                HighRate.ToRateString(Decimals),
                LowRate.ToRateString(Decimals),
                CloseRate.ToRateString(Decimals));
        }

        public string ToCsvString()
        {
            return string.Format("{0},{1},{2},{3},{4},{5}",
                OpenOn.ToDateTimeString(),
                CloseOn.ToDateTimeString(),
                OpenRate, HighRate, LowRate, CloseRate);
        }
    }
}

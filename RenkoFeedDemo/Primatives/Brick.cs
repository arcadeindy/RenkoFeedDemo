// Copyright (C) 2016 SquidEyes, LLC. - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and Confidential
// Written by Louis S. Berman <louis@squideyes.com>, 4/25/2016
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

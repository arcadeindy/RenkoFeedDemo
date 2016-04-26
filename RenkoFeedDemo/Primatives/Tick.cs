// Copyright (C) 2016 SquidEyes, LLC. - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and Confidential
// Written by Louis S. Berman <louis@squideyes.com>, 4/25/2016
using System;

namespace RenkoFeedDemo
{
    public class Tick
    {
        public DateTime TickOn { get; set; }
        public double BidRate { get; set; }
        public double AskRate { get; set; }
    }
}

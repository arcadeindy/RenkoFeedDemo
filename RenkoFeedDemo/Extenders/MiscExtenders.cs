// Copyright (C) 2016 SquidEyes, LLC. - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and Confidential
// Written by Louis S. Berman <louis@squideyes.com>, 4/25/2016
using System;

namespace RenkoFeedDemo
{
    public static class MiscExtenders
    {
        public static string ToDateString(this DateTime value) =>
            value.ToString("MM/dd/yyyy");

        public static string ToTimeString(this DateTime value) =>
            value.ToString("HH:mm:ss.fff");

        public static string ToDateTimeString(this DateTime value) =>
            value.ToString("MM/dd/yyyy HH:mm:ss.fff");

        public static string ToRateString(this double value, Decimals decimals) =>
            value.ToString((decimals == Decimals.Five) ? "N5" : "N3");
    }
}

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
    public static class MiscExtenders
    {
        public static double PipsToRate(this double value, Decimals decimals)
        {
            var factor = (decimals == Decimals.Five ? 10000.0 : 100.0);

            return Math.Round(value / factor, (int)decimals);
        }

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

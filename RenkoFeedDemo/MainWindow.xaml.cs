#region Copyright and Author Details
// Project: RenkoFeedDemo, Namespace: RenkoFeedDemo
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

using Abt.Controls.SciChart.Model.DataSeries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Linq;

namespace RenkoFeedDemo
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Symbol symbol;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            Symbols = Enum.GetValues(typeof(Symbol)).Cast<Symbol>().ToList();

            Symbol = Symbol.EURUSD;
        }

        public List<Symbol> Symbols { get; }

        public Symbol Symbol
        {
            get
            {
                return symbol;
            }
            set
            {
                symbol = value;

                PropertyChanged?.Invoke(this, 
                    new PropertyChangedEventArgs(nameof(Symbol)));

                LoadBricks(Symbol);
            }
        }

        private void LoadBricks(Symbol symbol)
        {
            var ticks = GetTicks(symbol);

            var decimals = (symbol == Symbol.USDJPY) ? Decimals.Three : Decimals.Five;

            var feed = new RenkoFeed(3.2, decimals);

            foreach (var tick in ticks)
                feed.HandleTick(tick);

            var dataSeries = new OhlcDataSeries<DateTime, double>();

            foreach (var brick in feed)
            {
                if (brick.Trend == Trend.Rising)
                {
                    dataSeries.Append(brick.OpenOn, brick.OpenRate,
                        brick.CloseRate, brick.OpenRate, brick.CloseRate);
                }
                else
                {
                    dataSeries.Append(brick.OpenOn, brick.OpenRate,
                        brick.OpenRate, brick.CloseRate, brick.CloseRate);
                }
            }

            chart.RenderableSeries[0].DataSeries = dataSeries;

            chart.ZoomExtents();
        }

        private List<Tick> GetTicks(Symbol symbol)
        {
            var ticks = new List<Tick>();

            string data;

            switch (symbol)
            {
                case Symbol.AUDCAD:
                    data = Properties.Resources.AUDUSD;
                    break;
                case Symbol.EURUSD:
                    data = Properties.Resources.EURUSD;
                    break;
                case Symbol.GBPUSD:
                    data = Properties.Resources.GBPUSD;
                    break;
                case Symbol.USDCAD:
                    data = Properties.Resources.USDCAD;
                    break;
                case Symbol.USDCHF:
                    data = Properties.Resources.USDCHF;
                    break;
                case Symbol.USDJPY:
                    data = Properties.Resources.USDJPY;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(symbol));
            }

            using (var reader = new StringReader(data))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    var fields = line.Split(',');

                    ticks.Add(new Tick()
                    {
                        TickOn = DateTime.ParseExact(fields[1],
                            "MM/dd/yyyy HH:mm:ss.fff", null),
                        BidRate = double.Parse(fields[2]),
                        AskRate = double.Parse(fields[3])
                    });
                }
            }

            return ticks;
        }
    }
}

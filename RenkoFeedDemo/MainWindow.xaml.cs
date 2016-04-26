// Copyright (C) 2016 SquidEyes, LLC. - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and Confidential
// Written by Louis S. Berman <louis@squideyes.com>, 4/25/2016
using Abt.Controls.SciChart.Model.DataSeries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace RenkoFeedDemo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                var ticks = LoadTicks(Symbol.EURUSD);

                var feed = new RenkoFeed(0.0004, Decimals.Five);

                foreach (var tick in ticks)
                    feed.HandleTick(tick);

                var dataSeries = new OhlcDataSeries<DateTime, double>();

                foreach (var brick in feed)
                {
                    dataSeries.Append(brick.OpenOn, brick.OpenRate,
                        brick.HighRate, brick.LowRate, brick.CloseRate);
                }

                stockChart.RenderableSeries[0].DataSeries = dataSeries;

                stockChart.ZoomExtents();

                DataContext = this;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Queue<Tick> LoadTicks(Symbol symbol)
        {
            var ticks = new Queue<Tick>();

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

                    ticks.Enqueue(new Tick()
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

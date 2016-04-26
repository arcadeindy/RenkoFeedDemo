// Copyright (C) 2016 SquidEyes, LLC. - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and Confidential
// Written by Louis S. Berman <louis@squideyes.com>, 4/25/2016
using Abt.Controls.SciChart.Visuals;
using System;
using System.IO;
using System.Windows;

namespace RenkoFeedDemo
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            const string FILENAME = "SciChartLicense.xml";

            try
            {
                ShutdownMode = ShutdownMode.OnLastWindowClose;

                string key;

                using (var reader = new StreamReader(FILENAME))
                    key = reader.ReadToEnd();

                SciChartSurface.SetLicenseKey(key);

                var window = new MainWindow();

                window.Show();
            }
            catch (Exception error)
            {
                var message = $"The license file couldn't be loaded (Error: {error.Message}).";

                MessageBox.Show(message, "Missing License",
                    MessageBoxButton.OK, MessageBoxImage.Warning);

                Shutdown();
            }
        }
    }
}

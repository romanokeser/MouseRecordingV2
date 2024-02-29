﻿using MouseTrackingV2.Utils;
using System.Configuration;
using System.Data;
using System.Windows;
using Application = System.Windows.Application;

namespace MouseTrackingV2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InitializeComponent();
            DatabaseHelper.InitializeDatabase();

        }
    }

}

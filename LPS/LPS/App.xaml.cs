﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace LPS
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        Application currApp = Application.Current;
        currApp.StartupUri = new Uri("MainMain.xaml", UriKind.RelativeOrAbsolute);
    }
    }
}

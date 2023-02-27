﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Platinum.Windows
{
    /// <summary>
    /// Interaction logic for StartupWindow.xaml
    /// </summary>
    public partial class StartupWindow : Wpf.Ui.Controls.UiWindow
    {
        async Task PutTaskDelayWelcomeFirstTime()
        {
            await Task.Delay(2500);
        }

        async Task PutTaskDelayWelcomeBack()
        {
            await Task.Delay(1100);
        }

        public StartupWindow()
        {
            InitializeComponent();
            OnStartup();
            WelcomeProcessRing.IsIndeterminate = true;
        }

        private async void OnStartup()
        {
            PreparingAppBlock.Text = "  Loading The Application";
            await PutTaskDelayWelcomeBack();
            //Settings.SettingsValues.bShouldShowWelcomeBackWindow = false;
            this.Hide();
            MainWindow betterMainWindow = new MainWindow();
            betterMainWindow.Show();
            betterMainWindow.ShowActivated = true;
            betterMainWindow.ShowInTaskbar = true;
        }
    }
}
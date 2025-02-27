﻿using GeePasswordGenerator.WPF.Factories;
using GeePasswordGenerator.WPF.Stores;
using GeePasswordGenerator.WPF.View;
using GeePasswordGenerator.WPF.ViewModel;
using Generators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GeePasswordGenerator.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string[]? args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<MainWindowViewModel>();
                    services.AddSingleton<DefaultGeneratorView>();
                    services.AddSingleton<DefaultGeneratorViewModel>();
                    services.AddSingleton<INavigationStore, NavigationStore>();
                    services.AddSingleton<IViewModelsFactory, ViewModelsFactory>();
                    services.AddSingleton<Generator, DefaultGenerator>();
                });
        }

        private readonly IHost? _host;
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.DataContext = _host.Services.GetRequiredService<MainWindowViewModel>();
            mainWindow.Show();
            base.OnStartup(e);
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }
    }
}

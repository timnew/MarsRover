using System.Windows;
using GalaSoft.MvvmLight.Threading;
using System;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }

        private void OnStartUp(object sender, StartupEventArgs e)
        {
            Console.Title = "Mars Rovers Controller Center";
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine("Welcome To Windy's Mars Rovers Simulator");
        }
    }
}

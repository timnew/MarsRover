using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Windy;
using Windy.IO;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    class Program
    {
        static void Main(string[] args)
        {
            Properties.Resources.WelcomeMessage.Display(ConsoleColor.White);

            var timer = new InstantTimer();
            var provier = new TextReaderConfigurationProvider();

            Plateau plateau = new Plateau();

            plateau.Rovers.CollectionChanged += MonitorRoverCollection;

            do
            {
                ConsoleExntesion.SetConsoleColor(ConsoleColor.Cyan);
                provier.InitializeScenario(timer, plateau);
                ConsoleExntesion.SetConsoleColor();

                plateau.Display();

                "Simulation Start".Display(ConsoleColor.White);

                timer.Start();

                "Simulation Completed".Display(ConsoleColor.White);

                plateau.Display();
            }
            while ("Try once again? ".Ask(ConsoleColor.White, Answers.RQ) == Answers.Retry);

            #region Pause "Press any key to Exit..."
            Console.WriteLine("Press any key to Exit...");
            Console.ReadKey();
            #endregion
        }

        private static void MonitorRoverCollection(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    var rover = e.NewItems[0] as Rover;
                    rover.PropertyChanged += MonitorRover;
                    "Rover {0} has been deployed"
                        .ApplyFormat(rover.GetName())
                        .Display(ConsoleColor.Green);
                    rover.Display();

                    break;
                case NotifyCollectionChangedAction.Reset:
                    foreach (Rover fo in e.OldItems)
                    {
                        fo.PropertyChanged -= MonitorRover;
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Replace:
                default:
                    throw new NotSupportedException();
            }
        }

        static void MonitorRover(object sender, PropertyChangedEventArgs e)
        {
            Contract.Requires<ArgumentException>(string.IsNullOrWhiteSpace(e.PropertyName));

            var rover = sender as Rover;
            "Rover: {0} Property {1} Changed to {2}"
                .ApplyFormat(
                    rover.GetName(),
                    e.PropertyName,
                    typeof(Rover).GetProperty(e.PropertyName).GetValue(sender, null))
                .Display(ConsoleColor.Magenta);

        }


    }
}
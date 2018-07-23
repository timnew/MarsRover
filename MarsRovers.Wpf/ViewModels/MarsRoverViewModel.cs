using GalaSoft.MvvmLight;
using System.ComponentModel;
using GalaSoft.MvvmLight.Command;
using System;

namespace ThoughtWorks.CodingTests.MarsRovers.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class MarsRoverViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MarsRoverViewModel class.
        /// </summary>
        public MarsRoverViewModel()
        {
            Plateau = new Plateau();
            Plateau.PropertyChanged += OnPlateauPropertyChanged;
            Timer = new WpfTimer();

            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
                ConfigurationProvier = new HardCodeConfigurationProvider();
                ConfigurationProvier.InitializeScenario(Timer, Plateau);
            }
            else
            {
                // Code runs "for real": Connect to service, etc...
                ConfigurationProvier = new TextReaderConfigurationProvider();
            }

            ConfigureCommand = new RelayCommand
            (
                () =>
                {
                    Console.Clear();
                    ConfigurationProvier.InitializeScenario(Timer, Plateau);
                    Console.WriteLine("Configuration Completed");
                },
                () => !Timer.IsTimerTicking
            );

            StartTimerCommand = new RelayCommand
            (
                Timer.Start,
                () => !Timer.IsTimerTicking
            );

            StopTimerCommand = new RelayCommand
            (
                Timer.Stop,
                () => Timer.IsTimerTicking
            );

        }

        protected void OnPlateauPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Size":
                    UpdatePlateauVisualSize();
                    break;
                default:
                    break;
            }
        }
        protected void UpdatePlateauVisualSize()
        {
            PlateauVisualWidth = (Plateau.Size.Width + 1) * PlateauCellVisualSize;
            PlateauVisualHeight = (Plateau.Size.Height + 1) * PlateauCellVisualSize;
        }

        public Plateau Plateau { get; private set; }

        public WpfTimer Timer { get; private set; }

        public IConfigurationProvider ConfigurationProvier { get; private set; }

        #region Notify Property PlateauCellVisualSize
        private double plateauCellVisualSize = 30;
        public double PlateauCellVisualSize
        {
            get { return plateauCellVisualSize; }
            set
            {
                if (plateauCellVisualSize == value)
                    return;

                plateauCellVisualSize = value;
                RaisePropertyChanged("PlateauCellVisualSize");
                UpdatePlateauVisualSize();
            }
        }

        #endregion

        #region Notify Property PlateauVisualWidth
        private double plateauVisualWidth = 100.0;
        public double PlateauVisualWidth
        {
            get { return plateauVisualWidth; }
            set
            {
                if (plateauVisualWidth == value)
                    return;

                plateauVisualWidth = value;
                RaisePropertyChanged("PlateauVisualWidth");
            }
        }
        #endregion

        #region Notify Property PlateauVisualHeight
        private double plateauVisualHeight = 100.0;
        public double PlateauVisualHeight
        {
            get { return plateauVisualHeight; }
            set
            {
                if (plateauVisualHeight == value)
                    return;

                plateauVisualHeight = value;
                RaisePropertyChanged("PlateauVisualHeight");
            }
        }
        #endregion

        public RelayCommand ConfigureCommand { get; private set; }
        public RelayCommand StartTimerCommand { get; private set; }
        public RelayCommand StopTimerCommand { get; private set; }

        public override void Cleanup()
        {
            // Clean own resources if needed
            Plateau = null;
            Timer = null;
            ConfigurationProvier = null;
            base.Cleanup();
        }
    }
}
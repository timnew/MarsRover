using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public class Plateau : INotifyPropertyChanged
    {
        public Plateau()
        {
            Rovers = new ObservableCollection<Rover>();
        }

        #region Properties

        public ObservableCollection<Rover> Rovers { get; private set; }

        #region Notify Property Size
        private Size size;
        public Size Size
        {
            get { return size; }
            set
            {
                if (size == value)
                    return;

                size = value;
                Notify("Size");
            }
        }
        #endregion

        #endregion

        #region Behaviors

        public Rover DeployRover(Point position, Orientation orientation)
        {
            var rover = new Rover(this, position, orientation);
            this.Rovers.Add(rover);

            return rover;
        }

        public void Reset()
        {
            Size = Size.Empty;
            Rovers.Clear();
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
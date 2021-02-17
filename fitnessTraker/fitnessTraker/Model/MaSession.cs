using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace fitnessTraker.Model
{
    public class MaSession : INotifyPropertyChanged
    {
        public string _id { get; set; }
        public List<Position> positions { get; set; }
        public string date { get; set; } 
        public  Location startPosition { get; set; }
        private int _pas;
        public string adresse { get; set; }
        public int Pas
        {
            get { return _pas; }
            set
            {
                _pas = value;
                OnPropertyChanged();
            }
        }
        private double _distance;
        public double Distance
        {
            get { return _distance; }
            set
            {
                _distance = value;
                OnPropertyChanged();
            }
        }
        private string _temps;
        public string Temps
        {
            get { return _temps; }
            set
            {
                _temps = value;
                OnPropertyChanged();
            }
        }

    public MaSession()
        {
            _id = "x";
            Pas = 0;
            Distance = 0;
            positions = new List<Position>();
            date = DateTime.Now.ToString();
            startPosition = new Location(0, 0);
            Temps="::"; 
         }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    public event PropertyChangedEventHandler PropertyChanged;
    }
}

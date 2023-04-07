using ScadaShablon.tool;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaShablon.model
{
    class OilTank: ObservableObject, IProperty
    {
        private string _name;
        public string Name
        {
            get => _name; set { _name = value; OnPropertyChanged ( ); }
        }
        public ObservableCollection<FlameDetector> flameDetector { get; set; } = new ObservableCollection<FlameDetector>
        {
            new FlameDetector(),
            new FlameDetector(),
            new FlameDetector()
        };
        public ObservableCollection<Valve> valve { get; set; } = new ObservableCollection<Valve>
        {
            new Valve(),
            new Valve(),
            new Valve(),
            new Valve()
        };
        public ObservableCollection<HeatDetector> heatDetector { get; set; } = new ObservableCollection<HeatDetector>
        {
            new HeatDetector(),
            new HeatDetector(),
            new HeatDetector()
        };
    }
}

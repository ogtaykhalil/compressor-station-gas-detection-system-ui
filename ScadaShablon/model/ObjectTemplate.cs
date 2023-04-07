using ScadaShablon.tool;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaShablon.model
{
    abstract class ObjectTemplate : ObservableObject
    {
        public virtual ObservableCollection<FreshAirFan> freshAirFan { get; set; }
        public virtual ObservableCollection<SmokeDetector> smokeDetector { get; set; } 
        public virtual ObservableCollection<AlarmOutput> alarmOutput { get; set; } 
        public virtual ObservableCollection<ManualCallPoint> manualCallPoint { get; set; } 
        public virtual ObservableCollection<ExhaustFan> exhaustFan { get; set; } 
        public virtual ObservableCollection<FlameDetector> flameDetector { get; set; }
        public virtual ObservableCollection<GasDetector> gasDetector { get; set; }
        public virtual ObservableCollection<Valve> valve { get; set; }
        public virtual ObservableCollection<Pump> pump { get; set; }
    }
}

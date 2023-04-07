using ScadaShablon.tool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ScadaShablon.model
{
    class Compressor : ObjectTemplate, IProperty
    {
        SQLData data = new SQLData ( );
        private string _name;
        private bool _popup;
        private bool _enable;
        private bool _enableTemp;
        public ICommand PopupC { get; set; }
        public ICommand EnableC { get; set; }
        public ICommand DisableC { get; set; }

        public string Name
        {
            get => _name; set { _name = value; OnPropertyChanged ( ); }
        }
        public bool Popup
        {
            get => _popup;
            set { _popup = value; OnPropertyChanged ( ); }
        }
        public bool Enable
        {
            get => _enable;
            set { _enable = value; OnPropertyChanged ( ); }
        }
        public bool EnableTemp
        {
            get => _enableTemp;
            set { _enableTemp = value; OnPropertyChanged ( ); }
        }

        public override ObservableCollection<FlameDetector> flameDetector { get; set; } = new ObservableCollection<FlameDetector>
        {
             new FlameDetector(),
            new FlameDetector()
        };
        public override ObservableCollection<GasDetector> gasDetector { get; set; } = new ObservableCollection<GasDetector>
        {
            new GasDetector(),
            new GasDetector()
        };
        public override ObservableCollection<ExhaustFan> exhaustFan { get; set; } = new ObservableCollection<ExhaustFan>
        {
            new ExhaustFan()
        };
        
        public Compressor ( )
        {
            PopupC = new RelayCommand ( PopupExecuted, CanExecuted );
            EnableC = new RelayCommand ( EnableExecuted, CanExecuted );
            DisableC = new RelayCommand ( DisableExecuted, CanExecuted );
        }        

        public static void SetName( IEnumerable<IProperty> compressors, string name )
        {
            int x = 1;
            foreach ( var compressor in compressors )
            {
                compressor.Name = name + x;
                x++;
            }
        }
        public static void TransferEventsToDatebase(IEnumerable<Compressor> compressor )
        {
            foreach ( var item in compressor )
            {
                Methods.TransferEventsToDatabase ( item.flameDetector );
                Methods.TransferEventsToDatabase ( item.gasDetector );
                Methods.TransferEventsToDatabase ( item.exhaustFan );
            }
        }
        private bool CanExecuted ( object parametr )
        {
            return true;
        }
        private void PopupExecuted ( object parametr )
        {
            if ( Popup || !EnableTemp ) Popup = false;
            else if ( EnableTemp ) Popup = true;
        }
        //private void PopupExecuted ( object parametr )
        //{
        //    if ( Enable )
        //        Popup = true;
        //    else Popup = false;
        //}
        public void EnableExecuted ( object parametr )
        {
            foreach ( var item in flameDetector )
            {
                item.Enable = false;
            }
            foreach ( var item in gasDetector )
            {
                item.Enable = false;
            }
            data.Event ( _name, "Detectorları Aktiv Rejimə Keçirildi" );
        }
        public void DisableExecuted ( object parametr )
        {
            foreach ( var item in flameDetector )
            {
                item.Enable = true;
            }
            foreach ( var item in gasDetector )
            {
                item.Enable = true;
            }
            data.Event ( _name, "Detectorları Passiv Rejimə Keçirildi" );
        }
        public void ExhaustFanStartExecuted ( object parametr )
        {
            foreach ( var item in exhaustFan )
            {
                if ( item.AutoMan )
                {
                    if ( item.Fault ) { item.Start = false; item.Stop = true; }
                    else { item.Stop = false; item.Start = true; }
                }
            }
        }
        public void ExhaustFanStopExecuted ( object parametr )
        {
            foreach ( var item in exhaustFan )
            {
                if ( item.AutoMan )
                {
                    item.Start = false; item.Stop = true;
                }
            }
        }
        public void ExhaustFanAutoManExecuted ( object parametr )
        {
            foreach ( var item in exhaustFan )
            {
                    item.Start = false; item.AutoMan = item.AutoMan ? false : true;
            }
        }
        private bool FlameAlarm ( )
        {
            bool alarm = false;
            foreach ( var item in flameDetector )
            {
                if ( item.Alarm )
                {
                    alarm = true;
                    break;
                }
            }
            return alarm;   
        }
        private bool GasAlarm ( )
        {
            bool alarm = false;
            foreach ( var item in gasDetector )
            {
                if ( item.Alarm )
                {
                    alarm = true;
                    break;
                }
            }
            return alarm;
        }
        public bool Alarm ( )
        {
            return FlameAlarm ( ) || GasAlarm ( ) ? true : false;
        }
    }
}

using ScadaShablon.tool;
using System;
using System.Text;
using System.Windows.Input;

namespace ScadaShablon.model
{
    abstract class Sensor : ObservableObject, IStatus
    {
        SQLData data = new SQLData ( );
        #region PRIVATE FIELDS
        private bool _alarm;
        private bool _preAlarm;
        private bool _fault;
        private bool _wireBreak; // bütün transmitter ve sensorlar üçün istifade olunacaq
        private bool _wireShortcut;          // bütün transmitter ve sensorlar üçün istifade olunacaq
        private bool _popup;
        private bool _enable;
        private bool _enableTemp;
        private bool _enableStatus;
        private bool _lowLevelAlarm;
        private bool _highLevelAlarm;
        protected bool faultTemp;
        protected bool alarmTemp;
        protected bool preAlarmTemp;
        protected bool wireBreakTemp;
        protected bool wireShortcutTemp;
        protected bool highLevelAlarmTemp;
        protected bool lowLevelAlarmTemp;
        private int _prossesAnalogValue;                       // qaz, tezyiq, seviyye transmitter üçün istifade olunacaq
        private int _prossesAnalogValueHighRange;  // qaz, tezyiq, seviyye transmitter üçün istifade olunacaq
        private int _prossesAnalogValueLowRange;   // qaz, tezyiq, seviyye transmitter üçün istifade olunacaq
        private double _prossesScaledValue;                // qaz, tezyiq, seviyye transmitter üçün istifade olunacaq
        private double _highLevelAlarmValue;              // qaz, tezyiq, seviyye transmitter üçün istifade olunacaq
        private double _lowLevelAlarmValue;               // qaz, tezyiq, seviyye transmitter üçün istifade olunacaq
        private double _alarmOffset;                                // qaz, tezyiq, seviyye transmitter üçün istifade olunacaq
        private double _prossesScaledValueHighRange;  // qaz, tezyiq, seviyye transmitter üçün istifade olunacaq
        private double _prossesScaledValueLowRange;  // qaz, tezyiq, seviyye transmitter üçün istifade olunacaq
        private string _name;
        private string _status;
        protected string alarmStatus;
        #endregion
        public Sensor ()
        {
            PopupC = new RelayCommand(PopupExecuted, CanExecuted);
            EnableC = new RelayCommand(EnableExecuted, CanExecuted);
            DisableC = new RelayCommand(DisableExecuted, CanExecuted);
        }

        public ICommand PopupC { get; set; }
        public ICommand EnableC { get; set; }
        public ICommand DisableC { get; set; }
        #region PROPERTIES
        public bool Alarm
        {
            get => _alarm;
            set { _alarm = value; OnPropertyChanged(); }
        }
        public bool PreAlarm
        {
            get => _preAlarm;
            set { _preAlarm = value; OnPropertyChanged(); }
        }
        public bool Fault
        {
            get => _fault;
            set { _fault = value; OnPropertyChanged(); }
        }
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }
        public string Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(); }
        }
        public bool Popup
        {
            get => _popup;
            set { _popup = value; OnPropertyChanged(); }
        }
        public bool Enable
        {
            get => _enable;
            set { _enable = value; OnPropertyChanged(); }
        }
        public bool EnableTemp
        {
            get => _enableTemp;
            set { _enableTemp = value; OnPropertyChanged(); }
        }
        public bool Fault_Temp
        {
            get => faultTemp;
            set { faultTemp = value; OnPropertyChanged(); }
        }
        public bool AlarmTemp
        {
            get => alarmTemp;
            set { alarmTemp = value; OnPropertyChanged(); }
        }
        public bool PreAlarmTemp
        {
            get => preAlarmTemp;
            set { preAlarmTemp = value; OnPropertyChanged ( ); }
        }
        public bool WireBreak
        {
            get => _wireBreak;
            set { _wireBreak = value; OnPropertyChanged(); }
        }
        public bool WireShortcut
        {
            get => _wireShortcut;
            set { _wireShortcut = value; OnPropertyChanged(); }
        }
        public bool WireBreakTemp
        {
            get => wireBreakTemp;
            set { wireBreakTemp = value; OnPropertyChanged(); }
        }
        public bool WireShortcutTemp
        {
            get => wireShortcutTemp;
            set { wireShortcutTemp = value; OnPropertyChanged(); }
        }
        public bool EnableStatus
        {
            get => _enableStatus;
            set { _enableStatus = value; OnPropertyChanged(); }
        }
        public bool HighLevelAlarm
        {
            get => _highLevelAlarm;
            set { _highLevelAlarm = value; OnPropertyChanged(); }
        }
        public bool LowLevelAlarm
        {
            get => _lowLevelAlarm;
            set { _lowLevelAlarm = value; OnPropertyChanged(); }
        }
        public bool HighLevelAlarmTemp
        {
            get => highLevelAlarmTemp;
            set { highLevelAlarmTemp = value; OnPropertyChanged(); }
        }
        public bool LowLevelAlarmTemp
        {
            get => lowLevelAlarmTemp;
            set { lowLevelAlarmTemp = value; OnPropertyChanged(); }
        }
        public int ProssesAnalogValue
        {
            get => _prossesAnalogValue;
            set { _prossesAnalogValue = value; OnPropertyChanged(); }
        }
        public int ProssesAnalogValueHighRange
        {
            get => _prossesAnalogValueHighRange;
            set { _prossesAnalogValueHighRange = 27648; OnPropertyChanged(); }
        }
        public int ProssesAnalogValueLowRange
        {
            get => _prossesAnalogValueLowRange;
            set { _prossesAnalogValueLowRange = 0; OnPropertyChanged(); }
        }
        public double ProssesScaledValueHighRange
        {
            get => _prossesScaledValueHighRange;
            set { _prossesScaledValueHighRange = 100.0; OnPropertyChanged(); }
        }
        public double ProssesScaledValueLowRange
        {
            get => _prossesScaledValueLowRange;
            set { _prossesScaledValueLowRange = 0.0; OnPropertyChanged(); }
        }
        public double ProssesScaledValue
        {
            get => Math.Round(_prossesScaledValue, 3);
            set
            {
                _prossesScaledValue = _prossesScaledValueLowRange
                                      + ( (double)_prossesAnalogValue - _prossesAnalogValueLowRange )
                                      / ( (double) _prossesAnalogValueHighRange - _prossesAnalogValueLowRange )
                                      * ( _prossesScaledValueHighRange - _prossesScaledValueLowRange );
               // _prossesScaledValue = (double)_prossesAnalogValue / 100.0;
                OnPropertyChanged ();
            }
        }
        public double AlarmOffset
        {
            get => _alarmOffset;
            set { _alarmOffset = value; OnPropertyChanged(); }
        }
        public double HighAlarmLevelValue
        {
            get => _highLevelAlarmValue;
            set { _highLevelAlarmValue = value; OnPropertyChanged(); }
        }
        public double LowAlarmLevelValue
        {
            get => _lowLevelAlarmValue;
            set { _lowLevelAlarmValue = value; OnPropertyChanged(); }
        }
        #endregion
        private bool CanExecuted(object parametr)
        {
            return true;
        }
        //private void PopupExecuted(object parametr)
        //{
        //    if (Popup || !EnableTemp) Popup = false;
        //    else if (EnableTemp) Popup = true;
        //}
        private void PopupExecuted ( object parametr )
        {
            if ( Popup || !EnableTemp )
                Popup = false;
            else if ( EnableTemp ) Popup = true;
        }
        private void EnableExecuted(object parametr)
        {
            Enable = false;
            data.Event ( _name, "Detector Aktiv Rejimə Keçirildi" );
        }
        private void DisableExecuted(object parametr)
        {
            Enable = true;
            data.Event ( _name, "Detector Passiv Rejimə Keçirildi" );
        }

        public void SetAlarm( bool bit )
        {
            Alarm = bit;
        }
        public void SetPreAlarm ( bool bit )
        {
            PreAlarm = bit;
        }
        public void SetFault ( bool bit )
        {
            Fault = bit;
        }
        virtual public string AlarmStatus
        {
            get
            {
                if (Fault)
                {
                    alarmStatus = "fault";
                }
                else if (_prossesScaledValue >= LowAlarmLevelValue * 0.20 && _prossesScaledValue < LowAlarmLevelValue * 0.50)
                {
                    alarmStatus = "prealarm";
                }
                else if (_prossesScaledValue >= LowAlarmLevelValue * 0.50)
                {
                    alarmStatus = "alarm";
                }
                else
                {
                    alarmStatus = "noalarm";
                }
                return alarmStatus;
            }
            set { alarmStatus = value; OnPropertyChanged(); }
        }

        public bool FaultTemp { get; set; }

        virtual public void AlarmFault()
        {
            data.Alarm ( this, "Yanğın Qeydə Alındı" );
            data.PreAlarm(this, "İlkin Yanğın Qeydə Alındı");
            data.Fault ( this, "Detektorda Xəta Əmələ Gəldi" );
            
         
            
            //data.Alarm(Wire_Break, ref _wire_Break_Temp, Name, "Flame Detector Wire Is Break");
            //data.Alarm(Wire_Shortcut, ref _wire_Shortcut_Temp, Name, "Flame Detector Wire Is Shortcut");
        }
    }
}

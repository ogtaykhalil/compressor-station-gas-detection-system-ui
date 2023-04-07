using ScadaShablon.tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;


namespace ScadaShablon.model
{
    abstract class Actuator: ObservableObject, IStatus
    {
        DispatcherTimer startTimer = new DispatcherTimer();
        DispatcherTimer stopTimer = new DispatcherTimer();
        DispatcherTimer generalTimer = new DispatcherTimer();
        SQLData data = new SQLData ( );
        private bool _alarm; // not used
        private bool _preAlarm; // not used
        private bool _faultGMR; // from modbus
        private string _name;
        private string _status;
        private string _statusGMR;
        private bool _feedback; // from modbus
        private bool _isOpen; // from modbus
        private bool _isClosed; // from modbus
        private bool _fault; // local variable
        protected bool faultTemp; // local variable
        protected bool faultGMRTemp; // local variable
        private bool _startStatus; // from modbus
        private bool _stopStatus; // from modbus
        private bool _autoManStatus; // from modbus
        private bool _start; // to modbus
        private bool _stop; // to modbus
        private bool _autoMan; // to modbus
        private bool _popup; // local variable
        private bool _enable;

        public ICommand PopupC { get; set; }
        public ICommand ManualC { get; set; }
        public ICommand AutoC { get; set; }
        public ICommand StartC { get; set; }
        public ICommand StopC { get; set; }

        public Actuator()
        {
            ManualC = new RelayCommand(ManualExecuted, CanExecuted);
            AutoC = new RelayCommand(AutoExecuted, CanExecuted);
            StartC = new RelayCommand(StartExecuted, CanExecuted);
            StopC = new RelayCommand(StopExecuted, CanExecuted);
            PopupC = new RelayCommand(PopupExecuted, CanExecuted);
            startTimer.Tick += Timer_Tick_Start;
            stopTimer.Tick += Timer_Tick_Stop;
            generalTimer.Tick += Timer_Tick_Stop1;
            startTimer.Interval = TimeSpan.FromSeconds(2);
            stopTimer.Interval = TimeSpan.FromSeconds ( 2 );
            generalTimer.Interval = TimeSpan.FromSeconds ( 2 );
        }

        public bool Fault // local 
        { 
            get 
            {
                if ( FaultGMR )
                {
                    _fault = true;
                    Status = "GMI Relesində xəta mövcuddur";
                    StatusGMR = $"{Name} xəta mövcuddur";
                }
                else if ( StartStatus && !Feedback )
                {
                    _fault = true;
                    Status = "Kontaktor çəkmədi";
                    StatusGMR = $"{Name} xəta mövcuddur";
                }
                else if ( ( !StartStatus && Feedback ) || ( StopStatus && Feedback ) )
                {
                    _fault = true;
                    Status = "Kontaktorun kontaktları yapışıb";
                    StatusGMR = $"{Name} xəta mövcuddur";
                }
                else if ( !StopStatus && !Feedback )
                {
                    _fault = true;
                    Status = "Kontaktorun və ya stop əmrininin statusu mövcud deyildir";
                    StatusGMR = $"{Name} xəta mövcuddur";
                }
                else
                {
                    _fault = false;
                    Status = $"{Name} normal vəziyyətdədir";
                    StatusGMR = $"{Name} normal vəziyyətdədir";
                }
                return _fault;
            } 
            set { _fault = value; OnPropertyChanged(); } }
        public bool Feedback { get => _feedback; set { _feedback = value; OnPropertyChanged(); } } // from modbus
        public bool IsOpen { get => _isOpen; set { _isOpen = value; OnPropertyChanged(); } } // from modbus
        public bool IsClosed { get => _isClosed; set { _isClosed = value; OnPropertyChanged(); } } // from modbus
        public bool Alarm { get => _alarm; set { _alarm = value; OnPropertyChanged(); } } // not used
        public bool PreAlarm { get => _preAlarm; set { _preAlarm = value; OnPropertyChanged(); } } // not used
        public bool FaultGMR { get => _faultGMR; set { _faultGMR = value; OnPropertyChanged(); } }  // from modbus
        public bool FaultTemp { get => faultTemp; set { faultTemp = value; OnPropertyChanged ( ); } }
        public bool FaultGMRTemp { get => faultGMRTemp; set { faultGMRTemp = value; OnPropertyChanged ( ); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } } // global
        public string Status { get => _status; set { _status = value; OnPropertyChanged(); } } // local
        public string StatusGMR { get {  return _statusGMR; } set { _statusGMR = value; OnPropertyChanged ( ); } } // local
        public bool StartStatus{ get => _startStatus; set { _startStatus = value; OnPropertyChanged(); } } // from modbus
        public bool StopStatus { get => _stopStatus; set { _stopStatus = value; OnPropertyChanged(); } } // from modbus
        public bool AutoManStatus { get => _autoManStatus; set { _autoManStatus = value; OnPropertyChanged(); } } // from modbus
        public bool AutoMan { get => _autoMan; set { _autoMan = value; OnPropertyChanged(); } } // to modbus
        public bool Start { get => _start; set { _start = value; OnPropertyChanged(); } } // to modbus
        public bool Popup { get => _popup; set { _popup = value; OnPropertyChanged(); } } // local
        public bool Stop { get => _stop; set { _stop = value; OnPropertyChanged(); } } // to modbus
        public bool Enable { get => _enable; set { _enable = value; OnPropertyChanged ( ); } }

        public bool AlarmTemp { get; set; }
        public bool PreAlarmTemp { get; set; }

        private void Timer_Tick_Start(object sender, EventArgs e) { Start = false; startTimer.Stop(); }
        private void Timer_Tick_Stop(object sender, EventArgs e) { Stop = false; stopTimer.Stop(); }
        private void Timer_Tick_Stop1(object sender, EventArgs e) { Start = Stop = false; generalTimer.Stop(); }

        private bool CanExecuted(object parametr) => true; 
        private void ManualExecuted(object parametr) => AutoMan = true;
        private void AutoExecuted(object parametr)
        {
            if ( Fault || FaultGMR ) 
            { 
                AutoMan = false; 
                generalTimer.Start();
            }
            else 
            { 
                AutoMan = false; 
                Start = false; 
            }
        }
        private void StartExecuted(object parametr)
        {
            if (Fault)
            { 
                Stop = true;
                Start = false;
            }
            else 
            { 
                Stop = false; 
                Start = true; 
                startTimer.Start(); 
            }
            data.Event ( _name, "Start Düyməsi Basıldı" );
        }
        private void StopExecuted(object parametr)
        {         
            Start = false;
            Stop = true;          
            stopTimer.Start();
            data.Event ( _name, "Stop Düyməsi Basıldı" );
        }
        private void PopupExecuted(object parametr)
        {
            Popup = !Popup;
        }
        public virtual void AlarmFault ( )
        {
            //data.Alarm ( FaultGMR, ref faultGMRTemp, Name, StatusGMR );
            //data.Fault ( this, StatusGMR );
            //data.Alarm(Wire_Break, ref _wire_Break_Temp, Name, "Gas Detector Wire Is Break");
            //data.Alarm(Wire_Shortcut, ref _wire_Shortcut_Temp, Name, "Gas Detector Wire Is Shortcut");
        }
    }
}

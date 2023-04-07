using ScadaShablon.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyModbus;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using ScadaShablon.tool;
using System.Windows.Threading;
using ScadaShablon.view;
using System.Threading;
using System.Windows.Controls;
using System.Media;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
//using ScadaShablon.view;

namespace ScadaShablon.view_model
{
    class ViewModel : ObservableObject
    {
        public CompressorStation compressorStation { get; set; } = new CompressorStation();
        public Office office { get; set; } = new Office();
        public OperatorRoom operatorRoom { get; set; } = new OperatorRoom();
        public model.PumpStation pumpStation { get; set; } = new model.PumpStation();
        public DressingRoom dressingRoom { get; set; } = new DressingRoom();
        public OilTankStation oilTankStation { get; set; } = new OilTankStation();
        public static SQLData data { get; set; } = new SQLData();
        DataClassesDataContext DetTronics = new DataClassesDataContext();
        public static string IPAddress { get; set; } = "169.254.192.119";
        ModbusClient mb = new ModbusClient(IPAddress, 502);

        string path = Directory.GetCurrentDirectory();

        private ObservableCollection<int[]> modbusReadDataOffice = new ObservableCollection<int[]>()
        {
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ]
        };
        private ObservableCollection<int[]> modbusWriteDataOffice = new ObservableCollection<int[]>()
        {
            new int[1]
        };
        private ObservableCollection<int[]> modbusReadDataCompressor = new ObservableCollection<int[]>()
        {
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ]
        };
        private ObservableCollection<int[]> modbusWriteDataCompressor = new ObservableCollection<int[]>()
        {
            new int[1],
            new int[1],
            new int[1],
            new int[1],
            new int[1],
            new int[1],
            new int[1],
            new int[1],
            new int[1]
        };
        private ObservableCollection<int[]> modbusReadDataPumpStation = new ObservableCollection<int[]>()
        {
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ]
        };
        private ObservableCollection<int[]> modbusWriteDataPumpStation = new ObservableCollection<int[]>()
        {
            new int[1]
        };
        private ObservableCollection<int[]> modbusReadDataOperatorRoom = new ObservableCollection<int[]>()
        {
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ]
        };
        private ObservableCollection<int[]> modbusWriteDataOperatorRoom = new ObservableCollection<int[]>()
        {
            new int [ 1 ]
        };
        private ObservableCollection<int[]> modbusReadDataDressingRoom = new ObservableCollection<int[]>()
        {
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ],
            new int [ 1 ]
        };
        private ObservableCollection<int[]> modbusWriteDataDressingRoom = new ObservableCollection<int[]>()
        {
            new int [ 1 ]
        };
        private BitArray scadaCommand = new BitArray(16);
        private int[] modbusReadProssesData = new int[12];
        private int _screen;
        private bool _compressorStationAlarm;
        private bool[] _pumpStationAlarm;
        private bool[] _officeAlarm;
        private bool[] _operatorRoomAlarm;
        private bool[] _dressingRoomAlarm;
        private bool _allExhaustFanAutoManStatus;
        private bool _allVentilyatorsAutoManStatus;
        private bool _allDetectorStatus;
        private bool _passwordWindow;
        private string _password;
        private bool _detectorEnable;
        private bool _controllerConnection;
        private DispatcherTimer AlarmResetTimer = new DispatcherTimer();
        private DispatcherTimer GasPreAlarmTestTimer = new DispatcherTimer();
        private DispatcherTimer GasAlarmTestTimer = new DispatcherTimer();
        private DispatcherTimer FireAlarmTestTimer = new DispatcherTimer();
        private DispatcherTimer ExhaustFanResetTimer = new DispatcherTimer();
        private DispatcherTimer FreshAirFanStopTimer = new DispatcherTimer();
        private DispatcherTimer FreshAirFanStartTimer = new DispatcherTimer();
        private DispatcherTimer ExhaustFanStopTimer = new DispatcherTimer();
        private DispatcherTimer ExhaustFanStartTimer = new DispatcherTimer();
        private DispatcherTimer PasswordTimer = new DispatcherTimer();
        private DispatcherTimer AlarmTimer = new DispatcherTimer();
        private DispatcherTimer ConnectionBreakTimer = new DispatcherTimer();

        public int Screen { get => _screen; set { _screen = value; OnPropertyChanged(); } }
        public bool[] PumpStationAlarm { get => _pumpStationAlarm; set { _pumpStationAlarm = value; OnPropertyChanged(); } }
        public bool[] OfficeAlarm { get => _officeAlarm; set { _officeAlarm = value; OnPropertyChanged(); } }
        public bool[] OperatorRoomAlarm { get => _operatorRoomAlarm; set { _operatorRoomAlarm = value; OnPropertyChanged(); } }
        public bool CompressorStationAlarm { get => _compressorStationAlarm; set { _compressorStationAlarm = value; OnPropertyChanged(); } }
        public bool[] DressingRoomAlarm { get => _dressingRoomAlarm; set { _dressingRoomAlarm = value; OnPropertyChanged(); } }
        public bool PasswordWindow { get => _passwordWindow; set { _passwordWindow = value; OnPropertyChanged(); } }
        public string password { get => _password; set { _password = value; OnPropertyChanged(); } }
        public bool detectorEnable { get => _detectorEnable; set { _detectorEnable = value; OnPropertyChanged(); } }
        public bool AllDetectorStatus { get => _allDetectorStatus; set { _allDetectorStatus = value; OnPropertyChanged(); } }
        public bool AllFansAutoManStatus { get; set; }
        public bool ControllerConnection { get => _controllerConnection; set { _controllerConnection = value; OnPropertyChanged(); } }
        public bool AllVentilyatorsAutoManStatus { get => _allVentilyatorsAutoManStatus; set { _allVentilyatorsAutoManStatus = value; OnPropertyChanged(); } }
        public bool AllExhaustFanAutoManStatus { get => _allExhaustFanAutoManStatus; set { _allExhaustFanAutoManStatus = value; OnPropertyChanged(); } }

        public ICommand OpenPasswordDialog { get; set; }
        public ICommand Password { get; set; }
        public ICommand AllDetectors { get; set; }
        public ICommand AllDetectorsEnable { get; set; }
        public ICommand AllDetectorsDisable { get; set; }
        public ICommand CompressorScreen { get; set; }
        public ICommand PumpstationScreen { get; set; }
        public ICommand ControlRoomScreen { get; set; }
        public ICommand LockRoomScreen { get; set; }
        public ICommand OfisScreen { get; set; }
        public ICommand StartScreen { get; set; }
        public ICommand AllFansAutoMan { get; set; }
        public ICommand AllFansStart { get; set; }
        public ICommand AllFansStop { get; set; }
        public ICommand AllVentilyatorsAutoMan { get; set; }
        public ICommand AllVentilyatorsStart { get; set; }
        public ICommand AllVentilyatorsStop { get; set; }
        public ICommand ShowAllAlarmsTable { get; set; }
        public ICommand ShowAllEventTable { get; set; }
        public ICommand ShowInstruction { get; set; }
        public ICommand ShowCurrentAlarmTable { get; set; }
        public ICommand AlarmReset { get; set; }
        public ICommand ExhaustFanReset { get; set; }
        public ICommand FireAlarmTest { get; set; }
        public ICommand GasPreAlarmTest { get; set; }
        public ICommand GasAlarmTest { get; set; }

        public ViewModel()
        {
            Methods.CompressorStatinonSetName(compressorStation);
            Methods.PumpStationSetName(pumpStation);
            Methods.OfficeSetName(office);
            Methods.DressingRoomSetName(dressingRoom);
            Methods.OperatorRoomSetName(operatorRoom);
            AlarmResetTimer.Tick += TimerScadaReset;
            GasPreAlarmTestTimer.Tick += TimerGasPreAlarmTest;
            GasAlarmTestTimer.Tick += TimerGasAlarmTest;
            FireAlarmTestTimer.Tick += TimerFireAlarmTest;
            ExhaustFanResetTimer.Tick += TimerExhaustFanReset;
            FreshAirFanStopTimer.Tick += TimerVentlStop;
            FreshAirFanStartTimer.Tick += TimerVentlStart;
            ExhaustFanStopTimer.Tick += TimerExhaustStop;
            ExhaustFanStartTimer.Tick += TimerExhaustStart;
            PasswordTimer.Tick += TimerEnable;
            AlarmTimer.Tick += TimerAlarm;
            ConnectionBreakTimer.Tick += TimerBreak;
            AlarmResetTimer.Interval = TimeSpan.FromSeconds(5);
            GasPreAlarmTestTimer.Interval = TimeSpan.FromSeconds(5);
            GasAlarmTestTimer.Interval = TimeSpan.FromSeconds(5);
            FireAlarmTestTimer.Interval = TimeSpan.FromSeconds(5);
            ExhaustFanResetTimer.Interval = TimeSpan.FromSeconds(5);
            FreshAirFanStopTimer.Interval = TimeSpan.FromSeconds(5);
            FreshAirFanStartTimer.Interval = TimeSpan.FromSeconds(5);
            ExhaustFanStopTimer.Interval = TimeSpan.FromSeconds(5);
            ExhaustFanStartTimer.Interval = TimeSpan.FromSeconds(5);
            PasswordTimer.Interval = TimeSpan.FromMinutes(5);
            AlarmTimer.Interval = TimeSpan.FromSeconds(3);
            ConnectionBreakTimer.Interval = TimeSpan.FromSeconds(5);
            Methods.SetGasDetectorProssesValue(compressorStation.compressor);
            mb.ConnectionTimeout = 3000;
            AllFansAutoMan = new RelayCommand(AllExhaustFansAutoManExecuted, CanExecuted1);
            AllFansStart = new RelayCommand(AllExhaustFanStartExecuted, CanExecuted1);
            AllFansStop = new RelayCommand(AllExhaustFansStopExecuted, CanExecuted1);
            AllVentilyatorsAutoMan = new RelayCommand(AllVentilyatorsAutoManExecuted, CanExecuted1);
            AllVentilyatorsStart = new RelayCommand(AllVentilyatorsStartExecuted, CanExecuted1);
            AllVentilyatorsStop = new RelayCommand(AllVentilyatorsStopExecuted, CanExecuted1);
            //ShowAllAlarmsTable = new RelayCommand(ShowAllAlarms, CanExecuted);
            Password = new RelayCommand(PasswordExecuted, CanExecuted);
            OpenPasswordDialog = new RelayCommand(PasswordWindowDialogExecuted, CanExecuted);
            CompressorScreen = new RelayCommand(one, CanExecuted);
            PumpstationScreen = new RelayCommand(two, CanExecuted);
            ControlRoomScreen = new RelayCommand(three, CanExecuted);
            LockRoomScreen = new RelayCommand(five, CanExecuted);
            OfisScreen = new RelayCommand(four, CanExecuted);
            StartScreen = new RelayCommand(zero, CanExecuted);
            ShowInstruction = new RelayCommand(ShowIns, CanExecuted);
            ShowAllAlarmsTable = new RelayCommand(ShowAllAlarms, CanExecuted);
            ShowAllEventTable = new RelayCommand(ShowAllEvents, CanExecuted);
            ShowCurrentAlarmTable = new RelayCommand(ShowCurrentAlarm, CanExecuted);

            AllDetectorsEnable = new RelayCommand(AllDetectorEnableEx, CanExecuted1);
            AllDetectorsDisable = new RelayCommand(AllDetectorDisableEx, CanExecuted1);
            AlarmReset = new RelayCommand(alarmReset, CanExecuted1);
            ExhaustFanReset = new RelayCommand(exhaustFanReset, CanExecuted1);
            FireAlarmTest = new RelayCommand(fireAlarmTest, CanExecuted1);
            GasPreAlarmTest = new RelayCommand(gasPreAlarmTest, CanExecuted1);
            GasAlarmTest = new RelayCommand(gasAlarmTest, CanExecuted1);
        }
        #region +++ EXECUTE COMMAND METHODS +++
        private bool CanExecuted1(object parametr)
        {
            if (data.Status == "Controller İlə Bağlantı Quruldu")
            {
                return true;
            }
            else return false;
        }
        private bool CanExecuted(object parametr)
        {
            return true;
        }
        private void PasswordWindowDialogExecuted(object obj)
        {
            PasswordWindow = !PasswordWindow;
        }
        private void PasswordExecuted(object obj)
        {
            PasswordBox psbox = (PasswordBox)obj;
            password = psbox.Password;
            if (password == "123456")
            {
                //MessageBox.Show("Detektorlara Müdaxilə Edə bilərsiniz");
                detectorEnable = true;
                foreach (var item in compressorStation.gasDetector)
                {
                    item.EnableTemp = true;
                }
                foreach (var compressor in compressorStation.compressor)
                {
                    compressor.EnableTemp = true;
                    foreach (var detector in compressor.flameDetector)
                    {
                        detector.EnableTemp = true;
                    }
                    foreach (var item in compressor.gasDetector)
                    {
                        item.EnableTemp = true;
                    }
                }

                psbox.Password = "";
                PasswordWindow = false;

                PasswordTimer.Start();
            }
            else
            {
                psbox.Password = "";
                System.Windows.Forms.MessageBox.Show("Yanlış şifrə daxil etdiniz!");
                detectorEnable = false;
                foreach (var item in compressorStation.gasDetector)
                {
                    item.EnableTemp = false;
                }
                foreach (var compressor in compressorStation.compressor)
                {
                    compressor.EnableTemp = false;
                    foreach (var detector in compressor.flameDetector)
                    {
                        detector.EnableTemp = false;
                    }
                    foreach (var item in compressor.gasDetector)
                    {
                        item.EnableTemp = false;
                    }
                }
            }
        }
        private void AllDetectorDisableEx(object obj)
        {
            if (detectorEnable)
            {
                foreach (var item in compressorStation.gasDetector)
                {
                    item.Enable = true;
                }
                foreach (var compressor in compressorStation.compressor)
                {
                    foreach (var detector in compressor.flameDetector)
                    {
                        detector.Enable = true;
                    }
                    foreach (var detector in compressor.gasDetector)
                    {
                        detector.Enable = true;
                    }
                }

                AllDetectorStatus = true;
                data.Event("ZONA-1 Detektorları", "Passiv Rejimə Keçirilmişdir");
            }
        }
        private void AllDetectorEnableEx(object obj)
        {
            foreach (var item in compressorStation.gasDetector)
            {
                item.Enable = false;
            }
            foreach (var compressor in compressorStation.compressor)
            {
                foreach (var detector in compressor.flameDetector)
                {
                    detector.Enable = false;
                }
                foreach (var detector in compressor.gasDetector)
                {
                    detector.Enable = false;
                }
            }
            AllDetectorStatus = false;
            data.Event("ZONA-1 Detektorları", "Aktiv Rejimə Keçirilmişdir");
        }
        private void AllExhaustFansAutoManExecuted(object obj)
        {
            if (AllExhaustFanAutoManStatus)
            {
                foreach (var compressor in compressorStation.compressor)
                {
                    foreach (var fan in compressor.exhaustFan)
                    {
                        fan.Start = false;
                        fan.AutoMan = false;
                    }
                }
                AllExhaustFanAutoManStatus = false;
            }
            else
            {
                foreach (var compressor in compressorStation.compressor)
                {
                    foreach (var fan in compressor.exhaustFan)
                    {
                        fan.AutoMan = true;
                    }
                }
                AllExhaustFanAutoManStatus = true;
            }
        }
        private void AllExhaustFanStartExecuted(object obj)
        {
            foreach (var compressor in compressorStation.compressor)
            {
                foreach (var fan in compressor.exhaustFan)
                {
                    if (fan.AutoMan)
                    {
                        if (fan.Fault)
                        {
                            fan.Start = false;
                            fan.Stop = true;
                        }
                        else
                        {
                            fan.Stop = false;
                            fan.Start = true;
                        }
                    }
                }
            }
            data.Event("Bütün Sovurucu Ventilyatorlar", "Start Düyməsi Basıldı");
            ExhaustFanStartTimer.Start();
        }
        private void AllExhaustFansStopExecuted(object obj)
        {
            foreach (var compressor in compressorStation.compressor)
            {
                foreach (var fan in compressor.exhaustFan)
                {
                    if (fan.AutoMan)
                    {
                        fan.Start = false;
                        fan.Stop = true;
                    }
                }
            }
            data.Event("Bütün Sovurucu Ventilyatorlar", "Stop Düyməsi Basıldı");
            ExhaustFanStopTimer.Start();
        }
        private void AllVentilyatorsAutoManExecuted(object obj)
        {
            if (AllVentilyatorsAutoManStatus)
            {
                foreach (var fan in compressorStation.freshAirFan)
                {
                    fan.Start = false;
                    fan.AutoMan = false;
                }
                AllVentilyatorsAutoManStatus = false;
            }
            else
            {
                foreach (var fan in compressorStation.freshAirFan)
                {
                    fan.AutoMan = true;
                }
                AllVentilyatorsAutoManStatus = true;
            }
        }
        private void AllVentilyatorsStartExecuted(object obj)
        {
            foreach (var fan in compressorStation.freshAirFan)
            {
                if (fan.AutoMan)
                {
                    if (fan.Fault)
                    {
                        fan.Start = false;
                        fan.Stop = true;
                    }
                    else
                    {
                        fan.Stop = false;
                        fan.Start = true;
                    }
                }
            }
            data.Event("Bütün Verici Ventilyatorlar", "Start Düyməsi Basıldı");
            FreshAirFanStartTimer.Start();
        }
        private void AllVentilyatorsStopExecuted(object obj)
        {
            foreach (var fan in compressorStation.freshAirFan)
            {
                if (fan.AutoMan)
                {
                    fan.Start = false;
                    fan.Stop = true;
                }
            }
            data.Event("Bütün Verici Ventilyatorlar", "Stop Düyməsi Basıldı");
            FreshAirFanStopTimer.Start();
        }

        private void ShowAllAlarms(object obj)
        {
            AllAlarms allAlarm = new AllAlarms();
            allAlarm.ShowDialog();
        }
        private void ShowIns(object obj)
        {
            Instruction ins = new Instruction();
            ins.Show();

            //ins.WindowState = WindowState.Normal;
        }
        private void ShowAllEvents(object obj)
        {
            AllEvents allEvents = new AllEvents();
            allEvents.ShowDialog();
        }
        private void ShowCurrentAlarm(object obj)
        {
            CurrentAlarm currentAlarm = new CurrentAlarm();
            currentAlarm.ShowDialog();
        }
        private void two(object sender)
        {
            if (Screen != 2)
                Screen = 2;
        }
        private void three(object sender)
        {
            if (Screen != 3)
                Screen = 3;
        }
        private void four(object sender)
        {
            if (Screen != 4)
                Screen = 4;
        }
        private void five(object sender)
        {
            if (Screen != 5)
                Screen = 5;
        }
        private void one(object sender)
        {
            if (Screen != 1)
                Screen = 1;
        }
        private void zero(object sender)
        {
            if (Screen != 0)
                Screen = 0;
        }

        private void alarmReset(object sender)
        {
            scadaCommand[15] = true;
            data.Event("Yanğın və ya Qaz Sızması Həyəcanı", "Operator Tərəfindən Ləğv Edildi");
            AlarmResetTimer.Start();
        }
        private void exhaustFanReset(object sender)
        {
            scadaCommand[14] = true;
            data.Event("Sovurucu və Vurucu Ventilyatorlar", "Başlanğıc Vəziyyətə Gətirildi");
            ExhaustFanResetTimer.Start();
        }

        private void fireAlarmTest(object sender)
        {
            scadaCommand[13] = true;
            data.Event("Yanğın Həyəcanı Testi", "Operator Tərəfindən Verildi");
            FireAlarmTestTimer.Start();
        }
        private void gasPreAlarmTest(object sender)
        {
            scadaCommand[12] = true;
            data.Event("5%-lik Qaz Sızması Testi", "Operator Tərəfindən Verildi");
            GasPreAlarmTestTimer.Start();
        }
        private void gasAlarmTest(object sender)
        {
            scadaCommand[11] = true;
            data.Event("25%-lik Qaz Sızması Testi", "Operator Tərəfindən Verildi");
            GasAlarmTestTimer.Start();
        }
        #endregion

        #region ++++ Methods For Timer Tick Events  ++++
        private void TimerFireAlarmTest(object sender, EventArgs e)
        {
            scadaCommand[13] = false;
            AlarmResetTimer.Stop();
        }
        private void TimerGasPreAlarmTest(object sender, EventArgs e)
        {
            scadaCommand[12] = false;
            GasPreAlarmTestTimer.Stop();
        }
        private void TimerGasAlarmTest(object sender, EventArgs e)
        {
            scadaCommand[11] = false;
            GasAlarmTestTimer.Stop();
        }
        private void TimerExhaustStart(object sender, EventArgs e)
        {
            foreach (var compressor in compressorStation.compressor)
                foreach (var fan in compressor.exhaustFan)
                    fan.Start = false;
            ExhaustFanStartTimer.Stop();
        }
        private void TimerExhaustStop(object sender, EventArgs e)
        {
            foreach (var compressor in compressorStation.compressor)
                foreach (var fan in compressor.exhaustFan)
                    fan.Stop = false;
            ExhaustFanStopTimer.Stop();
        }
        private void TimerVentlStart(object sender, EventArgs e)
        {
            foreach (var fan in compressorStation.freshAirFan)
                fan.Start = false;
            FreshAirFanStartTimer.Stop();
        }
        private void TimerVentlStop(object sender, EventArgs e)
        {
            foreach (var fan in compressorStation.freshAirFan)
                fan.Stop = false;
            FreshAirFanStopTimer.Stop();
        }
        private void TimerBreak(object sender, EventArgs e)
        {
            SoundPlayer soundPlayer = new SoundPlayer
            {
                SoundLocation = $@"{path}\break.wav"
            };
            soundPlayer.Play ( );
        }
        private void TimerScadaReset(object sender, EventArgs e)
        {
            scadaCommand[15] = false;
            AlarmResetTimer.Stop();
        }
        private void TimerExhaustFanReset(object sender, EventArgs e)
        {
            scadaCommand[14] = false;
            ExhaustFanResetTimer.Stop();
        }
        private void TimerAlarm(object sender, EventArgs e)
        {
            SoundPlayer soundPlayer = new SoundPlayer
            {
                SoundLocation = $@"{path}\sound.wav"
            };
            soundPlayer.Play();
        }
        private void TimerEnable(object sender, EventArgs e)
        {
            detectorEnable = false;
            foreach (var item in compressorStation.gasDetector)
            {
                item.EnableTemp = true;
            }
            foreach (var compressor in compressorStation.compressor)
            {
                compressor.EnableTemp = false;

                foreach (var detector in compressor.flameDetector)
                    detector.EnableTemp = false;

                foreach (var item in compressor.gasDetector)
                    item.EnableTemp = false;
            }
            PasswordTimer.Stop();
        }

        #endregion

        private void MoveData(Object sender)
        {
            AssignModbusReadDataCompressor();
            AssignModbusReadProssesValue();
            CompressorStation.GetDeviceStatus(compressorStation, modbusReadDataCompressor, data);
            CompressorStation.GetProssesValue(compressorStation, modbusReadProssesData);
        }
        private void ReadData(Object sender)
        {
            //CompressorStation.GetDeviceStatus ( compressorStation, modbusReadDataCompressor, data );
            //data.GetAlarmTab = null;
        }
        private void WriteData(Object sender)
        {
            CompressorStation.SetDeviceCommand(compressorStation, modbusWriteDataCompressor);
            AssignModbusWriteDataCompressor();
        }
        private void AssignModbusReadProssesValue()
        {
            modbusReadProssesData = mb.ReadHoldingRegisters(40, 12);
        }
        private void AssignModbusReadDataCompressor()
        {
            for (int index = 0; index < modbusReadDataCompressor.Count; index++)
            {
                modbusReadDataCompressor[index] = mb.ReadHoldingRegisters(index, 1);
            }
        }
        private void AssignModbusWriteDataCompressor()
        {
            for (int index = 0; index < modbusWriteDataCompressor.Count; index++)
            {
                mb.WriteSingleRegister(30 + index, modbusWriteDataCompressor[index][0]);
            }
        }
        private void AssignModbusReadDataOffice()
        {
            for (int index = 0; index < modbusReadDataOffice.Count; index++)
            {
                modbusReadDataOffice[index] = mb.ReadHoldingRegisters(60 + index, 1);
            }
        }
        private void AssignModbusWriteDataOffice()
        {
            for (int index = 0; index < modbusWriteDataOffice.Count; index++)
            {
                mb.WriteSingleRegister(69 + index, modbusWriteDataOffice[index][0]);
            }
        }
        private void AssignModbusReadDataControlRoom()
        {
            for (int index = 0; index < modbusReadDataOperatorRoom.Count; index++)
            {
                modbusReadDataOperatorRoom[index] = mb.ReadHoldingRegisters(70 + index, 1);
            }
        }
        private void AssignModbusWriteDataControlRoom()
        {
            for (int index = 0; index < modbusWriteDataOperatorRoom.Count; index++)
            {
                mb.WriteSingleRegister(79 + index, modbusWriteDataOperatorRoom[index][0]);
            }
        }
        private void AssignModbusReadDataPumpStation()
        {
            for (int index = 0; index < modbusReadDataPumpStation.Count; index++)
            {
                modbusReadDataPumpStation[index] = mb.ReadHoldingRegisters(80 + index, 1);
            }
        }
        private void AssignModbusWriteDataPumpStation()
        {
            for (int index = 0; index < modbusWriteDataPumpStation.Count; index++)
            {
                mb.WriteSingleRegister(89 + index, modbusWriteDataPumpStation[index][0]);
            }
        }
        private void AssignModbusReadDataDressingRoom()
        {
            for (int index = 0; index < modbusReadDataDressingRoom.Count; index++)
            {
                modbusReadDataDressingRoom[index] = mb.ReadHoldingRegisters(90 + index, 1);
            }
        }
        private void AssignModbusWriteDataDressingRoom()
        {
            for (int index = 0; index < modbusWriteDataDressingRoom.Count; index++)
            {
                mb.WriteSingleRegister(99 + index, modbusWriteDataDressingRoom[index][0]);
            }
        }
        private void TransferEventsToDatabase()
        {
            CompressorStation.TransferEventsToDatebase(compressorStation);
            Office.TransferEventsToDatebase(office);
            OperatorRoom.TransferEventsToDatebase(operatorRoom);
            model.PumpStation.TransferEventsToDatebase(pumpStation);
            DressingRoom.TransferEventsToDatebase(dressingRoom);
        }
        private void Indigators()
        {
            CompressorStationAlarm = compressorStation.Alarm();
            OperatorRoomAlarm = operatorRoom.GeneralAlarm();
            DressingRoomAlarm = dressingRoom.GeneralAlarm();
            PumpStationAlarm = pumpStation.GeneralAlarm();
            OfficeAlarm = office.GeneralAlarm();
        }
        public void ConnectionBreak()
        {
            if (!mb.Connected)
            {
                foreach (var compressor in compressorStation.compressor)
                {
                    foreach (var detector in compressor.flameDetector)
                    {
                        detector.Alarm = false; ;
                        detector.Enable = false;
                        detector.Fault = false;
                    }
                    foreach (var detector in compressor.gasDetector)
                    {
                        detector.Alarm = false; ;
                        detector.Enable = false;
                        detector.Fault = false;
                        detector.ProssesAnalogValue = 0;
                        detector.ProssesScaledValue = 0;
                    }
                    foreach (var fan in compressor.exhaustFan)
                    {
                        fan.StartStatus = false;
                        fan.StopStatus = false;
                        fan.Fault = false;
                    }
                }

                foreach (var fan in compressorStation.freshAirFan)
                {
                    fan.StartStatus = false;
                    fan.StopStatus = false;
                    fan.Fault = false;
                }

            }
        }
        public void AlarmSound()
        {
            if (compressorStation.Alarm() || office.Alarm() || operatorRoom.Alarm() || pumpStation.Alarm() || dressingRoom.Alarm())
            {
                AlarmTimer.Start();
            }
            else
            {
                AlarmTimer.Stop();
            }
        }
        private void ConnectionEstablished()
        {
            if (mb.Connected && ControllerConnection)
            {
                CurrentAlarmTable currentAlarm = DetTronics.CurrentAlarmTables.FirstOrDefault(k => k.device_name == "Kontroller");
                if (currentAlarm.device_name != null)
                {
                    DetTronics.CurrentAlarmTables.DeleteOnSubmit(currentAlarm);
                    DetTronics.SubmitChanges();
                }
                ControllerConnection = false;
                data.ControllerConnection = false;
            }
        }
        private void connectionBreak()
        {
            if (!mb.Connected && !ControllerConnection)
            {

                AlarmTable alarmTab = new AlarmTable
                {
                    device_name = "Kontroller",
                    date_and_time = DateTime.Now,
                    message = data.Status
                };
                CurrentAlarmTable currentAlarm = new CurrentAlarmTable
                {
                    device_name = "Kontroller",
                    date_and_time = DateTime.Now,
                    message = data.Status
                };

                DetTronics.CurrentAlarmTables.InsertOnSubmit(currentAlarm);
                DetTronics.AlarmTables.InsertOnSubmit(alarmTab);
                DetTronics.SubmitChanges();

                ControllerConnection = true;
            }
        }
        
        public void Run()
        {

            while (true)
            {
                //Thread th = new Thread(s => { data.GetCurrentAlarmTab = null; });
               // th.Start();
                try
                {
                    mb.Connect();
                    if (mb.Connected)
                    {
                        try
                        {
                            AlarmSound();
                            mb.WriteMultipleRegisters(25, Methods.MirrorWrite(scadaCommand));
                            Indigators();
                            AssignModbusWriteDataCompressor();
                            AssignModbusReadDataCompressor();
                            AssignModbusReadProssesValue();
                            AssignModbusWriteDataOffice();
                            AssignModbusReadDataOffice();
                            AssignModbusWriteDataControlRoom();
                            AssignModbusReadDataControlRoom();
                            AssignModbusWriteDataPumpStation();
                            AssignModbusReadDataPumpStation();
                            AssignModbusWriteDataDressingRoom();
                            AssignModbusReadDataDressingRoom();

                            CompressorStation.GetDeviceStatus(compressorStation, modbusReadDataCompressor, data);
                            CompressorStation.GetProssesValue(compressorStation, modbusReadProssesData);
                            CompressorStation.SetDeviceCommand(compressorStation, modbusWriteDataCompressor);
                            Office.SetDeviceCommand(office, modbusWriteDataOffice);
                            Office.GetDeviceStatus(office, modbusReadDataOffice);
                            OperatorRoom.SetDeviceCommand(operatorRoom, modbusWriteDataOperatorRoom);
                            OperatorRoom.GetDeviceStatus(operatorRoom, modbusReadDataOperatorRoom);
                            model.PumpStation.SetDeviceCommand(pumpStation, modbusWriteDataPumpStation);
                            model.PumpStation.GetDeviceStatus(pumpStation, modbusReadDataPumpStation);
                            DressingRoom.SetDeviceCommand(dressingRoom, modbusWriteDataDressingRoom);
                            DressingRoom.GetDeviceStatus(dressingRoom, modbusReadDataDressingRoom);
                            Task task = Task.Run(() => TransferEventsToDatabase());
                            ConnectionEstablished();
                            data.ControllerConnection = false;
                            ControllerConnection = false;
                            ConnectionBreakTimer.Stop();
                            data.Status = "Controller İlə Bağlantı Quruldu";
                            //data.CA = null;
                        }
                        catch
                        {
                            AlarmTimer.Stop();
                            ConnectionBreakTimer.Start();
                            mb.Disconnect();
                            data.Status = "Controller İlə Bağlantı Kəsildi";
                            connectionBreak();
                            data.ControllerConnection = true;
                        }
                    }
                }
                catch (System.IO.IOException ex)
                {
                    data.Status = "Xəbərləşmə Portu Aktiv Deyil";
                    mb.Disconnect();
                    connectionBreak();
                    data.ControllerConnection = true;
                    //System.Windows.MessageBox.Show(ex.Message);
                }
                catch (System.Net.Sockets.SocketException ex)
                {
                    data.Status = "LAN Port Bağlantı Xətası";
                    mb.Disconnect();
                    connectionBreak();
                    data.ControllerConnection = true;
                    //System.Windows.MessageBox.Show("SCADA ilə Kontroller arasında fiziki bağlantı mövcud deyildir\n"+ ex.Message, "BAĞLANTI XƏTASI", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (EasyModbus.Exceptions.ConnectionException ex)
                {
                    AlarmTimer.Stop();
                    ConnectionBreakTimer.Start();
                    data.Status = "Controller İlə Bağlantı Kəsildi";
                    mb.Disconnect();
                    connectionBreak();
                    data.ControllerConnection = true;
                    //System.Windows.MessageBox.Show ( "SCADA ilə Kontroller arasında əlaqə kəsildi\n" + ex.Message, "BAĞLANTI XƏTASI", MessageBoxButton.OK, MessageBoxImage.Error );                    
                }

            }
        }

        public void Run3()
        {
            while (true)
            {
                try
                {
                    data.GetCurrentAlarmTab = null;
                }
                catch (System.ArgumentException ex)
                {


                }
                catch (System.NullReferenceException ex)
                {


                }
            }

        }
        public void Transfer()
        {
            while (true)
            {
                try
                {
                    //mb.Connect ( );
                    TransferEventsToDatabase();
                }
                catch (System.IO.IOException ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
                catch (System.Net.Sockets.SocketException ex)
                {
                    System.Windows.MessageBox.Show("SCADA ilə Kontroller arasında fiziki bağlantı mövcud deyildir\n" + ex.Message, "BAĞLANTI XƏTASI", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (EasyModbus.Exceptions.ConnectionException ex)
                {
                    System.Windows.MessageBox.Show("SCADA ilə Kontroller arasında əlaqə kəsildi\n" + ex.Message, "BAĞLANTI XƏTASI", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public void Read()
        {
            while (true)
            {
                try
                {
                    //mb.Connect ( );
                    data.GetCurrentAlarmTab = null;
                }
                catch (System.IO.IOException ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
                catch (System.Net.Sockets.SocketException ex)
                {
                    System.Windows.MessageBox.Show("SCADA ilə Kontroller arasında fiziki bağlantı mövcud deyildir\n" + ex.Message, "BAĞLANTI XƏTASI", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (EasyModbus.Exceptions.ConnectionException ex)
                {
                    System.Windows.MessageBox.Show("SCADA ilə Kontroller arasında əlaqə kəsildi\n" + ex.Message, "BAĞLANTI XƏTASI", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}

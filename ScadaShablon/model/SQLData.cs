using ScadaShablon.tool;
using ScadaShablon.view_model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Forms;
using System.Windows;

namespace ScadaShablon.model
{
     class SQLData : ObservableObject
     {
        //private ObservableCollection<CurrentAlarms> ca;
        private IEnumerable<CurrentAlarmTable> _getAlarmTab;
        private string _status;
        private bool _alarmTemp;
        private bool _controllerConnection;
        
        DispatcherTimer timer = new DispatcherTimer ( );

        public DataClassesDataContext DetTronics { get; set; } = new DataClassesDataContext( );
        //public ObservableCollection<Event> Event { get; set; } = new ObservableCollection<Event>();

        //public void ShowCurrentAlarms()
        //{
        //    if (Start_Year != null && Start_Month!=null && Start_Day!=null && Start_Hour!=null &&Start_Minute!=null &&
        //        End_Year != null && End_Month != null && End_Day != null && End_Hour != null && End_Minute != null)
        //    {
        //        GetAlarmTab = Det_Tronics.Current_Alarm_Tabs.Where( item => item.DATE_AND_TIME.Year >= Start_Year 
        //                                                                                                          &&  item.DATE_AND_TIME.Month>=Start_Month
        //                                                                                                          &&  item.DATE_AND_TIME.Day >= Start_Day
        //                                                                                                          &&  item.DATE_AND_TIME.Hour>=Start_Hour
        //                                                                                                          && item.DATE_AND_TIME.Minute>=Start_Minute
        //                                                                                                          && item.DATE_AND_TIME.Year <= End_Year
        //                                                                                                          && item.DATE_AND_TIME.Month <= End_Month
        //                                                                                                          && item.DATE_AND_TIME.Day <= End_Day
        //                                                                                                          && item.DATE_AND_TIME.Hour <= End_Hour
        //                                                                                                        && item.DATE_AND_TIME.Minute <= End_Minute ).Select(item => item);
        //    }
        //}
        //public ObservableCollection<CurrentAlarms> CA { get => ca; set { ca = value; OnPropertyChanged(); } }
        //public IEnumerable<CurrentAlarmTable> GetCurrentAlarmTab
        //{
        //    get
        //    {
        //        // var itemss = from ca in DetTronics.CurrentAlarmTables orderby ca.date_and_time descending select ca;
        //        var items = from e in DetTronics.CurrentAlarmTables
        //                                                orderby  e.date_and_time 
        //                                                descending
        //                                                select e;
        //        return items;
        //    }
        //    set { _getAlarmTab = value; OnPropertyChanged ( ); }
        //}
        public IEnumerable<AlarmTable> GetAlarmTab
        {
            get
            {
                var a = from e in DetTronics.AlarmTables
                        where e.date_and_time >= StartDate
                        && e.date_and_time <= EndDate
                        orderby e.date_and_time 
                        descending
                        select e;
                return a;
            }
            set { OnPropertyChanged ( ); }
        }
        public IEnumerable<EventTable> GetEventTab
        {
            get
            {
                var a = from e in DetTronics.EventTables
                        where e.date_and_time >= StartDate
                       && e.date_and_time <= EndDate
                        orderby e.date_and_time 
                        descending
                        select e;
                return a;
            }
            set { OnPropertyChanged ( ); }
        }
        public IEnumerable<CurrentAlarmTable> GetCurrentAlarmTab
        {
            get
            {
                var items = from e in DetTronics.CurrentAlarmTables
                                                        orderby e.date_and_time 
                                                        descending
                                                        select e;
                return items;
            }
            set { OnPropertyChanged ( ); }
        }

        public ICommand UpdateCurrentAlarm { get; set; }
        public ICommand GetCurrentAlarm { get; set; }
        public ICommand GetAlarm { get; set; }
        public ICommand GetEvent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public SQLData ( )
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            GetAlarm = new RelayCommand ( ShowSelectedAlarms, CanExecuted );
            GetEvent = new RelayCommand ( ShowSelectedEvents, CanExecuted );
            GetCurrentAlarm = new RelayCommand ( ShowCurrentAlarms, CanExecuted );
            UpdateCurrentAlarm = new RelayCommand ( ShowCurrentAlarms1, CanExecuted );
        }

        
        private void UpdateCurrentAlarms ( object obj )
        {
            //DetTronics.ExecuteCommand ( "create table TestTable(id int not null primary key identity(1,1), chek int)" );
            DetTronics.ExecuteCommand ( "select * from Data where table_name=TestTable drop" );
            DetTronics.SubmitChanges ( );
        }

        public bool ControllerConnection
        {
            get { return _controllerConnection; }
            set { _controllerConnection = value; OnPropertyChanged ( ); }
        }
        public string Status
        {
            get
            { return _status; }
            set { _status = value; OnPropertyChanged ( ); }
        }
        public bool AlarmTemp
        {
            get { return _alarmTemp; }
            set { _alarmTemp = value; OnPropertyChanged ( ); }
        }
        public void SelectData ( )
        {
            if ( StartDate != null && EndDate != null )
            {
                //DatabaseDataContext db = new DatabaseDataContext ( );
                var a = from e in DetTronics.AlarmTables
                        where e.date_and_time >= StartDate
                        && e.date_and_time <= EndDate
                        orderby e.date_and_time
                        select e;
            }
        }
       
        public void Alarm(IStatus device, string message)
        {
            if (device.Alarm && !device.AlarmTemp)
            {

                AlarmTable alarmTab = new AlarmTable
                {
                    device_name = device.Name,
                    date_and_time = DateTime.Now,
                    message = message
                };
                CurrentAlarmTable currentAlarm = new CurrentAlarmTable
                {
                    device_name = device.Name,
                    date_and_time = DateTime.Now,
                    message = message
                };

                DetTronics.CurrentAlarmTables.InsertOnSubmit(currentAlarm);
                DetTronics.AlarmTables.InsertOnSubmit(alarmTab);
                DetTronics.SubmitChanges();

                device.AlarmTemp = true;
            }
            else if (!device.Alarm && device.AlarmTemp)
            {
                CurrentAlarmTable currentAlarm = DetTronics.CurrentAlarmTables.FirstOrDefault(k => k.message == message);

                if (currentAlarm.device_name != null)
                {
                    DetTronics.CurrentAlarmTables.DeleteOnSubmit(currentAlarm);
                    DetTronics.SubmitChanges();
                }

                device.AlarmTemp = false;
            }
        }

        public void PreAlarm(IStatus device, string message)
        {
            if (device.PreAlarm && !device.PreAlarmTemp)
            {
                AlarmTable alarmTab = new AlarmTable
                {
                    device_name = device.Name,
                    date_and_time = DateTime.Now,
                    message = message
                };
                CurrentAlarmTable currentAlarm = new CurrentAlarmTable
                {
                    device_name = device.Name,
                    date_and_time = DateTime.Now,
                    message = message
                };

                DetTronics.CurrentAlarmTables.InsertOnSubmit(currentAlarm);
                DetTronics.AlarmTables.InsertOnSubmit(alarmTab);
                DetTronics.SubmitChanges();

                device.PreAlarmTemp = true;
            }
            else if (!device.PreAlarm && device.PreAlarmTemp)
            {
                CurrentAlarmTable currentAlarm = DetTronics.CurrentAlarmTables.FirstOrDefault(k => k.message == message);

                if (currentAlarm.device_name != null)
                {
                    DetTronics.CurrentAlarmTables.DeleteOnSubmit(currentAlarm);
                    DetTronics.SubmitChanges();
                }

                device.PreAlarmTemp = false;
            }

        }

        public void Fault(IStatus device, string message)
        {
            if (device.Fault && !device.FaultTemp && device != null && message != null)
            {

                AlarmTable alarmTab = new AlarmTable
                {
                    device_name = device.Name,
                    date_and_time = DateTime.Now,
                    message = message
                };
                CurrentAlarmTable currentAlarm = new CurrentAlarmTable
                {
                    device_name = device.Name,
                    date_and_time = DateTime.Now,
                    message = message
                };

                DetTronics.CurrentAlarmTables.InsertOnSubmit(currentAlarm);
                DetTronics.AlarmTables.InsertOnSubmit(alarmTab);
                DetTronics.SubmitChanges();

                device.FaultTemp = true;
            }
            else if (!device.Fault && device.FaultTemp)
            {
                CurrentAlarmTable currentAlarm = DetTronics.CurrentAlarmTables.FirstOrDefault(k => k.message == message);

                if (currentAlarm.device_name != null)
                {
                    DetTronics.CurrentAlarmTables.DeleteOnSubmit(currentAlarm);
                    DetTronics.SubmitChanges();
                }
                device.FaultTemp = false;
            }
        }
        public void Event ( string name, string message )
        {
            EventTable eventTab = new EventTable
            {
                device_name = name,
                date_and_time = DateTime.Now,
                message = message
            };
            DetTronics.EventTables.InsertOnSubmit ( eventTab );
            DetTronics.SubmitChanges ( );
        }       
        private void ShowSelectedAlarms ( object obj )
        {
            GetAlarmTab=null;
        }       
        public void ShowCurrentAlarms ( object obj )
        {
            GetCurrentAlarmTab = null;
        }
        private void ShowCurrentAlarms1 ( object obj)
        {

            GetCurrentAlarmTab = null;
        }
        public void ShowSelectedEvents ( object obj )
        {

            GetEventTab = null;
        }
        private bool CanExecuted ( object parametr )
        {
            //if (_controllerConnection == "connected")
            //{
            //    return true;
            //}
            return true;
        }

       
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaShablon.model
{
    class Office : ObjectTemplate
    {
        public override ObservableCollection<SmokeDetector> smokeDetector { get; set; } = new ObservableCollection<SmokeDetector>
        {
            new SmokeDetector(),
            new SmokeDetector(),
            new SmokeDetector(),
            new SmokeDetector(),
            new SmokeDetector(),
            new SmokeDetector(),
            new SmokeDetector(),
            new SmokeDetector()
        };
        public override ObservableCollection<AlarmOutput> alarmOutput { get; set; } = new ObservableCollection<AlarmOutput>
        {
            new AlarmOutput(),
            new AlarmOutput()
        };
        public override ObservableCollection<ManualCallPoint> manualCallPoint { get; set; } = new ObservableCollection<ManualCallPoint> ( )
        {
            new ManualCallPoint()
        };
        public override ObservableCollection<FreshAirFan> freshAirFan { get; set; } = new ObservableCollection<FreshAirFan>
        {
            new FreshAirFan(),
            new FreshAirFan(),
            new FreshAirFan()
        };
        public override ObservableCollection<ExhaustFan> exhaustFan { get; set; } = new ObservableCollection<ExhaustFan>
        {
            new ExhaustFan()
        };
        public Office ( )
        {
            //Methods.SetName ( smokeDetector, "SD-" );
            //Methods.SetName ( manualCallPoint, "MCP-" );
            //Methods.SetName ( exhaustFan, "EF-" );
            //Methods.SetName ( freshAirFan, "FA-" );
            //Methods.SetName ( alarmOutput, "BIALS-" );
        }
        public static void GetDeviceStatus ( Office office, ObservableCollection<int [ ]> modbusRegisterAddress )
        {
            Methods.GetAlarm ( office.smokeDetector, modbusRegisterAddress [ 0 ] );
            Methods.GetPreAlarm ( office.smokeDetector, modbusRegisterAddress [ 1 ] );
            Methods.GetFault ( office.smokeDetector, modbusRegisterAddress [ 2 ] );
            Methods.GetFanStatus ( office.freshAirFan, modbusRegisterAddress [3] );
            Methods.GetFanStatus ( office.exhaustFan, modbusRegisterAddress [ 4 ] );
            Methods.GetManualCallPointStatus ( office.manualCallPoint, modbusRegisterAddress [ 5 ] );
            Methods.GetOutputStatus ( office.alarmOutput, modbusRegisterAddress [ 6 ] );
        }
        public static void SetDeviceCommand ( Office office, ObservableCollection<int [ ]> modbusRegisterAddress )
        {
            BitArray bt = new BitArray ( 16 );
            int x = 0;

            foreach ( var item in office.freshAirFan )
            {
                bt [ x ] = item.Start;
                x++;
                bt [ x ] = item.Stop;
                x++;
            }
            foreach ( var item in office.exhaustFan )
            {
                bt [ x ] = item.Start;
                x++;
                bt [ x ] = item.Stop;
                x++;
            }          
            modbusRegisterAddress [ 0 ] = Methods.MirrorWrite ( bt ); 
        }
        public static void TransferEventsToDatebase ( Office office )
        {
            Methods.TransferEventsToDatabase ( office.smokeDetector );
            Methods.TransferEventsToDatabase ( office.manualCallPoint );
            Methods.TransferEventsToDatabase ( office.freshAirFan );
            Methods.TransferEventsToDatabase ( office.exhaustFan );
        }
        private bool MCPAlarm ( )
        {
            bool alarm = false;
            foreach ( var item in manualCallPoint )
            {
                if ( item.Alarm )
                {
                    alarm = true; break;
                }
            }
            return alarm;
        }
        private bool SmokeAlarm ( )
        {
            bool alarm = false;
            foreach ( var item in smokeDetector )
            {
                if ( item.Alarm )
                {
                    alarm = true; break;
                }
            }
            return alarm;
        }
        public bool Alarm ( )
        {
            return SmokeAlarm ( ) || MCPAlarm ( ) ? true : false;
        }
        private bool Zona9Alarm ( )
        {
            return smokeDetector [ 6 ].Alarm || smokeDetector [ 7 ].Alarm ? true : false;
        }
        private bool Zona10Alarm ( )
        {
            return smokeDetector [ 0 ].Alarm || smokeDetector [ 1 ].Alarm || manualCallPoint [ 0 ].Alarm ? true : false;
        }
        private bool Zona11Alarm ( )
        {
            return smokeDetector [ 2 ].Alarm || smokeDetector [ 3 ].Alarm ? true : false;
        }
        private bool Zona12Alarm ( )
        {
            return smokeDetector [ 4 ].Alarm || smokeDetector [ 5 ].Alarm ? true : false;
        }
        public bool [ ] GeneralAlarm ( )
        {
            bool [ ] alarm = new bool [ 5 ];
            alarm [ 0 ] = Zona9Alarm ( );
            alarm [ 1 ] = Zona10Alarm ( );
            alarm [ 2 ] = Zona11Alarm ( );
            alarm [ 3 ] = Zona12Alarm ( );
            alarm [ 4 ] = Alarm ( );
            return alarm;
        }
    }
}

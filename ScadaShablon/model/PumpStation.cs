using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaShablon.model
{
    class PumpStation : ObjectTemplate
    {
        public override ObservableCollection<SmokeDetector> smokeDetector { get; set; } = new ObservableCollection<SmokeDetector>
        {
            new SmokeDetector(),
            new SmokeDetector(),
            new SmokeDetector(),
            new SmokeDetector()
        };
        public override ObservableCollection<AlarmOutput> alarmOutput { get; set; } = new ObservableCollection<AlarmOutput>
        {
            new AlarmOutput()
        };
        public override ObservableCollection<ManualCallPoint> manualCallPoint { get; set; } = new ObservableCollection<ManualCallPoint> ( )
        {
            new ManualCallPoint(),
            new ManualCallPoint()
        };
        public override ObservableCollection<FreshAirFan> freshAirFan { get; set; } = new ObservableCollection<FreshAirFan>
        {
            new FreshAirFan()
        };
        public PumpStation ( )
        {
            //Methods.SetName ( smokeDetector, "SD-" );
            //Methods.SetName ( manualCallPoint, "MCP-" );
            //Methods.SetName ( freshAirFan, "FA-" );
            //Methods.SetName ( alarmOutput, "BIALS-" );
        }
        public static void GetDeviceStatus ( PumpStation ps, ObservableCollection<int [ ]> modbusRegisterAddress )
        {
            Methods.GetAlarm ( ps.smokeDetector, modbusRegisterAddress [ 0 ] );
            Methods.GetPreAlarm ( ps.smokeDetector, modbusRegisterAddress [ 1 ] );
            Methods.GetFault ( ps.smokeDetector, modbusRegisterAddress [ 2 ] );
            Methods.GetFanStatus ( ps.freshAirFan, modbusRegisterAddress [ 3 ] );
            Methods.GetManualCallPointStatus ( ps.manualCallPoint, modbusRegisterAddress [ 4 ] );
            Methods.GetOutputStatus ( ps.alarmOutput, modbusRegisterAddress [ 5 ] );
            //Methods.GetFanStatus ( ps.exhaustFan, modbusRegisterAddress [ 5 ] );
        }
        public static void SetDeviceCommand ( PumpStation ps, ObservableCollection<int [ ]> modbusRegisterAddress )
        {
            BitArray bt = new BitArray ( 16 );
            int x = 0;

            foreach ( var item in ps.freshAirFan )
            {
                bt [ x ] = item.Start;
                x++;
                bt [ x ] = item.Stop;
                x++;
            }
            modbusRegisterAddress [ 0 ] = Methods.MirrorWrite ( bt );
        }
        public static void TransferEventsToDatebase(PumpStation ps )
        {
            Methods.TransferEventsToDatabase ( ps.smokeDetector );
            Methods.TransferEventsToDatabase ( ps.freshAirFan );
            Methods.TransferEventsToDatabase ( ps.manualCallPoint );
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
        private bool Zona7Alarm ( )
        {
            return smokeDetector [ 0 ].Alarm || smokeDetector [ 1 ].Alarm || manualCallPoint [ 0 ].Alarm ? true : false;
        }
        private bool Zona8Alarm ( )
        {
            return smokeDetector [ 2 ].Alarm || smokeDetector [ 3 ].Alarm || manualCallPoint [ 1 ].Alarm ? true : false;
        }

        public bool [ ] GeneralAlarm ( )
        {
            bool [ ] alarm = new bool [ 3 ];
            alarm [ 0 ] = Zona7Alarm ( );
            alarm [ 1 ] = Zona8Alarm ( );
            alarm [ 2 ] = Alarm ( );
            return alarm;
        }
    }
}

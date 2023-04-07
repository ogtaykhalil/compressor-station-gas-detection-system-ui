using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaShablon.model
{
    class OperatorRoom : ObjectTemplate
    {
        public override ObservableCollection<SmokeDetector> smokeDetector { get; set; } = new ObservableCollection<SmokeDetector>
        {
            new SmokeDetector(),
            new SmokeDetector(),
            new SmokeDetector(),
            new SmokeDetector(),
            new SmokeDetector(),
            new SmokeDetector()
        };
        public override ObservableCollection<ExhaustFan> exhaustFan { get; set; } = new ObservableCollection<ExhaustFan>
        {
            new ExhaustFan(),
            new ExhaustFan()
        };
        public override ObservableCollection<ManualCallPoint> manualCallPoint { get; set; } = new ObservableCollection<ManualCallPoint> ( )
        {
            new ManualCallPoint()
        };
        public override ObservableCollection<FreshAirFan> freshAirFan { get; set; } = new ObservableCollection<FreshAirFan>
        {
            new FreshAirFan(),
            new FreshAirFan()
        };
        
        public OperatorRoom ( )
        {
            //Methods.SetName ( smokeDetector, "SD-" );
            //Methods.SetName ( manualCallPoint, "MCP-" );
            //Methods.SetName ( exhaustFan, "EF-" );
            //Methods.SetName ( freshAirFan, "FA-" );
        }
        public static void GetDeviceStatus ( OperatorRoom cr, ObservableCollection<int [ ]> modbusRegisterAddress )
        {
            Methods.GetAlarm ( cr.smokeDetector, modbusRegisterAddress [ 0 ] );
            Methods.GetPreAlarm ( cr.smokeDetector, modbusRegisterAddress [ 1 ] );
            Methods.GetFault ( cr.smokeDetector, modbusRegisterAddress [ 2 ] );
            Methods.GetFanStatus ( cr.freshAirFan, modbusRegisterAddress [ 3 ] );
            Methods.GetManualCallPointStatus ( cr.manualCallPoint, modbusRegisterAddress [ 4 ] );
            Methods.GetFanStatus ( cr.exhaustFan, modbusRegisterAddress [ 5 ] );
            //Methods.GetFanStatus ( ps.exhaustFan, modbusRegisterAddress [ 5 ] );
        }
        public static void SetDeviceCommand ( OperatorRoom cr, ObservableCollection<int [ ]> modbusRegisterAddress )
        {
            BitArray bt = new BitArray ( 16 );
            int x = 0;

            foreach ( var item in cr.freshAirFan )
            {
                bt [ x ] = item.Start;
                x++;
                bt [ x ] = item.Stop;
                x++;
            }
            foreach ( var item in cr.exhaustFan )
            {
                bt [ x ] = item.Start;
                x++;
                bt [ x ] = item.Stop;
                x++;
            }
            modbusRegisterAddress [ 0 ] = Methods.MirrorWrite ( bt );
        }
        public static void TransferEventsToDatebase ( OperatorRoom cr )
        {
            Methods.TransferEventsToDatabase ( cr.smokeDetector );
            Methods.TransferEventsToDatabase ( cr.manualCallPoint );
            Methods.TransferEventsToDatabase ( cr.freshAirFan );
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
        private bool Zona2Alarm ( )
        {
            return smokeDetector [ 2 ].Alarm || smokeDetector [ 3 ].Alarm ? true : false;
        }
        private bool Zona3Alarm ( )
        {
            return smokeDetector [ 0 ].Alarm || smokeDetector [ 1 ].Alarm || manualCallPoint[0].Alarm ? true : false;
        }
        private bool Zona4Alarm ( )
        {
            return smokeDetector [ 4 ].Alarm || smokeDetector [ 5 ].Alarm ? true : false;
        }
        public bool[] GeneralAlarm ( )
        {
            bool [ ] alarm = new bool [ 4 ];
            alarm [ 0 ] = Zona2Alarm ( );
            alarm [ 1 ] = Zona3Alarm ( );
            alarm [ 2 ] = Zona4Alarm ( );
            alarm [ 3 ] = Alarm ( );
            return alarm;
        }
    }
}

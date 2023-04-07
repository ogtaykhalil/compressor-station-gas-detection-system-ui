using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaShablon.model
{
    class DressingRoom : ObjectTemplate
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
            new ManualCallPoint()
        };
        public override ObservableCollection<FreshAirFan> freshAirFan { get; set; } = new ObservableCollection<FreshAirFan>
        {
            new FreshAirFan()
        };
        public override ObservableCollection<ExhaustFan> exhaustFan { get; set; } = new ObservableCollection<ExhaustFan>
        {
            new ExhaustFan()
        };
        public DressingRoom ( )
        {
            //Methods.SetName ( smokeDetector, "SD-" );
            //Methods.SetName ( manualCallPoint, "MCP-" );
            //Methods.SetName ( exhaustFan, "EF-" );
            //Methods.SetName ( freshAirFan, "FA-" );
            //Methods.SetName ( alarmOutput, "BIALS-" );
        }
        public static void GetDeviceStatus ( DressingRoom dr, ObservableCollection<int [ ]> modbusRegisterAddress )
        {
            Methods.GetAlarm ( dr.smokeDetector, modbusRegisterAddress [ 0 ] );
            Methods.GetPreAlarm ( dr.smokeDetector, modbusRegisterAddress [ 1 ] );
            Methods.GetFault ( dr.smokeDetector, modbusRegisterAddress [ 2 ] );
            Methods.GetFanStatus ( dr.exhaustFan, modbusRegisterAddress [ 3 ] );
            Methods.GetFanStatus ( dr.freshAirFan, modbusRegisterAddress [ 4 ] );
            Methods.GetManualCallPointStatus ( dr.manualCallPoint, modbusRegisterAddress [ 5 ] );
            Methods.GetOutputStatus ( dr.alarmOutput, modbusRegisterAddress [ 6 ] );
        }
        public static void SetDeviceCommand ( DressingRoom dr, ObservableCollection<int [ ]> modbusRegisterAddress )
        {
            BitArray bt = new BitArray ( 16 );
            int x = 0;

            foreach ( var item in dr.exhaustFan )
            {
                bt [ x ] = item.Start;
                x++;
                bt [ x ] = item.Stop;
                x++;
            }
            foreach ( var item in dr.freshAirFan )
            {
                bt [ x ] = item.Start;
                x++;
                bt [ x ] = item.Stop;
                x++;
            }
            modbusRegisterAddress [ 0 ] = Methods.MirrorWrite ( bt );
        }
        public static void TransferEventsToDatebase ( DressingRoom dr )
        {
            Methods.TransferEventsToDatabase ( dr.smokeDetector );
            Methods.TransferEventsToDatabase ( dr.exhaustFan );
            Methods.TransferEventsToDatabase ( dr.manualCallPoint );
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
        private bool Zona5Alarm ( )
        {
            return smokeDetector [ 2 ].Alarm || smokeDetector [ 3 ].Alarm ? true : false;
        }
        private bool Zona6Alarm ( )
        {
            return smokeDetector [ 0 ].Alarm || smokeDetector [ 1 ].Alarm || manualCallPoint [ 0 ].Alarm ? true : false;
        }
      
        public bool [ ] GeneralAlarm ( )
        {
            bool [ ] alarm = new bool [ 3 ];
            alarm [ 0 ] = Zona5Alarm ( );
            alarm [ 1 ] = Zona6Alarm ( );
            alarm [ 2 ] = Alarm ( );
            return alarm;
        }
    }
}

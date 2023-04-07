using ScadaShablon.tool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ScadaShablon.model
{
    struct Methods
    {
        public static BitArray MirrorRead ( int [ ] register )
        {
            BitArray bitArray = new BitArray ( register );
            BitArray bit = new BitArray ( 16 );
            for ( int i = 0 ; i < bit.Length - 1 ; i++ )
            {
                bit [ i ] = bitArray [ Math.Abs ( i - 15 ) ];
            }
            return bit;
        }
        public static int [ ] MirrorWrite ( BitArray bt )
        {
            int [ ] x = new int [ 1 ];
            BitArray buffer = new BitArray ( bt.Length );
            for ( int i = 0 ; i < bt.Length ; i++ )
            {
                buffer [ i ] = bt [ ( bt.Length - 1 ) - i ];
            }
            buffer.CopyTo ( x, 0 );
            return x;
        }
        
        #region ALTERNATIV METHODLAR
        public static void GetCompressorDeviceStatus ( CompressorStation compressorStation, ObservableCollection<int [ ]> holdingRegister )
        {
            GetGasDetectorScaledValue ( compressorStation.compressor );
            GetExhaustFanFault ( compressorStation.compressor );
            GetFlameDetectorAlarm ( compressorStation.compressor, holdingRegister [ 0 ] );
            GetFlameDetectorPreAlarm ( compressorStation.compressor, holdingRegister [ 1 ] );
            GetFlameDetectorFault ( compressorStation.compressor, holdingRegister [ 2 ] );
            GetGasDetectorAlarm ( compressorStation.compressor, holdingRegister [ 3 ] );
            GetGasDetectorPreAlarm ( compressorStation.compressor, holdingRegister [ 4 ] );
            GetGasDetectorFault ( compressorStation.compressor, holdingRegister [ 5 ] );
            GetExhaustFanFeedback ( compressorStation.compressor, holdingRegister [ 6 ] );
            GetExhaustFanStartStatus ( compressorStation.compressor, holdingRegister [ 7 ] );
            GetExhaustFanStopStatus ( compressorStation.compressor, holdingRegister [ 8 ] );
            GetExhaustFanAutoManStatus ( compressorStation.compressor, holdingRegister [ 9 ] );
            GetExhaustFanGMReleFault ( compressorStation.compressor, holdingRegister [ 10 ] );
            GetFlameDetectorEnableStatus ( compressorStation.compressor, holdingRegister [ 11 ] );
            GetGasDetectorEnableStatus( compressorStation.compressor, holdingRegister [ 12 ] );
            //GetFreshAirFanStopStatus ( compressorStation.compressor, holdingRegister [ 12 ] );
            //GetFreshAirFanAutoManStatus ( compressorStation.compressor, holdingRegister [ 13 ] );
        }
        public static void SetCompressorDeviceCommand ( CompressorStation compressorStation, ObservableCollection<int [ ]> modbusRegisterAddress )
        {
            modbusRegisterAddress [ 0 ] = SetFlameDetectorEnableCommand ( compressorStation.compressor );
            modbusRegisterAddress [ 1 ] = SetGasDetectorEnableCommand ( compressorStation.compressor );
            modbusRegisterAddress [ 2 ] = SetExhaustFanStartCommand ( compressorStation.compressor );
            modbusRegisterAddress [ 3 ] = SetExhaustFanStopCommand ( compressorStation.compressor );
            modbusRegisterAddress [ 4 ] = SetExhaustFanAutoManCommand ( compressorStation.compressor );
            //modbusRegisterAddress [ 5 ] = SetFreshAirFanStartCommand ( compressorStation.compressor );
            //modbusRegisterAddress [ 6 ] = SetFreshAirFanStopCommand ( compressorStation.compressor );
            //modbusRegisterAddress [ 7 ] = SetFreshAirFanAutoManCommand ( compressorStation.compressor );
        }
        
        private static void GetFlameDetectorAlarm ( IEnumerable<Compressor> compressors, int [ ] holdingRegister )
        {
            BitArray bit = Methods.MirrorRead ( holdingRegister );
            int x = 0;
            foreach ( var compressor in compressors )
            {
                foreach ( var detector in compressor.flameDetector )
                {
                    detector.Alarm = bit [ x ];
                    x++;
                }
            }
        }
        private static void GetFlameDetectorEnableStatus(IEnumerable<Compressor> compressors, int[] holdingRegister)
        {
            BitArray bit = Methods.MirrorRead(holdingRegister);
            int x = 0;
            foreach (var compressor in compressors)
            {
                foreach (var detector in compressor.flameDetector)
                {
                    detector.EnableStatus = bit[x];
                    x++;
                }
            }
        }
        private static void GetGasDetectorEnableStatus(IEnumerable<Compressor> compressors, int[] holdingRegister)
        {
            BitArray bit = Methods.MirrorRead(holdingRegister);
            int x = 0;
            foreach (var compressor in compressors)
            {
                foreach (var detector in compressor.gasDetector)
                {
                    detector.EnableStatus = bit[x];
                    x++;
                }
            }
        }
        private static void GetFlameDetectorPreAlarm ( IEnumerable<Compressor> compressors, int [ ] holdingRegister )
        {
            BitArray bit = Methods.MirrorRead ( holdingRegister );
            int x = 0;
            foreach ( var compressor in compressors )
            {
                foreach ( var detector in compressor.flameDetector )
                {
                    detector.PreAlarm = bit [ x ];
                    x++;
                }
            }
        }
        private static void GetFlameDetectorFault ( IEnumerable<Compressor> compressors, int [ ] holdingRegister )
        {
            BitArray bit = Methods.MirrorRead ( holdingRegister );
            int x = 0;
            foreach ( var compressor in compressors )
            {
                foreach ( var detector in compressor.flameDetector )
                {
                    detector.Fault = bit [ x ];
                    x++;
                }
            }
        }
        private static void GetGasDetectorAlarm ( IEnumerable<Compressor> compressors, int [ ] holdingRegister )
        {
            BitArray bit = Methods.MirrorRead ( holdingRegister );
            int x = 0;
            foreach ( var compressor in compressors )
            {
                foreach ( var detector in compressor.gasDetector )
                {
                    detector.Alarm = bit [ x ];
                    x++;
                }
            }
        }
        private static void GetGasDetectorPreAlarm ( IEnumerable<Compressor> compressors, int [ ] holdingRegister )
        {
            BitArray bit = Methods.MirrorRead ( holdingRegister );
            int x = 0;
            foreach ( var compressor in compressors )
            {
                foreach ( var detector in compressor.gasDetector )
                {
                    detector.PreAlarm = bit [ x ];
                    x++;
                }
            }
        }
        private static void GetGasDetectorFault ( IEnumerable<Compressor> compressors, int [ ] holdingRegister )
        {
            BitArray bit = Methods.MirrorRead ( holdingRegister );
            int x = 0;
            foreach ( var compressor in compressors )
            {
                foreach ( var detector in compressor.gasDetector )
                {
                    detector.Fault = bit [ x ];
                    x++;
                }
            }
        }
        private static void GetExhaustFanFeedback ( IEnumerable<Compressor> compressors, int [ ] holdingRegister )
        {
            BitArray bit = Methods.MirrorRead ( holdingRegister );
            int x = 0;
            foreach ( var compressor in compressors )
            {
                foreach ( var fan in compressor.exhaustFan )
                {
                    fan.Feedback = bit [ x ];
                    x++;
                }
            }
        }
        private static void GetExhaustFanGMReleFault ( IEnumerable<Compressor> compressors, int [ ] holdingRegister )
        {
            BitArray bit = Methods.MirrorRead ( holdingRegister );
            int x = 0;
            foreach ( var compressor in compressors )
            {
                foreach ( var fan in compressor.exhaustFan )
                {
                    fan.FaultGMR = bit [ x ];
                    x++;
                }
            }
        }
        private static void GetExhaustFanStartStatus ( IEnumerable<Compressor> compressors, int [ ] holdingRegister )
        {
            BitArray bit = Methods.MirrorRead ( holdingRegister );
            int x = 0;
            foreach ( var compressor in compressors )
            {
                foreach ( var fan in compressor.exhaustFan )
                {
                    fan.StartStatus = bit [ x ];
                    x++;
                }
            }
        }
        private static void GetExhaustFanStopStatus ( IEnumerable<Compressor> compressors, int [ ] holdingRegister )
        {
            BitArray bit = Methods.MirrorRead ( holdingRegister );
            int x = 0;
            foreach ( var compressor in compressors )
            {
                foreach ( var fan in compressor.exhaustFan )
                {
                    fan.StopStatus = bit [ x ];
                    x++;
                }
            }
        }
        private static void GetExhaustFanAutoManStatus ( IEnumerable<Compressor> compressors, int [ ] holdingRegister )
        {
            BitArray bit = Methods.MirrorRead ( holdingRegister );
            int x = 0;
            foreach ( var compressor in compressors )
            {
                foreach ( var fan in compressor.exhaustFan )
                {
                    fan.AutoManStatus = bit [ x ];
                    x++;
                }
            }
        }
        private static void GetExhaustFanFault ( IEnumerable<Compressor> compressors )
        {
            
            foreach ( var compressor in compressors )
            {
                foreach ( var fan in compressor.exhaustFan )
                {
                    fan.Fault = false;
                   
                }
            }
        }
        private static void GetGasDetectorScaledValue ( IEnumerable<Compressor> compressors )
        {

            foreach ( var compressor in compressors )
            {
                foreach ( var detector in compressor.gasDetector )
                {
                    detector.ProssesScaledValue = 0.0;

                }
            }
        }
        public static void GetGasDetectorProssesValue ( IEnumerable<Compressor> compressors, int [ ] holdingRegister )
        {
            int x = 0;
            foreach ( var compressor in compressors )
            {
                foreach ( var detector in compressor.gasDetector )
                {
                    detector.ProssesAnalogValue = holdingRegister [ x ];
                    x++;
                }
            }
        }
        public static void SetGasDetectorProssesValue ( IEnumerable<Compressor> compressors )
        {
            foreach ( var compressor in compressors )
            {
                foreach ( var detector in compressor.gasDetector )
                {
                    detector.ProssesScaledValueLowRange = 0.0;
                    detector.ProssesScaledValueHighRange = 100.0;
                    detector.ProssesAnalogValueLowRange = 0;
                    detector.ProssesAnalogValueHighRange = 27648;
                }
            }
        }
               
        public static void GetBuildingDeviceStatus<T> ( T buildings, ObservableCollection<int [ ]> holdingRegister ) where T: ObjectTemplate
        {    
            GetAlarm ( buildings.smokeDetector, holdingRegister [ 0 ] );
            GetPreAlarm ( buildings.smokeDetector, holdingRegister [ 1 ] );
            GetFault ( buildings.smokeDetector, holdingRegister [ 2 ] );
           
           
              
            
                GetOutputStatus ( buildings.alarmOutput, holdingRegister [ 6 ] );
                      
        }
       
        public static void GetFanStatus<T> ( IEnumerable< T> device, int [ ] holdingRegister ) where T : Actuator
        {
            int x = 0;
            BitArray bit = MirrorRead ( holdingRegister );
            foreach ( var fan in device )
            {
                fan.Feedback = bit [ x ];
                x++;
                fan.StartStatus = bit [ x ];
                x++;
                fan.StopStatus = bit [ x ];
                x++;
                fan.Fault = bit [ x ];
                x++;
            }
        }
        public static void GetManualCallPointStatus<T> ( IEnumerable<T> device, int [ ] holdingRegister ) where T : IStatus
        {
            int x = 0;
            BitArray bit = MirrorRead ( holdingRegister );
            foreach ( var mcp in device )
            {
                mcp.Alarm = bit [ x ];
                x++;
                mcp.PreAlarm = bit [ x ];
                x++;
                mcp.Fault = bit [ x ];
                x++;
            }
        }

        public static int [ ] SetBuildingDeviceCommand<T> ( T buildings ) where T : ObjectTemplate
        {
            BitArray bt = new BitArray ( 16 );
            int x = 0;
           
                foreach ( var item in buildings.freshAirFan )
                {
                    bt [ x ] = item.Start;
                    x++;
                    bt [ x ] = item.Stop;
                    x++;
                }
           
            
                foreach ( var item in buildings.exhaustFan )
                {
                    bt [ x ] = item.Start;
                    x++;
                    bt [ x ] = item.Stop;
                    x++;
                }
                    
            return MirrorWrite ( bt );
        }

        /* private static void GetFreshAirFanFeedback ( IEnumerable<Compressor> compressors, int [ ] holdingRegister )
         {
             BitArray bit = Methods.MirrorRead ( holdingRegister );
             int x = 0;
             foreach ( var compressor in compressors )
             {
                 foreach ( var fan in compressor.freshAirFan )
                 {
                     fan.Feedback = bit [ x ];
                     x++;
                 }
             }
         }
         private static void GetFreshAirFanStartStatus ( IEnumerable<Compressor> compressors, int [ ] holdingRegister )
         {
             BitArray bit = Methods.MirrorRead ( holdingRegister );
             int x = 0;
             foreach ( var compressor in compressors )
             {
                 foreach ( var fan in compressor.freshAirFan )
                 {
                     fan.StartStatus = bit [ x ];
                     x++;
                 }
             }
         }
         private static void GetFreshAirFanStopStatus ( IEnumerable<Compressor> compressors, int [ ] holdingRegister )
         {
             BitArray bit = Methods.MirrorRead ( holdingRegister );
             int x = 0;
             foreach ( var compressor in compressors )
             {
                 foreach ( var fan in compressor.freshAirFan )
                 {
                     fan.StopStatus = bit [ x ];
                     x++;
                 }
             }
         }
         private static void GetFreshAirFanAutoManStatus ( IEnumerable<Compressor> compressors, int [ ] holdingRegister )
         {
             BitArray bit = Methods.MirrorRead ( holdingRegister );
             int x = 0;
             foreach ( var compressor in compressors )
             {
                 foreach ( var fan in compressor.freshAirFan )
                 {
                     fan.AutoManStatus = bit [ x ];
                     x++;
                 }
             }
         }
         
             
             
             private static int [ ] SetFreshAirFanStartCommand ( IEnumerable<Compressor> compressors )
        {
            BitArray bt = new BitArray ( 16 );
            int x = 0;
            foreach ( var compressor in compressors )
            {
                foreach ( var fan in compressor.freshAirFan )
                {
                    bt [ x ] = fan.Start;
                    x++;
                }
            }
            return Methods.MirrorWrite ( bt );
        }
        private static int [ ] SetFreshAirFanStopCommand ( IEnumerable<Compressor> compressors )
        {
            BitArray bt = new BitArray ( 16 );
            int x = 0;
            foreach ( var compressor in compressors )
            {
                foreach ( var fan in compressor.freshAirFan )
                {
                    bt [ x ] = fan.Stop;
                    x++;
                }
            }
            return Methods.MirrorWrite ( bt );
        }
        private static int [ ] SetFreshAirFanAutoManCommand ( IEnumerable<Compressor> compressors )
        {
            BitArray bt = new BitArray ( 16 );
            int x = 0;
            foreach ( var compressor in compressors )
            {
                foreach ( var fan in compressor.freshAirFan )
                {
                    bt [ x ] = fan.AutoMan;
                    x++;
                }
            }
            return Methods.MirrorWrite ( bt );
        }*/

        public static void SetCompressorDeviceName ( IEnumerable<Compressor> compressors, string f, string g, string e )
        {
            int s=1, m=1, n=1; 
            foreach ( var compressor in compressors )
            {
                foreach ( var item in compressor.flameDetector )
                {
                    item.Name = f + s;
                    s++;
                }
                
                foreach ( var item in compressor.gasDetector )
                {
                    item.Name = g + m;
                    m++;
                }
                
                foreach ( var item in compressor.exhaustFan )
                {
                    item.Name = e + n;
                    n++;
                }
                
                //foreach ( var item in compressor.freshAirFan )
                //{
                //    item.Name = fa + n;
                //    n++;
                //}
            }
            
        }
        private static int [ ] SetFlameDetectorEnableCommand ( IEnumerable<Compressor> compressors )
        {
            BitArray bt = new BitArray ( 16 );
            int x = 0;
            foreach ( var compressor in compressors )
            {
                foreach ( var detector in compressor.flameDetector )
                {
                    bt [ x ] = detector.Enable;
                    x++;
                }
            }
            return Methods.MirrorWrite ( bt );
        }
        private static int [ ] SetGasDetectorEnableCommand ( IEnumerable<Compressor> compressors )
        {
            BitArray bt = new BitArray ( 16 );
            int x = 0;
            foreach ( var compressor in compressors )
            {
                foreach ( var detector in compressor.gasDetector )
                {
                    bt [ x ] = detector.Enable;
                    x++;
                }
            }
            return Methods.MirrorWrite ( bt );
        }
        private static int [ ] SetExhaustFanStartCommand ( IEnumerable<Compressor> compressors )
        {
            BitArray bt = new BitArray ( 16 );
            int x = 0;
            foreach ( var compressor in compressors )
            {
                foreach ( var fan in compressor.exhaustFan )
                {
                    bt [ x ] = fan.Start;
                    x++;
                }
            }
            return Methods.MirrorWrite ( bt );
        }
        private static int [ ] SetExhaustFanStopCommand ( IEnumerable<Compressor> compressors )
        {
            BitArray bt = new BitArray ( 16 );
            int x = 0;
            foreach ( var compressor in compressors )
            {
                foreach ( var fan in compressor.exhaustFan )
                {
                    bt [ x ] = fan.Stop;
                    x++;
                }
            }
            return Methods.MirrorWrite ( bt );
        }
        private static int [ ] SetExhaustFanAutoManCommand ( IEnumerable<Compressor> compressors )
        {
            BitArray bt = new BitArray ( 16 );
            int x = 0;
            foreach ( var compressor in compressors )
            {
                foreach ( var fan in compressor.exhaustFan )
                {
                    bt [ x ] = fan.AutoMan;
                    x++;
                }
            }
            return Methods.MirrorWrite ( bt );
        }

        #endregion
        public static void TransferEventsToDatabase<T> ( IEnumerable<T> devices ) where T:IStatus
        {         
             foreach ( var device in devices )
             {
                 device.AlarmFault ( );
             }     
        }
        public static void GetAutoManStatus<T> ( IEnumerable<T> device, int [ ] holdingRegister ) where T : Actuator
        {
            int x = 0;
            BitArray bit = MirrorRead ( holdingRegister );
            foreach ( var item in device )
            {
                item.AutoManStatus = bit [ x ];
                x++;
            }
        }
        public static void Feedback<T> ( IEnumerable<T> device, int [ ] holdingRegister ) where T : Actuator
        {
            int x = 0;
            BitArray bit = MirrorRead ( holdingRegister );
            foreach ( var item in device )
            {
                item.Feedback = bit [ x ];
                x++;
            }
        }
        public static void IsClosed<T> ( IEnumerable<T> device, int [ ] holdingRegister ) where T : Actuator
        {
            int x = 0;
            BitArray bit = MirrorRead ( holdingRegister );
            foreach ( var item in device )
            {
                item.IsClosed = bit [ x ];
                x++;
            }
        }
        public static void IsOpen<T> ( IEnumerable<T> device, int [ ] holdingRegister ) where T : Actuator
        {
            int x = 0;
            BitArray bit = MirrorRead ( holdingRegister );
            foreach ( var item in device )
            {
                item.IsOpen = bit [ x ];
                x++;
            }
        }
        public static void IsStarted<T> ( IEnumerable<T> device, int [ ] holdingRegister ) where T : Actuator
        {
            int x = 0;
            BitArray bit = MirrorRead ( holdingRegister );
            foreach ( var item in device )
            {
                item.StartStatus = bit [ x ];
                x++;
            }
        }
        public static void IsStopped<T> ( IEnumerable<T> device, int [ ] holdingRegister ) where T : Actuator
        {
            int x = 0;
            BitArray bit = MirrorRead ( holdingRegister );
            foreach ( var item in device )
            {
                item.StopStatus = bit [ x ];
                x++;
            }
        }
        public static void GetAlarm<T> ( IEnumerable<T> device, int [ ] holdingRegister ) where T : IStatus
        {
            int x = 0;
            BitArray bit = MirrorRead ( holdingRegister );
            foreach ( var item in device )
            {
                item.Alarm = bit [ x ];
                x++;
            }
        }
        public static void GetPreAlarm<T> ( IEnumerable<T> device, int [ ] holdingRegister ) where T : IStatus
        {
            int x = 0;
            BitArray bit = MirrorRead ( holdingRegister );
            foreach ( var item in device )
            {
                item.PreAlarm = bit [ x ];
                x++;
            }
        }
        public static void GetFault<T> ( IEnumerable<T> device, int [ ] holdingRegister ) where T : IStatus
        {
            int x = 0;
            BitArray bit = MirrorRead ( holdingRegister );
            foreach ( var item in device )
            {
                item.Fault = bit [ x ];
                x++;
            }
        }
        public static void GetGMIReleFault<T> ( IEnumerable<T> device, int [ ] holdingRegister ) where T : Actuator
        {
            int x = 0;
            BitArray bit = MirrorRead ( holdingRegister );
            foreach ( var item in device )
            {
                item.FaultGMR = bit [ x ];
                x++;
            }
        }
        public static void GetProssesValue<T> ( IEnumerable<T> device, int [ ] holdingRegister ) where T : Sensor
        {
            int x = 0;
            foreach ( var item in device )
            {
                item.ProssesScaledValue = holdingRegister [ x ];
                x++;
            }
        }
        public static void GetOutputStatus<T> ( IEnumerable<T> device, int [ ] holdingRegister ) where T : AlarmOutput
        {
            int x = 0;
            BitArray bit = MirrorRead ( holdingRegister );
            foreach ( var item in device )
            {
                item.OutputStatus = bit [ x ];
                x++;
            }
        }
        public static void SetName<T> ( IEnumerable<T> device, string name ) where T : IStatus
        {
            int n = 1;
            foreach ( var item in device )
            {
                item.Name = name + n;
                n++;
            }
        }
        public static void SetName<T> ( IEnumerable<T> device, string name, string name1 ) where T : IStatus
        {
            int n = 1;
            foreach ( var item in device )
            {
                if ( n < 5 )
                    item.Name = name + n;
                else
                    item.Name = name +( n - 4 ) + name1;
                n++;
            }
        }
        public static void SetOpenLoopDetectorName<T> ( IEnumerable<T> device, string name ) where T : IStatus
        {
            int n = 13;
            foreach ( var item in device )
            {
                item.Name = name + n;
                n++;
            }
            n = 13;
        }
        public static int [ ] SetEnableCommand<T> ( IEnumerable<T> device ) where T : IStatus
        {
            BitArray bt = new BitArray ( 16 );
            int x = 0;                        
            foreach ( var item in device )
            {
                bt[x] = item.Enable;
                x++;
            }                
            return MirrorWrite ( bt );
        }
        public static int [ ] SetStartCommand<T> ( IEnumerable<T> device ) where T : Actuator
        {
            BitArray bt = new BitArray ( 16 );
            int x = 0;
            foreach ( var item in device )
            {
                bt [ x ] = item.Start;
                x++;
            }
            return MirrorWrite ( bt );
        }
        public static int [ ] SetStopCommand<T> ( IEnumerable<T> device ) where T : Actuator
        {
            BitArray bt = new BitArray ( 16 );
            int x = 0;
            foreach ( var item in device )
            {
                bt [ x ] = item.Stop;
                x++;
            }
            return MirrorWrite ( bt );
        }
        public static int [ ] SetAutoManCommand<T> ( IEnumerable<T> device ) where T : Actuator
        {
            BitArray bt = new BitArray ( 16 );
            int x = 0;
            foreach ( var item in device )
            {
                bt [ x ] = item.AutoMan;
                x++;
            }
            return MirrorWrite ( bt );
        }
        public static int [ ] SetOpenCommand<T> ( IEnumerable<T> device ) where T : Actuator
        {
            BitArray bt = new BitArray ( 16 );
            int x = 0;
            foreach ( var item in device )
            {
                bt [ x ] = item.Start;
                x++;
            }
            return MirrorWrite ( bt );
        }
        public static int [ ] SetCloseCommand<T> ( IEnumerable<T> device ) where T : Actuator
        {
            BitArray bt = new BitArray ( 16 );
            int x = 0;
            foreach ( var item in device )
            {
                bt [ x ] = item.Stop;
                x++;
            }
            return MirrorWrite ( bt );
        }      
        public static void CompressorStatinonSetName ( CompressorStation cp )
        {
            cp.compressor [ 0 ].flameDetector [ 0 ].Name = "BTF1.1";
            cp.compressor [ 0 ].flameDetector [ 1 ].Name = "BTF1.7";
            cp.compressor [ 1 ].flameDetector [ 0 ].Name = "BTF1.2";
            cp.compressor [ 1 ].flameDetector [ 1 ].Name = "BTF1.8";
            cp.compressor [ 2 ].flameDetector [ 0 ].Name = "BTF1.3";
            cp.compressor [ 2 ].flameDetector [ 1 ].Name = "BTF1.9";
            cp.compressor [ 3 ].flameDetector [ 0 ].Name = "BTF1.4";
            cp.compressor [ 3 ].flameDetector [ 1 ].Name = "BTF1.10";
            cp.compressor [ 4 ].flameDetector [ 0 ].Name = "BTF1.5";
            cp.compressor [ 4 ].flameDetector [ 1 ].Name = "BTF1.11";
            cp.compressor [ 5 ].flameDetector [ 0 ].Name = "BTF1.6";
            cp.compressor [ 5 ].flameDetector [ 1 ].Name = "BTF1.12";
            
            cp.compressor [ 0 ].gasDetector [ 0 ].Name = "QFT1.1";
            cp.compressor [ 0 ].gasDetector [ 1 ].Name = "QFT1.7";
            cp.compressor [ 1 ].gasDetector [ 0 ].Name = "QFT1.2";
            cp.compressor [ 1 ].gasDetector [ 1 ].Name = "QFT1.8";
            cp.compressor [ 2 ].gasDetector [ 0 ].Name = "QFT1.3";
            cp.compressor [ 2 ].gasDetector [ 1 ].Name = "QFT1.9";
            cp.compressor [ 3 ].gasDetector [ 0 ].Name = "QFT1.4";
            cp.compressor [ 3 ].gasDetector [ 1 ].Name = "QFT1.10";
            cp.compressor [ 4 ].gasDetector [ 0 ].Name = "QFT1.5";
            cp.compressor [ 4 ].gasDetector [ 1 ].Name = "QFT1.11";
            cp.compressor [ 5 ].gasDetector [ 0 ].Name = "QFT1.6";
            cp.compressor [ 5 ].gasDetector [ 1 ].Name = "QFT1.12";
            
            cp.gasDetector [ 0 ].Name = "QFE1.13";
            cp.gasDetector [ 1 ].Name = "QFE1.14";
            
            cp.compressor [ 0 ].exhaustFan [ 0 ].Name = "QS1.1";
            cp.compressor [ 1 ].exhaustFan [ 0 ].Name = "QS1.2";
            cp.compressor [ 2 ].exhaustFan [ 0 ].Name = "QS1.3";
            cp.compressor [ 3 ].exhaustFan [ 0 ].Name = "QS1.4";
            cp.compressor [ 4 ].exhaustFan [ 0 ].Name = "QS1.5";
            cp.compressor [ 5 ].exhaustFan [ 0 ].Name = "QS1.6";

            cp.freshAirFan [ 0 ].Name = "QV1.1";
            cp.freshAirFan [ 1 ].Name = "QV1.2";
            cp.freshAirFan [ 2 ].Name = "QV1.3";
            cp.freshAirFan [ 3 ].Name = "QV1.4";

            cp.manualCallPoint [ 0 ].Name = "BTM1.1";
            cp.manualCallPoint [ 1 ].Name = "BTM1.2";
            cp.manualCallPoint [ 2 ].Name = "BTM1.3";
            cp.manualCallPoint [ 3 ].Name = "BTM1.4";
            cp.manualCallPoint [ 4 ].Name = "BTM1.6";
            cp.manualCallPoint [ 5 ].Name = "BTM1.5";
            
            cp.alarmOutput [ 0 ].Name = "BIAL1.1Q";
            cp.alarmOutput [ 1 ].Name = "BIAL1.1";
            cp.alarmOutput [ 2 ].Name = "BIAL1.2Q";
            cp.alarmOutput [ 3 ].Name = "BIAL1.2";
            cp.alarmOutput [ 4 ].Name = "BIAL1.3Q";
            cp.alarmOutput [ 5 ].Name = "BIAL1.6Q";
            cp.alarmOutput [ 6 ].Name = "BIAL1.4";
            cp.alarmOutput [ 7 ].Name = "BIAL1.5Q";
            cp.alarmOutput [ 8 ].Name = "BIAL1.3";
            cp.alarmOutput [ 9 ].Name = "BIAL1.4Q";
        }
        public static void PumpStationSetName(PumpStation ps )
        {
            ps.manualCallPoint [ 0 ].Name = "BTM7.3";
            ps.manualCallPoint [ 1 ].Name = "BTM8.3";
            ps.smokeDetector [ 0 ].Name = "BTH7.1";
            ps.smokeDetector [ 1 ].Name = "BTH7.2";
            ps.smokeDetector [ 2 ].Name = "BTHK8.1";
            ps.smokeDetector [ 3 ].Name = "BTHK8.2";
            ps.alarmOutput [ 0 ].Name = "BIALS7.4";
            ps.freshAirFan [ 0 ].Name = "K7.1";
        }
        public static void OfficeSetName ( Office o )
        {
            o.manualCallPoint [ 0 ].Name = "BTM10.1";
            o.smokeDetector [ 0 ].Name = "BTH9.1";
            o.smokeDetector [ 1 ].Name = "BTH9.2";
            o.smokeDetector [ 2 ].Name = "BTH10.2";
            o.smokeDetector [ 3 ].Name = "BTH10.3";
            o.smokeDetector [ 4 ].Name = "BTHK11.1";
            o.smokeDetector [ 5 ].Name = "BTHK11.2";
            o.smokeDetector [ 6 ].Name = "BTH12.1";
            o.smokeDetector [ 7 ].Name = "BTH12.3";
            o.alarmOutput [ 0 ].Name = "BIALS9.3";
            o.alarmOutput [ 1 ].Name = "BIALS12.2";
            o.freshAirFan [ 0 ].Name = "V9.1";
            o.freshAirFan [ 1 ].Name = "K9.1";
            o.freshAirFan [ 2 ].Name = "K9.2";
            o.exhaustFan [ 0 ].Name = "S9.1";
        }
        public static void DressingRoomSetName ( DressingRoom dr )
        {
            dr.manualCallPoint [ 0 ].Name = "BTM6.3";
            dr.smokeDetector [ 0 ].Name = "BTHK5.1";
            dr.smokeDetector [ 1 ].Name = "BTHK5.2";
            dr.smokeDetector [ 2 ].Name = "BTH6.2";
            dr.smokeDetector [ 3 ].Name = "BTH6.4";
            dr.alarmOutput [ 0 ].Name = "BIALS6.1";
            dr.freshAirFan [ 0 ].Name = "V5.1";
            dr.exhaustFan [ 0 ].Name = "S5.1";
        }
        public static void OperatorRoomSetName ( OperatorRoom or )
        {
            or.manualCallPoint [ 0 ].Name = "BTM3.3";
            or.smokeDetector [ 0 ].Name = "BTH2.1";
            or.smokeDetector [ 1 ].Name = "BTH2.2";
            or.smokeDetector [ 2 ].Name = "BTH3.1";
            or.smokeDetector [ 3 ].Name = "BTH3.2";
            or.smokeDetector [ 4 ].Name = "BTH4.1";
            or.smokeDetector [ 5 ].Name = "BTH4.2";
            or.exhaustFan [ 0 ].Name = "S2.1";
            or.exhaustFan [ 1 ].Name = "S4.1";
            or.freshAirFan [ 0 ].Name = "K2.1";
            or.freshAirFan [ 1 ].Name = "K4.1";
        }
    }
}

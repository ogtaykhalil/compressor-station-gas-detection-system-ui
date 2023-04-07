using ScadaShablon.tool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaShablon.model
{
    class CompressorStation : ObservableObject
    {
        public ObservableCollection<Compressor> compressor { get; set; } = new ObservableCollection<Compressor>
        {
            new Compressor(),
            new Compressor(),
            new Compressor(),
            new Compressor(),
            new Compressor(),
            new Compressor()
        };
        public ObservableCollection<GasDetector> gasDetector { get; set; } = new ObservableCollection<GasDetector>
        {
            new GasDetector(),
            new GasDetector()
        };
        public ObservableCollection<ManualCallPoint> manualCallPoint { get; set; } = new ObservableCollection<ManualCallPoint>()
        {
            new ManualCallPoint(),
            new ManualCallPoint(),
            new ManualCallPoint(),
            new ManualCallPoint(),
            new ManualCallPoint(),
            new ManualCallPoint()
        };
        public ObservableCollection<FreshAirFan> freshAirFan { get; set; } = new ObservableCollection<FreshAirFan>
        {
            new FreshAirFan(),
            new FreshAirFan(),
            new FreshAirFan(),
            new FreshAirFan()
        };
        public ObservableCollection<AlarmOutput> alarmOutput { get; set; } = new ObservableCollection<AlarmOutput>
        {
            new AlarmOutput(),
            new AlarmOutput(),
            new AlarmOutput(),
            new AlarmOutput(),
            new AlarmOutput(),
            new AlarmOutput(),
            new AlarmOutput(),
            new AlarmOutput(),
            new AlarmOutput(),
            new AlarmOutput()
        };

        public CompressorStation()
        {
            //Methods.SetOpenLoopDetectorName ( gasDetector, "QFE1." );
            //Methods.SetName ( manualCallPoint, "BTM1." );
            //Methods.SetName ( freshAirFan, "QV1." );
            //Methods.SetName ( alarmOutput, "BIAL1.", "Q" );
            //Methods.SetCompressorDeviceName ( compressor, "BTF1.", "QFT1.", "QS1." );
            Compressor.SetName(compressor, "QAZ KOMPRESSORU-");
        }
        public static void TransferEventsToDatebase(CompressorStation compressorStation)
        {
            Compressor.TransferEventsToDatebase(compressorStation.compressor);
            Methods.TransferEventsToDatabase(compressorStation.freshAirFan);
            Methods.TransferEventsToDatabase(compressorStation.manualCallPoint);
            Methods.TransferEventsToDatabase(compressorStation.gasDetector);
        }
        public static void GetDeviceStatus(CompressorStation compressorStation, ObservableCollection<int[]> modbusRegisterAddress, SQLData data)
        {
            Methods.GetCompressorDeviceStatus(compressorStation, modbusRegisterAddress);
            Methods.GetAlarm(compressorStation.manualCallPoint, modbusRegisterAddress[13]);
            Methods.GetFault(compressorStation.manualCallPoint, modbusRegisterAddress[14]);
            Methods.GetFault(compressorStation.freshAirFan, modbusRegisterAddress[15]);
            Methods.IsStarted(compressorStation.freshAirFan, modbusRegisterAddress[16]);
            Methods.IsStopped(compressorStation.freshAirFan, modbusRegisterAddress[17]);
            Methods.Feedback(compressorStation.freshAirFan, modbusRegisterAddress[18]);
            Methods.GetAutoManStatus(compressorStation.freshAirFan, modbusRegisterAddress[19]);
            Methods.GetGMIReleFault(compressorStation.freshAirFan, modbusRegisterAddress[20]);
            Methods.GetAlarm(compressorStation.gasDetector, modbusRegisterAddress[21]);
            Methods.GetFault(compressorStation.gasDetector, modbusRegisterAddress[22]);
            Methods.GetPreAlarm(compressorStation.gasDetector, modbusRegisterAddress[23]);
            Methods.GetOutputStatus(compressorStation.alarmOutput, modbusRegisterAddress[24]);

            /*  Methods.GetAlarm ( compressorStation.flameDetector, modbusRegisterAddress [ 0 ] );
              Methods.GetFault ( compressorStation.flameDetector, modbusRegisterAddress [ 1 ] );
              Methods.GetPreAlarm ( compressorStation.flameDetector, modbusRegisterAddress [ 2 ] );
              Methods.GetAlarm ( compressorStation.smokeDetector, modbusRegisterAddress [ 6 ] );
              Methods.GetFault ( compressorStation.smokeDetector, modbusRegisterAddress [ 7 ] );
              Methods.GetPreAlarm ( compressorStation.smokeDetector, modbusRegisterAddress [ 8 ] );
              Methods.GetFault ( compressorStation.exhaustFan, modbusRegisterAddress [ 11 ] );
              Methods.IsStarted ( compressorStation.exhaustFan, modbusRegisterAddress [ 12 ] );
              Methods.IsStopped ( compressorStation.exhaustFan, modbusRegisterAddress [ 13 ] );
              Methods.Feedback ( compressorStation.exhaustFan, modbusRegisterAddress [ 14 ] );
               */
        }
        public static void SetDeviceCommand(CompressorStation compressorStation, ObservableCollection<int[]> modbusRegisterAddress)
        {
            Methods.SetCompressorDeviceCommand(compressorStation, modbusRegisterAddress);
            modbusRegisterAddress[5] = Methods.SetEnableCommand(compressorStation.gasDetector);
            modbusRegisterAddress[6] = Methods.SetStartCommand(compressorStation.freshAirFan);
            modbusRegisterAddress[7] = Methods.SetStopCommand(compressorStation.freshAirFan);
            modbusRegisterAddress[8] = Methods.SetAutoManCommand(compressorStation.freshAirFan);
            /* modbusRegisterAddress [ 0 ] = Methods.SetEnableCommand ( compressorStation.flameDetector );
             modbusRegisterAddress [ 2 ] = Methods.SetStartCommand ( compressorStation.exhaustFan );
             modbusRegisterAddress [ 3 ] = Methods.SetStopCommand ( compressorStation.exhaustFan );
             modbusRegisterAddress [ 4 ] = Methods.SetAutoManCommand ( compressorStation.exhaustFan );
              */
        }
        public static void GetProssesValue(CompressorStation compressorStation, int[] modbusRegisterAddress)
        {
            Methods.GetGasDetectorProssesValue(compressorStation.compressor, modbusRegisterAddress);
        }
        private bool MCPAlarm()
        {
            bool alarm = false;
            foreach (var item in manualCallPoint)
            {
                if (item.Alarm)
                {
                    alarm = true; break;
                }
            }
            return alarm;
        }
        private bool CompressorAlarm()
        {
            bool alarm = false;
            foreach (var item in compressor)
            {
                if (item.Alarm())
                {
                    alarm = true; break;
                }
            }
            return alarm;
        }
        public bool Alarm()
        {
            return CompressorAlarm() || MCPAlarm() ? true : false;
        }
    }
}

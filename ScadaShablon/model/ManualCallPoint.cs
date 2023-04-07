using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaShablon.model
{
    class ManualCallPoint : Sensor
    {
        SQLData data = new SQLData ( );
        private bool _isPushed;
        protected bool _buttonTemp;
        public bool IsPushed
        {
            get { return _isPushed; }
            set { _isPushed = value; OnPropertyChanged(); }
        }
        public bool ButtonTemp
        {
            get { return _buttonTemp; }
            set { _buttonTemp = value; OnPropertyChanged(); }
        }
        public override void AlarmFault ( )
        {
            data.Alarm ( this, "Yanğın Tüstülənmə həyəcan siqnalı verildi" );
            data.Fault ( this, "Yanğın xəbərvericisində xəta əmələ gəldi" );
            //data.Alarm(Wire_Break, ref _wire_Break_Temp, Name, "Flame Detector Wire Is Break");
            //data.Alarm(Wire_Shortcut, ref _wire_Shortcut_Temp, Name, "Flame Detector Wire Is Shortcut");
        }
    }
}

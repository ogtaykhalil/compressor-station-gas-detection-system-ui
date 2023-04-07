using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaShablon.model
{
    class SmokeDetector : Sensor
    {
        SQLData data = new SQLData ( );
        public override void AlarmFault ( )
        {
            data.Alarm ( this, "Yanğın Tüstülənmə həyəcanı mövcuddur" );
            data.PreAlarm ( this, "Yanğın Tüstülənmə həyəcanı önsiqnallanması mövcuddur" );
            data.Fault ( this, "Yanğın xəbərvericisində xəta əmələ gəldi" );
            //data.Alarm(Wire_Break, ref _wire_Break_Temp, Name, "Flame Detector Wire Is Break");
            //data.Alarm(Wire_Shortcut, ref _wire_Shortcut_Temp, Name, "Flame Detector Wire Is Shortcut");
        }
    }
}

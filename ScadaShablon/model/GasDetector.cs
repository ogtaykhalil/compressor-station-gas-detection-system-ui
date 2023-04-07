using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaShablon.model
{
    class GasDetector : Sensor
    {
        SQLData data = new SQLData ( );
        public override void AlarmFault ( )
        {
            data.Alarm(this, "25%-lik Qaz Sızması Qeydə Alındı");
            data.PreAlarm(this, "5%-lik Qaz Sızması Qeydə Alındı");
            data.Fault(this, "Detektorda Xəta Əmələ Gəldi");
            
           
        }
    }
}

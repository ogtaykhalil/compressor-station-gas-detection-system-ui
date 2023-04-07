using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ScadaShablon.model
{
    interface IStatus
    {
        bool Alarm { get; set; }
        bool PreAlarm { get; set; }
        bool PreAlarmTemp { get; set; }
        bool AlarmTemp { get; set; }
        bool FaultTemp { get; set; }
        bool Fault { get; set; }
        bool Popup { get; set; }
        string Name { get; set; }
        string Status { get; set; }
        bool Enable { get; set; }
        ICommand PopupC { get; set; }
        

        void AlarmFault ( );

    }
}

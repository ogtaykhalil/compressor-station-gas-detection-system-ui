using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaShablon.model
{
    class AlarmOutput: Sensor
    {     
        private bool _outputStatus;
        public bool OutputStatus
        {
            get { return _outputStatus; }
            set { _outputStatus = value; OnPropertyChanged(); }
        }
    }
}

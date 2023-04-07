using ScadaShablon.tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaShablon.model
{
    class CurrentAlarms : ObservableObject
    {
        string _name;
        string _message;
        DateTime _dateTime;
        string _description;
        public CurrentAlarms()
        {
            Name = "oqtay";
            Message = "blah blah blah";
            DateTime = DateTime.Now;
            Description = "same as blah";
        }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public string Message { get => _message; set { _message = value; OnPropertyChanged(); } }
        public DateTime DateTime { get => _dateTime; set { _dateTime = value; OnPropertyChanged(); } }
        public string Description { get => _description; set { _description = value; OnPropertyChanged(); } }
        
    }
}

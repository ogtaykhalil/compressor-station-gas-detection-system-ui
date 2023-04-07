using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using ScadaShablon.view_model;
using System.Windows.Shapes;
using ScadaShablon.model;
using System.Windows.Markup;

namespace ScadaShablon.view
{
    /// <summary>
    /// Interaction logic for CurrentAlarm.xaml
    /// </summary>
    public partial class CurrentAlarm : Window
    {
        SQLData data = new SQLData();
        public CurrentAlarm ( )
        {
            
	        InitializeComponent ( );
            data.GetCurrentAlarmTab = null;

        }

      
    }
}

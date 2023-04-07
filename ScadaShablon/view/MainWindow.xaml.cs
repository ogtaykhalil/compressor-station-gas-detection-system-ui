using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EasyModbus;
using System.Drawing;
using ScadaShablon.view_model;
using SciChart.Charting.Model.DataSeries;
using SciChart.Examples.ExternalDependencies.Common;
using SciChart.Examples.ExternalDependencies.Data;
using ScadaShablon.model;
using System.Threading;
using System.IO;

namespace ScadaShablon.view
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SQLData data = new SQLData ( );
        ViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = new ViewModel();
            this.DataContext = vm;
            
            //Thread th = new Thread ( vm.Run );
            //th.Start ( );
            data.DetTronics.ExecuteCommand ( "truncate table CurrentAlarmTable" );
            Task task = Task.Run(() => { vm.Run(); });
            
            //Task task1 = Task.Run ( ( ) => { vm.Run2 ( ); } );
            
            //Task task2 = Task.Run ( ( ) => { vm.Run3 ( ); } );
            //Thread th = new Thread ( vm.Run3 );
            //th.Start ( );
            Screen scr = Screen.AllScreens [ 0 ];
            System.Drawing.Rectangle r1 = scr.WorkingArea;
            this.Top = r1.Top + 42;
            this.Left = r1.Left + 633;
            
        }
        private void Window_Closing ( object sender, System.ComponentModel.CancelEventArgs e )
        {
            System.Diagnostics.Process.GetCurrentProcess ( ).Kill ( );
        }




        private void Close_Click ( object sender, RoutedEventArgs e )
        {
            System.Diagnostics.Process.GetCurrentProcess ( ).Kill ( );
        }

    }
}

using ScadaShablon.tool;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaShablon.model
{
    class OilTankStation: ObservableObject, IProperty
    {
        private string _name;

        public string Name { get => _name; set { _name = value; OnPropertyChanged ( ); } }
        ObservableCollection<OilTank> oilTank { get; set; } = new ObservableCollection<OilTank>
        {
            new OilTank(),
            new OilTank(),
            new OilTank(),
            new OilTank(),
            new OilTank(),
            new OilTank()
        };

        public static void SetName ( IEnumerable<IProperty> oilTanks, string name )
        {
            int x = 1;
            foreach ( var oilTank in oilTanks )
            {
                oilTank.Name = name + x;
                x++;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaShablon.model
{
    interface IProperty
    {
        string Name { get; set; }
        //void SetName ( IEnumerable<IProperty> item, string name );
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
namespace BionicProject
{
    class Receivers : ObservableCollection<User>
    {
        StoreDB store = new StoreDB();
        public Receivers(string Surname, string Name)
        {
            
            /*foreach (var m in store.PossibleReceivers(Surname, Name))
            { Add(m); }*/
        }
    }
}

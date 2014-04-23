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
            
            List<User> users= store.PossibleReceivers(Surname, Name);
            foreach (var u in users)
                Add(u);
        }
    }
}

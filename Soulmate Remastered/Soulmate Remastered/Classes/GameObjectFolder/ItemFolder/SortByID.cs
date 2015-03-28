using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder
{
    class SortByID : Comparer<Stack<AbstractItem>>
    {
        public override int Compare(Stack<AbstractItem> x, Stack<AbstractItem> y)
        {
            return (int)(x.Peek().ID - y.Peek().ID);
        }
    }
}

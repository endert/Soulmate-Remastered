using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.ItemFolder
{
    class UseEventArgs : EventArgs
    {
        public int Index;

        public UseEventArgs(int i) { Index = i; }
    }
}

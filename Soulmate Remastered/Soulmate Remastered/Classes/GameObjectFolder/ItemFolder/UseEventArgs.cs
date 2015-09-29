using System;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder
{
    class UseEventArgs : EventArgs
    {
        public int Index;

        public UseEventArgs(int i) { Index = i; }
    }
}

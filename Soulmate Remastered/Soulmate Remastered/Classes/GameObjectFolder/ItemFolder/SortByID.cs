using System.Collections.Generic;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder
{
    class SortByID : Comparer<ItemStack>
    {
        public override int Compare(ItemStack x, ItemStack y)
        {
            return (int)x.Peek().ID - (int)y.Peek().ID;
        }
    }
}

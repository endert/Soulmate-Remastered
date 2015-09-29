using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using System;
using Soulmate_Remastered.Core;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.TreasureChestFolder
{
    /// <summary>
    /// base class for all kinds of treasure chests
    /// </summary>
    abstract class AbstractTreasureChest : Entity
    {
        protected bool isOpen = false;
        /// <summary>
        /// for random drop direction
        /// </summary>
        protected Random random = new Random();

        /// <summary>
        /// drops the items
        /// </summary>
        public override void Drop()
        {
            int count = random.Next(100);
            for (int i = 0; i < count; i++)
            {
                int rand = random.Next(Drops.Length);
                if (Drops[rand].DropRate > 100 * random.NextDouble())
                {
                    Drops[rand].CloneAndDrop(new Vector2(Position.X + random.Next(50), Position.Y + random.Next(50)));
                }
            }
        }

        protected override void OnKeyPress(object sender, KeyEventArgs e)
        {
            base.OnKeyPress(sender, e);

            if (e.Key == Controls.Key.Interact)
                if (!isOpen && HitBox.DistanceTo(PlayerHandler.Player.HitBox) <= 50)
                {
                    isOpen = true;
                    Drop();
                }
        }
    }
}

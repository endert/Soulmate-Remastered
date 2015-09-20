using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
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
            for (int i = 0; i < random.Next(100); i++)
            {
                int rand = random.Next(Drops.Length);
                if (Drops[rand].DropRate > 100 * random.NextDouble())
                {
                    Drops[rand].CloneAndDrop(new Vector2(Position.X + random.Next(50), Position.Y + random.Next(50)));
                }
            }
        }

        /// <summary>
        /// interactes with the player
        /// </summary>
        public void Interaction()
        {
            if (HitBox.DistanceTo(PlayerHandler.Player.HitBox) <= 50 && Keyboard.IsKeyPressed((Controls.Interact)) && !isOpen && !Game.IsPressed)
            {
                isOpen = true;
                Game.IsPressed = true;
                Drop();
            }
            
        }
    }
}

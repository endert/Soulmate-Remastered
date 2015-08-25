using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.TreasureChestFolder
{
    abstract class AbstractTreasureChest : Entity
    {
        public override string Type
        {
            get
            {
                return base.Type + ".TreasureChest";
            }
        }

        protected bool isOpen = false;
        protected Random random = new Random(); //random movement

        public override void Drop()
        {
            for (int i = 0; i < random.Next(100); i++)
            {
                int rand = random.Next(drops.Length);
                if (drops[rand].DROPRATE > 100 * random.NextDouble())
                {
                    drops[rand].cloneAndDrop(new Vector2f(Position.X + random.Next(50), Position.Y + random.Next(50)));
                }
            }
        }

        public void interaction()
        {
            if (HitBox.DistanceTo(PlayerHandler.player.HitBox) <= 50 && Keyboard.IsKeyPressed((Controls.Interact)) && !isOpen && !Game.isPressed)
            {
                isOpen = true;
                Game.isPressed = true;
                Drop();
            }
            
        }
    }
}

using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder.ShopFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder
{
    class NPCHandler
    {
        private static List<AbstractNPC> NPCList;
        public static List<AbstractNPC> NPCs { get { return NPCList; } }
        public static Shop shop { get; set; }

        public NPCHandler()
        {
            NPCList = new List<AbstractNPC>();
            if (GameObjectHandler.lvl == 0)
            {
                new PetStorageGuy(new Vector2f(300, 400));
                new Shopkeeper(new Vector2f(600, 600));
            }
        }

        public static void add(AbstractNPC npc)
        {
            NPCList.Add(npc);
            EntityHandler.add(npc);
        }

        public static void deleate()
        {
            foreach (AbstractNPC npc in NPCList)
            {
                npc.kill();
            }
        }

        public static void deleateType(String _type)
        {
            bool foundEntry = false;
            for (int i = 0; i < NPCList.Count; i++)
            {
                if (NPCList[i].type.Equals(_type))
                {
                    NPCList.RemoveAt(i);
                    foundEntry = true;
                    i--;
                }
            }

            if (foundEntry)
            {
                EntityHandler.deleateType(_type);
            }
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < NPCList.Count; i++)
            {
                if (!NPCList[i].isAlive)
                {
                    NPCList.RemoveAt(i);
                    i--;
                }
                if (NPCList[i].InIteractionRange && !Game.isPressed && Keyboard.IsKeyPressed(Controls.Interact) && !NPCList[i].isInteracting)
                {
                    Game.isPressed = true;
                    NPCList[i].interact();
                }
                if (NPCList[i].isInteracting && (!NPCList[i].InIteractionRange || (Keyboard.IsKeyPressed(Controls.Escape) && !Game.isPressed)))
                {
                    Game.isPressed = true;
                    NPCList[i].stopIteraction();
                }
            }
        }

        public static void updateShop(GameTime gameTime)
        {
            if (Keyboard.IsKeyPressed(Controls.Escape) && !Game.isPressed)
            {
                Game.isPressed = true;

                foreach (AbstractNPC npc in NPCs)
                {
                    npc.stopIteraction();
                }
                shop = null;
            }
            else
            {
                if (shop != null)
                    shop.Shopmanagement();
                PlayerHandler.player.update(gameTime);
            }
        }
    }
}

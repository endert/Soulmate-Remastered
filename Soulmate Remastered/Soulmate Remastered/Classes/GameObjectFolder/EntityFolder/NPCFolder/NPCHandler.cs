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
    /// <summary>
    /// Handles all Npc's
    /// </summary>
    class NPCHandler
    {
        /// <summary>
        /// List with all npc's
        /// </summary>
        public static List<AbstractNPC> NPCs { get; protected set; }

        /// <summary>
        /// the open Shop
        /// </summary>
        public static Shop shop { get; set; }

        /// <summary>
        /// initialize Npc Handler and Npc's for the current map
        /// </summary>
        public NPCHandler()
        {
            NPCs = new List<AbstractNPC>();
            if (GameObjectHandler.lvl == 0)
            {
                new PetStorageGuy(new Vector2f(300, 400));
                new Shopkeeper(new Vector2f(600, 600));
            }
        }

        /// <summary>
        /// add a npc to the npc list
        /// </summary>
        /// <param name="npc"></param>
        public static void add(AbstractNPC npc)
        {
            NPCs.Add(npc);
            EntityHandler.add(npc);
        }

        /// <summary>
        /// kills all npc's
        /// </summary>
        public static void deleate()
        {
            foreach (AbstractNPC npc in NPCs)
            {
                npc.kill();
            }
        }

        /// <summary>
        /// deletes all Npc with the given type
        /// </summary>
        /// <param name="_type"></param>
        public static void deleateType(String _type)
        {
            bool foundEntry = false;
            for (int i = 0; i < NPCs.Count; i++)
            {
                if (NPCs[i].type.Equals(_type))
                {
                    NPCs.RemoveAt(i);
                    foundEntry = true;
                    i--;
                }
            }

            if (foundEntry)
            {
                EntityHandler.deleateType(_type);
            }
        }

        /// <summary>
        /// updates all npc's should only be called once
        /// </summary>
        /// <param name="gameTime"></param>
        public void update(GameTime gameTime)
        {
            for (int i = 0; i < NPCs.Count; i++)
            {
                //if the npc is not alive
                if (!NPCs[i].isAlive)
                {
                    //it should be removed and then continue the loop
                    NPCs.RemoveAt(i);
                    i--;
                    continue;
                }

                //interaction update
                if (NPCs[i].InIteractionRange && !Game.isPressed && Keyboard.IsKeyPressed(Controls.Interact) && !NPCs[i].Interacting)
                {
                    Game.isPressed = true;
                    NPCs[i].interact();
                }
                if (NPCs[i].Interacting && (!NPCs[i].InIteractionRange || (Keyboard.IsKeyPressed(Controls.Escape) && !Game.isPressed)))
                {
                    Game.isPressed = true;
                    NPCs[i].stopIteraction();
                }
            }
        }

        /// <summary>
        /// updates the shop
        /// </summary>
        /// <param name="gameTime"></param>
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

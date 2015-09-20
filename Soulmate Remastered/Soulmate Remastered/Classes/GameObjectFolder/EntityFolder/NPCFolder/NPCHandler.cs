using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder.ShopFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Core;
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
        public static Shop Shop_ { get; set; }

        /// <summary>
        /// initialize Npc Handler and Npc's for the current map
        /// </summary>
        public NPCHandler()
        {
            NPCs = new List<AbstractNPC>();
            if (GameObjectHandler.Lvl == 0)
            {
                new PetStorageGuy(new Vector2(300, 400));
                new Shopkeeper(new Vector2(600, 600));
            }
        }

        /// <summary>
        /// add a npc to the npc list
        /// </summary>
        /// <param name="npc"></param>
        public static void Add_(AbstractNPC npc)
        {
            NPCs.Add(npc);
            EntityHandler.Add(npc);
        }

        /// <summary>
        /// kills all npc's
        /// </summary>
        public static void Deleate()
        {
            foreach (AbstractNPC npc in NPCs)
            {
                npc.Kill();
            }
        }

        /// <summary>
        /// deletes all Npc with the given type
        /// </summary>
        /// <param name="_type"></param>
        public static void DeleateType(Type _type)
        {
            bool foundEntry = false;
            for (int i = 0; i < NPCs.Count; i++)
            {
                if (NPCs[i].GetType().Equals(_type))
                {
                    NPCs.RemoveAt(i);
                    foundEntry = true;
                    i--;
                }
            }

            if (foundEntry)
            {
                EntityHandler.DeleateType(_type);
            }
        }

        /// <summary>
        /// updates all npc's should only be called once
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < NPCs.Count; i++)
            {
                //if the npc is not alive
                if (!NPCs[i].IsAlive)
                {
                    //it should be removed and then continue the loop
                    NPCs.RemoveAt(i);
                    i--;
                    continue;
                }

                //interaction update
                if (NPCs[i].InIteractionRange && !Game.IsPressed && Keyboard.IsKeyPressed(Controls.Interact) && !NPCs[i].Interacting)
                {
                    Game.IsPressed = true;
                    NPCs[i].Interact();
                }
                if (NPCs[i].Interacting && (!NPCs[i].InIteractionRange || (Keyboard.IsKeyPressed(Controls.Escape) && !Game.IsPressed)))
                {
                    Game.IsPressed = true;
                    NPCs[i].StopIteraction();
                }
            }
        }

        /// <summary>
        /// updates the shop
        /// </summary>
        /// <param name="gameTime"></param>
        public static void UpdateShop(GameTime gameTime)
        {
            if (Keyboard.IsKeyPressed(Controls.Escape) && !Game.IsPressed)
            {
                Game.IsPressed = true;

                foreach (AbstractNPC npc in NPCs)
                {
                    npc.StopIteraction();
                }
                Shop_ = null;
            }
            else
            {
                if (Shop_ != null)
                    Shop_.Shopmanagement();
                PlayerHandler.Player.Update(gameTime);
            }
        }
    }
}

﻿using SFML.Window;
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

        public NPCHandler()
        {
            NPCList = new List<AbstractNPC>();
            if (GameObjectHandler.lvl == 0)
            {
                new PetStorageGuy(new Vector2f(300, 400));
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
                if (NPCList[i].InIteractionRange && Keyboard.IsKeyPressed(Controls.Interact) && !NPCList[i].isInteracting)
                {
                    NPCList[i].interact();
                }
                if (!NPCList[i].InIteractionRange)
                {
                    NPCList[i].stopIteraction();
                }
            }
        }

    }
}
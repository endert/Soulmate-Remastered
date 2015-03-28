using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.EnemyFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PetFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.ProjectileFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.TreasureChestFolder;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder
{
    class EntityHandler
    {
        public static List<Entity> entityList { get; set; }
        public static PlayerHandler playerHandler { get; set; }
        public static EnemyHandler enemyHandler { get; set; }
        public static PetHandler petHandler { get; set; }
        public static ProjectileHandler projectileHandler { get; set; }
        public static NPCHandler npcHandler { get; set; }
        public static TreasureChestHandler treasureChestHandler { get; set; }

        public EntityHandler()
        {
            entityList = new List<Entity>();

            playerHandler = new PlayerHandler();
            enemyHandler = new EnemyHandler();
            petHandler = new PetHandler();
            projectileHandler = new ProjectileHandler();
            npcHandler = new NPCHandler();
            treasureChestHandler = new TreasureChestHandler();
        }

        public static void add(Entity entity)
        {
            entityList.Add(entity);
            GameObjectHandler.add(entity);
        }

        public static void add(List<Entity> entities)
        {
            foreach (Entity entity in entities)
            {
                entityList.Add(entity);
                GameObjectHandler.add(entity);
            }
        }

        public static void deleate()
        {
            foreach (Entity entity in entityList)
            {
                entity.kill();
            }
            PlayerHandler.deleate();
            EnemyHandler.deleate();
            PetHandler.deleate();
            ProjectileHandler.deleate();
            NPCHandler.deleate();
            TreasureChestHandler.deleate();
        }

        public static void deleateType(String _type)
        {
            bool foundEntry = false;
            for (int i = 0; i < entityList.Count; i++)
            {
                if (entityList[i].type.Equals(_type))
                {
                    entityList.RemoveAt(i);
                    foundEntry = true;
                    i--;
                }
            }

            if (foundEntry)
            {
                GameObjectHandler.deleateType(_type);
            }
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < entityList.Count; i++)
            {
                if (!entityList[i].isAlive)
                {
                    entityList[i].drop();
                    entityList.RemoveAt(i);
                    i--;
                }
            }
            enemyHandler.update(gameTime);
            petHandler.update();
            projectileHandler.update(gameTime);
            npcHandler.update(gameTime);
            treasureChestHandler.update(gameTime);
        }
    }
}

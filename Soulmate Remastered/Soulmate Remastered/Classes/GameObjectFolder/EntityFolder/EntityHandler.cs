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
    /// <summary>
    /// handles the other handler plus the entitylist
    /// </summary>
    class EntityHandler
    {
        /// <summary>
        /// all entities
        /// </summary>
        public static List<Entity> EntityList { get; set; }
        /// <summary>
        /// the playerhandler
        /// </summary>
        public static PlayerHandler PlayerHandler { get; set; }
        /// <summary>
        /// the enemyhandler
        /// </summary>
        public static EnemyHandler EnemyHandler { get; set; }
        /// <summary>
        /// the pethandler
        /// </summary>
        public static PetHandler PetHandler { get; set; }
        /// <summary>
        /// the projectilehandler 
        /// </summary>
        public static ProjectileHandler ProjectileHandler { get; set; }
        /// <summary>
        /// the npchandler 
        /// </summary>
        public static NPCHandler NpcHandler { get; set; }
        /// <summary>
        /// the treasurechesthandler 
        /// </summary>
        public static TreasureChestHandler TreasureChestHandler { get; set; }

        /// <summary>
        /// initialize this
        /// </summary>
        public static void Initialize()
        {
            EntityList = new List<Entity>();

            PlayerHandler = new PlayerHandler();
            EnemyHandler = new EnemyHandler();
            PetHandler = new PetHandler();
            ProjectileHandler = new ProjectileHandler();
            NpcHandler = new NPCHandler();
            TreasureChestHandler = new TreasureChestHandler();
        }

        /// <summary>
        /// adds the entity
        /// </summary>
        /// <param name="entity"></param>
        public static void Add(Entity entity)
        {
            EntityList.Add(entity);
            GameObjectHandler.Add(entity);
        }

        /// <summary>
        /// clears the list => deleates all enities
        /// </summary>
        public static void Deleate()
        {
            EntityList.Clear();

            PlayerHandler.Deleate();
            EnemyHandler.Deleate();
            PetHandler.Deleate();
            ProjectileHandler.Deleate();
            NPCHandler.Deleate();
            TreasureChestHandler.Deleate();
        }

        /// <summary>
        /// deleates all entities that matches this type
        /// </summary>
        /// <param name="_type"></param>
        public static void DeleateType(Type _type)
        {
            bool foundEntry = false;
            for (int i = 0; i < EntityList.Count; i++)
                if (EntityList[i].GetType().Equals(_type))
                {
                    EntityList.RemoveAt(i);
                    foundEntry = true;
                    i--;
                }

            if (foundEntry)
                GameObjectHandler.DeleateType(_type);
        }

        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < EntityList.Count; i++)
            {
                if (!EntityList[i].IsAlive)
                {
                    EntityList[i].Drop();
                    EntityList.RemoveAt(i);
                    i--;
                }
            }
            EnemyHandler.Update(gameTime);
            PetHandler.Update();
            ProjectileHandler.Update(gameTime);
            NpcHandler.Update(gameTime);
            TreasureChestHandler.Update(gameTime);
        }
    }
}

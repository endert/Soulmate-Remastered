using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PetFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.MapFolder;
using Soulmate_Remastered.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.EnemyFolder
{
    /// <summary>
    /// handles Enemies
    /// </summary>
    class EnemyHandler
    {
        /// <summary>
        /// List wich contains all enemies
        /// </summary>
        public static List<AbstractEnemy> EnemyList { get; private set; }
        /// <summary>
        /// random generator for spawn positions
        /// </summary>
        static Random Random { get; set; }

        /// <summary>
        /// initialize the enemy List
        /// </summary>
        public EnemyHandler()
        {
            EnemyList = new List<AbstractEnemy>();
            Random = new Random();
        }

        /// <summary>
        /// initialize the enemies on each lvl
        /// </summary>
        public static void EnemyInitialize()
        {
            switch (GameObjectHandler.lvl)
            {
                case 0:
                    break;

                case 1:
                    for (int i = 0; i < 10; i++)
                    {
                        float rX = 1000 + Random.Next(1000);
                        float rY = 800 + Random.Next(400);
                        Vector2 spawnPos = new Vector2(rX, rY);

                        if (spawnPos.Distance(PlayerHandler.player.Position) > 200)
                        {
                            EnemyBlott blott = new EnemyBlott(spawnPos);
                            
                                Add_(blott);
                            
                        }
                            else
                            i--;
                    }
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// adds an enemie to the enemieList and calls add methode of entity Handler
        /// </summary>
        /// <param name="enemy"></param>
        public static void Add_(AbstractEnemy enemy)
        {
            EnemyList.Add(enemy);
            EntityHandler.add(enemy);
        }

        /// <summary>
        /// trys to delete all enemies from Entity Handler
        /// </summary>
        public static void Deleate()
        {
            EntityHandler.deleateType("Enemy");
        }

        /// <summary>
        /// updates all enemies should only be called once
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < EnemyList.Count; i++)
            {
                /* 
                 * if the enemy took a leatal blow the player is revarded with exp and the fusion value is increased
                 * also the enemie must be removed from the List
                 */
                if (!EnemyList[i].IsAlive)
                {
                    PlayerHandler.player.RiseCurrentFusionValue();
                    PlayerHandler.player.RiseCurrentEXP();
                    
                    EnemyList.RemoveAt(i);
                    i--;
                }
                else
                {
                    //if enemy is hit by the players attack hit box, it suffers damage
                    if (EnemyList[i].HitBox.Hit(PlayerHandler.player.AttackHitBox) && PlayerHandler.player.Attacking)
                        EnemyList[i].takeDmg(PlayerHandler.player.Att);

                    //if the player is hit by the enemy the player suffers damage
                    if (EnemyList[i].touchedPlayer())
                        PlayerHandler.player.takeDmg(EnemyList[i].Att);

                    //if the pet is hit by the enemy the pet suffers damage
                    if (PetHandler.Pet != null && EnemyList[i].HitBox.Hit(PetHandler.Pet.HitBox))
                        PetHandler.Pet.takeDmg(EnemyList[i].Att);
                }
            }
        }
    }
}

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
        public static List<AbstractEnemy> enemyList { get; private set; }
        /// <summary>
        /// random generator for spawn positions
        /// </summary>
        static Random random { get { return new Random(); } }

        /// <summary>
        /// initialize the enemy List
        /// </summary>
        public EnemyHandler()
        {
            enemyList = new List<AbstractEnemy>();
        }

        /// <summary>
        /// initialize the enemies on each lvl
        /// </summary>
        public static void enemyInitialize()
        {
            switch (GameObjectHandler.lvl)
            {
                case 0:
                    break;

                case 1:
                    for (int i = 0; i < 10; i++)
                    {
                        float rX = 1000 + random.Next(1000);
                        float rY = 800 + random.Next(400);
                        Vector2 spawnPos = new Vector2(rX, rY);

                        if (spawnPos.distance(PlayerHandler.player.position) > 200)
                        {
                            EnemyBlott blott = new EnemyBlott(spawnPos);
                            
                                add(blott);
                            
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
        public static void add(AbstractEnemy enemy)
        {
            enemyList.Add(enemy);
            EntityHandler.add(enemy);
        }

        /// <summary>
        /// trys to delete all enemies from Entity Handler
        /// </summary>
        public static void deleate()
        {
            EntityHandler.deleateType("Enemy");
        }

        /// <summary>
        /// updates all enemies should only be called once
        /// </summary>
        /// <param name="gameTime"></param>
        public void update(GameTime gameTime)
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                /* 
                 * if the enemy took a leatal blow the player is revarded with exp and the fusion value is increased
                 * also the enemie must be removed from the List
                 */
                if (!enemyList[i].isAlive)
                {
                    PlayerHandler.player.setCurrentFusionValue();
                    PlayerHandler.player.setCurrentEXP();
                    
                    enemyList.RemoveAt(i);
                    i--;
                }
                else
                {
                    //if enemy is hit by the players attack hit box, it suffers damage
                    if (enemyList[i].hitBox.Hit(PlayerHandler.player.attackHitBox) && PlayerHandler.player.isAttacking)
                        enemyList[i].takeDmg(PlayerHandler.player.Att);

                    //if the player is hit by the enemy the player suffers damage
                    if (enemyList[i].touchedPlayer())
                        PlayerHandler.player.takeDmg(enemyList[i].Att);

                    //if the pet is hit by the enemy the pet suffers damage
                    if (PetHandler.pet != null && enemyList[i].hitBox.Hit(PetHandler.pet.hitBox))
                        PetHandler.pet.takeDmg(enemyList[i].Att);
                }
            }
        }
    }
}

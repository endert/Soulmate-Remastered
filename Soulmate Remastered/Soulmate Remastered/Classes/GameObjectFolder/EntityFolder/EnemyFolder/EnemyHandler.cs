﻿using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PetFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.MapFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.EnemyFolder
{
    class EnemyHandler
    {
        public static AbstractEnemy enemy { get; set; }
        public static List<AbstractEnemy> enemyList { get; set; }

        static Random random = new Random();

        public EnemyHandler()
        {
            enemyList = new List<AbstractEnemy>();
        }

        public static void enemyInitialize()
        {
            switch (GameObjectHandler.lvl)
            {
                case 0:
                    break;

                case 1:
                    for (int i = 0; i < 10; i++)
                    {
                        float rX = 600 + random.Next(1000);
                        float rY = 400 + random.Next(400);
                        Vector2f spawnPos = new Vector2f(rX, rY);

                        EnemyBlott blott = new EnemyBlott(spawnPos);
                        if (blott.hitBox.distanceTo(PlayerHandler.player.hitBox) > 200 && GameObjectHandler.lvlMap.getWalkable(blott.hitBox, spawnPos))
                        {
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

        public static void add(AbstractEnemy enemy)
        {
            enemyList.Add(enemy);
            EntityHandler.add(enemy);
        }

        public static void deleate()
        {
            enemy = null;

            EntityHandler.deleateType("Enemy");
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (!enemyList[i].isAlive)
                {
                    PlayerHandler.player.setCurrentFusionValue();
                    PlayerHandler.player.setCurrentEXP();
                    
                    enemyList.RemoveAt(i);
                    i--;
                }
                else
                {
                    if (enemyList[i].hitBox.hit(PlayerHandler.player.getAttackHitBox) && PlayerHandler.player.isAttacking)
                    {
                        enemyList[i].takeDmg(PlayerHandler.player.getAtt);
                    }

                    if (enemyList[i].touchedPlayer())
                    {
                        PlayerHandler.player.takeDmg(enemyList[i].getAtt);
                    }

                    if (PetHandler.pet != null && enemyList[i].hitBox.hit(PetHandler.pet.hitBox))  //if touched object equals pet
                    {
                        PetHandler.pet.takeDmg(enemyList[i].getAtt);
                    }
                }
            }
        }
    }
}

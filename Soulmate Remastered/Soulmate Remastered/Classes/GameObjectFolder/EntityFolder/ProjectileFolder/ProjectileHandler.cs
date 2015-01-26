using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.ProjectileFolder
{
    class ProjectileHandler
    {
        public static List<AbstractProjectile> projectileList { get; set; }

        public ProjectileHandler()
        {
            projectileList = new List<AbstractProjectile>();
        }

        public static void add(AbstractProjectile projectile)
        {
            projectileList.Add(projectile);
            EntityHandler.add(projectile);
        }

        static public void deleate()
        {
            projectileList.Clear();
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < projectileList.Count; i++)
            {
                if (!projectileList[i].isAlive)
                {
                    projectileList.RemoveAt(i);
                    i--;
                }
                else
                {
                    if (projectileList[i].touchedPlayer())
                    {
                        PlayerHandler.player.takeDmg(projectileList[i].getAtt);
                    }
                }
            }
        }
    }
}

using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.ProjectileFolder
{
    /// <summary>
    /// handles the projectiles
    /// </summary>
    class ProjectileHandler
    {
        /// <summary>
        /// all projectiles
        /// </summary>
        public static List<AbstractProjectile> ProjectileList { get; set; }

        /// <summary>
        /// initialize the projectile list
        /// </summary>
        public ProjectileHandler()
        {
            ProjectileList = new List<AbstractProjectile>();
        }

        /// <summary>
        /// add the given projectile to the list
        /// </summary>
        /// <param name="projectile"></param>
        public static void Add(AbstractProjectile projectile)
        {
            ProjectileList.Add(projectile);
            EntityHandler.add(projectile);
        }

        /// <summary>
        /// clears the list -> deleates all projectiles
        /// </summary>
        static public void Deleate()
        {
            ProjectileList.Clear();
        }

        /// <summary>
        /// updates all projectiles
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < ProjectileList.Count; i++)
            {
                if (!ProjectileList[i].IsAlive)
                {
                    ProjectileList.RemoveAt(i);
                    i--;
                }
                else
                {
                    if (ProjectileList[i].touchedPlayer())
                    {
                        PlayerHandler.Player.takeDmg(ProjectileList[i].Att);
                    }
                }
            }
        }
    }
}

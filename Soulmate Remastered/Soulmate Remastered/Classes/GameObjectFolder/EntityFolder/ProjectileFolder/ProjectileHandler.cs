using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using System.Collections.Generic;

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
            EntityHandler.Add(projectile);
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
                    if (ProjectileList[i].TouchedPlayer())
                    {
                        PlayerHandler.Player.TakeDmg(ProjectileList[i].Att);
                    }
                }
            }
        }
    }
}

using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.HUDFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.MoneyFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.NormalItemFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.PotionFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.EquipmentFolder;
using Soulmate_Remastered.Core;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.EnemyFolder
{
    /// <summary>
    /// a nearly neutral Slime that does not that much
    /// </summary>
    class EnemyBlott : AbstractEnemy
    {
        /// <summary>
        /// The type of this Instance
        /// </summary>
        public override String type { get { return base.type + ".EnemyBlott"; } }
        
        /// <summary>
        /// creates a Blott at the given Position
        /// </summary>
        /// <param name="spawnPos">Vector2 spawn position</param>
        public EnemyBlott(Vector2 spawnPos)
        {
            //initialize the gameObject attributes******************************************************************

            //initialize the TextureList
            textureList.Add(new Texture("Pictures/Entities/Enemy/Blott/Vulnerable/BlottFront.png"));
            textureList.Add(new Texture("Pictures/Entities/Enemy/Blott/Vulnerable/BlottBack.png"));
            textureList.Add(new Texture("Pictures/Entities/Enemy/Blott/Vulnerable/BlottRight.png"));
            textureList.Add(new Texture("Pictures/Entities/Enemy/Blott/Vulnerable/BlottLeft.png"));
            textureList.Add(new Texture("Pictures/Entities/Enemy/Blott/Invulnerable/BlottFrontInvulnerable.png"));

            sprite = new Sprite(textureList[0]);
            position = spawnPos;
            sprite.Position = position;
            isAlive = true;
            hitBox = new HitBox(sprite.Position, sprite.Texture.Size.X, sprite.Texture.Size.Y);

            //******************************************************************************************************
            //initialize Entity Attributes**************************************************************************

            MaxHP = 100;
            CurrentHP = MaxHP;
            Att = 6;
            Def = 5;
            aggroRange = 150f;
            knockBack = 50f;
            BaseMovementSpeed = 0.2f;
            lifeBar = new LifeBarForOthers();

            //initialize the AbstractItem array wich contains the drops
            drops = new AbstractItem[] 
            { 
                new Sword(), //broken Sword ;)
                new TestItem(), 
                new FusionPotion (FusionPotion.fusionPotionSize.small), 
                new HealPotion(HealPotion.healPotionSize.small), 
                new Sword((float)(1+50 * random.NextDouble()), (float)(1+50 * random.NextDouble()), (float)(1+50 * random.NextDouble()))
            };

            //******************************************************************************************************
        }

        /// <summary>
        /// move in Player direction
        /// </summary>
        protected override void react()
        {
            if (!touchedPlayer())
                move(getPlayerDirection());
            else
            {
                move(new Vector2f(-getPlayerDirection().X, -getPlayerDirection().Y));
                FacingDirection = getPlayerDirection();
            }
        }

        /// <summary>
        /// move random
        /// </summary>
        public override void notReact()
        {
            moveRandom();
        }
    }
}

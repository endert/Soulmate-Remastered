using SFML.Graphics;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
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
        protected override void LoadTextures()
        {
            TextureList.Add(new Texture("Pictures/Entities/Enemy/Blott/Vulnerable/BlottFront.png"));
            TextureList.Add(new Texture("Pictures/Entities/Enemy/Blott/Vulnerable/BlottBack.png"));
            TextureList.Add(new Texture("Pictures/Entities/Enemy/Blott/Vulnerable/BlottRight.png"));
            TextureList.Add(new Texture("Pictures/Entities/Enemy/Blott/Vulnerable/BlottLeft.png"));

            TextureList.Add(new Texture("Pictures/Entities/Enemy/Blott/Invulnerable/BlottFrontInvulnerable.png"));
            TextureList.Add(new Texture("Pictures/Entities/Enemy/Blott/Invulnerable/BlottBackInvulnerable.png"));
            TextureList.Add(new Texture("Pictures/Entities/Enemy/Blott/Invulnerable/BlottRightInvulnerable.png"));
            TextureList.Add(new Texture("Pictures/Entities/Enemy/Blott/Invulnerable/BlottLeftInvulnerable.png"));
        }

        /// <summary>
        /// creates a Blott at the given Position
        /// </summary>
        /// <param name="spawnPos">Vector2 spawn position</param>
        public EnemyBlott(Vector2 spawnPos)
        {
            Position = spawnPos;
            Sprite.Position = Position;

            MaxHP = 100;
            CurrentHP = MaxHP;
            Att = 6;
            Def = 5;
            aggroRange = 150f;
            KnockBack = 50f;
            BaseMovementSpeed = 0.2f;

            //initialize the AbstractItem array wich contains the drops
            Drops = new AbstractItem[] 
            { 
                new Sword(), //broken Sword ;)
                new TestItem(), 
                new FusionPotion (FusionPotion.PotionSize.small), 
                new HealPotion(HealPotion.PotionSize.small), 
                new Sword((float)(1+50 * random.NextDouble()), (float)(1+50 * random.NextDouble()), (float)(1+50 * random.NextDouble()))
            };

            //******************************************************************************************************
        }

        /// <summary>
        /// move in Player direction
        /// </summary>
        protected override void React()
        {
            if (!TouchedPlayer())
                Move(GetPlayerDirection());
            else
            {
                Move(-GetPlayerDirection());
                FacingDirection = GetPlayerDirection();
            }
        }

        /// <summary>
        /// move random
        /// </summary>
        public override void NotReact()
        {
            MoveRandom();
        }
    }
}

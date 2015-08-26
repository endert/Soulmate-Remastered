using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PetFolder;
using Soulmate_Remastered.Core;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder
{
    /// <summary>
    /// player in human form
    /// </summary>
    class HumanPlayer : AbstractPlayer
    {
        /// <summary>
        /// the type of this instance
        /// </summary>
        public override String Type { get { return base.Type + ".HumanPlayer"; } }

        /// <summary>
        /// creates new human player at the given spawn position
        /// </summary>
        /// <param name="spawnPosition"></param>
        /// <param name="spawnFacingDirection"></param>
        public HumanPlayer(Vector2 spawnPosition, Vector2 spawnFacingDirection)
        {
            //initialize game object data***************************************************************************
            
            TextureList.Add(new Texture("Pictures/Entities/Player/PlayerFront.png"));
            TextureList.Add(new Texture("Pictures/Entities/Player/PlayerBack.png"));
            TextureList.Add(new Texture("Pictures/Entities/Player/PlayerRightSword.png"));
            TextureList.Add(new Texture("Pictures/Entities/Player/PlayerLeftSword.png"));
            
            Sprite = new Sprite(TextureList[0]);
            Sprite.Position = spawnPosition;
            Position = spawnPosition;
            HitBox = new HitBox(Sprite.Position, TextureList[0].Size.X, TextureList[0].Size.Y);
            IsAlive = true;
            IsVisible = true;

            //******************************************************************************************************
            
            //initialize entity data********************************************************************************
            
            FacingDirection = spawnFacingDirection;
            BaseMovementSpeed = 0.4f;
            BaseHp = 100f;
            BaseAtt = 10;
            BaseDef = 5;
            
            //******************************************************************************************************
            
            //initialize abstract player data***********************************************************************

            AttackHitBox = new HitBox(GetHitBoxPosition(), TextureList[2].Size.X - TextureList[0].Size.X, 85);
            Lvl = 1;
            FusionDuration = 50000;
            MaxFusionValue = 500f;
            CurrentFusionValue = 0f;
            StatsUpdate();

            //******************************************************************************************************
            //erst nach statsupdate möglich
            CurrentHP = MaxHP;
        }

        /// <summary>
        /// fuse with the pet
        /// </summary>
        public void Fusion()
        {
            if (CurrentFusionValue >= MaxFusionValue)
            {
                new PlayerPetFusion(this, PetHandler.Pet);
            }
        }

        /// <summary>
        /// returns the position for the attack hitbox
        /// </summary>
        /// <returns></returns>
        public override Vector2 GetHitBoxPosition()
        {
            if (Direction == EDirection.Right)
                return new Vector2(Sprite.Position.X + 70, Sprite.Position.Y + 94);

            else if (Direction == EDirection.Left)
                return new Vector2(Sprite.Position.X, Sprite.Position.Y + 94);

            else
                return Vector2.ZERO;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                Fusion();
            }
        }
    }
}

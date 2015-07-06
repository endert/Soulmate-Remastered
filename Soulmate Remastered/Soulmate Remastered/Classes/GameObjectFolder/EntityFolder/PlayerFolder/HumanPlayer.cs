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
    class HumanPlayer : AbstractPlayer
    {
        public override String Type { get { return base.Type + ".HumanPlayer"; } }

        public HumanPlayer(Vector2 spawnPosition, Vector2 spawnFacingDirection)
        {
            TextureList.Add(new Texture("Pictures/Entities/Player/PlayerFront.png")); 
            TextureList.Add(new Texture("Pictures/Entities/Player/PlayerBack.png"));
            TextureList.Add(new Texture("Pictures/Entities/Player/PlayerRightSword.png"));
            TextureList.Add(new Texture("Pictures/Entities/Player/PlayerLeftSword.png"));
            
            FacingDirection = spawnFacingDirection;
            Sprite = new Sprite(TextureList[0]);
            Sprite.Position = spawnPosition;
            Position = spawnPosition;
            HitBox = new HitBox(Sprite.Position, TextureList[0].Size.X, TextureList[0].Size.Y);
            AttackHitBox = new HitBox(GetHitBoxPosition(), TextureList[2].Size.X - TextureList[0].Size.X, 85);
            BaseMovementSpeed = 0.4f;
            IsAlive = true;
            IsVisible = true;

            Lvl = 1;
            BaseHp = 100f;
            BaseAtt = 10;
            BaseDef = 5;

            FusionDuration = 50000;
            MaxFusionValue = 500f;
            CurrentFusionValue = 0f;
            StatsUpdate();
            CurrentHP = MaxHP;
        }

        public void fusion()
        {
            if (CurrentFusionValue >= MaxFusionValue)
            {
                new PlayerPetFusion(this, PetHandler.Pet);
            }
        }

        public override Vector2f GetHitBoxPosition()
        {
            if (Direction == EDirection.Right)
            {
                return new Vector2f(Sprite.Position.X + 70, Sprite.Position.Y + 94);
            }

            else if (Direction == EDirection.Left)
            {
                return new Vector2f(Sprite.Position.X, Sprite.Position.Y + 94);
            }

            else
                return new Vector2f(0, 0);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                fusion();
            }
        }
    }
}

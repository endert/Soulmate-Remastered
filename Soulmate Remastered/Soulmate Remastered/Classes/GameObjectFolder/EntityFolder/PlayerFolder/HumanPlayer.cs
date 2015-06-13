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
        public override String type { get { return base.type + ".HumanPlayer"; } }

        public HumanPlayer(Vector2 spawnPosition, Vector2 spawnFacingDirection)
        {
            textureList.Add(new Texture("Pictures/Entities/Player/PlayerFront.png")); 
            textureList.Add(new Texture("Pictures/Entities/Player/PlayerBack.png"));
            textureList.Add(new Texture("Pictures/Entities/Player/PlayerRightSword.png"));
            textureList.Add(new Texture("Pictures/Entities/Player/PlayerLeftSword.png"));
            
            FacingDirection = spawnFacingDirection;
            sprite = new Sprite(textureList[0]);
            sprite.Position = spawnPosition;
            position = spawnPosition;
            hitBox = new HitBox(sprite.Position, textureList[0].Size.X, textureList[0].Size.Y);
            attackHitBox = new HitBox(attackHitBoxPositionUpdate(), textureList[2].Size.X - textureList[0].Size.X, 85);
            BaseMovementSpeed = 0.4f;

            lvl = 1;
            BaseHp = 100f;
            BaseAtt = 10;
            BaseDef = 5;

            fusionDuration = 50;
            maxFusionValue = 500f;
            currentFusionValue = 0f;
            statsUpdate();
            CurrentHP = MaxHP;
        }

        public void fusion()
        {
            if (currentFusionValue >= maxFusionValue)
            {
                new PlayerPetFusion(this, PetHandler.pet);
            }
        }

        public override Vector2f attackHitBoxPositionUpdate()
        {
            if (Direction == EDirection.Right)
            {
                return new Vector2f(sprite.Position.X + 70, sprite.Position.Y + 94);
            }

            else if (Direction == EDirection.Left)
            {
                return new Vector2f(sprite.Position.X, sprite.Position.Y + 94);
            }

            else
                return new Vector2f(0, 0);
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                fusion();
            }
        }
    }
}

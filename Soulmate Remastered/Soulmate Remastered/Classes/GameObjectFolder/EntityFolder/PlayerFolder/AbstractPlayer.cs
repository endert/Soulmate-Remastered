using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Classes.GameObjectFolder;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder
{
    abstract class AbstractPlayer : Entity
    {
        //FusionBar fusionBar;
        protected float maxFusionValue;
            public float getMaxFusionValue { get { return maxFusionValue; } }
        public float currentFusionValue { get; set; } 
        protected float fusionDuration; //in sec
        protected HitBox attackHitBox;
        public String type { get { return base.type + ".Player"; } }

        //Inventory???

        public HitBox getAttackHitBox()
        {
            return attackHitBox;
        }

        public virtual Vector2f getKeyPressed(float movementSpeed)
        {
            Vector2f result = new Vector2f(0, 0);

            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                result.X = -movementSpeed;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                result.Y = -movementSpeed;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                result.Y = movementSpeed;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                result.X = movementSpeed;

            return result;
        }

        public virtual void spritePositionUpdate()
        {
            switch (numFacingDirection)
            {
                case 0:
                    {
                        sprite.Position = position;
                        break;
                    }
                case 1:
                    {
                        sprite.Position = position;
                        break;
                    }
                case 2:
                    {
                        sprite.Position = position;
                        break;
                    }
                case 3:
                    {
                        sprite.Position = new Vector2f(position.X - (textureList[2].Size.X - hitBox.getWidth()), position.Y);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        public virtual void update(GameTime gameTime)
        {
            movementSpeed = 0.4f * (float)gameTime.EllapsedTime.TotalMilliseconds;
            animate(textureList);
            spritePositionUpdate();
            hitBox.setPosition(position);

            movement = new Vector2f(0, 0);
            movement = getKeyPressed(movementSpeed);
            move(movement);

            hitFromDirections.Clear();
        }
    }
}

using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Classes.GameObjectFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.ProjectileFolder;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder
{
    abstract class AbstractPlayer : Entity
    {
        public override String type { get { return base.type + ".Player"; } }
        
        //FusionBar fusionBar;
        protected float maxFusionValue;
            public float getMaxFusionValue { get { return maxFusionValue; } }
        public float currentFusionValue { get; set; } 
        protected float fusionDuration; //in sec
        protected HitBox attackHitBox;
            public HitBox getAttackHitBox { get { return attackHitBox; } }
        protected bool attacking;
        protected bool isPressedForAttack;
        protected bool isPressedForShoot;
        public bool isAttacking { get { return attacking; } }
        public float Gold { get; set; } //money money money...
        //Inventory???

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

        public bool pressedKeyForAttack()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && !isPressedForAttack)
            {
                isPressedForAttack = true;
                return true;
            }

            if (isPressedForAttack && !Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                isPressedForAttack = false;
            }
            return false;
        }

        public bool pressedKeyForShoot()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && !isPressedForShoot)
            {
                isPressedForShoot = true;
                return true;
            }

            if (isPressedForShoot && !Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                isPressedForShoot = false;
            }
            return false;
        }

        public virtual void spritePositionUpdate()
        {
            switch (getNumFacingDirection)
            {
                case 0:
                    sprite.Position = position;
                    break;
                case 1:
                    sprite.Position = position;
                    break;
                case 2:
                    sprite.Position = position;
                    break;
                case 3:
                    sprite.Position = new Vector2f(position.X - (textureList[2].Size.X - hitBox.width), position.Y);
                    break;
                default:
                    break;
            }
        }
        public virtual Vector2f attckHitBoxPositionUpdate()
        {
            return new Vector2f(0, 0);
        }

        public override void update(GameTime gameTime)
        {
            //Cheats==============
            moreHP();
            makeHeile();
            cheatDef();
            cheatAtt();
            cheatFusionValue();
            //====================
            
            movementSpeed = movementSpeedConstant * (float)gameTime.EllapsedTime.TotalMilliseconds;
            animate(textureList);
            spritePositionUpdate();
            hitBox.setPosition(position);
            attackHitBox.Position = attckHitBoxPositionUpdate();

            if (pressedKeyForAttack())
            {
                attacking = true;
                //start animate
            }
            else
            {
                attacking = false;
            }

            if (pressedKeyForShoot())
            {
                new ProjectileArrow(8f, 0.8f, 15f, facingDirection, new Vector2f(position.X+ textureList[0].Size.X+10, position.Y+textureList[0].Size.Y/2));
            }

            movement = new Vector2f(0, 0);
            movement = getKeyPressed(movementSpeed);
            move(movement);

            hitFromDirections.Clear();
        }

        //Cheats==============================================================
        public void moreHP()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.H))
            {
                maxHP += 5000000000000000000;
                currentHP += 5000000000000000000;
            }
        }

        public void makeHeile()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.B))
            {
                currentHP = maxHP;
            }
        }

        public void cheatDef()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                def += 1;
            }
        }

        public void cheatAtt()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.F))
            {
                att += 1;
            }
        }

        public void cheatFusionValue()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
            {
                currentFusionValue += 50f;
                if (currentFusionValue > maxFusionValue)
                    currentFusionValue = maxFusionValue;
            }
        }
        //====================================================================
    }
}

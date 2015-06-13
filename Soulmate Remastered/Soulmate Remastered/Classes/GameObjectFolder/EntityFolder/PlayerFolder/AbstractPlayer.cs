using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Classes.GameObjectFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.ProjectileFolder;
using SFML.Graphics;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PetFolder;
using System.Diagnostics;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder.ShopFolder;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder
{
    abstract class AbstractPlayer : Entity
    {
        public override String type { get { return base.type + ".Player"; } }

        protected Stopwatch[] coolDown = new Stopwatch[] { new Stopwatch() };
                                                              //Arrow
        /// <summary>
        /// in sec
        /// </summary>
        protected int arrowCoolDown = 1;
        protected bool arrowOnCoolDown = false;
        public List<Texture> getTexture { get { return textureList; } }
        public int MaxLvl { get { return 100; } }
        /// <summary>
        /// current level of the player
        /// </summary>
        public int lvl { get; protected set; }

        //FusionBar fusionBar;
        public float maxFusionValue { get; protected set; }
        public float currentFusionValue { get; set; }
        /// <summary>
        /// time of fusion with pet in sec
        /// </summary>
        public float fusionDuration { get { return _fusionDuration * 1000; } protected set { _fusionDuration = value; } }
        private float _fusionDuration = 0;
        public HitBox attackHitBox { get; protected set; }
        protected bool attacking;
        protected bool isPressedForAttack;
        protected bool isPressedForShoot;
        public bool isAttacking { get { return attacking; } }
        public float gold { get; set; } //money money money...
        protected float currentEXP { get; set; }
        public float getCurrentEXP { get { return currentEXP; } }
        protected float maxEXP;
        public float getMaxEXP { get { return maxEXP; } }

        protected bool transforming = false;
        protected bool animating = false;
        //Inventory???

        public String toStringForMapChange()
        {
            String playerString = "pl" + lineBreak.ToString();

            playerString += lvl + lineBreak.ToString();    //splitPlayerString[1]
            playerString += currentFusionValue + lineBreak.ToString(); //splitPlayerString[2]
            playerString += gold + lineBreak.ToString();   //splitPlayerString[3]
            playerString += currentEXP + lineBreak.ToString(); //splitPlayerString[4]
            playerString += CurrentHP + lineBreak.ToString();   //splitPlayerString[5]

            return playerString;
        }

        public String toStringForSave()
        {
            String playerForSave = toStringForMapChange();

            playerForSave += position.X + lineBreak.ToString(); //splitPlayerString[6]
            playerForSave += position.Y + lineBreak.ToString(); //splitPlayerString[7]

            return playerForSave;
        }

        public void load(String playerString)
        {
            String[] splitPlayerString = playerString.Split(lineBreak);

            loadMapChange(playerString);
            position = new Vector2f(Convert.ToSingle(splitPlayerString[6]), Convert.ToSingle(splitPlayerString[7]));
        }

        public void loadMapChange(String playerString)
        {
            String[] splitPlayerString = playerString.Split(lineBreak);

            lvl = Convert.ToInt32(splitPlayerString[1]);
            statsUpdate();
            currentFusionValue = Convert.ToSingle(splitPlayerString[2]);
            gold = Convert.ToSingle(splitPlayerString[3]);
            currentEXP = Convert.ToSingle(splitPlayerString[4]);
            CurrentHP = Convert.ToSingle(splitPlayerString[5]);
        }

        public virtual Vector2f getKeyPressed(float movementSpeed)
        {
            Vector2f result = new Vector2f(0, 0);

            if (Keyboard.IsKeyPressed(Controls.Left))
                result.X = -movementSpeed;

            if (Keyboard.IsKeyPressed(Controls.Up))
                result.Y = -movementSpeed;

            if (Keyboard.IsKeyPressed(Controls.Down))
                result.Y = movementSpeed;

            if (Keyboard.IsKeyPressed(Controls.Right))
                result.X = movementSpeed;

            return result;
        }

        public void setCurrentFusionValue()
        {
            currentFusionValue += 50;
            if (currentFusionValue >= maxFusionValue)
            {
                currentFusionValue = maxFusionValue;
            }
        }

        public void setCurrentEXP()
        {
            currentEXP += 50;
            if (currentEXP >= maxEXP)
            {
                currentEXP = maxEXP;
            }
        }

        public void adapt(AbstractPlayer player)
        {
            FacingDirection = player.FacingDirection; // RECHTS
            sprite = new Sprite(textureList[(int)player.Direction]);
            sprite.Position = player.position;
            position = player.position;
            maxFusionValue = player.maxFusionValue;
            currentFusionValue = player.currentFusionValue;

            BaseHp = player.BaseHp;
            MaxHP = player.MaxHP;
            CurrentHP = player.CurrentHP;

            BaseAtt = player.BaseAtt;
            Att = player.Att;

            BaseDef = player.BaseDef;
            Def = player.Def;

            lvl = player.lvl;
            maxEXP = player.getMaxEXP;
            currentEXP = player.getCurrentEXP;
            BaseMovementSpeed = player.BaseMovementSpeed;
        }

        public bool pressedKeyForAttack()
        {
            if ((Keyboard.IsKeyPressed(Controls.ButtonForAttack) || Mouse.IsButtonPressed(Mouse.Button.Left)) && !isPressedForAttack)
            {
                isPressedForAttack = true;
                return true;
            }

            if (isPressedForAttack && !Keyboard.IsKeyPressed(Controls.ButtonForAttack) && !Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                isPressedForAttack = false;
            }
            return false;
        }

        public bool pressedKeyForShoot()
        {
            if (Keyboard.IsKeyPressed(Controls.ButtonForShoot) && !isPressedForShoot)
            {
                isPressedForShoot = true;
                return true;
            }

            if (isPressedForShoot && !Keyboard.IsKeyPressed(Controls.ButtonForShoot))
            {
                isPressedForShoot = false;
            }
            return false;
        }

        public virtual void spritePositionUpdate()
        {
            switch (Direction)
            {
                case EDirection.Back:
                    sprite.Position = position;
                    break;
                case EDirection.Front:
                    sprite.Position = position;
                    break;
                case EDirection.Right:
                    sprite.Position = position;
                    break;
                case EDirection.Left:
                    sprite.Position = new Vector2f(position.X - (textureList[2].Size.X - hitBox.width), position.Y);
                    break;
                default:
                    break;
            }
        }

        public virtual Vector2f attackHitBoxPositionUpdate()
        {
            return new Vector2f(0, 0);
        }

        public void lvlUp()
        {
            if (lvl < MaxLvl)
            {
                while (currentEXP >= maxEXP)
                {
                    lvl++;
                    currentEXP -= maxEXP;
                    statsUpdate();
                    CurrentHP = MaxHP;
                }
            }
        }

        public void statsUpdate()
        {
            if (lvl >= MaxLvl)
            {
                lvl = MaxLvl;
                maxEXP = 0;
                currentEXP = 0;
            }
            else
            {
                maxEXP = (float)Math.Round(((5 * (float)Math.Pow(lvl + 1, 3)) / 4) * 10);
            }

            if (ItemHandler.playerInventory != null)
            {
                Att = BaseAtt + (lvl) * 1 + ItemHandler.playerInventory.getAttBonus();
                Def = BaseDef + (lvl) * 0.5f + ItemHandler.playerInventory.getDefBonus();
                MaxHP = BaseHp + (lvl) * 50 + ItemHandler.playerInventory.getHpBonus();
            }
            else
            {
                Att = BaseAtt + (lvl) * 1;
                Def = BaseDef + (lvl) * 0.5f;
                MaxHP = BaseHp + (lvl) * 50;
            }
        }

        public override void update(GameTime gameTime)
        {
            lvlUp();

            if (CurrentHP > MaxHP)
                CurrentHP = MaxHP;

            if(!Shop.shopIsOpen)
            {
                movementSpeed = BaseMovementSpeed * (float)gameTime.EllapsedTime.TotalMilliseconds;
            }
            else
            {
                movementSpeed = 0;
            }
            
            if (!animating)
            {
                animate();
            }
            hitBox.update(sprite);
            spritePositionUpdate();

            if (attackHitBox != null)
            {
                attackHitBox.Position = attackHitBoxPositionUpdate();
            }

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
                coolDown[0].Start();
                if (!arrowOnCoolDown)
                {
                    new ProjectileArrow((Att / 2) + 2, 0.8f, 2f, FacingDirection, position);
                }
                if (coolDown[0].ElapsedMilliseconds < arrowCoolDown * 1000)
                {
                    arrowOnCoolDown = true;
                }
                else
                {
                    arrowOnCoolDown = false;
                    coolDown[0].Reset();
                }
            }

            if (!transforming)
            {
                movement = new Vector2f(0, 0);
                movement = getKeyPressed(movementSpeed);
                move(movement);
            }

            if (lvl >= MaxLvl)
            {
                lvl = MaxLvl;
                currentEXP = 0;
            }

            hitFromDirections.Clear();
        }

        //Cheats==============================================================

        public void activateCheat(float parameter, Cheat cheat)
        {
            cheat(parameter);
        }

        public delegate void Cheat(float value);

        public void HealFor(float value)
        {
            CurrentHP += value;
            if (CurrentHP > MaxHP)
            {
                CurrentHP = MaxHP;
            }
        }

        public void SetHp(float value)
        {
            CurrentHP = value;

            if (CurrentHP > MaxHP)
            {
                MaxHP = CurrentHP;
            }
        }

        public void Heal(float value)
        {
            CurrentHP = MaxHP;
        }

        public void SetExp(float value)
        {
            currentEXP = value;

            lvlUp();
        }

        public void SetLvl(float _value)
        {
            int value = (int)_value;
            if (value > 0 && value <= MaxLvl)
            {
                lvl = value;
            }
            else
            {
                if (value <= 0)
                {
                    lvl = 1;
                }
                else
                {
                    lvl = MaxLvl;
                }
            }
            statsUpdate();
        }

        public void SetMoney(float value)
        {
            gold = value;
        }

        public void SetDef(float value)
        {
            Def = value;
        }

        public void SetAtt(float value)
        {
            Att = value;
        }

        public void SetFusionValue(float value)
        {
            if (value > 0 && value <= maxFusionValue)
            {
                currentFusionValue = value;
            }
            else
            {
                if (value <= 0)
                {
                    currentFusionValue = 1;
                }
                else
                {
                    currentFusionValue = maxFusionValue;
                }
            }

        }

        public void HealFusionFor(float value)
        {
            currentFusionValue += value;

            if (currentFusionValue > maxFusionValue)
                currentFusionValue = maxFusionValue;
        }
        //====================================================================

        public override void debugDraw(RenderWindow window)
        {
            base.debugDraw(window);
            attackHitBox.draw(window);
        }

    }
}

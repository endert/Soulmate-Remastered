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

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder
{
    abstract class AbstractPlayer : Entity
    {
        public override String type { get { return base.type + ".Player"; } }

        protected Stopwatch[] coolDown = new Stopwatch[] { new Stopwatch() };
        //                                                  Arrow
        protected int arrowCd = 1; //in sec
        protected bool arrowOnCoolDown = false;
        public List<Texture> getTexture { get { return textureList; } }
        public int MaxLvl { get { return 100; } }
        protected int lvl;
            public int getLvl { get { return lvl; } }
        //FusionBar fusionBar;
        protected float maxFusionValue;
            public float getMaxFusionValue { get { return maxFusionValue; } }
        public float currentFusionValue { get; set; } 
        protected float fusionDuration; //in sec
            public float getFusionDuration { get { return fusionDuration * 1000; } }
        protected HitBox attackHitBox;
            public HitBox getAttackHitBox { get { return attackHitBox; } }
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
            playerString += getCurrentHP + lineBreak.ToString();   //splitPlayerString[5]

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
            currentHP = Convert.ToSingle(splitPlayerString[5]);
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
            facingDirection = player.getFacingDirection; // RECHTS
            sprite = new Sprite(textureList[player.getNumFacingDirection]);
            sprite.Position = player.position;
            position = player.position;
            maxFusionValue = player.getMaxFusionValue;
            currentFusionValue = player.currentFusionValue;

            baseHp = player.getBaseHp;
            maxHP = player.getMaxHP;
            currentHP = player.getCurrentHP;

            baseAtt = player.getBaseAtt;
            att = player.getAtt;

            baseDef = player.getBaseDef;
            def = player.getDef;

            lvl = player.getLvl;
            maxEXP = player.getMaxEXP;
            currentEXP = player.getCurrentEXP;

            movementSpeedConstant = player.getMovementSpeedConstant;
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

        public void lvlUp()
        {
            if (lvl < MaxLvl)
            {
                if (currentEXP >= maxEXP)
                {
                    lvl += ((int)currentEXP / (int)maxEXP);
                    currentEXP %= maxEXP;
                    statsUpdate();
                    currentHP = maxHP;
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
            att = baseAtt + (lvl) * 1;
            def = baseDef + (lvl) * 0.5f;
            maxHP = baseHp + (lvl) * 50;
        }

        public void cheatUpdate()
        {
            drawHitBoxOnOff();
            moreHP();
            makeHeile();
            expCheat();
            moneyMoneyMoney();
            cheatDef();
            cheatAtt();
            cheatFusionValue();
        }

        public override void update(GameTime gameTime)
        {
            lvlUp();
            //Cheats==============
            cheatUpdate();
            //====================
            movementSpeed = movementSpeedConstant * (float)gameTime.EllapsedTime.TotalMilliseconds;
            if (!animating)
            {
                animate(textureList);
            }
            hitBox.setPosition(position);
            spritePositionUpdate();

            if (attackHitBox != null)
            {
                attackHitBox.Position = attckHitBoxPositionUpdate();
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
                    new ProjectileArrow((att / 2) + 2, 0.8f, 2f, facingDirection, position);
                }
                if (coolDown[0].ElapsedMilliseconds < arrowCd * 1000)
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
        public void moreHP()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.H))
            {
                float c = 50;
                maxHP += c;
                currentHP += c;
            }
        }

        public void makeHeile()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.B))
            {
                currentHP = maxHP;
            }
        }

        public void expCheat()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.E))
            {
                currentEXP += 500;
            }
        }

        public void moneyMoneyMoney()
        {
            if(Keyboard.IsKeyPressed(Keyboard.Key.M))
            {
                gold += 500;
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
                setCurrentFusionValue();
            }
        }

        public void drawHitBoxOnOff()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.F1))
            {
                if (HitBox.VISIBLE == false)
                {
                    HitBox.VISIBLE = true;
                }
                else
                {
                    HitBox.VISIBLE = false;
                }
            }
        }
        //====================================================================
    }
}

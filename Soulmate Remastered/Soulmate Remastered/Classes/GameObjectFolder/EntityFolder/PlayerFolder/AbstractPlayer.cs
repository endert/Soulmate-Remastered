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
using Soulmate_Remastered.Core;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder
{
    /// <summary>
    /// base class for player instances
    /// </summary>
    abstract class AbstractPlayer : Entity
    {
        /// <summary>
        /// the type of this instance
        /// </summary>
        public override String Type { get { return base.Type + ".Player"; } }

        /// <summary>
        /// returns the max Level
        /// </summary>
        public int MaxLvl { get { return 100; } }
        /// <summary>
        /// current level of the player
        /// </summary>
        public int Lvl { get; protected set; }
        /// <summary>
        /// gold value (money, money, money)
        /// </summary>
        public float Gold { get; set; }
        /// <summary>
        /// current Exp value
        /// </summary>
        public float CurrentEXP { get; protected set; }
        /// <summary>
        /// max exp value
        /// </summary>
        public float MaxEXP { get; protected set; }
        /// <summary>
        /// max fusion units
        /// </summary>
        public float MaxFusionValue { get; protected set; }
        /// <summary>
        /// current fusion units
        /// </summary>
        public float CurrentFusionValue { get; set; }
        /// <summary>
        /// time of fusion with pet in milli sec
        /// </summary>
        public float FusionDuration { get; protected set; }

        /// <summary>
        /// hit box for attacking
        /// </summary>
        public HitBox AttackHitBox { get; protected set; }
        /// <summary>
        /// bool if the player is attacking or not
        /// </summary>
        public bool Attacking { get; protected set; }

        /// <summary>
        /// array with all cooldowns
        /// </summary>
        protected Cooldown[] CoolDowns { get { return new Cooldown[] { new Cooldown(1) }; } }
        /// <summary>
        /// enum for better Cooldown access
        /// </summary>
        protected enum ECooldown
        {
            Arrow
        }
        /// <summary>
        /// returns bool if the button for attack is pressed or not
        /// </summary>
        public bool IsPressedForAttack
        {
            get
            {
                if ((Keyboard.IsKeyPressed(Controls.ButtonForAttack) || Mouse.IsButtonPressed(Mouse.Button.Left)) && !IsPressedForAttack)
                    return true;

                if (IsPressedForAttack && !Keyboard.IsKeyPressed(Controls.ButtonForAttack) && !Mouse.IsButtonPressed(Mouse.Button.Left))
                    return false;

                return false;
            }
        }
        /// <summary>
        /// returns bool if the button for shooting is pressed or not
        /// </summary>
        public bool IsPressedForShoot
        {
            get
            {
                if (Keyboard.IsKeyPressed(Controls.ButtonForShoot) && !IsPressedForShoot)
                    return true;

                if (IsPressedForShoot && !Keyboard.IsKeyPressed(Controls.ButtonForShoot))
                    return false;

                return false;
            }
        }
        protected bool transforming = false;
        protected bool animating = false;
        
        /// <summary>
        /// <para>creates a string wich contains the players stats</para>
        /// <para>[1] lvl</para>
        /// <para>[2] current fusion value</para>
        /// <para>[3] gold</para>
        /// <para>[4] current exp</para>
        /// <para>[5] current hp</para>
        /// </summary>
        /// <returns></returns>
        public String ToStringForMapChange()
        {
            String playerString = "pl" + LineBreak.ToString();

            playerString += Lvl + LineBreak.ToString();
            playerString += CurrentFusionValue + LineBreak.ToString();
            playerString += Gold + LineBreak.ToString();
            playerString += CurrentEXP + LineBreak.ToString();
            playerString += CurrentHP + LineBreak.ToString();

            return playerString;
        }

        /// <summary>
        /// <para>creates a string wich contains the players stats</para>
        /// <para>[1] lvl</para>
        /// <para>[2] current fusion value</para>
        /// <para>[3] gold</para>
        /// <para>[4] current exp</para>
        /// <para>[5] current hp</para>
        /// <para>[6] Position.X</para>
        /// <para>[7] Position.Y</para>
        /// </summary>
        /// <returns></returns>
        public String ToStringForSave()
        {
            String playerForSave = ToStringForMapChange();

            playerForSave += Position.X + LineBreak.ToString();
            playerForSave += Position.Y + LineBreak.ToString();

            return playerForSave;
        }
        
        /// <summary>
        /// load player attributes from a string
        /// </summary>
        /// <param name="playerString"></param>
        public void Load(String playerString)
        {
            String[] splitPlayerString = playerString.Split(LineBreak);

            LoadMapChange(playerString);
            Position = new Vector2(Convert.ToSingle(splitPlayerString[6]), Convert.ToSingle(splitPlayerString[7]));
        }

        /// <summary>
        /// load player attributes from a string
        /// </summary>
        /// <param name="playerString"></param>
        public void LoadMapChange(String playerString)
        {
            String[] splitPlayerString = playerString.Split(LineBreak);

            Lvl = Convert.ToInt32(splitPlayerString[1]);
            StatsUpdate();
            CurrentFusionValue = Convert.ToSingle(splitPlayerString[2]);
            Gold = Convert.ToSingle(splitPlayerString[3]);
            CurrentEXP = Convert.ToSingle(splitPlayerString[4]);
            CurrentHP = Convert.ToSingle(splitPlayerString[5]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="movementSpeed"></param>
        /// <returns></returns>
        public virtual Vector2 GetKeyPressed(float movementSpeed)
        {
            Vector2 result = new Vector2(0, 0);

            if (Keyboard.IsKeyPressed(Controls.Left))
                result += Vector2.LEFT * movementSpeed;

            if (Keyboard.IsKeyPressed(Controls.Up))
                result += Vector2.FRONT * movementSpeed;

            if (Keyboard.IsKeyPressed(Controls.Down))
                result += Vector2.BACK * movementSpeed;

            if (Keyboard.IsKeyPressed(Controls.Right))
                result += Vector2.RIGHT * movementSpeed;

            return result;
        }

        /// <summary>
        /// adds 50 to the current fusion value
        /// </summary>
        public void RiseCurrentFusionValue()
        {
            CurrentFusionValue += 50;
            if (CurrentFusionValue >= MaxFusionValue)
            {
                CurrentFusionValue = MaxFusionValue;
            }
        }

        /// <summary>
        /// adds 50 to the current amount of exp
        /// </summary>
        public void RiseCurrentEXP()
        {
            CurrentEXP += 50;
            if (CurrentEXP >= MaxEXP)
            {
                CurrentEXP = MaxEXP;
            }
        }

        /// <summary>
        /// adapt all stats from the given player, used for fusion
        /// </summary>
        /// <param name="player"></param>
        public void Adapt(AbstractPlayer player)
        {
            FacingDirection = player.FacingDirection;
            Sprite = new Sprite(TextureList[(int)player.Direction]);
            Sprite.Position = player.Position;
            Position = player.Position;
            MaxFusionValue = player.MaxFusionValue;
            CurrentFusionValue = player.CurrentFusionValue;

            BaseHp = player.BaseHp;
            MaxHP = player.MaxHP;
            CurrentHP = player.CurrentHP;

            BaseAtt = player.BaseAtt;
            Att = player.Att;

            BaseDef = player.BaseDef;
            Def = player.Def;

            Lvl = player.Lvl;
            MaxEXP = player.MaxEXP;
            CurrentEXP = player.CurrentEXP;
            BaseMovementSpeed = player.BaseMovementSpeed;
        }

        /// <summary>
        /// updates the sprite position, according to wich form the player is in
        /// </summary>
        public virtual void SpritePositionUpdate()
        {
            switch (Direction)
            {
                case EDirection.Back:
                    Sprite.Position = Position;
                    break;
                case EDirection.Front:
                    Sprite.Position = Position;
                    break;
                case EDirection.Right:
                    Sprite.Position = Position;
                    break;
                case EDirection.Left:
                    Sprite.Position = new Vector2(Position.X - (TextureList[2].Size.X - HitBox.width), Position.Y);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// returns the hitbox position according to the players form and viewing direction
        /// </summary>
        public virtual Vector2 GetHitBoxPosition()
        {
            return Vector2.ZERO;
        }

        /// <summary>
        /// raise all stats accordingly
        /// </summary>
        public void LvlUp()
        {
            if (Lvl < MaxLvl)
            {
                while (CurrentEXP >= MaxEXP)
                {
                    Lvl++;
                    CurrentEXP -= MaxEXP;
                    StatsUpdate();
                    CurrentHP = MaxHP;
                }
            }
        }

        /// <summary>
        /// updates all stats if needed
        /// </summary>
        public void StatsUpdate()
        {
            //check the level border
            if (Lvl >= MaxLvl)
            {
                Lvl = MaxLvl;
                MaxEXP = 0;
                CurrentEXP = 0;
            }
            else
                MaxEXP = (float)Math.Round(((5 * (float)Math.Pow(Lvl + 1, 3)) / 4) * 10);

            //set the states accordingly
            if (ItemHandler.playerInventory != null)
            {
                Att = BaseAtt + (Lvl) * 1 + ItemHandler.playerInventory.getAttBonus();
                Def = BaseDef + (Lvl) * 0.5f + ItemHandler.playerInventory.getDefBonus();
                MaxHP = BaseHp + (Lvl) * 50 + ItemHandler.playerInventory.getHpBonus();
            }
            else
            {
                Att = BaseAtt + (Lvl) * 1;
                Def = BaseDef + (Lvl) * 0.5f;
                MaxHP = BaseHp + (Lvl) * 50;
            }
        }

        /// <summary>
        /// updates all stats and calls all help methods
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            LvlUp();

            if (CurrentHP > MaxHP)
                CurrentHP = MaxHP;

            if(!Shop.ShopIsOpen)
                movementSpeed = BaseMovementSpeed * (float)gameTime.EllapsedTime.TotalMilliseconds;
            else
                movementSpeed = 0;
            
            if (!animating)
                animate();

            HitBox.update(Sprite);
            SpritePositionUpdate();

            if (AttackHitBox != null)
                AttackHitBox.Position = GetHitBoxPosition();

            if (IsPressedForAttack)
            {
                Attacking = true;
                //start animate
            }
            else
                Attacking = false;

            if (IsPressedForShoot)
            {
                CoolDowns[(int)ECooldown.Arrow].Start();
                if (!CoolDowns[(int)ECooldown.Arrow].OnCooldown)
                    new ProjectileArrow((Att / 2) + 2, 0.8f, 2f, FacingDirection, Position);
            }

            if (!transforming)
            {
                movement = Vector2.ZERO;
                movement = GetKeyPressed(movementSpeed);
                move(movement);
            }

            if (Lvl >= MaxLvl)
            {
                Lvl = MaxLvl;
                CurrentEXP = 0;
            }

            foreach (Cooldown c in CoolDowns)
                c.Update();

            hitFromDirections.Clear();
        }

        //Cheats==============================================================

        public void ActivateCheat(float parameter, Cheat cheat)
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
            CurrentEXP = value;

            LvlUp();
        }

        public void SetLvl(float _value)
        {
            int value = (int)_value;
            if (value > 0 && value <= MaxLvl)
            {
                Lvl = value;
            }
            else
            {
                if (value <= 0)
                {
                    Lvl = 1;
                }
                else
                {
                    Lvl = MaxLvl;
                }
            }
            StatsUpdate();
        }

        public void SetMoney(float value)
        {
            Gold = value;
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
            if (value > 0 && value <= MaxFusionValue)
            {
                CurrentFusionValue = value;
            }
            else
            {
                if (value <= 0)
                {
                    CurrentFusionValue = 1;
                }
                else
                {
                    CurrentFusionValue = MaxFusionValue;
                }
            }

        }

        public void HealFusionFor(float value)
        {
            CurrentFusionValue += value;

            if (CurrentFusionValue > MaxFusionValue)
                CurrentFusionValue = MaxFusionValue;
        }
        //====================================================================

        /// <summary>
        /// enables debug drawing
        /// </summary>
        /// <param name="window"></param>
        public override void DebugDraw(RenderWindow window)
        {
            base.DebugDraw(window);
            AttackHitBox.draw(window);
        }

    }
}

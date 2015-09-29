using System;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.ProjectileFolder;
using SFML.Graphics;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using Soulmate_Remastered.Core;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder
{
    /// <summary>
    /// base class for player instances
    /// </summary>
    abstract class AbstractPlayer : Entity
    {
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

        protected override void OnKeyPress(object sender, KeyEventArgs e)
        {
            base.OnKeyPress(sender, e);

            if(e.Key == Controls.Key.Attack)
                Attacking = true;
            
            if(e.Key == Controls.Key.Shoot)
            {
                CoolDowns[(int)ECooldown.Arrow].Start();
                if (!CoolDowns[(int)ECooldown.Arrow].OnCooldown)
                    new ProjectileArrow(this);
            }

            if (e.Key == Controls.Key.Left)
                movement += Vector2.LEFT;

            if (e.Key == Controls.Key.Right)
                movement += Vector2.RIGHT;

            if (e.Key == Controls.Key.Down)
                movement += Vector2.FRONT;

            if (e.Key == Controls.Key.Up)
                movement += Vector2.BACK;
        }

        protected override void OnKeyRelease(object sender, KeyEventArgs e)
        {
            if (e.Key == Controls.Key.Attack)
                Attacking = false;

            if (e.Key == Controls.Key.Left)
                movement -= Vector2.LEFT;

            if (e.Key == Controls.Key.Right)
                movement -= Vector2.RIGHT;

            if (e.Key == Controls.Key.Down)
                movement -= Vector2.FRONT;

            if (e.Key == Controls.Key.Up)
                movement -= Vector2.BACK;

        }

        protected override void OnMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Left)
                Attacking = true;
        }

        protected override void OnMouseButtonRelease(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Left)
                Attacking = false;
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
        public string ToStringForMapChange()
        {
            string playerString = base.ToStringForSave() + LineBreak.ToString();

            playerString += Lvl + LineBreak.ToString();
            playerString += CurrentFusionValue + LineBreak.ToString();
            playerString += Gold + LineBreak.ToString();
            playerString += CurrentEXP + LineBreak.ToString();
            playerString += CurrentHP + LineBreak.ToString();

            return playerString;
        }

        /// <summary>
        /// suttf mgcrf, sglc suiw ah lgb äfftli rfmmlbwuc
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
        public override string ToStringForSave()
        {
            string playerForSave = ToStringForMapChange();

            playerForSave += Position.X + LineBreak.ToString();
            playerForSave += Position.Y + LineBreak.ToString();

            return playerForSave;
        }
        
        /// <summary>
        /// load player attributes from a string
        /// </summary>
        /// <param name="playerString"></param>
        public void Load(string playerString)
        {
            string[] splitPlayerString = playerString.Split(LineBreak);

            LoadMapChange(playerString);
            Position = new Vector2(Convert.ToSingle(splitPlayerString[6]), Convert.ToSingle(splitPlayerString[7]));
        }

        /// <summary>
        /// load player attributes from a string
        /// </summary>
        /// <param name="playerString"></param>
        public void LoadMapChange(string playerString)
        {
            string[] splitPlayerString = playerString.Split(LineBreak);

            Lvl = Convert.ToInt32(splitPlayerString[1]);
            StatsUpdate();
            CurrentFusionValue = Convert.ToSingle(splitPlayerString[2]);
            Gold = Convert.ToSingle(splitPlayerString[3]);
            CurrentEXP = Convert.ToSingle(splitPlayerString[4]);
            CurrentHP = Convert.ToSingle(splitPlayerString[5]);
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
        /// <para>calls player.SubstractEvents methode to denie any control over that instance</para>
        /// </summary>
        /// <param name="player"></param>
        public void Adapt(AbstractPlayer player)
        {
            //calls the substractEvents methode to denie any control over that instance
            player.SubstractEvents();

            movement = player.movement;
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
                    Sprite.Position = new Vector2(Position.X - (TextureList[2].Size.X - HitBox.Width), Position.Y);
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
            if (ItemHandler.ṔlayerInventory != null)
            {
                Att = BaseAtt + (Lvl) * 1 + ItemHandler.ṔlayerInventory.GetAttBonus();
                Def = BaseDef + (Lvl) * 0.5f + ItemHandler.ṔlayerInventory.GetDefBonus();
                MaxHP = BaseHp + (Lvl) * 50 + ItemHandler.ṔlayerInventory.GetHpBonus();
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
            
            if (!animating)
                Animate();

            HitBox.Update(Sprite);
            SpritePositionUpdate();

            if (AttackHitBox != null)
                AttackHitBox.Position = GetHitBoxPosition();

            if (transforming)
                movement = Vector2.ZERO;

            Move(movement * MovementSpeed);

            if (Lvl >= MaxLvl)
            {
                Lvl = MaxLvl;
                CurrentEXP = 0;
            }

            foreach (Cooldown c in CoolDowns)
                c.Update();
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
            AttackHitBox.Draw(window);
        }

    }
}

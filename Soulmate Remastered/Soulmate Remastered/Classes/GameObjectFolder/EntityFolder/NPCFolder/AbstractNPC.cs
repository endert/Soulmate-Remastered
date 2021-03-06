﻿using Soulmate_Remastered.Classes.DialogeBoxFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Core;
using System.IO;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder
{
    /// <summary>
    /// Non playable Charakter
    /// </summary>
    abstract class AbstractNPC : Entity
    {
        /// <summary>
        /// randius in wich the Player can interact with this NPC constant
        /// </summary>
        protected float InteractionRadius { get { return 50f; } }
        /// <summary>
        /// the dialoge Box of this instance
        /// </summary>
        protected DialogeBox Dialoge
        { 
            get 
            {
                if (Interacting)
                    return new DialogeBox(new Vector2(Position.X, Position.Y - 100), LoadDialoge(), this);
                else
                    return null;
            } 
        }
        /// <summary>
        /// <para>bool if the Player is interacting with this NPC</para>
        /// <para>should be inititalized with false in every constructor</para>
        /// </summary>
        public bool Interacting { get; protected set; }
        /// <summary>
        /// bool if a dialoge is open
        /// </summary>
        protected bool dialogeIsOn = false;
        /// <summary>
        /// Path of the dialoge File
        /// </summary>
        protected virtual string DialogePath { get { return ""; } }
        /// <summary>
        /// bool if the Player is in interaction range of this instance
        /// </summary>
        public bool InInteractionRange { get { return HitBox.DistanceTo(PlayerHandler.Player.HitBox) <= InteractionRadius; } }

        public AbstractNPC() : base()
        {
            Interacting = false;
            NPCHandler.Add_(this);
        }

        /// <summary>
        /// load the Dialoge from the dialoge File
        /// </summary>
        /// <returns>dialoge String</returns>
        private string LoadDialoge()
        {
            string res = "";

            using (StreamReader reader = new StreamReader(DialogePath))
            {
                while (!reader.EndOfStream)
                {
                    res += reader.ReadLine() + "\n";
                }
            }

            return res;
        }

        protected override void OnKeyPress(object sender, KeyEventArgs e)
        {
            base.OnKeyPress(sender, e);

            if(e.Key == Controls.Key.Interact)
            {
                if (InInteractionRange && !Interacting)
                    Interact();
            }

            if (e.Key == Controls.Key.Escape)
            {
                if (Interacting)
                {
                    StopIteraction();
                    NPCHandler.Shop_ = null;
                }
            }
        }

        /// <summary>
        /// stops interaction with this instance
        /// </summary>
        public virtual void StopIteraction()
        {
            Interacting = false;
            DialogeHandler.Clear();
        }

        /// <summary>
        /// interacts with the Player
        /// </summary>
        public virtual void Interact()
        {

        }

        /// <summary>
        /// updates this instance
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Sprite.Position = Position;
            HitBox.Position = Position;
            Animate();
        }
    }
}

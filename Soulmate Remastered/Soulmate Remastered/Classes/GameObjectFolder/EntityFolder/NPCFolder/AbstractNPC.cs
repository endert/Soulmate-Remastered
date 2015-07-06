using SFML.Window;
using Soulmate_Remastered.Classes.DialogeBoxFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder
{
    /// <summary>
    /// Non playable Charakter
    /// </summary>
    abstract class AbstractNPC : Entity
    {
        /// <summary>
        /// the type of this instance
        /// </summary>
        public override string Type { get { return base.Type + ".NPC"; } }
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
                    return new DialogeBox(new Vector2f(Position.X, Position.Y - 100), LoadDialoge(), this);
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
        protected virtual String DialogePath { get { return ""; } }
        /// <summary>
        /// bool if the Player is in interaction range of this instance
        /// </summary>
        public bool InIteractionRange { get { return HitBox.DistanceTo(PlayerHandler.player.HitBox) <= InteractionRadius; } }

        /// <summary>
        /// load the Dialoge from the dialoge File
        /// </summary>
        /// <returns>dialoge String</returns>
        private String LoadDialoge()
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

        /// <summary>
        /// stops interaction with this instance
        /// </summary>
        public virtual void StopIteraction()
        {
            Interacting = false;
            DialogeHandler.DialogeList.Clear();
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
            animate();
        }
    }
}

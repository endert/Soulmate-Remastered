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
    abstract class AbstractNPC : Entity
    {
        public override string type { get { return base.type + ".NPC"; } }

        protected float interactionRadius = 50f;
        protected DialogeBox dialoge 
        { 
            get 
            {
                if (interacted)
                    return new DialogeBox(new Vector2f(position.X, position.Y - 100), dialogeText, this);
                else
                    return null;
            } 
        }
        protected bool interacted = false;
        protected bool dialogeIsOn = false;
        protected virtual String dialogePath { get { return ""; } }
        protected String dialogeText { get { return loadDialoge(); } }
        public bool isInteracting { get { return interacted; } }
        public bool InIteractionRange { get { return hitBox.distanceTo(PlayerHandler.player.hitBox) <= interactionRadius; } }

        protected String loadDialoge()
        {
            string res = "";

            StreamReader reader = new StreamReader(dialogePath);

            while (!reader.EndOfStream)
            {
                res += reader.ReadLine() + "\n";
            }

            reader.Close();

            return res;
        }

        public virtual void stopIteraction()
        {
            interacted = false;
            DialogeHandler.dialogeList.Clear();
        }

        public virtual void interact()
        {

        }

        public override void update(GameTime gameTime)
        {
            
        }

    }
}

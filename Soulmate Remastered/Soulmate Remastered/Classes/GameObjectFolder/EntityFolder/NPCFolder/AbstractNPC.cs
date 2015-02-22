using Soulmate_Remastered.Classes.DialogeBoxFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder
{
    abstract class AbstractNPC : Entity
    {
        public override string type { get { return base.type + ".NPC"; } }

        protected float interactionRadius = 50f;
        protected DialogeBox dialoge;
        protected bool interacted = false;
        protected bool dialogeIsOn = false;
        public bool isInteracting { get { return interacted; } }
        public bool InIteractionRange { get { return hitBox.distanceTo(PlayerHandler.player.hitBox) <= interactionRadius; } }

        public virtual void stopIteraction()
        {
            interacted = false;
            dialoge = null;
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

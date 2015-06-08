using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Classes.DialogeBoxFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using System.IO;
using Soulmate_Remastered.Core;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder
{
    /// <summary>
    /// Guy who keeps an eye on your pet
    /// </summary>
    class PetStorageGuy : AbstractNPC
    {
        /// <summary>
        /// the type of this instance
        /// </summary>
        public override String type { get { return base.type + ".PetStorageGuy"; } }
        /// <summary>
        /// path to the dialoge file
        /// </summary>
        protected override string dialogePath{ get { return "Dialoges/Test.txt"; } }

        /// <summary>
        /// create an instance of this class
        /// </summary>
        /// <param name="_position"></param>
        public PetStorageGuy(Vector2 _position)
        {
            //Initialize GameObject Data*********************************************************************
            
            textureList.Add(new Texture("Pictures/Entities/NPC/PetStorageGuy/PetStorageGuyFrontTest.png"));
            textureList.Add(new Texture("Pictures/Entities/NPC/PetStorageGuy/PetStorageGuyBackTest.png"));
            textureList.Add(new Texture("Pictures/Entities/NPC/PetStorageGuy/PetStorageGuyRightTest.png"));
            textureList.Add(new Texture("Pictures/Entities/NPC/PetStorageGuy/PetStorageGuyLeftTest.png"));

            sprite = new Sprite(textureList[0]);
            position = _position;
            sprite.Position = position;
            hitBox = new HitBox(position, sprite.Texture.Size.X, sprite.Texture.Size.Y);
            
            //***********************************************************************************************
            //Initialize Entity Data*************************************************************************

            FacingDirection = Vector2.FRONT;

            //***********************************************************************************************
            //Initialize NPC Data****************************************************************************

            Interacting = false;

            NPCHandler.add(this);
        }

        /// <summary>
        /// interaction with the player
        /// </summary>
        public override void interact()
        {
            Interacting = true;
            DialogeHandler.dialogeList.Add(dialoge);
        }
    }
}

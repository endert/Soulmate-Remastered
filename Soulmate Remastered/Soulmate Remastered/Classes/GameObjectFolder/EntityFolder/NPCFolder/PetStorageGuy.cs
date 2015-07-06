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
        public override String Type { get { return base.Type + ".PetStorageGuy"; } }
        /// <summary>
        /// path to the dialoge file
        /// </summary>
        protected override string DialogePath{ get { return "Dialoges/Test.txt"; } }

        /// <summary>
        /// create an instance of this class
        /// </summary>
        /// <param name="_position"></param>
        public PetStorageGuy(Vector2 _position)
        {
            //Initialize GameObject Data*********************************************************************
            
            TextureList.Add(new Texture("Pictures/Entities/NPC/PetStorageGuy/PetStorageGuyFrontTest.png"));
            TextureList.Add(new Texture("Pictures/Entities/NPC/PetStorageGuy/PetStorageGuyBackTest.png"));
            TextureList.Add(new Texture("Pictures/Entities/NPC/PetStorageGuy/PetStorageGuyRightTest.png"));
            TextureList.Add(new Texture("Pictures/Entities/NPC/PetStorageGuy/PetStorageGuyLeftTest.png"));

            IsAlive = true;
            IsVisible = true;
            Sprite = new Sprite(TextureList[0]);
            Position = _position;
            Sprite.Position = Position;
            HitBox = new HitBox(Position, Sprite.Texture.Size.X, Sprite.Texture.Size.Y);
            
            //***********************************************************************************************
            //Initialize Entity Data*************************************************************************

            FacingDirection = Vector2.FRONT;

            //***********************************************************************************************
            //Initialize NPC Data****************************************************************************

            Interacting = false;

            NPCHandler.Add_(this);
        }

        /// <summary>
        /// interaction with the player
        /// </summary>
        public override void Interact()
        {
            Interacting = true;
            DialogeHandler.DialogeList.Add(Dialoge);
        }
    }
}

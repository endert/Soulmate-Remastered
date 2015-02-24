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

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder
{
    class PetStorageGuy : AbstractNPC
    {
        public override String type { get { return base.type + ".PetStorageGuy"; } }
        protected override string dialogePath{ get { return "Dialoges/Test.txt"; } }

        public PetStorageGuy(Vector2f _position)
        {
            textureList.Add(new Texture("Pictures/Entities/NPC/PetStorageGuy/PetStorageGuyFrontTest.png"));
            textureList.Add(new Texture("Pictures/Entities/NPC/PetStorageGuy/PetStorageGuyBackTest.png"));
            textureList.Add(new Texture("Pictures/Entities/NPC/PetStorageGuy/PetStorageGuyRightTest.png"));
            textureList.Add(new Texture("Pictures/Entities/NPC/PetStorageGuy/PetStorageGuyLeftTest.png"));

            facingDirection = new Vector2f(0, 1);

            sprite = new Sprite(textureList[getNumFacingDirection]);

            position = _position;
            sprite.Position = position;
            hitBox = new HitBox(position, sprite.Texture.Size.X, sprite.Texture.Size.Y);
            interacted = false;
            NPCHandler.add(this);
        }

        public override void interact()
        {
            interacted = true;
            DialogeHandler.dialogeList.Add(dialoge);
        }
    }
}

﻿using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Classes.DialogeBoxFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.NPCFolder
{
    class PetStorageGuy : AbstractNPC
    {
        public override String type { get { return base.type + ".PetStorageGuy"; } }

        String test = "lulululu ... heiliege Kuh; . . . *trinkt einen Schluck* . . . dieser verkackte INDER XD, "
                    + "n bissl Rassismus muss halt sein außerdem weiß ich nicht wie ich den Text noch strecken kann ;) "
                    + "ist jetzt auch schon lang genug für Testzwecke ^^";

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
            dialoge = new DialogeBox(new Vector2f(position.X, position.Y-100), test);
            DialogeHandler.dialogeList.Add(dialoge);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.EquipmentFolder;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.TreasureChestFolder
{
    class TreasureChest : AbstractTreasureChest
    {
        public TreasureChest(Vector2f _position)
        {
            textureList.Add(new Texture("Pictures/Treasure Chest/Treasure Chest.jpg"));
            sprite = new Sprite(textureList[0]);
            isOpen = false;
            position = _position;
            sprite.Position = position;
            hitBox = new HitBox(position, textureList[0].Size.X, textureList[0].Size.Y);
            drops = new AbstractItem[] { new Sword(100,100,100,"Super Awesome Mega Sword of DOOOOOOM!!!!")};
            TreasureChestHandler.add(this);
        }

        public override void update(GameTime gameTime)
        {
            interaction();
        }
    }
}

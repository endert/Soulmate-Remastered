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
            TextureList.Add(new Texture("Pictures/Treasure Chest/TreasureChest.png"));
            IsVisible = true;
            Sprite = new Sprite(TextureList[0]);
            isOpen = false;
            Position = _position;
            Sprite.Position = Position;
            HitBox = new HitBox(Position, TextureList[0].Size.X, TextureList[0].Size.Y);
            drops = new AbstractItem[] { new Sword(100,100,100,"Super Awesome Mega Sword of DOOOOOOM!!!!")};
            TreasureChestHandler.add(this);
        }

        public override void Update(GameTime gameTime)
        {
            interaction();
        }
    }
}

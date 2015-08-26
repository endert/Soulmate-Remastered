using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.EquipmentFolder;
using Soulmate_Remastered.Core;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.TreasureChestFolder
{
    /// <summary>
    /// treasure chest
    /// </summary>
    class TreasureChest : AbstractTreasureChest
    {
        public TreasureChest(Vector2 _position, params AbstractItem[] _drops)
        {
            //game object*************************************************************

            TextureList.Add(new Texture("Pictures/Treasure Chest/TreasureChest.png"));
            IsVisible = true;
            Sprite = new Sprite(TextureList[0]);
            Position = _position;
            Sprite.Position = Position;
            HitBox = new HitBox(Position, TextureList[0].Size.X, TextureList[0].Size.Y);

            //************************************************************************
            
            //entity******************************************************************

            Drops = _drops;

            //************************************************************************
            
            //treasure chest**********************************************************

            isOpen = false;
            TreasureChestHandler.Add(this);

            //************************************************************************
        }

        public override void Update(GameTime gameTime)
        {
            Interaction();
        }
    }
}

using SFML.Graphics;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using Soulmate_Remastered.Core;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.TreasureChestFolder
{
    /// <summary>
    /// treasure chest
    /// </summary>
    class TreasureChest : AbstractTreasureChest
    {
        protected override void LoadTextures()
        {
            TextureList.Add(new Texture("Pictures/Treasure Chest/TreasureChest.png"));
        }

        public TreasureChest(Vector2 _position, params AbstractItem[] _drops)
        {
            //game object*************************************************************

            Position = _position;
            Sprite.Position = Position;
            HitBox.Position = Position;

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

        }
    }
}

using SFML.Graphics;
using Soulmate_Remastered.Classes.GameObjectFolder;
using Soulmate_Remastered.Core;

namespace Soulmate_Remastered.Classes.MapFolder
{
    class Blocks
    {
        bool walkable;
        Sprite blockSprite;
        BaseHitBox blockHitBox;

        public Sprite getSprite { get { return blockSprite; } }

        public bool getWalkable()
        {
            return this.walkable;
        }

        public BaseHitBox getBlockHitBox { get { return blockHitBox; } }

        public Blocks(int blockType, Vector2 position, Texture bodenTex)
        {
            switch (blockType)
            {
                case 0://Boden
                    {
                        this.blockSprite = new Sprite(bodenTex);
                        this.blockSprite.Position = position;
                        this.walkable = true;
                        break;
                    }
                case 1://Wald
                    {
                        this.blockSprite = new Sprite(new Texture("Pictures/Map/Ground/Wald.png"));
                        this.blockSprite.Position = position;
                        this.walkable = false;
                        blockHitBox = new BaseHitBox(position, (float)blockSprite.Texture.Size.X, (float)blockSprite.Texture.Size.Y);
                        break;
                    }
            }
        }

        public void draw(RenderWindow window)
        {
            window.Draw(this.blockSprite);
        }
    }
}

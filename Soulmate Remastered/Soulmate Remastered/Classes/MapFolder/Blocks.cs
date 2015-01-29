using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Classes.GameObjectFolder;

namespace Soulmate_Remastered.Classes.MapFolder
{
    class Blocks
    {
        bool walkable;
        Sprite blockSprite;
        HitBox blockHitbox;

        public bool getWalkable()
        {
            return this.walkable;
        }

        public Blocks(int blockType, Vector2f position, Texture bodenTex)
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
                        this.blockSprite = new Sprite(new Texture("Pictures/Map/Wald.png"));
                        this.blockSprite.Position = position;
                        this.walkable = false;
                        blockHitbox = new HitBox(position, bodenTex.Size.X, bodenTex.Size.Y);
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

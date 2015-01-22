using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Classes.GameObjectFolder;

namespace Soulmate_Remastered.Classes.HUDFolder
{
    class LifeBarForOthers
    {
        Texture lifeTextureBackground = new Texture("Pictures/Life/BarBackground.png");
        Texture lifeTextureBar = new Texture("Pictures/Life/LifeBar.png");
        Sprite lifeSpriteBackground;
        Sprite lifeSpriteBar;

        public LifeBarForOthers()
        {
            lifeSpriteBackground = new Sprite(lifeTextureBackground);
            lifeSpriteBar = new Sprite(lifeTextureBar);
        }

        public Sprite scale(GameObject gameObject)
        {
            lifeSpriteBar.Scale = new Vector2f((float)gameObject.getCurrentHP() / (float)gameObject.getMaxHP(), 1);

            return lifeSpriteBar;
        }

        public void setPosition(GameObject gameObject)
        {
            if (!gameObject.TYPE.Equals("player"))
            {
                if (gameObject != null)
                {
                    lifeSpriteBackground.Position = new Vector2f((gameObject.position.X + gameObject.sprite.Texture.Size.X / 2) - lifeTextureBackground.Size.X / 2, gameObject.position.Y - 20);
                    lifeSpriteBar.Position = new Vector2f(lifeSpriteBackground.Position.X + 2, lifeSpriteBackground.Position.Y + 2);
                }
            }
        }

        public void update(GameObject gameObject)
        {
            setPosition(gameObject);
            scale(gameObject);
        }

        public void draw(RenderWindow window)
        {
            window.Draw(lifeSpriteBackground);
            window.Draw(lifeSpriteBar);
        }
    }
}

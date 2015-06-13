using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Classes.GameObjectFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder;

namespace Soulmate_Remastered.Classes.HUDFolder
{
    class LifeBarForOthers
    {
        Texture lifeTextureBackground = new Texture("Pictures/Bars/BarBackgroundHead.png");
        Texture lifeTextureBar = new Texture("Pictures/Bars/LifeBarHead.png");
        Sprite lifeSpriteBackground;
        Sprite lifeSpriteBar;

        public LifeBarForOthers()
        {
            lifeSpriteBackground = new Sprite(lifeTextureBackground);
            lifeSpriteBar = new Sprite(lifeTextureBar);
        }

        public Sprite scale(Entity entity)
        {
            lifeSpriteBar.Scale = new Vector2f((float)entity.CurrentHP / (float)entity.MaxHP, 1);

            return lifeSpriteBar;
        }

        public void setPosition(Entity entity)
        {
            //if (!entity.type.Split('.')[2].Equals("Player"))
            {
                if (entity != null)
                {
                    lifeSpriteBackground.Position = new Vector2f((entity.position.X + entity.sprite.Texture.Size.X / 2) - lifeTextureBackground.Size.X / 2, entity.position.Y - 20);
                    lifeSpriteBar.Position = new Vector2f(lifeSpriteBackground.Position.X + 2, lifeSpriteBackground.Position.Y + 2);
                }
            }
        }

        public void update(Entity entity)
        {
            setPosition(entity);
            scale(entity);
        }

        public void draw(RenderWindow window)
        {
            window.Draw(lifeSpriteBackground);
            window.Draw(lifeSpriteBar);
        }
    }
}

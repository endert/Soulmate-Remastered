using SFML.Graphics;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder;
using Soulmate_Remastered.Core;

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

        public Sprite Scale(Entity entity)
        {
            lifeSpriteBar.Scale = new Vector2((float)entity.CurrentHP / (float)entity.MaxHP, 1);

            return lifeSpriteBar;
        }

        public void SetPosition(Entity entity)
        {
            //if (!entity.type.Split('.')[2].Equals("Player"))
            {
                if (entity != null)
                {
                    lifeSpriteBackground.Position = new Vector2((entity.Position.X + entity.Sprite.Texture.Size.X / 2) - lifeTextureBackground.Size.X / 2, entity.Position.Y - 20);
                    lifeSpriteBar.Position = new Vector2(lifeSpriteBackground.Position.X + 2, lifeSpriteBackground.Position.Y + 2);
                }
            }
        }

        public void Update(Entity entity)
        {
            SetPosition(entity);
            Scale(entity);
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(lifeSpriteBackground);
            window.Draw(lifeSpriteBar);
        }
    }
}

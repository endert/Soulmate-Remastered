using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.ProjectileFolder
{
    class ProjectileArrow : AbstractProjectile
    {
        public override String Type { get { return base.Type + ".Arrow"; } }
        
        public ProjectileArrow(float _att, float _movementSpeed, float _duration, Vector2f _facingDirection, Vector2f _position)
        {
            TextureList.Add(new Texture("Pictures/Projectile/Arrow/ArrowBottom.png"));
            TextureList.Add(new Texture("Pictures/Projectile/Arrow/ArrowTop.png"));
            TextureList.Add(new Texture("Pictures/Projectile/Arrow/ArrowRight.png"));
            TextureList.Add(new Texture("Pictures/Projectile/Arrow/ArrowLeft.png"));

            Att = _att;
            BaseMovementSpeed = _movementSpeed;
            duration = _duration;
            FacingDirection = _facingDirection;
            Sprite = new Sprite(TextureList[(int)Direction]);
  
            if (FacingDirection.X > 0) //right
            {
                Position = new Vector2f(_position.X + PlayerHandler.player.HitBox.width + 10, _position.Y + PlayerHandler.player.HitBox.height * 5 / 6);
            }

            else if(FacingDirection.X < 0) //left
            {
                Position = new Vector2f(_position.X - PlayerHandler.player.HitBox.width - Sprite.Texture.Size.X, _position.Y + PlayerHandler.player.HitBox.height * 5 / 6);
            }

            else if (FacingDirection.Y < 0) //up
            {
                Position = new Vector2f(_position.X + PlayerHandler.player.HitBox.width / 2, _position.Y - 20);
            }

            else if(FacingDirection.Y > 0) //down
            {
                Position = new Vector2f(_position.X + PlayerHandler.player.HitBox.width / 2, _position.Y + PlayerHandler.player.HitBox.height * 5 / 6 + 10);
            }

            Sprite.Position = Position;
            startPosition = Position;
            IsAlive = true;
            HitBox = new HitBox(Position, (float)Sprite.Texture.Size.X, (float)Sprite.Texture.Size.Y);

            ProjectileHandler.add(this);
        }        
    }
}

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
        public override String type { get { return base.type + ".Arrow"; } }
        
        public ProjectileArrow(float _att, float _movementSpeed, float _duration, Vector2f _facingDirection, Vector2f _position)
        {
            textureList.Add(new Texture("Pictures/Projectile/Arrow/ArrowBottom.png"));
            textureList.Add(new Texture("Pictures/Projectile/Arrow/ArrowTop.png"));
            textureList.Add(new Texture("Pictures/Projectile/Arrow/ArrowRight.png"));
            textureList.Add(new Texture("Pictures/Projectile/Arrow/ArrowLeft.png"));

            att = _att;
            BaseMovementSpeed = _movementSpeed;
            duration = _duration;
            facingDirection = _facingDirection;
            sprite = new Sprite(textureList[getNumFacingDirection]);
  
            if (facingDirection.X > 0) //right
            {
                position = new Vector2f(_position.X + PlayerHandler.player.hitBox.width + 10, _position.Y + PlayerHandler.player.hitBox.height * 5 / 6);
            }

            else if(facingDirection.X < 0) //left
            {
                position = new Vector2f(_position.X - PlayerHandler.player.hitBox.width - sprite.Texture.Size.X, _position.Y + PlayerHandler.player.hitBox.height * 5 / 6);
            }

            else if (facingDirection.Y < 0) //up
            {
                position = new Vector2f(_position.X + PlayerHandler.player.hitBox.width / 2, _position.Y - 20);
            }

            else if(facingDirection.Y > 0) //down
            {
                position = new Vector2f(_position.X + PlayerHandler.player.hitBox.width / 2, _position.Y + PlayerHandler.player.hitBox.height * 5 / 6 + 10);
            }

            sprite.Position = position;
            startPosition = position;
            isAlive = true;
            hitBox = new HitBox(this);

            ProjectileHandler.add(this);
        }        
    }
}

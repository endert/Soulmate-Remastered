using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.ProjectileFolder
{
    class ProjectileArrow : AbstractProjectile
    {
        /// <summary>
        /// the type of this instance
        /// </summary>
        public override String Type { get { return base.Type + ".Arrow"; } }
        
        public ProjectileArrow(Entity _owner, float _movementSpeed = 0.8f, float _duration = 2f) : base()
        {
            FacingDirection = _owner.FacingDirection; //important for the game object initialization

            //initialize game object********************************************************************************

            TextureList.Add(new Texture("Pictures/Projectile/Arrow/ArrowBottom.png"));
            TextureList.Add(new Texture("Pictures/Projectile/Arrow/ArrowTop.png"));
            TextureList.Add(new Texture("Pictures/Projectile/Arrow/ArrowRight.png"));
            TextureList.Add(new Texture("Pictures/Projectile/Arrow/ArrowLeft.png"));

            Sprite = new Sprite(TextureList[(int)Direction]);
            switch (Direction)
            {
                case EDirection.Right:
                    Position = _owner.Position + _owner.HitBox.SizeX + _owner.HitBox.SizeY / 2 + Vector2.OFFSETŖ;
                    break;
                case EDirection.Left:
                    Position = _owner.Position + _owner.HitBox.SizeY / 2 + Vector2.OFFSETL - new Vector2(Sprite.Texture.Size.X, Sprite.Texture.Size.Y / 2);
                    break;
                case EDirection.Back:
                    Position = _owner.Position + _owner.HitBox.SizeX / 2 + Vector2.OFFSETB - new Vector2(Sprite.Texture.Size.X / 2, Sprite.Texture.Size.Y);
                    break;
                case EDirection.Front:
                    Position = _owner.Position + _owner.HitBox.SizeX / 2 + _owner.HitBox.SizeY + Vector2.OFFSETF;
                    break;
            }
            Sprite.Position = Position;
            HitBox = new HitBox(Position, (float)Sprite.Texture.Size.X, (float)Sprite.Texture.Size.Y);
            
            //******************************************************************************************************
            
            //initialize entity*************************************************************************************

            Att = (_owner.Att / 2) + 2;
            BaseMovementSpeed = _movementSpeed;
            
            //******************************************************************************************************
            
            //initialize abstract arrow*****************************************************************************

            duration = _duration;
            owner = _owner;
            startPosition = Position;
            ProjectileHandler.Add(this);
            
            //******************************************************************************************************
        }        
    }
}

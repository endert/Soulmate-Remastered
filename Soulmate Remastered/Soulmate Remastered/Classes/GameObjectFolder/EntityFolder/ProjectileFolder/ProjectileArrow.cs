﻿using SFML.Graphics;
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
            textureList.Add(new Texture("Pictures/Projectile/Error!!!Top.png"));
            textureList.Add(new Texture("Pictures/Projectile/Error!!!Bottom.png"));
            textureList.Add(new Texture("Pictures/Projectile/Error!!!Right.png"));
            textureList.Add(new Texture("Pictures/Projectile/Error!!!Left.png"));
            
            att = _att;
            movementSpeedConstant = _movementSpeed;
            duration = _duration;
            facingDirection = _facingDirection;
            sprite = new Sprite(textureList[0]);
            position = _position;
            sprite.Position = position;
            startPosition = _position;
            isAlive = true;
            hitBox = new HitBox(sprite.Position, textureList[0].Size.X, textureList[0].Size.Y);

            ProjectileHandler.add(this);
        }        
    }
}

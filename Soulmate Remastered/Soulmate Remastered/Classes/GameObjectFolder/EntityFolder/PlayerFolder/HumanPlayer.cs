using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder
{
    class HumanPlayer : AbstractPlayer
    {
        public String type { get { return base.type + ".HumanPlayer"; } }

        public HumanPlayer(Vector2f spawnPosition, int spawnFacingDirection)
        {
            textureList.Add( new Texture("Pictures/Player/SpielerFront.png")); 
            textureList.Add(new Texture("Pictures/Player/SpielerRueck.png"));
            textureList.Add(new Texture("Pictures/Player/SpielerSeiteRechtsSchwert.png"));
            textureList.Add(new Texture("Pictures/Player/SpielerSeiteLinksSchwert.png"));
            
            numFacingDirection = spawnFacingDirection;
            facingDirection = new Vector2f(1, 0); // RECHTS
            sprite = new Sprite(textureList[0]);
            sprite.Position = spawnPosition;
            position = spawnPosition;
            hitBox = new HitBox(sprite.Position, textureList[0].Size.X, textureList[0].Size.Y);

            maxHP = 100f;
            currentHP = maxHP;
            att = 10;
            def = 5;
            fusionDuration = 50;
            maxFusionValue = 500f;
            currentFusionValue = 0f;
        }
    }
}

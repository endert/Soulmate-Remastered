using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Classes.HUDFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PetFolder
{
    class PetWolf : AbstractPet
    {
        public String type { get { return base.type + ".PetWolf"; } }
        public PetWolf(AbstractPlayer player)
        {
            textureList.Add(new Texture("Pictures/Player/WolfFront.png"));
            textureList.Add(new Texture("Pictures/Player/WolfRueck.png"));
            textureList.Add(new Texture("Pictures/Player/WolfSeiteRechts.png"));
            textureList.Add(new Texture("Pictures/Player/WolfSeiteLinks.png"));

            sprite = new Sprite(textureList[0]);
            sprite.Position = new Vector2f(player.position.X - 150, player.position.Y + player.hitBox.height - textureList[0].Size.Y);
            position = sprite.Position;
            hitBox = new HitBox(sprite.Position, textureList[0].Size.X, textureList[0].Size.Y);
            lifeBar = new LifeBarForOthers();

            maxHP = 50f;
            currentHP = maxHP;
        }

    }
}

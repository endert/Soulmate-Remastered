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
        public override String type { get { return base.type + ".PetWolf"; } }
        public PetWolf(AbstractPlayer player)
        {
            textureList.Add(new Texture("Pictures/Pet/WolfFront.png"));
            textureList.Add(new Texture("Pictures/Pet/WolfRueck.png"));
            textureList.Add(new Texture("Pictures/Pet/WolfSeiteRechts.png"));
            textureList.Add(new Texture("Pictures/Pet/WolfSeiteLinks.png"));

            sprite = new Sprite(textureList[0]);
            position = new Vector2f(player.position.X - 150, player.position.Y + player.hitBox.height - textureList[0].Size.Y);
            sprite.Position = position;
            hitBox = new HitBox(sprite.Position, textureList[0].Size.X, textureList[0].Size.Y);
            lifeBar = new LifeBarForOthers();

            maxHP = 50f;
            currentHP = maxHP;
        }

    }
}

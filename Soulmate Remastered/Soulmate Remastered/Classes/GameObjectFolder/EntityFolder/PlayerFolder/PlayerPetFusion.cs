using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PetFolder;
using System.Diagnostics;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder
{
    class PlayerPetFusion : AbstractPlayer
    {
        AbstractPlayer fusionedPlayer;
        AbstractPet fusionedPet;
        
        public override String type { get { return base.type + ".PlayerPetFusion"; } } 
        
        public PlayerPetFusion(AbstractPlayer player, AbstractPet pet)
        {
            textureList.Add(new Texture("Pictures/Player/PlayerWerewolf/WerwolfFront.png"));
            textureList.Add(new Texture("Pictures/Player/PlayerWerewolf/WerwolfRueck.png"));
            textureList.Add(new Texture("Pictures/Player/PlayerWerewolf/WerwolfSeiteRechts.png"));
            textureList.Add(new Texture("Pictures/Player/PlayerWerewolf/WerwolfSeiteLinks.png"));

            fusionedPlayer = player;
            fusionedPet = pet;

            facingDirection = player.getFacingDirection; // RECHTS
            sprite = new Sprite(textureList[player.getNumFacingDirection]);
            sprite.Position = player.position;
            position = player.position;
            currentFusionValue = player.currentFusionValue;
            hitBox = PlayerHandler.player.hitBox;

            maxHP = player.getMaxHP;
            currentHP = player.getCurrentHP;
            att = player.getAtt;
            def = player.getDef;

            PlayerHandler.player = this;
            EntityHandler.add(this);
            EntityHandler.deleateType("Player");
            EntityHandler.deleateType("Pet");

            stopWatchList[2].Start();
        }
    }
}

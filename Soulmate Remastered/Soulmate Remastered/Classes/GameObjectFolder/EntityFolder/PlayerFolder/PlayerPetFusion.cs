using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PetFolder;
using System.Diagnostics;
using SFML.Window;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder
{
    class PlayerPetFusion : AbstractPlayer
    {
        List<Texture> fusionAnimationList = new List<Texture>();

        AbstractPlayer fusionedPlayer;
        AbstractPet fusionedPet;

        bool animatingFusion;
        
        public override String type { get { return base.type + ".PlayerPetFusion"; } } 
        
        public PlayerPetFusion(AbstractPlayer player, AbstractPet pet)
        {
            textureSetting(pet);

            transforming = true;
            vulnerable = false;

            fusionedPlayer = player;
            fusionedPet = pet;

            adapt(player);

            PlayerHandler.player = this;
            EntityHandler.deleateType(player.type);
            EntityHandler.deleateType(pet.type);
            EntityHandler.add(this);

            stopWatchList[2].Start();
            statsUpdate();
            animatingFusion = true;
        }

        public void textureSetting(AbstractPet pet)
        {
            if (pet.type.Split('.')[3].Equals("PetWolf"))
            {
                textureList.Add(new Texture("Pictures/Player/PlayerWerewolf/WerwolfFront.png"));
                textureList.Add(new Texture("Pictures/Player/PlayerWerewolf/WerwolfRueck.png"));
                textureList.Add(new Texture("Pictures/Player/PlayerWerewolf/WerwolfSeiteRechts.png"));
                textureList.Add(new Texture("Pictures/Player/PlayerWerewolf/WerwolfSeiteLinks.png"));

                fusionAnimationList.Add(new Texture("Pictures/Player/PlayerWerewolf/VerwandlungWerwolf1.png"));
                fusionAnimationList.Add(new Texture("Pictures/Player/PlayerWerewolf/VerwandlungWerwolf2.png"));
            }
        }

        public void animateFusion()
        {
            stopWatchList[0].Start();
            if (stopWatchList[0].ElapsedMilliseconds <= 500)
            {
                sprite = new Sprite(fusionAnimationList[0]);
                sprite.Position = position;
            }
            else if(stopWatchList[0].ElapsedMilliseconds <= 1000)
            {
                sprite = new Sprite(fusionAnimationList[1]);
                sprite.Position = position;
            }
            else
            {
                stopWatchList[0].Reset();
                animatingFusion = false;
            }
        }

        public void defuse()
        {
            currentFusionValue = maxFusionValue - stopWatchList[2].ElapsedMilliseconds / 100;
            if (stopWatchList[2].ElapsedMilliseconds >= fusionedPlayer.getFusionDuration || Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                fusionedPlayer.adapt(this);
                fusionedPet.position = position;

                PlayerHandler.player = fusionedPlayer;
                PetHandler.pet = fusionedPet;
                EntityHandler.add(fusionedPlayer);
                EntityHandler.add(fusionedPet);
                EntityHandler.deleateType(type);
            }
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
            hitBox.update(sprite);
            if (animatingFusion)
            {
                animateFusion();
            }
            else
            {
                animating = false;
                transforming = false;
                vulnerable = true;
            }
            defuse();
        }
    }
}

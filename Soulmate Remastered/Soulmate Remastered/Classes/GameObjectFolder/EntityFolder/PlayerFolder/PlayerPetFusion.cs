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
            hitBox = new HitBox(position, textureList[0].Size.X, textureList[0].Size.Y);

            PlayerHandler.player = this;
            EntityHandler.deleateType(player.type);
            EntityHandler.deleateType(pet.type);
            EntityHandler.add(this);

            statsUpdate();
            animatingFusion = true;
        }

        public void textureSetting(AbstractPet pet)
        {
            if (pet.type.Split('.')[3].Equals("PetWolf"))
            {
                textureList.Add(new Texture("Pictures/Entities/Player/Fusion/Werewolf/WerewolfFront.png"));
                textureList.Add(new Texture("Pictures/Entities/Player/Fusion/Werewolf/WerewolfBack.png"));
                textureList.Add(new Texture("Pictures/Entities/Player/Fusion/Werewolf/WerewolfRight.png"));
                textureList.Add(new Texture("Pictures/Entities/Player/Fusion/Werewolf/WerewolfLeft.png"));

                fusionAnimationList.Add(new Texture("Pictures/Entities/Player/Fusion/Werewolf/FusionWerewolf1.png"));
                fusionAnimationList.Add(new Texture("Pictures/Entities/Player/Fusion/Werewolf/FusionWerewolf2.png"));
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
            if (stopWatchList[2].ElapsedMilliseconds >= fusionedPlayer.fusionDuration || Keyboard.IsKeyPressed(Keyboard.Key.Space))
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
            if (!transforming)
            {
                stopWatchList[2].Start();
                defuse();
            }
        }
    }
}

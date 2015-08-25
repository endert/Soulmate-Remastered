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
        
        public override String Type { get { return base.Type + ".PlayerPetFusion"; } } 
        
        public PlayerPetFusion(AbstractPlayer player, AbstractPet pet)
        {
            textureSetting(pet);

            transforming = true;
            vulnerable = false;

            fusionedPlayer = player;
            fusionedPet = pet;

            IsAlive = true;
            IsVisible = true;


            Adapt(player);
            HitBox = new HitBox(Position, TextureList[0].Size.X, TextureList[0].Size.Y);

            PlayerHandler.player = this;
            EntityHandler.deleateType(player.Type);
            EntityHandler.deleateType(pet.Type);
            EntityHandler.add(this);

            StatsUpdate();
            animatingFusion = true;
        }

        public void textureSetting(AbstractPet pet)
        {
            if (pet.Type.Split('.')[3].Equals("PetWolf"))
            {
                TextureList.Add(new Texture("Pictures/Entities/Player/Fusion/Werewolf/WerewolfFront.png"));
                TextureList.Add(new Texture("Pictures/Entities/Player/Fusion/Werewolf/WerewolfBack.png"));
                TextureList.Add(new Texture("Pictures/Entities/Player/Fusion/Werewolf/WerewolfRight.png"));
                TextureList.Add(new Texture("Pictures/Entities/Player/Fusion/Werewolf/WerewolfLeft.png"));

                fusionAnimationList.Add(new Texture("Pictures/Entities/Player/Fusion/Werewolf/FusionWerewolf1.png"));
                fusionAnimationList.Add(new Texture("Pictures/Entities/Player/Fusion/Werewolf/FusionWerewolf2.png"));
            }
        }

        public void animateFusion()
        {
            stopWatchList[0].Start();
            if (stopWatchList[0].ElapsedMilliseconds <= 500)
            {
                Sprite = new Sprite(fusionAnimationList[0]);
                Sprite.Position = Position;
            }
            else if(stopWatchList[0].ElapsedMilliseconds <= 1000)
            {
                Sprite = new Sprite(fusionAnimationList[1]);
                Sprite.Position = Position;
            }
            else
            {
                stopWatchList[0].Reset();
                animatingFusion = false;
            }
        }

        public void defuse()
        {
            CurrentFusionValue = MaxFusionValue - stopWatchList[2].ElapsedMilliseconds / 100;
            if (stopWatchList[2].ElapsedMilliseconds >= fusionedPlayer.FusionDuration || Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                fusionedPlayer.Adapt(this);
                fusionedPet.Position = Position;

                PlayerHandler.player = fusionedPlayer;
                PetHandler.Pet = fusionedPet;
                EntityHandler.add(fusionedPlayer);
                EntityHandler.add(fusionedPet);
                EntityHandler.deleateType(Type);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            HitBox.update(Sprite);
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

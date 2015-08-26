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
    /// <summary>
    /// the fusion of player and pet
    /// </summary>
    class PlayerPetFusion : AbstractPlayer
    {
        /// <summary>
        /// the fusion animation
        /// </summary>
        List<Texture> fusionAnimationList = new List<Texture>();
        /// <summary>
        /// the fusioned player
        /// </summary>
        AbstractPlayer fusionedPlayer;
        /// <summary>
        /// the fusioned pet
        /// </summary>
        AbstractPet fusionedPet;
        /// <summary>
        /// bool if the fusion is animated at the moment
        /// </summary>
        bool animatingFusion;
        
        /// <summary>
        /// the type of this instance
        /// </summary>
        public override String Type { get { return base.Type + ".PlayerPetFusion"; } } 
        
        /// <summary>
        /// "fuse" the pet and the player
        /// </summary>
        /// <param name="player"></param>
        /// <param name="pet"></param>
        public PlayerPetFusion(AbstractPlayer player, AbstractPet pet)
        {
            TextureSetting(pet);

            transforming = true;
            vulnerable = false;

            fusionedPlayer = player;
            fusionedPet = pet;

            IsAlive = true;
            IsVisible = true;


            Adapt(player);
            HitBox = new HitBox(Position, TextureList[0].Size.X, TextureList[0].Size.Y);

            PlayerHandler.Player = this;
            EntityHandler.deleateType(player.Type);
            EntityHandler.deleateType(pet.Type);
            EntityHandler.add(this);

            StatsUpdate();
            animatingFusion = true;
        }

        /// <summary>
        /// desides wich textures should be used for the fusion
        /// </summary>
        /// <param name="pet"></param>
        public void TextureSetting(AbstractPet pet)
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

        /// <summary>
        /// animate the fusion
        /// </summary>
        public void AnimateFusion()
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

        /// <summary>
        /// splits the fusion into player and pet
        /// </summary>
        public void Defuse()
        {
            CurrentFusionValue = MaxFusionValue - stopWatchList[2].ElapsedMilliseconds / 100;
            if (stopWatchList[2].ElapsedMilliseconds >= fusionedPlayer.FusionDuration || Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                fusionedPlayer.Adapt(this);
                fusionedPet.Position = Position;

                PlayerHandler.Player = fusionedPlayer;
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
                AnimateFusion();
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
                Defuse();
            }
        }
    }
}

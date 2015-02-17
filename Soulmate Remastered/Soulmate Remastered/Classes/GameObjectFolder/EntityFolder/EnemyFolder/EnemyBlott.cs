﻿using SFML.Graphics;
using SFML.Window;
using Soulmate_Remastered.Classes.HUDFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.MoneyFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.NormalItemFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.PotionFolder;
using Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.EquipmentFolder;

namespace Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.EnemyFolder
{
    class EnemyBlott : AbstractEnemy
    {
        public override String type { get { return base.type + ".EnemyBlott"; } }
        
        public EnemyBlott(Vector2f spawnPos)
        {
            textureList.Add(new Texture("Pictures/Entities/Enemy/Blott/Vulnerable/BlottFront.png"));
            textureList.Add(new Texture("Pictures/Entities/Enemy/Blott/Vulnerable/BlottBack.png"));
            textureList.Add(new Texture("Pictures/Entities/Enemy/Blott/Vulnerable/BlottRight.png"));
            textureList.Add(new Texture("Pictures/Entities/Enemy/Blott/Vulnerable/BlottLeft.png"));
            textureList.Add(new Texture("Pictures/Entities/Enemy/Blott/Invulnerable/BlottFrontInvulnerable.png"));

            drops = new AbstractItem[] { new Gold(), new TestItem(), new HealPotion(HealPotion.healPotionSize.small), new Sword((float)(50 * random.NextDouble()), (float)(50 * random.NextDouble()), (float)(50 * random.NextDouble())) };

            sprite = new Sprite(textureList[0]);
            position = spawnPos;
            sprite.Position = position;
            isAlive = true;
            hitBox = new HitBox(sprite.Position, sprite.Texture.Size.X, sprite.Texture.Size.Y);
            
            maxHP = 100;
            currentHP = maxHP;
            att = 6;
            def = 5;
            aggroRange = 150f;
            knockBack = 50f;
            lifeBar = new LifeBarForOthers();
        }

        public override void react()
        {
            if (!touchedPlayer())
                move(getPlayerDirection());
            else
            {
                move(new Vector2f(-getPlayerDirection().X, -getPlayerDirection().Y));
                facingDirection = getPlayerDirection();
            }
        }

        public override void notReact()
        {
            moveRandom();
        }
    }
}

using SFML.Window;
using Soulmate_Remastered.Classes.GameObjectFolder.EntityFolder.PlayerFolder;
using Soulmate_Remastered.Classes.ItemFolder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder
{
    abstract class AbstractItem : GameObject, IComparable<AbstractItem>
    {
        public override String type { get { return base.type + ".Item"; } }
        public virtual float ID { get { return 0; } }
        public override bool walkable { get { return true; } }
        protected int dropRate; // in percent
        public int DROPRATE { get { return dropRate; } }
        public bool onMap { get; set; }
        protected bool wasOnMap = false; //not longer on map but it was droped once
        public float pickUpRange { get { return 50f; } }
        protected Stopwatch decay = new Stopwatch();
        protected int decayingIn = 60000; //60sec

        public virtual String toStringForSave()
        {
            String itemForSave = "it" + lineBreak.ToString();

            itemForSave += type.Split('.')[type.Split('.').Length-1] + lineBreak.ToString();
            itemForSave += position.X + lineBreak.ToString();
            itemForSave += position.Y + lineBreak.ToString();

            return itemForSave;
        }

        public void setPositionMatrix(int x, int y)
        {
            position = new Vector2f(x * ItemHandler.playerInventory.FIELDSIZE + 1 + ItemHandler.playerInventory.inventoryMatrixPosition.X,
                y * ItemHandler.playerInventory.FIELDSIZE + 1 + ItemHandler.playerInventory.inventoryMatrixPosition.Y);
        }

        public virtual void pickUp(GameTime gameTime)
        {
            if (type.Equals("Object.Item.Gold"))
            {
                PlayerHandler.player.gold += 1;
                return;
            }
            for (int i = 0; i < ItemHandler.playerInventory.inventoryMatrix.GetLength(0); i++) //row -> x-coordinate
            {
                for (int j = 0; j < ItemHandler.playerInventory.inventoryMatrix.GetLength(1); j++) //collum -> y-coordinate
                {
                    if (ItemHandler.playerInventory.inventoryMatrix[i,j] == null)
                    {
                        ItemHandler.playerInventory.inventoryMatrix[i, j] = new Stack<AbstractItem>();
                        ItemHandler.playerInventory.inventoryMatrix[i, j].Push(this);
                        position = new Vector2f((j * ItemHandler.playerInventory.FIELDSIZE + 1 + ItemHandler.playerInventory.inventoryMatrixPosition.X),
                                                (i * ItemHandler.playerInventory.FIELDSIZE + 1 + ItemHandler.playerInventory.inventoryMatrixPosition.Y));
                        onMap = false;
                        GameObjectHandler.removeAt(indexObjectList);
                        return;

                    }
                    if (ItemHandler.playerInventory.inventoryMatrix[i, j].Count < Inventory.MaxStackCount && 
                       (ItemHandler.playerInventory.inventoryMatrix[i, j].Peek() == null || ItemHandler.playerInventory.inventoryMatrix[i, j].Peek().CompareTo(this) == 0))
                    {
                        try
                        {
                            ItemHandler.playerInventory.inventoryMatrix[i, j].Peek().setVisible(false);
                        }
                        catch (Exception)
                        {
                        }
                        ItemHandler.playerInventory.inventoryMatrix[i, j].Push(this);
                        position = new Vector2f((j * ItemHandler.playerInventory.FIELDSIZE + 1 + ItemHandler.playerInventory.inventoryMatrixPosition.X),
                            (i * ItemHandler.playerInventory.FIELDSIZE + 1 + ItemHandler.playerInventory.inventoryMatrixPosition.Y));
                        onMap = false;
                        GameObjectHandler.removeAt(indexObjectList);
                        return;
                    }
                }
            }
        }

        public virtual void use()
        {

        } 

        public void drop(Vector2f dropPosition)
        {
            position = dropPosition;
            sprite.Position = position;
            onMap = true;
        }

        public void setVisible(bool _visible)
        {
            visible = _visible;
        }

        public abstract void cloneAndDrop(Vector2f dropPosition);

        public override void update(GameTime gameTime)
        {
            hitBox.Position = position;
            sprite.Position = position;
            if (onMap)
            {
                visible = true;
                wasOnMap = true;
                decay.Start();
                if (decay.ElapsedMilliseconds >= decayingIn)
                {
                    isAlive = false;
                }
            }
            if (wasOnMap && !onMap)
            {
                wasOnMap = false;
                onMap = false;
                decay.Reset();
            }
            if (!onMap)
            {
                visible = false;
            }
        }

        public int CompareTo(AbstractItem other)
        {
            return (int)(ID - other.ID);
        }
    }
}

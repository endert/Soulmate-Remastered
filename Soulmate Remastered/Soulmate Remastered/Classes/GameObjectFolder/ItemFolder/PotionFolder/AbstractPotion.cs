namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.PotionFolder
{
    /// <summary>
    /// base class for all potions
    /// </summary>
    abstract class AbstractPotion : AbstractItem
    {
        /// <summary>
        /// the ID 12x
        /// </summary>
        public override float ID
        {
            get
            {
                return base.ID * 10 + 2;
            }
        }
        /// <summary>
        /// indicates what size the potion has
        /// </summary>
        public PotionSize Size { get;protected set; }

        /// <summary>
        /// the amount this potion fills the value it is for
        /// </summary>
        protected float recoveryValue;

        /// <summary>
        /// the potion size
        /// <para>-1 = UNDEF</para>
        /// <para>0 = small</para>
        /// <para>1 = middle</para>
        /// <para>2 = large</para>
        /// </summary>
        public enum PotionSize
        {
            UNDEF = -1,
            small,
            middle,
            large,


            SizeCount
        }

        /// <summary>
        /// creates a string out of the current attributes, to save them
        /// </summary>
        /// <returns></returns>
        public override string ToStringForSave()
        {
            return base.ToStringForSave() + (int)Size + LineBreak.ToString();
        }
    }
}

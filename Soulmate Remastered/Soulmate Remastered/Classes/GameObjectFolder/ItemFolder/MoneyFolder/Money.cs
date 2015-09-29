namespace Soulmate_Remastered.Classes.GameObjectFolder.ItemFolder.MoneyFolder
{
    /// <summary>
    /// the base class for all currencies
    /// </summary>
    abstract class Currency : AbstractItem
    {
        /// <summary>
        /// the ID 10x
        /// </summary>
        public override float ID
        {
            get
            {
                return base.ID* 10 + 0;
            }
        }
    }
}

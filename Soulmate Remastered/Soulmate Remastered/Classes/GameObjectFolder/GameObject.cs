using SFML.Graphics;
using Soulmate_Remastered.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder
{
    abstract class GameObject : IComparable<GameObject>
    {
        //Events*************************************************************************************

        //Constants**********************************************************************************

        /// <summary>
        /// for save purpose
        /// </summary>
        public static char LineBreak { get { return ';'; } }
        /// <summary>
        /// bool, if this is passable
        /// </summary>
        public virtual bool Walkable { get { return false; } }

        //Properties*********************************************************************************

        /// <summary>
        /// for sorting the List, the highest y coordinate
        /// </summary>
        public float YCoordinate { get { return Position.Y + Sprite.Texture.Size.Y; } }
        /// <summary>
        /// the curren
        /// </summary>
        public Texture CurrentTexture { get { return Sprite.Texture; } }
        /// <summary>
        /// the "Name" of this
        /// </summary>
        public string Name { get { if (!CustomName.Equals("")) return CustomName; else return GetType().Name; } }

        /// <summary>
        /// the HitBox
        /// </summary>
        public HitBox HitBox { get; set; }
        /// <summary>
        /// the sprite
        /// </summary>
        public Sprite Sprite { get; set; }
        /// <summary>
        /// the position in world coordinates
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// bool if this is alive
        /// </summary>
        public bool IsAlive { get; protected set; }
        /// <summary>
        /// the index in the object list
        /// </summary>
        public int IndexObjectList { get; set; }
        /// <summary>
        /// bool if this is visible, there for if this must be drawn
        /// </summary>
        public bool IsVisible { get; protected set; }

        //Attributes*********************************************************************************

        /// <summary>
        /// the textures
        /// </summary>
        protected List<Texture> TextureList = new List<Texture>();
        /// <summary>
        /// the custom name
        /// </summary>
        protected string CustomName = "";

        //Methodes***********************************************************************************

        /// <summary>
        /// abstract constructors are allways called when a subclass is initialized, good for standarts
        /// </summary>
        public GameObject()
        {
            AddEvents();

            LoadTextures();
            IsAlive = true;
            IsVisible = true;
            Sprite = new Sprite(TextureList[0]);
            HitBox = new HitBox(Sprite.Position, TextureList[0].Size.X, TextureList[0].Size.Y);
        }

        void DebugInitializedOutput() { Console.WriteLine(GetType() + " is initialized"); }

        /// <summary>
        /// the things that happen when a key is pressed
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnKeyPress(object sender, KeyEventArgs e)
        {
            //DebugInitializedOutput();
        }
        /// <summary>
        /// the things that happen when a key is released
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnKeyRelease(object sender, KeyEventArgs e) { }
        /// <summary>
        /// the things that happen when a mousebutton is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnMouseButtonPressed(object sender, MouseButtonEventArgs e) { }
        /// <summary>
        /// the things that happen when a Mousebutton is released
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnMouseButtonRelease(object sender, MouseButtonEventArgs e) { }

        /// <summary>
        /// Adds the ControlMethods to ControlEvents, to gain control
        /// </summary>
        protected void AddEvents()//even if no reference is found, it is called via reflection so still important
        {
            KeyboardControler.KeyPressed += OnKeyPress;
            KeyboardControler.KeyReleased += OnKeyRelease;
            MouseControler.ButtonPressed += OnMouseButtonPressed;
            MouseControler.Relessed += OnMouseButtonRelease;
        }

        /// <summary>
        /// Substract the ControlMethods from the events, to denie further control
        /// </summary>
        protected void SubstractEvents()
        {
            KeyboardControler.KeyPressed -= OnKeyPress;
            KeyboardControler.KeyReleased -= OnKeyRelease;
            MouseControler.ButtonPressed -= OnMouseButtonPressed;
            MouseControler.Relessed -= OnMouseButtonRelease;
        }

        public virtual string ToStringForSave()
        {
            return GetType().FullName;
        }

        /// <summary>
        /// Loads the textures
        /// </summary>
        protected abstract void LoadTextures();

        /// <summary>
        /// for sorting the GameObject List
        /// </summary>
        public int CompareTo(GameObject gameObject)
        {
            return (int)(this.YCoordinate - gameObject.YCoordinate);
        }

        /// <summary>
        /// "kills" this by setting isAlive to false
        /// </summary>
        public void Kill()
        {
            IsAlive = false;
        }

        /// <summary>
        /// updates this
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// draws this
        /// </summary>
        /// <param name="window"></param>
        public virtual void Draw(RenderWindow window)
        {
            window.Draw(Sprite);
        }

        /// <summary>
        /// draws the HitBox
        /// </summary>
        /// <param name="window"></param>
        public virtual void DebugDraw(RenderWindow window)
        {
            HitBox.Draw(window);
        }
    }
}

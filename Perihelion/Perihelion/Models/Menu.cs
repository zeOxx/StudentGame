using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Perihelion.Models
{
    class Menu
    {
        // DECLARATIONS
        protected List<string> items;
        protected Vector2 titlePosition;
        protected bool active;
        protected int spaceBetweenItems;
        protected int itemSelected;
        protected int screenWidth;
        protected int screenHeight;
        protected int titlePadding;
        protected Texture2D title;
        protected SpriteFont itemFont;

        // CONSTRUCTOR
        public Menu(ContentHolder content, int width, int height)
        {
            ScreenWidth = width;
            ScreenHeight = height;

            Items = new List<string>();

            Active = false;

            Title = content.title;
            TitlePadding = 150;
            TitlePosition = new Vector2((ScreenWidth / 2 - Title.Width / 2), TitlePadding - (Title.Height / 2));

            ItemFont = content.menuFont;

            SpaceBetweenItems = 50;
        }

        // ACCESSORS
        protected List<string> Items
        {
            get { return this.items; }
            set { this.items = value; }
        }

        protected Vector2 TitlePosition
        {
            get { return this.titlePosition; }
            set { this.titlePosition = value; }
        }

        public bool Active
        {
            get { return this.active; }
            set { this.active = value; }
        }

        protected int SpaceBetweenItems
        {
            get { return this.spaceBetweenItems; }
            set { this.spaceBetweenItems = value; }
        }

        protected int ItemSelected
        {
            get { return this.itemSelected; }
            set { this.itemSelected = value; }
        }

        protected int ScreenWidth
        {
            get { return this.screenWidth; }
            set { this.screenWidth = value; }
        }

        protected int ScreenHeight
        {
            get { return this.screenHeight; }
            set { this.screenHeight = value; }
        }

        protected int TitlePadding
        {
            get { return this.titlePadding; }
            set { this.titlePadding = value; }
        }

        protected Texture2D Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        protected SpriteFont ItemFont
        {
            get { return this.itemFont; }
            set { this.itemFont = value; }
        }

        // METHODS
        // NOT USED AT THE MOMENT
        public void changeResolution(int width, int height)
        {
            ScreenWidth = width;
            ScreenHeight = height;
        }

        // XNA METHODS
        public void update(int yAxis)
        {
            if (yAxis > 0)
                ItemSelected++;
            else if (yAxis < 0)
                ItemSelected--;

            if (ItemSelected < 0)
                ItemSelected = 0;

            if (ItemSelected > (Items.Count - 1))
                ItemSelected = (Items.Count - 1);

            //updateRectangle();
        }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            Vector2 centerText;
            Vector2 itemPosition;
            Color color;
            itemPosition = new Vector2(TitlePosition.X, TitlePosition.Y + (spaceBetweenItems * 3));

            spriteBatch.Draw(Title, TitlePosition, Color.White);

            for (int i = 0; i < Items.Count; i++)
            {
                    centerText = ItemFont.MeasureString(Items[i]);
                    itemPosition = new Vector2((ScreenWidth / 2 - centerText.X / 2), itemPosition.Y + SpaceBetweenItems);
                    if (i == ItemSelected)
                        color = Color.Green;
                    else
                        color = Color.White;
                    spriteBatch.DrawString(ItemFont, Items[i], itemPosition, color);
            }
        }
    }
}
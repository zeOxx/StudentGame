using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace Perihelion.Models
{
    class EndMenu : Menu
    {
        Texture2D arrowUp;
        Texture2D arrowDown;

        Thread thread;

        public string name;

        private char[] abc0;
        private char[] abc1;
        private char[] abc2;
        private int[] abcPosition;
        private int horizontalSelector;   // 0 <-> 2
        public bool sendScore;

        public EndMenu(ContentHolder content, int width, int height)
            : base(content, width, height)
        {
            arrowUp = content.arrow_up;
            arrowDown = content.arrow_down;

            abcPosition = new int[3];

            abcPosition[0] = 0;
            abcPosition[1] = 0;
            abcPosition[2] = 0;

            sendScore = false;

            populateAbcArrays();

            Active = false;
        }

        public void populateAbcArrays()
        {
            abc0 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            abc1 = abc0;
            abc2 = abc0;
        }

        public void update(int yAxis, int xAxis, bool aButton)
        {
            // Horizontal Movement
            if (xAxis > 0)
            {
                if (horizontalSelector > 0)
                    horizontalSelector = 2;
                else
                    horizontalSelector++;
            }
            else if (xAxis < 0)
            {
                if (horizontalSelector < 2)
                    horizontalSelector = 0;
                else
                    horizontalSelector--;
            }

            // Vertical Movement
            if (yAxis > 0)
            {
                for (int i = 0; i < abcPosition.Length; i++)
                {
                    if (horizontalSelector == i)
                    {
                        abcPosition[i]--;

                        // I KNOW I COULD USE A ARRAY OF CHAR ARRAYS, BUT TIME IS OF THE ESSENCE
                        if (abcPosition[i] < 0)
                        {
                            if (i == 0)
                                abcPosition[i] = (abc0.Length - 1);
                            else if (i == 1)
                                abcPosition[i] = (abc1.Length - 1);
                            else if (i == 2)
                                abcPosition[i] = (abc2.Length - 1);
                        }
                            
                    }
                }
            }
            else if (yAxis < 0)
            {
                for (int i = 0; i < abcPosition.Length; i++)
                {
                    if (horizontalSelector == i)
                    {
                        abcPosition[i]++;

                        // I KNOW I COULD USE A ARRAY OF CHAR ARRAYS, BUT TIME IS OF THE ESSENCE
                        if (i == 0)
                        {
                            if (abcPosition[i] > (abc0.Length - 1))
                                abcPosition[i] = 0;
                        }
                        else if (i == 1)
                        {
                            if (abcPosition[i] > (abc1.Length - 1))
                                abcPosition[i] = 0;
                        }
                        else if (i == 2)
                        {
                            if (abcPosition[i] > (abc2.Length - 1))
                                abcPosition[i] = 0;
                        }
                    }
                }
            }

            // Check for aButton
            if (aButton)
            {
                sendScore = true;
                name = (abc0[abcPosition[0]].ToString() + abc1[abcPosition[1]].ToString() + abc2[abcPosition[2]].ToString());
            }
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            spaceBetweenItems = 30;

            Vector2[] itemPosition = new Vector2[3];

            spriteBatch.Draw(Title, TitlePosition, Color.White);

            itemPosition[0] = new Vector2(TitlePosition.X + 150, TitlePosition.Y + (spaceBetweenItems * 8));
            spriteBatch.DrawString(ItemFont, abc0[abcPosition[0]].ToString(), itemPosition[0], Color.White);

            itemPosition[1] = new Vector2(itemPosition[0].X + 30, itemPosition[0].Y);
            spriteBatch.DrawString(ItemFont, abc1[abcPosition[1]].ToString(), itemPosition[1], Color.White);

            itemPosition[2] = new Vector2(itemPosition[1].X + 30, itemPosition[1].Y);
            spriteBatch.DrawString(ItemFont, abc2[abcPosition[2]].ToString(), itemPosition[2], Color.White);

            for (int i = 0; i < itemPosition.Length; i++)
            {
                if (horizontalSelector == i)
                {
                    itemPosition[i] = new Vector2(itemPosition[i].X - 2, itemPosition[i].Y - 20);
                    spriteBatch.Draw(arrowUp, itemPosition[i], Color.White);

                    itemPosition[i] = new Vector2(itemPosition[i].X, itemPosition[i].Y + 45);
                    spriteBatch.Draw(arrowDown, itemPosition[i], Color.White);
                }
            }

            spriteBatch.DrawString(ItemFont, "Press the A button to submit score!", new Vector2(TitlePosition.X + 50, TitlePosition.Y + 500), Color.White);
        }
    }
}

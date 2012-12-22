using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Perihelion.Models
{
    class Leaderboards : Menu
    {
        private Highscores hs;
        private HighscoreTable hst;
        private string name = "Leaderboards";

        List<string> scores;

        public Leaderboards(ContentHolder content, int width, int height, Highscores hs)
            : base(content, width, height)
        {
            Items = new List<string>();
            scores = new List<string>();

            this.hs = hs;

            hst = new HighscoreTable();

            Active = false;
        }

        public void populateLeaderboard()
        {
            Thread thread;
            thread = new Thread(() => hst = hs.getScores("1", "TOP10"));
            thread.Start();

            thread.Join();

            for (int i = 0; i < hst.Names.Length; i++)
            {
                Items.Add(hst.Names[i]);
                scores.Add(hst.scores[i].ToString());
            }
        }

        public void update(int yAxis, bool bButton)
        {
            base.update(yAxis);

            if (bButton)
                Active = false;
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            Vector2 centerText;
            Vector2 itemPosition;
            spaceBetweenItems = 30;
            itemPosition = new Vector2(TitlePosition.X, TitlePosition.Y + (spaceBetweenItems * 2));

            spriteBatch.Draw(Title, TitlePosition, Color.White);

            centerText = ItemFont.MeasureString(name);

            itemPosition = new Vector2((ScreenWidth / 2 - centerText.X / 2), itemPosition.Y + SpaceBetweenItems * 2);

            spriteBatch.DrawString(ItemFont, name, itemPosition, Color.White);


            itemPosition = new Vector2((itemPosition.X - 50), itemPosition.Y + SpaceBetweenItems);

            spriteBatch.DrawString(ItemFont, "Rank", itemPosition, Color.White);
            spriteBatch.DrawString(ItemFont, "Name", new Vector2(itemPosition.X + 100, itemPosition.Y), Color.White);
            spriteBatch.DrawString(ItemFont, "Score", new Vector2(itemPosition.X + 200, itemPosition.Y), Color.White);

            itemPosition = new Vector2(itemPosition.X, itemPosition.Y + SpaceBetweenItems);

            Vector2 nameItemPosition;

            for (int i = 0; i < Items.Count; i++)
            {
                spriteBatch.DrawString(ItemFont, (i).ToString(), itemPosition, Color.White);

                nameItemPosition = new Vector2(itemPosition.X + 100, itemPosition.Y);
                spriteBatch.DrawString(ItemFont, Items[i], nameItemPosition, Color.White);

                nameItemPosition = new Vector2(nameItemPosition.X + 100, itemPosition.Y);
                spriteBatch.DrawString(ItemFont, scores[i], nameItemPosition, Color.White);
                
                itemPosition = new Vector2(itemPosition.X, itemPosition.Y + SpaceBetweenItems);
            }
        }
    }
}

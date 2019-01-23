using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LiamCOOPAssessment
{
    class Tiles
    {
        #region Variables
        protected Texture2D texture;
        private Rectangle rectangle;
        private static ContentManager content;
        #endregion

        #region Properties
        public Rectangle Rectangle
        {
            get { return rectangle; }
            protected set { rectangle = value; }
        }
        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }
        #endregion

        #region Methods
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
        #endregion
    }
}

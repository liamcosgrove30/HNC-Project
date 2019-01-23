using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiamCOOPAssessment
{
    public class screenAnimation
    {
        #region Declare Variables
        protected Texture2D image;
        protected string text;
        protected SpriteFont font;
        protected Color colour;
        protected Rectangle originalRectangle;
        protected float rotation, scale;
        protected Vector2 origin, position;
        protected ContentManager content;
        protected bool isActive;
        protected float alpha;
        #endregion

        #region Properties
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public virtual float Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }

        public float Scale
        {
            set { scale = value; }
        }
        #endregion

        #region Methods
        public virtual void LoadContent(ContentManager Content, Texture2D image, string text, Vector2 position)
        {
            content = new ContentManager(Content.ServiceProvider, "Content");
            this.image = image;
            this.text = text;
            this.position = position;
            if (text != String.Empty)
            {
                font = content.Load<SpriteFont>("IntroFont");
                colour = new Color(114, 67, 255);
            }
            if (image != null)
                originalRectangle = new Rectangle(0, 0, image.Width, image.Height);
            rotation = 0.0f;
            scale = 1.0f;
            alpha = 1.0f;
            isActive = false;
        }

        public virtual void UnloadContent()
        {
            content.Unload();
            text = String.Empty;
        }

        public virtual void Update(GameTime gameTime)
        { }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //draw animation for images
            if (image != null)
            {
                origin = new Vector2(originalRectangle.Width / 2, originalRectangle.Height / 2);
                spriteBatch.Draw(image, position + origin, originalRectangle, Color.White * alpha, rotation, origin, scale, SpriteEffects.None, 0.0f);
            }

            //draw animation for text
            if (text != String.Empty)
            {
                origin = new Vector2(font.MeasureString(text).X / 2, font.MeasureString(text).Y / 2);
                spriteBatch.DrawString(font, text, position + origin, colour * alpha, rotation, origin, scale, SpriteEffects.None, 0.0f);
            }

        }
        #endregion

    }
}


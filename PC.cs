using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiamCOOPAssessment
{
    class PC:GameScreen
    {
        private Texture2D texture;
        private Vector2 position = new Vector2(700,500);
        private Vector2 velocity;
        private Rectangle rectangle;
        private bool hasJumped = false;
        private SoundEffect jumpSound;

        public Rectangle playerRectangle { get { return rectangle; } }

        public Vector2 Position
        {
            get { return position; }   
            set { position = value; }         
        }

        public PC() { }


       
        public override void LoadContent(ContentManager Content, InputManager inputManager)
        {
            base.LoadContent(Content, inputManager);
            texture = Content.Load<Texture2D>("Test");
            jumpSound = Content.Load<SoundEffect>("cooJumpSoundNew");
        }

        public override void Update(GameTime gameTime)
        {
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            
            Input(gameTime); 

            

            if (velocity.Y < 10)
                velocity.Y += 0.4f;
        }

        private void Input(GameTime gameTime)
        {
            if (inputManager.KeyDown(Keys.D))
            {
                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            }
            else if (inputManager.KeyDown(Keys.A))
            {
                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            }
            else velocity.X = 0f;

            if (inputManager.KeyDown(Keys.Space) && hasJumped == false)
            {
                position.Y -= 7.8f;
                velocity.Y = -7.8f;
                hasJumped = true;
                jumpSound.Play();
            }
        }

        public void tileCollision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTop(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
                hasJumped = false;
            }
            if (rectangle.TouchLeft(newRectangle))
            {
                position.X = newRectangle.X - rectangle.Width - 2;
            }
            if (rectangle.TouchRight(newRectangle))
            {
                position.X = newRectangle.X + newRectangle.Width + 2;
            }
            if (rectangle.TouchBottom(newRectangle))
            {
                velocity.Y = 1f;
            }

            if (position.X < 0)
            {
                position.X = 0;
            }
            if (position.X > xOffset - rectangle.Width) 
            {
                position.X = xOffset - rectangle.Width;
            }
            if (position.Y < 0) 
            {
                velocity.Y = 1f;
            }
            if (position.Y > yOffset - rectangle.Height) 
            {
                position.Y = yOffset - rectangle.Height;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
            base.Draw(spriteBatch);
        }
    }
}

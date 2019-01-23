using Microsoft.Xna.Framework;
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
    class Enemy : GameScreen
    {
        private Texture2D m_texture;
        private Rectangle rectangle;
        public Rectangle EnemyRectangle { get { return rectangle; } }
        private Vector2 m_velocity;
    

        public Texture2D EnemyTexture
        {

           get{return m_texture;}
            set { m_texture = value; }

        }

        public Vector2 EnemyPosition;

        public void tileCollision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTop(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                m_velocity.Y = 0f;
               
            }
            if (rectangle.TouchLeft(newRectangle))
            {
                EnemyPosition.X = newRectangle.X - rectangle.Width - 2;
            }
            if (rectangle.TouchRight(newRectangle))
            {
                EnemyPosition.X = newRectangle.X + newRectangle.Width + 2;
            }
            if (rectangle.TouchBottom(newRectangle))
            {
                m_velocity.Y = 1f;
            }

            if (EnemyPosition.X < 0)
            {
                EnemyPosition.X = 0;
            }
            if (EnemyPosition.X > xOffset - rectangle.Width)
            {
                EnemyPosition.X = xOffset - rectangle.Width;
            }
            if (EnemyPosition.Y < 0)
            {
                m_velocity.Y = 1f;
            }
            if (EnemyPosition.Y > yOffset - rectangle.Height)
            {
                EnemyPosition.Y = yOffset - rectangle.Height;
            }
        }


        public void LoadContent(ContentManager Content)
        {
            m_texture = Content.Load<Texture2D>("Enemy");
        }

        public override void Update(GameTime gameTime)
        {
            EnemyPosition += m_velocity;
            rectangle = new Rectangle((int)EnemyPosition.X, (int)EnemyPosition.Y, m_texture.Width, m_texture.Height);
        }

      

        public bool Collision(PC player)
        {
            return EnemyRectangle.Intersects(player.playerRectangle);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_texture, rectangle, Color.White);
            base.Draw(spriteBatch);                       
        }
    }
}

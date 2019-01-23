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
    class cButton
    {
        Rectangle m_rectangle;
        bool clicked;
        bool available;
        Texture2D m_texture;
        Color col;

        public cButton(Rectangle newRectangle, bool ava)
        {
            m_rectangle = newRectangle;
            available = ava;
            clicked = false;
        }

        public bool Clicked
        {
            get { return clicked; }
            set { clicked = value; }
        }

        public bool Available
        {
            get { return available; }
            set { available = value; }
        }

        public Rectangle PosSize
        {
            get { return m_rectangle; }
            set { m_rectangle = value; }

        }

        public void LoadContent(ContentManager Content, string name)
        {
            m_texture = Content.Load<Texture2D>(name);
            col = Color.White;
        }

        public bool Update(Vector2 mouse)
        {
            if (mouse.X >= m_rectangle.X && mouse.X <= m_rectangle.X + m_rectangle.Width && mouse.Y >= m_rectangle.Y && mouse.Y <= m_rectangle.Y + m_rectangle.Height)
            {
                clicked = true;
            }
            else
            {
                clicked = false;
            }

            if (!available)
            {
                clicked = false;
            }

            return clicked;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Color col = Color.White;

            if(!available)
            {
                col = new Color(50, 50, 50);
            }
            if (clicked)
            {
                col = Color.Green;
            }

            spriteBatch.Draw(m_texture, m_rectangle, col);
        }
    }
}

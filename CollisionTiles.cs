using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiamCOOPAssessment
{
    class CollisionTiles:Tiles
    {
        public CollisionTiles(int counter, Rectangle m_rectangle)
        {
            texture = Content.Load<Texture2D>("Tile" + counter);
            this.Rectangle = m_rectangle;
        }
    }
}

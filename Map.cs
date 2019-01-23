using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiamCOOPAssessment
{
    class Map
    {
        #region Variables
        private List<CollisionTiles> collisionTiles = new List<CollisionTiles>();
        private int m_width;
        private int m_height;
        #endregion

        #region Properties
        public List<CollisionTiles> CollisionTiles
        {
            get { return collisionTiles; }
        }

        public int Width
        {
            get { return m_width; }
        }
        public int Height
        {
            get
            {
                return m_height;
            }
        }
        #endregion

        public Map()
        {

        }

        public void Generate(int[,] m_map, int size)
        {
            for (int counter = 0; counter < m_map.GetLength(1); counter++)
            {
                for (int i = 0; i < m_map.GetLength(0); i++)
                {
                    int number = m_map[i, counter];

                    if (number > 0)
                    {
                        collisionTiles.Add(new CollisionTiles(number, 
                            new Rectangle(counter * size, i * size, size, size)));
                    }
                    m_width = (counter + 1) * size;
                    m_height = (i + 1) * size;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (CollisionTiles tile in collisionTiles)
            {
                tile.Draw(spriteBatch);
            }
        }
    }
}

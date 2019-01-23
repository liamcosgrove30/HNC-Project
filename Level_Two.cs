using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LiamCOOPAssessment
{
    class Level_Two:GameScreen
    {
        #region Variables
        Map map;
        PC player;
        List<Enemy> m_enemies = new List<Enemy>();
        #endregion



        #region Methods



        public override void LoadContent(ContentManager Content, InputManager inputManager)
        {
            base.LoadContent(Content, inputManager);

            Tiles.Content = Content;
            map = new Map();
            player = new PC();


            map.Generate(new int[,] {
                {14,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,15,},
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,},                
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,},                
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,},               
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,},  
                {4,0,0,0,0,0,0,12,12,12,12,12,12,12,12,12,12,12,0,0,0,0,0,0,5,},                             
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,},
                {4,12,12,12,12,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,12,12,12,12,5,}, 
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,}, 
                {4,12,12,12,12,0,0,0,0,0,12,12,12,12,0,0,0,0,0,0,12,12,12,12,5,},                
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,},
                {4,0,0,0,0,12,12,12,12,0,0,0,0,0,0,0,12,12,12,12,0,0,0,0,5,},
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,},               
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,},  
                {4,12,12,12,0,0,0,0,0,12,12,12,12,0,0,0,0,0,0,0,12,12,12,12,5,},               
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,},
                {4,0,0,0,0,10,12,12,12,11,0,0,0,0,0,0,10,12,12,11,0,0,0,0,5,},
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,},
                {7,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,8,},
                }, 32);

            player.LoadContent(Content, inputManager);

            Enemy enemy = new Enemy();
            enemy.EnemyTexture = Content.Load<Texture2D>("Enemy");
            enemy.EnemyPosition = new Vector2(300,420);
            m_enemies.Add(enemy);

            enemy = new Enemy();
            enemy.EnemyTexture = Content.Load<Texture2D>("Enemy");
            enemy.EnemyPosition = new Vector2(600, 325);
            m_enemies.Add(enemy);

            enemy = new Enemy();
            enemy.EnemyTexture = Content.Load<Texture2D>("Enemy");
            enemy.EnemyPosition = new Vector2(400, 132);
            m_enemies.Add(enemy);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update();
            player.Update(gameTime);

            foreach (Enemy enemy in m_enemies)
            {
                enemy.Update(gameTime);
            }

            for (int enemyNum = 0; enemyNum < m_enemies.Count(); enemyNum++)

            {
                if (m_enemies[enemyNum].Collision(player) && (Keyboard.GetState().IsKeyDown(Keys.Enter)))
                {
                    m_enemies.RemoveAt(enemyNum);
                    enemyNum--;

                    if (m_enemies.Count <= 0)
                    {
                        ScreenManager.Instance.AddScreen(new Level_Three(), inputManager);
                    }
                }
            }

            foreach (CollisionTiles tile in map.CollisionTiles)
            {
                player.tileCollision(tile.Rectangle, map.Width, map.Height);

                foreach (Enemy enemy in m_enemies)
                {
                    enemy.tileCollision(tile.Rectangle, map.Width, map.Height);
                }
            }

            if (inputManager.KeyDown(Keys.K))
                ScreenManager.Instance.AddScreen(new Level_Three(), inputManager);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            map.Draw(spriteBatch);
            player.Draw(spriteBatch);
            foreach (Enemy enemy in m_enemies)
            {
                enemy.Draw(spriteBatch);
            }
            base.Draw(spriteBatch);

        }
        #endregion
    }
}

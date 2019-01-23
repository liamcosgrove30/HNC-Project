using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace LiamCOOPAssessment
{
    class Level_One:GameScreen
    {
        #region Variables
        Map map;
        PC player;
        List<Enemy> m_enemies = new List<Enemy>();
        private int m_highScore;
        private int m_currentScore;
        private Song bgMusic;
        #endregion



        #region Methods



        public override void LoadContent(ContentManager Content, InputManager inputManager)
        {
            base.LoadContent(Content, inputManager);
            
            Tiles.Content = Content;
            map = new Map();
            player = new PC();
            bgMusic = Content.Load<Song>("froggerBackgroundMusic");

            m_currentScore = 0;
            map.Generate(new int[,] {
                {14,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,15,},
                {4,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,5,},
                {4,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,5,},                                
                {4,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,5,},
                {4,0,0,0,0,2,2,2,2,2,2,0,0,0,0,0,0,0,2,2,2,2,2,2,5,},
                {4,0,0,0,12,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,},
                {4,0,0,0,0,0,0,0,0,0,0,0,10,12,11,0,0,0,0,0,0,0,0,0,5,},
                {4,10,11,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,},
                {4,0,0,0,0,0,0,0,10,12,11,0,0,0,0,0,0,0,0,0,0,0,0,0,5,},
                {4,0,0,0,9,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,5,},
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,9,0,0,0,0,0,0,0,5,},
                {4,0,0,0,0,0,10,12,12,11,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,},
                {4,0,0,0,0,0,0,0,0,0,0,0,10,11,0,0,0,0,0,0,0,0,0,0,5,},
                {4,0,10,11,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,},
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,5,},
                {4,0,0,0,0,1,2,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,},
                {4,0,0,0,1,8,6,7,3,0,0,0,0,1,3,0,0,0,0,0,0,0,0,0,5,},
                {4,0,0,1,8,6,6,6,7,3,0,0,0,5,4,0,0,0,0,0,0,0,0,0,5,},
                {7,2,2,8,6,6,6,6,6,7,2,2,2,8,7,2,2,2,2,2,2,2,2,2,8,},
                }, 32);
            player.LoadContent(Content, inputManager);

           

            Enemy enemy = new Enemy();
            enemy.EnemyTexture = Content.Load<Texture2D>("Enemy");
            enemy.EnemyPosition = new Vector2(190, 100);
            m_enemies.Add(enemy);

            enemy = new Enemy();
            enemy.EnemyTexture = Content.Load<Texture2D>("Enemy");
            enemy.EnemyPosition = new Vector2(700, 420);
            m_enemies.Add(enemy);

            enemy = new Enemy();
            enemy.EnemyTexture = Content.Load<Texture2D>("Enemy");
            enemy.EnemyPosition = new Vector2(700, 100);
            m_enemies.Add(enemy);

            if (File.Exists(@"highscore.txt")) // This checks to see if the file exists
            {
                StreamReader sr = new StreamReader(@"highscore.txt");   // Open the file

                var line = sr.ReadLine();   // Read the first line in the text file

                m_highScore = Convert.ToInt32(line);
            }
        }
        
        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update();
            player.Update(gameTime);

            MediaPlayer.Play(bgMusic);
            foreach (Enemy enemy in m_enemies)
            {
                if ((enemy.EnemyPosition.X > player.Position.X) && enemy.EnemyPosition.X < 600)
                {
                    enemy.EnemyPosition.X += 10;
                }
            }
            foreach (Enemy enemy in m_enemies)
            {
                if ((enemy.EnemyPosition.X < player.Position.X) && enemy.EnemyPosition.X > 0)
                {
                    enemy.EnemyPosition.X -= 10;
                }
            }

            for (int enemyNum = 0; enemyNum < m_enemies.Count(); enemyNum++)
            {
                if (m_enemies[enemyNum].Collision(player) && (Keyboard.GetState().IsKeyDown(Keys.Enter)))
                {
                    m_enemies.RemoveAt(enemyNum);
                    enemyNum--;
                    m_currentScore++;

                    if (m_enemies.Count <= 0)
                    {
                        StreamWriter sw = new StreamWriter(@"highscore.txt");
                        if (m_currentScore > m_highScore)
                        {
                            sw.WriteLine(m_currentScore);
                        }
                        sw.Close();
                        ScreenManager.Instance.AddScreen(new Level_Two(), inputManager);
                    }
                }
            }

            foreach (Enemy enemy in m_enemies)
            {
                enemy.Update(gameTime);
            }

            foreach (CollisionTiles tile in map.CollisionTiles)
            {
                player.tileCollision(tile.Rectangle, map.Width, map.Height);

                foreach (Enemy enemy in m_enemies)
                {
                    enemy.tileCollision(tile.Rectangle, map.Width, map.Height);
                }
            }

            

            if (inputManager.KeyDown(Keys.J))
                ScreenManager.Instance.AddScreen(new Level_Two(), inputManager);
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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiamCOOPAssessment
{
    class TitleScreen:GameScreen
    {
        #region Declare Variables
        MouseState mouseState, prevMouseState;
        
        const byte MENU = 0, PLAY = 1, OPTIONS = 2, HIGHSCORE = 3;
        int m_CurrentScreen = MENU;

        Texture2D highscoreText, optionsText, playText, bg;
        cButton playButton, optionsButton, highscoreButton;

        Model m_gemModel;
        private float m_gemAngle;
        private Vector3[] position = new Vector3[1];
        #endregion

        #region Methods
        public override void LoadContent(ContentManager Content, InputManager inputManager)
        {
            base.LoadContent(Content, inputManager);
            highscoreText = Content.Load<Texture2D>("HighscoreButton");
            optionsText = Content.Load<Texture2D>("OptionsButton");
            playText = Content.Load<Texture2D>("PlayButton");
            bg = Content.Load<Texture2D>("MainMenu");
            highscoreButton = new cButton(new Rectangle(300, 300, highscoreText.Width, highscoreText.Height), true);
            highscoreButton.LoadContent(Content, "HighscoreButton");

            optionsButton = new cButton(new Rectangle(300, 200, optionsText.Width, optionsText.Height), true);
            optionsButton.LoadContent(Content, "OptionsButton");

            playButton = new cButton(new Rectangle(300, 100, playText.Width, playText.Height), true);
            playButton.LoadContent(Content, "PlayButton");

            m_gemModel = Content.Load<Model>("sphere");
            m_gemAngle = 0;
            position[0] = new Vector3(-2.5f, 0, 50);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update();
            mouseState = Mouse.GetState();

            if (inputManager.KeyDown(Keys.X))
                ScreenManager.Instance.AddScreen(new IntroScreen(), inputManager);                   

            switch (m_CurrentScreen)
            {
                case MENU:
                    if (playButton.Update(new Vector2(mouseState.X, mouseState.Y)) == true && mouseState != prevMouseState && mouseState.LeftButton == ButtonState.Pressed)
                    {
                        m_CurrentScreen = PLAY;
                        ScreenManager.Instance.AddScreen(new Level_One(), inputManager);
                    }
                    if (optionsButton.Update(new Vector2(mouseState.X, mouseState.Y)) == true && mouseState != prevMouseState && mouseState.LeftButton == ButtonState.Pressed)
                    {
                        m_CurrentScreen = OPTIONS;
                    }
                    if (highscoreButton.Update(new Vector2(mouseState.X, mouseState.Y)) == true && mouseState != prevMouseState && mouseState.LeftButton == ButtonState.Pressed)
                    {
                        m_CurrentScreen = HIGHSCORE;
                    }
                        break;
                case OPTIONS:
                    if (inputManager.KeyDown(Keys.Back))
                    {
                        m_CurrentScreen = MENU;
                    }
                    break;
                case HIGHSCORE:
                    if (inputManager.KeyDown(Keys.Back))
                    {
                        m_CurrentScreen = MENU;
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (m_CurrentScreen)
            {
                case MENU:
                    spriteBatch.Draw(bg, new Rectangle(0, 0, bg.Width, bg.Height), Color.White);
                    spriteBatch.Draw(playText, new Rectangle(300, 100, playText.Width, playText.Height), Color.White);
                    spriteBatch.Draw(optionsText, new Rectangle(300, 200, optionsText.Width, optionsText.Height), Color.White);
                    spriteBatch.Draw(highscoreText, new Rectangle(300, 300, highscoreText.Width, highscoreText.Height), Color.White);

                   base.Draw(spriteBatch);

                    spriteBatch.End();
                    spriteBatch.GraphicsDevice.DepthStencilState = new DepthStencilState { DepthBufferEnable = true };

                    Matrix world = Matrix.CreateRotationY(m_gemAngle) * Matrix.CreateTranslation(0, 0, 0) * Matrix.CreateScale(0.6F);
                    Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 15), Vector3.Zero, Vector3.UnitY);
                    Matrix proj = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, spriteBatch.GraphicsDevice.Viewport.AspectRatio, 1, 100);

                    DrawModel(m_gemModel, world, view, proj, position[0]);


                    spriteBatch.GraphicsDevice.DepthStencilState = new DepthStencilState { DepthBufferEnable = false };
                    spriteBatch.Begin();

                    break;
            }

            
        }

        private void DrawModel(Model model, Matrix world, Matrix view, Matrix proj, Vector3 position)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();

                    effect.PreferPerPixelLighting = true;
                    effect.World = world;
                    effect.View = view;
                    effect.Projection = proj;
                }

                mesh.Draw();
            }
        }
        #endregion
    }
}

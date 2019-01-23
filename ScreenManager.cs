using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiamCOOPAssessment
{
    public class ScreenManager
    {
        #region Declare Variables
        //screen manager instance
        private static ScreenManager instance;

        //creates content manager
        ContentManager content;

        //current screen on display 
        GameScreen currentScreen;
        //screen that will be displayed
        GameScreen newScreen;

        //screen stack
        Stack<GameScreen> screenStack = new Stack<GameScreen>();
        
        //screen dimensions
        Vector2 dimensions;

        FadeAnimation fade;

        Texture2D fadeTexture;

        bool transition;

        InputManager inputManager;
        #endregion

        #region Properties
        public static ScreenManager Instance
        { get { if (instance == null)
                    instance = new ScreenManager();
                return instance;
              }
        }

        public Vector2 Dimensions
        {
            get { return dimensions; }
            set { dimensions = value; }
        }
        #endregion

        #region Methods
        public void AddScreen(GameScreen screen, InputManager inputManager)
        {
            newScreen = screen;
            transition = true;
            fade.IsActive = true;
            fade.Alpha = 1.0f;
            fade.Activate = 1.0f;
            this.inputManager = inputManager;
        }
       
        public void Initialize()
        {
            currentScreen = new IntroScreen();
            fade = new FadeAnimation();
            inputManager = new InputManager();
        }

        public void LoadContent(ContentManager Content)
        {
            content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent(Content, inputManager);
            fadeTexture = content.Load<Texture2D>("fadeAnimation");
            fade.LoadContent(content, fadeTexture, "", Vector2.Zero);
            fade.Scale = dimensions.X;
        }

        public void Update(GameTime gameTime)
        {
            if (!transition)
                currentScreen.Update(gameTime);
            else
                Transition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
            if (transition)
                fade.Draw(spriteBatch);
        }
        #endregion

        private void Transition(GameTime gameTime)
        {
            fade.Update(gameTime);

            if (fade.Alpha == 1.0f && fade.Timer.TotalSeconds == 1.0f)
            {
                //if the fade is playing and has played for its duration
                //bring the new screen to top of the stack
                screenStack.Push(newScreen);
                //unload the current screen
                currentScreen.UnloadContent();
                //set the current screen to the newly pushed screen
                currentScreen = newScreen;
                //load the new current screen
                currentScreen.LoadContent(content, this.inputManager);
            }
            //if fade isnt playing
            else if (fade.Alpha == 0.0f)
            {
                //set transition and fade to false
                transition = false;
                fade.IsActive = false;
            }


        }
   }
}


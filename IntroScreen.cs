using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LiamCOOPAssessment
{
    public class IntroScreen:GameScreen
    {
        #region Variables
        
        SpriteFont Font;

        #endregion

        #region Methods
        public override void LoadContent(ContentManager Content, InputManager inputManager)
        {
            base.LoadContent(Content, inputManager);
            if (Font == null)
                Font = content.Load<SpriteFont>("IntroFont");

        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update();

           
            if (inputManager.KeyDown(Keys.H))
                ScreenManager.Instance.AddScreen(new Level_One(), inputManager);
            if (inputManager.KeyDown(Keys.Z))
                ScreenManager.Instance.AddScreen(new TitleScreen(), inputManager);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, "A game by Liam Cosgrove", new Vector2(300, 250), Color.White);
            spriteBatch.DrawString(Font, "Press Z to start!", new Vector2(300, 300), Color.White);
            base.Draw(spriteBatch);
        }
        #endregion

    }
}

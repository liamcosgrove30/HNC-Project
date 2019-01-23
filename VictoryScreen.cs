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
    class VictoryScreen:GameScreen
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


            if (inputManager.KeyDown(Keys.Enter))
                ScreenManager.Instance.AddScreen(new TitleScreen(), inputManager);
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, "Thanks for playing!", new Vector2(300, 250), Color.White);
            spriteBatch.DrawString(Font, "Press Enter to go to main menu or escape to exit!", new Vector2(300, 300), Color.White);
            base.Draw(spriteBatch);
        }
        #endregion
    }
}

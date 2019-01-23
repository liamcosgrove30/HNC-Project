using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LiamCOOPAssessment
{
    public class FadeAnimation : screenAnimation
    {
        #region Declare Variables
        bool increase;
        float fadeSpeed;
        TimeSpan defaultTime;
        TimeSpan timer;       
        float activate;
        bool stopUpdate;
        float defAlpha;
        #endregion

        #region Properties
        public override float Alpha
        {
            get
            {
                return alpha;
            }

            set
            {
                alpha = value;

                if (alpha == 1.0f)
                    increase = false;
                else if (alpha == 0.0f)
                    increase = true;
            }
        }

        public TimeSpan Timer
        {
            get { return timer; }
            set
            {
                defaultTime = value;
                timer = defaultTime;
            }
        }

        public float FadeSpeed
        {
            get { return fadeSpeed; }
            set { fadeSpeed = value; }
        }

        public float Activate
        {
            get { return activate; }
            set { activate = value; }
        }
        #endregion

        #region Methods
        public override void LoadContent(ContentManager Content, Texture2D image, string text, Vector2 position)
        {
            base.LoadContent(Content, image, text, position);
            increase = false;
            fadeSpeed = 1.0f;
            defaultTime = new TimeSpan(0, 0, 1);
            timer = defaultTime;
            activate = 0.0f;
            stopUpdate = false;
            defAlpha = alpha;
        }

        public override void Update(GameTime gameTime)
        {
            #region Black fade Transistion
            if (isActive)
            {
                if (!stopUpdate)
                {
                    //if fade is active and is updating 
                    //fade in and out
                    if (increase)
                        alpha -= fadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    else
                        alpha += fadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (alpha <= 0.0f)
                        alpha = 0.0f;
                    else if (alpha >= 1.0f)
                        alpha = 1.0f;
                }
                //if fade is equal to the activation timer
                if (alpha == activate)
                {
                    //stopupdating
                    stopUpdate = true;
                    timer -= gameTime.ElapsedGameTime;
                    if (timer.TotalSeconds <= 0)
                    {
                        increase = !increase;
                        timer = defaultTime;
                        stopUpdate = false;
                    }
                }
            }
            else
            {
                alpha = defAlpha;
            }
            #endregion
        }
        #endregion
    }
}

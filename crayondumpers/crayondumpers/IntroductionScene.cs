using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace crayondumpers
{
    public class IntroductionScene:DrawableGameComponent
    {
        public bool endIntroductionScene = false;
        cls bkgIntroduction;
        SpriteBatch spriteBatch;
        public IntroductionScene(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            bkgIntroduction = new cls(Vector2.Zero, Game.Content.Load<Texture2D>(@"background\bkgIntroduction"));
            base.LoadContent();
        }
        public virtual void Show()
        {
            Visible = true;
            Enabled = true;
        }
        public virtual void Hide()
        {
            Visible = false;
            Enabled = false;
        }
        public override void Update(GameTime gameTime)
        {
           
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                endIntroductionScene = true;
            }
            base.Update(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            bkgIntroduction.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}

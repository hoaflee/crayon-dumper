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
    public class HelpScene : Microsoft.Xna.Framework.DrawableGameComponent
    {

        public bool endHelpScene = false;
        cls bkgHelp;
        SpriteBatch spriteBatch;
        public HelpScene(Game game)
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
            bkgHelp = new cls(Vector2.Zero, Game.Content.Load<Texture2D>(@"background\bkgHelp"));
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
                endHelpScene = true;
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            bkgHelp.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}

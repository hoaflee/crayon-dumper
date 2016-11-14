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
    public class introductionMenu :DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        public bool endIntroduction = false;

        cls intro;
        public introductionMenu(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }
        protected override void  LoadContent()
        {
         spriteBatch=new SpriteBatch(GraphicsDevice);
         intro=new cls(Vector2.Zero,Game.Content.Load<Texture2D>(@"bacgroundMenu\background_02"));
 	     base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            if (Keyboard.GetState().IsKeyDown(Keys.A)) 
            base.Update(gameTime);
        }
        public override void  Draw(GameTime gameTime)
        {
            intro.Draw(spriteBatch);
 	        base.Draw(gameTime);
        }
    }
}

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
    public class ExitScene : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public bool endExitScene = false;
        cls bkgExit;
        cls yes;
        cls no;
        SpriteBatch spriteBatch;
        KeyboardState oldKeyboardState;
       
        private bool isYes = true;
        public ExitScene(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch=new SpriteBatch(GraphicsDevice);
            bkgExit=new cls(Vector2.Zero,Game.Content.Load<Texture2D>(@"background\bkgExit"));
            yes = new cls(new Vector2(450,400), Game.Content.Load<Texture2D>(@"Pictures\yes"));
            no = new cls(new Vector2(650,400), Game.Content.Load<Texture2D>(@"Pictures\no"));
            no.ColorA = 100;
        }
        public bool checkkey(Keys thekey)
        {
            KeyboardState currentKeyboard = Keyboard.GetState();

            return (oldKeyboardState.IsKeyDown(thekey) && currentKeyboard.IsKeyUp(thekey));

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
            KeyboardState currentKeyboard=Keyboard.GetState();
            if ( checkkey(Keys.Left))
            {
                yes.ColorA = 255;
                no.ColorA = 100;
                isYes = true;
            }
            if (checkkey(Keys.Right))
            {
                no.ColorA = 255;
                yes.ColorA = 100;
                isYes = false;
            }
            if (isYes && checkkey(Keys.Space))
            {
                this.Game.Exit();
            }
            else if (!isYes && checkkey(Keys.Space))
            {
                endExitScene = true;
            }
            oldKeyboardState = currentKeyboard;
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            bkgExit.Draw(spriteBatch);
            yes.Draw(spriteBatch);
            no.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}

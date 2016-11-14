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
    public class VictoryScene : Microsoft.Xna.Framework.DrawableGameComponent
    {

        public bool endVictoryScene = false;
        cls victoryScene;
        SpriteBatch spriteBatch;
        SoundEffect sound_victory;
        bool audio = false;

        public VictoryScene(Game game)
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
            victoryScene = new cls(Vector2.Zero, Game.Content.Load<Texture2D>(@"background\victoryScene"));
            sound_victory = Game.Content.Load<SoundEffect>(@"content\sound\victory");
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
            if (audio == false)
            {
                sound_victory.Play();
                audio = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                endVictoryScene = true;
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            victoryScene.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}

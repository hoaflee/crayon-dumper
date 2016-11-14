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
    public class GameOverScene : Microsoft.Xna.Framework.DrawableGameComponent
    {

        public bool endGameOverScene = false;
        cls gameoverScene;
        SpriteBatch spriteBatch;
        SoundEffect sound_gameover;
        bool audio = false;

        public GameOverScene(Game game)
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
            gameoverScene = new cls(Vector2.Zero, Game.Content.Load<Texture2D>(@"background\gameoverScene"));
            sound_gameover = Game.Content.Load<SoundEffect>(@"content\sound\gameover");
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
                sound_gameover.Play();
                audio = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                endGameOverScene = true;
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            gameoverScene.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}

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
using crayondumpers;

namespace crayondumpers
{
    public class CrayonDumpers : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        manageMenu menu1;
        IntroductionScene introduction1;
        HelpScene helpScene1;
        ExitScene exitScene1;
        VictoryScene victory;
        GameOverScene gameover;
        SpriteManager spriteManager;
        SpriteFont font;
        Texture2D background;

        int score;
        Song song2;

        public CrayonDumpers()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1366;
            graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            menu1 = new manageMenu(this);
            introduction1 = new IntroductionScene(this);
            helpScene1 = new HelpScene(this);
            exitScene1 = new ExitScene(this);
            spriteManager = new SpriteManager(this);
            gameover = new GameOverScene(this);
            victory = new VictoryScene(this);

            Components.Add(spriteManager);
            spriteManager.Enabled = false;
            spriteManager.Visible = false;

            Components.Add(menu1);
            Components.Add(introduction1);
            Components.Add(helpScene1);
            Components.Add(exitScene1);
            Components.Add(victory);
            Components.Add(gameover);

            introduction1.Hide();
            helpScene1.Hide();
            exitScene1.Hide();
            victory.Hide();
            gameover.Hide();

            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>(@"content\Font1");
            song2 = Content.Load<Song>(@"Music\Canon Rock - Jerry C");
            background = Content.Load<Texture2D>(@"background\background");

        }


        protected override void UnloadContent()
        {
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            //if (MediaPlayer.State == MediaState.Stopped) MediaPlayer.Play(song2);
            if (menu1.endManageMenu)
            {
                switch (menu1.SELECT)
                {
                    case 0:
                        menu1.Hide();                        
                        spriteManager.Enabled = true;
                        spriteManager.Visible = true;
                        menu1.SELECT = 10;
                        menu1.endManageMenu = false;       
                        break;

                    case 1:
                        {
                            menu1.Hide();
                            introduction1.Show();
                        }
                        break;

                    case 2:
                        {
                            menu1.Hide();
                            helpScene1.Show();
                        }
                        break;
                    case 3:
                        {
                            menu1.Hide();
                            exitScene1.Show();
                        }
                        break;
                }
            }
            if (spriteManager.Getvictory)
            {
                spriteManager.Enabled = false;
                spriteManager.Visible = false;
                victory.Show();
                spriteManager.Getvictory = false;
            }
            if (spriteManager.Getgameover)
            {
                spriteManager.Enabled = false;
                spriteManager.Visible = false;
                gameover.Show();
                spriteManager.Getgameover = false;
                spriteManager.Enabled = false;
            }
            if (introduction1.endIntroductionScene)
            {
                menu1.Show();
                introduction1.Hide();
                menu1.endManageMenu = false;
                introduction1.endIntroductionScene = false;
            }

            if (helpScene1.endHelpScene)
            {
                menu1.Show();
                helpScene1.Hide();
                menu1.endManageMenu = false;
                helpScene1.endHelpScene = false;
            }
            if (exitScene1.endExitScene)
            {
                menu1.Show();
                exitScene1.Hide();
                menu1.endManageMenu = false;
                exitScene1.endExitScene = false;
            }
            if (victory.endVictoryScene)
            {
                menu1.Show();
                victory.Hide();
                spriteManager.Statup();
                menu1.endManageMenu = false;
                victory.endVictoryScene = false;
                menu1.SELECT = 0;
            }
            if (gameover.endGameOverScene)
            {
                menu1.Show();
                gameover.Hide();
                spriteManager.Statup();
                menu1.endManageMenu = false;
                gameover.endGameOverScene = false;
                menu1.SELECT = 0;
            }
            base.Update(gameTime);
            score = spriteManager.GetScore;
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.DrawString(font, "Your Score " + score, new Vector2(500, 400), Color.GreenYellow);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

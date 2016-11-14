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
    public class manageMenu :DrawableGameComponent
    {

        int select;
        private double delay = 100;
        private double currentTime = 0;
        cls background;
        cls[] array = new cls[6];
        cls[] tree = new cls[3];
        cls[] cloud = new cls[2];
              
        Vector2 pstApple;
        Vector2 spApple;

        cls tree1;
        cls Apple1;

         
        Vector2 pstTree2;
        Vector2 spTree2;

        SoundEffect soundMenu;
        Song musicBackground;

        KeyboardState oldKeyboardState;
        
        int key = 0;
        public int SELECT = 0;
        public bool endManageMenu = false;
        
        SpriteBatch spriteBatch;
        public manageMenu(Game game)
            : base(game)
        {
        }
        public int Select
        {
            set { select = value; }
        }

            public override void Initialize()
        {
            pstApple = new Vector2(220, 185);
            spApple = new Vector2(1, 0);

            pstTree2 = new Vector2(Game.Window.ClientBounds.Width, 185);
            spTree2=new Vector2(1,0);
            base.Initialize();
        }
       
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background=new cls(Vector2.Zero,Game.Content.Load<Texture2D>(@"Pictures\bkg"));

            
        array[0] = new cls(new Vector2(Game.Window.ClientBounds.Width/2-270,Game.Window.ClientBounds.Height/2-75), Game.Content.Load<Texture2D>(@"Pictures\newgame"));
        array[1] = new cls(new Vector2(Game.Window.ClientBounds.Width / 2 - 350, Game.Window.ClientBounds.Height / 2+25), Game.Content.Load<Texture2D>(@"Pictures\introduction"));
        array[2] = new cls(new Vector2(Game.Window.ClientBounds.Width / 2 - 280, Game.Window.ClientBounds.Height / 2 + 125), Game.Content.Load<Texture2D>(@"Pictures\help"));
        array[3] = new cls(new Vector2(Game.Window.ClientBounds.Width / 2 - 280, Game.Window.ClientBounds.Height / 2 + 225), Game.Content.Load<Texture2D>(@"Pictures\exit"));
       //than cay
        array[4] = new cls(new Vector2(0, 350), Game.Content.Load<Texture2D>(@"Animation\left"));
        array[5] = new cls(new Vector2(1320, 0), Game.Content.Load<Texture2D>(@"Animation\right"));
 
       
        Apple1 = new cls(new Vector2(220, 185), Game.Content.Load<Texture2D>(@"Animation\apple"));

        tree1= new cls(new Vector2(-75,125), Game.Content.Load<Texture2D>(@"Animation\menuTree1"));
        tree[0]=new cls(new Vector2(Game.Window.ClientBounds.Width-600,100),Game.Content.Load<Texture2D>(@"Animation\menuTree2"));
        tree[1] = new cls(new Vector2(Game.Window.ClientBounds.Width - 350, Game.Window.ClientBounds.Height-150), Game.Content.Load<Texture2D>(@"Animation\menuTree3"));
        tree[2] = new cls(new Vector2(-50, Game.Window.ClientBounds.Height -180), Game.Content.Load<Texture2D>(@"Animation\menuTree4"));
        cloud[0]=new cls(new Vector2(0,0),Game.Content.Load<Texture2D>(@"Animation\cloud1"));
        cloud[1] = new cls(new Vector2(1200, 50), Game.Content.Load<Texture2D>(@"Animation\cloud2"));

        soundMenu = Game.Content.Load<SoundEffect>(@"Sound\menu_click");
        musicBackground = Game.Content.Load<Song>(@"Music\classified");

        if (MediaPlayer.State == MediaState.Stopped)
        {
            MediaPlayer.Play(musicBackground);
            MediaPlayer.Volume = 0.3f;
            MediaPlayer.IsRepeating = true;
        }
            
            base.LoadContent(); 
        }

        public bool checkkey(Keys thekey)
        {
            KeyboardState currentKeyboard = Keyboard.GetState();

            return (oldKeyboardState.IsKeyDown(thekey) && currentKeyboard.IsKeyUp(thekey));

        }

        public override void Update(GameTime gameTime)
        {

            KeyboardState currentKeyboard = Keyboard.GetState();
            
            if (checkkey(Keys.Up))
            {
                key--; SELECT = key;
                if (key == -1) key = 3;
                soundMenu.Play();
               
            }
           
            if (checkkey(Keys.Down))
            {
                key += 1; SELECT = key;
                if (key == 4) key = 0;
                soundMenu.Play();
                
            }
           
            for (int i = 0; i < 4; i++)
            {
                if (i == key)
                {
                    array[i].Bigger = 15;
                }
                else
                    array[i].Bigger = 0;
            }
            oldKeyboardState = currentKeyboard;
            if (currentKeyboard.IsKeyDown(Keys.Enter)) endManageMenu = true;

            //change position
            if (currentTime >= delay)
            {
                Apple1.changePositionX(10, new Vector2(220, 185));
                tree[0].changePositionY(10, new Vector2(Game.Window.ClientBounds.Width - 600, 100));

                tree[1].changePositionY(15, new Vector2(Game.Window.ClientBounds.Width - 350, Game.Window.ClientBounds.Height - 150));
                tree[1].changePositionX(10, new Vector2(Game.Window.ClientBounds.Width - 350, Game.Window.ClientBounds.Height - 150));

                tree[2].changePositionX(20, new Vector2(-50, Game.Window.ClientBounds.Height - 180));

                cloud[0].rollSpriteRight(new Vector2(Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height));
                cloud[1].rollSpriteLeft(new Vector2(Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height));
                currentTime = 0;
            }
            else
            {
                currentTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            base.Update(gameTime);
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
        public override void Draw(GameTime gameTime)
        {
            background.Draw(spriteBatch);
              for (int i = 0; i < 6; ++i)
            {
                array[i].Draw(spriteBatch);
            }
          
            //tree
            tree1.Draw(spriteBatch);
            tree[0].Draw(spriteBatch);
            tree[1].Draw(spriteBatch);
            tree[2].Draw(spriteBatch);

            cloud[0].Draw(spriteBatch);
            cloud[1].Draw(spriteBatch);
            
            
            
            Apple1.Draw(spriteBatch);
             
            base.Draw(gameTime);
        }
    }
}

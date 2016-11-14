using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace crayondumpers
{
    class boss
    {
        Texture2D textureImage, mHealthBar;
        protected Vector2 position;
        protected Point frameSize;
        int collisionOffset;
        Point currentFrame;
        Point sheetSize;
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame;
        protected Vector2 speed;
        protected float scale;
        int health = 500;

        public Rectangle collisionRect
        {
            get
            {
                return new Rectangle(
                (int)(position.X + (collisionOffset * scale)),
                (int)(position.Y + (collisionOffset * scale)),
                (int)((frameSize.X - (collisionOffset * 2)) * scale),
                (int)((frameSize.Y - (collisionOffset * 2)) * scale));
            }
        }

        public boss(Texture2D textureImage,Texture2D mHealthBar,Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            int millisecondsPerFrame,float scale)
        {
            this.textureImage = textureImage; this.mHealthBar = mHealthBar;
            this.position = position;
            this.frameSize = frameSize;
            this.collisionOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.speed = speed;
            this.millisecondsPerFrame = millisecondsPerFrame;
            this.scale = scale;
        }

        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame = 0;
                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    ++currentFrame.Y;
                    if (currentFrame.Y >= sheetSize.Y)
                        currentFrame.Y = 0;
                }
            }
            position.X += speed.X;
            position.Y += speed.Y;
            if ((position.X > clientBounds.Width - 350) || (position.X < 0))
            {
                speed.X *= -1;
            }
            if ((position.Y > clientBounds.Height -325) || (position.Y < 30))
            {
                speed.Y *= -1;
            }
            health = (int)MathHelper.Clamp(health, 0, 500); 
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //draw healthBar of boss
            //Draw the health for the health bar
            spriteBatch.Draw(mHealthBar, new Rectangle((int)position.X+40,
                  (int)position.Y-35, mHealthBar.Width, 34), new Rectangle(0, 41, mHealthBar.Width, 24), Color.White);

            //Draw the current health level based on the current Health
            spriteBatch.Draw(mHealthBar, new Rectangle((int)position.X+40,
                  (int)position.Y-35, (int)(mHealthBar.Width * ((double)health / 500)), 33),
                new Rectangle(0, 73, mHealthBar.Width, 22), Color.White);

            //Draw the box around the health bar
            spriteBatch.Draw(mHealthBar, new Rectangle((int)position.X+40,
                  (int)position.Y-35, mHealthBar.Width, 33), new Rectangle(0, 0, mHealthBar.Width, 33), Color.White);

            spriteBatch.Draw(textureImage,
                position,
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X, frameSize.Y),
                Color.White, 0, Vector2.Zero,
                scale, SpriteEffects.None, 0);
            
        }

        public Vector2 GetPosition
        {
            get { return position; }
        }
        public int GetHealth
        {
            get { return health; }
        }
        public Point Getsize
        {
            get { return frameSize; }
        }
        public void downHealth(int a)
        {
            health -= a;
        }
        public void UnloadContent()
        {
            textureImage.Dispose();
        }
    }
}

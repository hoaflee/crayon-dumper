using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace crayondumpers
{
    class handofboss
    {
        Texture2D textureImage;
        protected Vector2 position;
        protected Point frameSize;
        int collisionOffset;
        Point currentFrame;
        Point sheetSize;
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame;
        protected float scale;
        boss boss;
        int x; int y;
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

        public handofboss(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize,
            int millisecondsPerFrame, float scale,boss boss,int a,int b)
        {
            this.position = position;
            this.textureImage = textureImage;
            this.frameSize = frameSize;
            this.collisionOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.millisecondsPerFrame = millisecondsPerFrame;
            this.scale = scale;
            this.boss = boss;
            this.x = a;
            this.y = b;
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
            position.X = boss.GetPosition.X+x;
            position.Y = boss.GetPosition.Y+y;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureImage,
                position,
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X, frameSize.Y),
                Color.White, 0, Vector2.Zero,
                scale, SpriteEffects.None, 0);
        }
        protected void UnloadContent()
        {
            textureImage.Dispose();
        }
    }
}

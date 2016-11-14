using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace crayondumpers
{
    class Explosion
    {
        Texture2D textureImage;
        protected Vector2 position;
        protected Point frameSize;
        Point currentFrame;
        Point sheetSize;
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame;
        protected Vector2 speed;
        protected float scale;

        public Explosion(Texture2D textureImage, Vector2 position, Point frameSize, Point currentFrame, Point sheetSize, Vector2 speed,
            int millisecondsPerFrame, float scale)
        {
            this.textureImage = textureImage;
            this.position = position;
            this.frameSize = frameSize;
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
                }
            }
            position.X += speed.X;
            position.Y += speed.Y;
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

        public bool IsOutOfBounds(Rectangle clientRect)
        {
            if (position.X < -frameSize.X ||
            position.X > clientRect.Width ||
            position.Y < -frameSize.Y ||
            position.Y > clientRect.Height)
            {
                return true;
            }
            return false;
        }
        public Vector2 GetPosition
        {
            get { return position; }
        }

    }
}

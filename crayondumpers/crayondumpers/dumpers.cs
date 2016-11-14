using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace crayondumpers
{
    abstract class dumpers
    {
        Texture2D textureImage;
        protected Vector2 position;
        protected Point frameSize;
        int collisionOffset;
        Point currentFrame;
        Point sheetSize;
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame;
        //const int defaultMillisecondsPerFrame = 16;

              //Speed
        protected Vector2 speed;
        Vector2 originalSpeed;

        //Scale
        protected float scale;
 

        ////Audio cue name for collisions
        public string collisionCueName { get; private set; }

        public abstract Vector2 direction
        {
            get;
        }

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

        public dumpers(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, 
            int millisecondsPerFrame, string collisionCueName, /*int scoreValue,*/ float scale)
        {
            this.textureImage = textureImage;
            this.position = position;
            this.frameSize = frameSize;
            this.collisionOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.speed = speed;
            originalSpeed = speed;
            this.collisionCueName = collisionCueName;
            this.millisecondsPerFrame = millisecondsPerFrame;
            this.scale = scale;
        }

        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            //Update animation frame
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
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Draw the sprite
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

        public void ModifySpeed(float modifier)
        {
            speed *= modifier;
        }

        public void ResetSpeed()
        {
            speed = originalSpeed;
        }
    }
}

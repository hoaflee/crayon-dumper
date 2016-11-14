using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace crayondumpers
{

    class taget : dumpers
    {
        MouseState prevMouseState;

        public taget(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, int millisecondsPerFrame, float scale)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame, null, scale)
        {
        }

        public override Vector2 direction
        {
            get
            {
                //Return direction based on input from mouse and gamepad
                Vector2 inputDirection = Vector2.Zero;
                 return inputDirection;
            }
        }
        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            position += direction;

            MouseState currMouseState = Mouse.GetState();

            if (currMouseState.X != prevMouseState.X ||
            currMouseState.Y != prevMouseState.Y)
            {
                position = new Vector2(currMouseState.X, currMouseState.Y);
            }
            prevMouseState = currMouseState;

            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
            if (position.X > clientBounds.Width - frameSize.X * this.scale)
                position.X = clientBounds.Width - frameSize.X * this.scale;
            if (position.Y > clientBounds.Height - frameSize.Y * this.scale)
                position.Y = clientBounds.Height - frameSize.Y * this.scale;
            base.Update(gameTime, clientBounds);
        }            

    }
}

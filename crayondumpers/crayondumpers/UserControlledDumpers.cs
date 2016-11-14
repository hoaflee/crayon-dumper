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

    class UserControlledDumpers : dumpers
    {
        int protect;
        public UserControlledDumpers(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed,int millisecondsPerFrame,int protect, float scale)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame, null, scale)
        {
            this.protect = protect;
        }

        enum State
        {
            Running,
            Jumping
        }

        State mCurrentState = State.Running;
        Vector2 mStartingPosition = Vector2.Zero;
        Vector2 mDirection = Vector2.Zero;
        Vector2 mSpeed = Vector2.Zero;

        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;

        public int getProtect
        {
            get { return protect; }
        }

        public void addDefend()
        {
            protect += 5;
        }
        public void removeDefend()
        {
            protect -= 1;
        }
        public override Vector2 direction
        {
            get
            {
                Vector2 inputDirection = Vector2.Zero;
                inputDirection = mDirection;
                return inputDirection * speed;
            }
        }

        KeyboardState mPreviousKeyboardState;
        
        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();
            UpdateMovement(aCurrentKeyboardState);
            UpdateJump(aCurrentKeyboardState);

            mPreviousKeyboardState = aCurrentKeyboardState;
            position += direction;

            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
            if (position.X > clientBounds.Width - frameSize.X* this.scale)
                position.X = clientBounds.Width - frameSize.X * this.scale;
            if (position.Y > clientBounds.Height - frameSize.Y * this.scale)
                position.Y = clientBounds.Height - frameSize.Y * this.scale;
            base.Update(gameTime, clientBounds);
        }
        private void UpdateMovement(KeyboardState aCurrentKeyboardState)
        {
            if (mCurrentState == State.Running)
            {
                mSpeed = Vector2.Zero;
                mDirection = Vector2.Zero;

                if (aCurrentKeyboardState.IsKeyDown(Keys.A) == true)
                {
                    mSpeed.X = speed.X;
                    mDirection.X = MOVE_LEFT;
                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.D) == true)
                {
                    mSpeed.X = speed.Y;
                    mDirection.X = MOVE_RIGHT;
                }

            }
        }
        private void UpdateJump(KeyboardState aCurrentKeyboardState)
        {
            if (mCurrentState == State.Running)
            {
                if (aCurrentKeyboardState.IsKeyDown(Keys.Space) == true && mPreviousKeyboardState.IsKeyDown(Keys.Space) == false)
                {
                    Jump();
                }
            }

            if (mCurrentState == State.Jumping)
            {
                if (mStartingPosition.Y - position.Y > 140)
                {
                    mDirection.Y = MOVE_DOWN;
                }

                if (position.Y > mStartingPosition.Y)
                {
                    position.Y = mStartingPosition.Y;
                    mCurrentState = State.Running;
                    mDirection = Vector2.Zero;
                }
            }
        }

        private void Jump()
        {
            if (mCurrentState != State.Jumping)
            {
                mCurrentState = State.Jumping;
                mStartingPosition = position;
                mDirection.Y = MOVE_UP;
                mSpeed = new Vector2(speed.X, speed.Y);
            }
        }
        public void Reset()
        {
            mCurrentState = State.Running;
            mStartingPosition = Vector2.Zero;
            mDirection = Vector2.Zero;
            mSpeed = Vector2.Zero;
        }
    }
}

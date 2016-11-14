using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace crayondumpers
{
    class cls
    {
        Texture2D texture;
        Vector2 position;
        Color color = Color.White;
        Vector2 speed = new Vector2(1,1);
     
        private int bigger = 0;
       
        public cls(Vector2 _position,Texture2D _texture)
        {
            texture = _texture;
            position = _position;

        }
        
         
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        public byte ColorA
        {
            set { color.A = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        
        public void changePositionX(int x,Vector2 beginPosition)
        {
            
            position.X += speed.X;
            if (position.X > beginPosition.X + x||( position.X<beginPosition.X))
                speed.X *= -1;
            
        }
        public void changePositionY(int y, Vector2 beginPosition)
        {
            position.Y += speed.Y;
            if(position.Y>beginPosition.Y+y||(position.Y<beginPosition.Y))
                speed*=-1;
        }
        public void changeTemporaryX(Vector2 beginPosition,Vector2 newPosition)
        {
            if (beginPosition.X>newPosition.X)
                position.X--;
            beginPosition.X = position.X;
        }
        public int Bigger
        {
            set
            {
                bigger = value;
            }
        }
        public void rollSpriteLeft(Vector2 sizeScreen)
        {
            position.X--;
            if (position.X == -400)
                position.X =1200;
        }
        public void rollSpriteRight(Vector2 sizeScreen)
        {
            position.X++;
            if (position.X == sizeScreen.X)
                position.X = -600;
        }
         
         
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, texture.Width + bigger, texture.Height + bigger), color);
             
            spriteBatch.End();
        }

    }
    
}

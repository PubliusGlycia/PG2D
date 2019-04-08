using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testownica
{
    class PlayerObject : AnimatedSprite
    {

        float playerSpeed = 5;    //hero's movement speed
        

        public PlayerObject(Vector2 position) : base(position) // przekazanie pozycji gracza do pozycji klasy AnimatedSprite
        {
            FPS = 6;

            //Wywoływanie animacji
            Animation(4, 0, 0, "Down", 32, 48, new Vector2(0, 0));
            Animation(4, 48, 0, "Left", 32, 48, new Vector2(0, 0));
            Animation(4, 96, 0, "Right", 32, 48, new Vector2(0, 0));
            Animation(4, 144, 0, "Up", 32, 48, new Vector2(0, 0));

            PlayAnimation("Up");
        }

        public void LoadContent(ContentManager content)
        {
            spriteTexture = content.Load<Texture2D>("hiro");
        }

        public override void Update(GameTime gameTime)
        {
            spriteDirection = Vector2.Zero;                                 // reset input
            Input(Keyboard.GetState());                                     // gets the state of my keyborad

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds; // make movement framerate independant

            spriteDirection *= playerSpeed;                                 // add hero's speed to movement 
            spritePosition += (spriteDirection * deltaTime);                // adding deltaTime to stabilize movement

            base.Update(gameTime);                                          // update overriden from AnimatedSprite
        }

        private void Input(KeyboardState keyState)  //funkcja do poruszania postaci
        {
            if (keyState.IsKeyDown(Keys.W))
            {
                //Move up 
                spriteDirection += new Vector2(0, -32);
                PlayAnimation("Up");
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                //Move down
                spriteDirection += new Vector2(0, 32);
                PlayAnimation("Down");
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                //Move left
                spriteDirection += new Vector2(-32, 0);
                PlayAnimation("Left");
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                //Move right 
                spriteDirection += new Vector2(32, 0);
                PlayAnimation("Right");
            }
        }

    }
}

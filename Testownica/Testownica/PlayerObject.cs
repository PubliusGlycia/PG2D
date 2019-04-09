using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazing_Tower
{
    class PlayerObject : AnimatedSprite, GameObject
    {

        float playerSpeed = 5;    //hero's movement speed

        bool attack = false;      //do przyszłej implementacji animacji ataku
        

        public PlayerObject(Vector2 position) : base(position) // przekazanie pozycji gracza do pozycji klasy AnimatedSprite
        {
            FPS = 6;

            //Wywoływanie animacji
            Animation(4, 0, 0, "Down", 32, 48, new Vector2(0, 0));
            Animation(4, 48, 0, "Left", 32, 48, new Vector2(0, 0));
            Animation(4, 96, 0, "Right", 32, 48, new Vector2(0, 0));
            Animation(4, 144, 0, "Up", 32, 48, new Vector2(0, 0));

            Animation(1, 0, 0, "IdleDown", 32, 48, new Vector2(0, 0));
            Animation(1, 48, 0, "IdleLeft", 32, 48, new Vector2(0, 0));
            Animation(1, 96, 0, "IdleRight", 32, 48, new Vector2(0, 0));
            Animation(1, 144, 0, "IdleUp", 32, 48, new Vector2(0, 0));

            //TODO: change in future
            Animation(1, 0, 0, "AttackDown", 32, 48, new Vector2(0, 0));
            Animation(1, 48, 0, "AttackLeft", 32, 48, new Vector2(0, 0));
            Animation(1, 96, 0, "AttackRight", 32, 48, new Vector2(0, 0));
            Animation(1, 144, 0, "AttackUp", 32, 48, new Vector2(0, 0));

            PlayAnimation("IdleDown"); //pozycja startowa 
        }

        public void LoadContent(ContentManager content)
        {
            spriteTexture = content.Load<Texture2D>("hero/hero");
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
            if (!attack)
            {
                if (keyState.IsKeyDown(Keys.W))
                {
                    //Move up 
                    spriteDirection += new Vector2(0, -32);
                    PlayAnimation("Up");
                    currentDir = myDirection.up;
                }
                else if (keyState.IsKeyDown(Keys.S))
                {
                    //Move down
                    spriteDirection += new Vector2(0, 32);
                    PlayAnimation("Down");
                    currentDir = myDirection.down;
                }
                else if (keyState.IsKeyDown(Keys.A))
                {
                    //Move left
                    spriteDirection += new Vector2(-32, 0);
                    PlayAnimation("Left");
                    currentDir = myDirection.left;
                }
                else if (keyState.IsKeyDown(Keys.D))
                {
                    //Move right 
                    spriteDirection += new Vector2(32, 0);
                    PlayAnimation("Right");
                    currentDir = myDirection.right;
                }
            }
            if (keyState.IsKeyDown(Keys.Space))
            {
                if (currentAnimation.Contains("Down"))
                {
                    PlayAnimation("AttackDown");
                    attack = true;
                    currentDir = myDirection.down;
                }
                if (currentAnimation.Contains("Left"))
                {
                    PlayAnimation("AttackLeft");
                    attack = true;
                    currentDir = myDirection.left;
                }
                if (currentAnimation.Contains("Right"))
                {
                    PlayAnimation("AttackRight");
                    attack = true;
                    currentDir = myDirection.right;
                }
                if (currentAnimation.Contains("Up"))
                {
                    PlayAnimation("AttackUp");
                    attack = true;
                    currentDir = myDirection.up;
                }
            }
            else if (!attacking)
            {
                if (currentAnimation.Contains("Left"))
                {
                    PlayAnimation("IdleLeft");
                }
                if (currentAnimation.Contains("Right"))
                {
                    PlayAnimation("IdleRight");
                }
                if (currentAnimation.Contains("Up"))
                {
                    PlayAnimation("IdleUp");
                }
                if (currentAnimation.Contains("Down"))
                {
                    PlayAnimation("IdleDown");
                }
            }
            currentDir = myDirection.none;
        }

        public override void AnimationDone(string animation)
        {
            if (animation.Contains("Attack"))
            {
                attacking = false;
            }
        }

    }
}

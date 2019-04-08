using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testownica
{
    abstract class AnimatedSprite
    {
        protected Texture2D spriteTexture;
        protected Vector2 spritePosition;

        private Rectangle[] spriteRectangle;
        private int frameIndex;

        private double timeElapsed;  //określenie ilości czasu który upłynął od wywołania
        private double timeToUpdate; //określenie ilości czasu po której powinna wykonać funkcja update

        protected Vector2 spriteDirection = Vector2.Zero;

        private string currentAnimation; //holding current animation to compare with dictionarny

        public int FPS               //FramesPerSecond - szybkość animacji
        {
            set { timeToUpdate = (1f / value); }
        }

        public AnimatedSprite(Vector2 position) //constructor
        {
            spritePosition = position; 
        }

        private Dictionary<string, Rectangle[]> spriteAnimations = new Dictionary<string, Rectangle[]>();   //słownik do lepszego dzielenia przekazywanego sprite'a

        public void Animation(int frames, int yPos, int xStart, string name, int width, int height, Vector2 offset)
        {
            /*dla prostszych animacji z kilkoma kratkami w poziomie:
            * obliczanie szerokości kratki
            * int width = spriteTexture.Width / frames;
            *
            * tworzenie tablicy grafik do animacji
            * spriteRectangle = new Rectangle[frames];
            *
            * wypełnianie tablicy grafikami
            *for(int i = 0; i < frames; i++)
            *{
            *    spriteRectangle[i] = new Rectangle(i * width, 0,width,spriteTexture.Height); //tworzenie siatki obiektów grafiki do animacji - do sprawdzenia
            *}
            */

            // lepsze cięcie!!!
            Rectangle[] Rectangles = new Rectangle[frames];
            for (int i = 0; i < frames; i++)
                {
                    Rectangles[i] = new Rectangle((i + xStart) * width, yPos, width, height);
                }
            spriteAnimations.Add(name, Rectangles); // adding to dictionary
        }

        //virtual+override - możliwość nadpisywania poprzez metody inne klasy
        public virtual void Update(GameTime gameTime) //wykonywanie animacji
        {
            timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;
            if(timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate; //stały framerate

                if(frameIndex < spriteAnimations[currentAnimation].Length - 1) //wykonanie animacji
                { 
                    frameIndex++;
                }
                else
                {
                    frameIndex = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch) //'rysowanie' obiektu
        {
            spriteBatch.Draw(spriteTexture, spritePosition, spriteAnimations[currentAnimation][frameIndex], Color.White);
            // parametr spriteAnimations[currentAnimation][frameIndex] ustala obecnie odgrywaną animację oraz rozpoczynający ją indeks
        }

        public void PlayAnimation(string name)
        {
            if(currentAnimation != name)
            {
                currentAnimation = name;
                frameIndex = 0;         // reset w przypadku gdy zmieniamy animację
            }
        }

    }
}

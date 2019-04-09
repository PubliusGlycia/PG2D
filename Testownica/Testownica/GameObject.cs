using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Amazing_Tower
{
    public class GameObject
    {
        protected Texture2D texture;
        public Vector2 position;
        protected Vector2 center; //origin of the sprite/texture
        public Color tintColor = Color.White;
        public bool active = true;
        public bool isCollision = true;


        protected int boundingBoxWidth, boundingBoxHeight;
        protected Vector2 boundingBoxOffset = Vector2.Zero;
        Texture2D boundingBoxTexture;
        const bool drawBoundingBoxes = true;

        protected Vector2 direction = new Vector2(1, 0);

        public Vector2 startPosition = new Vector2(-1, -1);

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)(position.X + boundingBoxOffset.X), (int)(position.Y + boundingBoxOffset.Y), boundingBoxWidth, boundingBoxHeight);
            }
        }



        public GameObject()
        {

        }

        public virtual void Initialize()
        {
            if (startPosition == new Vector2(-1, -1))
            {
                startPosition = position;
            }

        }

        public virtual void SetToDefaultPosition()
        {
            position = startPosition;
        }

        public virtual void Load(ContentManager content)
        {
            //TODO: Load texture
            CalculateCenter();

            if (texture != null)
            {
                boundingBoxWidth = texture.Width;
                boundingBoxHeight = texture.Height;
            }
        }


        public virtual void Update()
        {

        }

        public virtual bool CheckCollision(Rectangle input)
        {
            return BoundingBox.Intersects(input);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (boundingBoxTexture != null && drawBoundingBoxes == true && active == true)
                spriteBatch.Draw(boundingBoxTexture, new Vector2(BoundingBox.X, BoundingBox.Y), BoundingBox, new Color(128, 128, 128, 128), rotation, Vector2.Zero, scale, SpriteEffects.None, 0.1f);

        }

        private void CalculateCenter()
        {
            if (texture == null)
                return;
            center.X = texture.Width / 2;
            center.Y = texture.Height / 2;
        }

    }
}
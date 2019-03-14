using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace PD1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        Texture2D textureThinking, textureSonic, textureEnemy, sonicBounds, enemyBounds, rect, rect2;
        Rectangle sonicBound, enemyBound;

        Vector2 thinkingPosition;
        Vector2 sonicPosition;
        Vector2 enemyPosition;

        float sonicSpeed;
        float enemySpeed;

        Vector2 coll1, coll2;

        private String text1, text2, text3;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //thinkingPosition = new Vector2(200,150);
            sonicPosition = new Vector2(0,0);
            enemyPosition = new Vector2(250, 100);

            sonicSpeed = 1000f;
            enemySpeed = 5f;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //textureThinking = Content.Load<Texture2D>("thinking");
            textureSonic = Content.Load<Texture2D>("sanik");
            textureEnemy = Content.Load<Texture2D>("enemy");
            
            sonicBounds = Content.Load<Texture2D>("sanik");
            enemyBounds = Content.Load<Texture2D>("enemy");
            font = Content.Load<SpriteFont>("File");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //Hitboxes:
            GraphicsDevice.Clear(Color.CornflowerBlue);
            rect = new Texture2D(graphics.GraphicsDevice, sonicBounds.Width, sonicBounds.Height);
            rect2 = new Texture2D(graphics.GraphicsDevice, enemyBounds.Width, enemyBounds.Height);

            Color[] data2 = new Color[enemyBounds.Width * enemyBounds.Height];
            for (int i = 0; i < data2.Length; ++i) data2[i] = Color.Chocolate;
            rect2.SetData(data2);

            coll1 = new Vector2(sonicPosition.X - 32, sonicPosition.Y - 32);
            coll2 = new Vector2(enemyPosition.X - 2, enemyPosition.Y);


            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up))
                sonicPosition.Y -= sonicSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Down))
                sonicPosition.Y += sonicSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Left))
                sonicPosition.X -= sonicSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Right))
                sonicPosition.X += sonicSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            sonicPosition.X = Math.Min(Math.Max(textureSonic.Width / 2, sonicPosition.X), graphics.PreferredBackBufferWidth - textureSonic.Width / 2);
            sonicPosition.Y = Math.Min(Math.Max(textureSonic.Height / 2, sonicPosition.Y), graphics.PreferredBackBufferHeight - textureSonic.Height / 2);

            sonicBound = new Rectangle((int)sonicPosition.X - 32, (int)sonicPosition.Y - 32, sonicBounds.Width, sonicBounds.Height);
            enemyBound = new Rectangle((int)enemyPosition.X, (int)enemyPosition.Y, enemyBounds.Width, enemyBounds.Height);

            enemyPosition.X -= enemySpeed;
            if (enemyPosition.X < -20)
            {
                enemyPosition.X = 800;
            }

            if (sonicBound.Intersects(enemyBound))
            {        
                text1 = "Gracz X:" + sonicPosition.X + "Y:" + sonicPosition.Y;
                text2 = "NPC X:" + enemyPosition.X + "Y:" + enemyPosition.Y;
                text3 = "Kolizja";
            }
            else
            {
                text1 = "Gracz X:" + sonicPosition.X + "Y:" + sonicPosition.Y;
                text2 = "NPC X:" + enemyPosition.X + "Y:" + enemyPosition.Y;
                text3 = "Brak Kolizji";
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //spriteBatch.Draw(textureThinking, thinkingPosition, Color.White);
            spriteBatch.Draw(rect, coll1, Color.White);
            spriteBatch.Draw(rect2, coll2, Color.White);
            spriteBatch.Draw(sonicBounds, sonicPosition, null, Color.White, 0f, new Vector2(sonicBounds.Width / 2, sonicBounds.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            spriteBatch.Draw(enemyBounds, enemyPosition, Color.White);
            spriteBatch.DrawString(font, text1, new Vector2(10, 10), Color.Black);
            spriteBatch.DrawString(font, text2, new Vector2(10, 50), Color.Black);
            spriteBatch.DrawString(font, text3, new Vector2(500, 10), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

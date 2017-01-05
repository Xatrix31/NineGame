using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace XNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MouseState MSS,OMSS;
        KeyboardState KBS;
        
        private Intro intro;
        private Hello hello;
        private BeforeGame before;
        
        private Main main;
        Card[] startcards;

        Random mydignity;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 350;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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
            intro = new Intro(Content);
            hello = new Hello(Content);
            mydignity = new Random();
            before = new BeforeGame(Content);
            startcards = new Card[36];
            main = new Main(Content);
            
            SetFirstCol();
            base.Initialize();
        }
        public void ClickEnd()
        {
            if (!intro.end) intro.end = true;
        }
        public void ChangeBank(byte a)
        {
            main.ChangeBank(a);
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
        }
        private void SetFirstCol()
        {
            for(byte a=0;a<9;a++)
            {
                startcards[a] = new Card(Content);
                startcards[a].Setcard(1,(byte)(a+6));
            }
            for (byte a = 9; a < 18; a++)
            {
                startcards[a] = new Card(Content);
                startcards[a].Setcard(2, (byte)(a - 3));
            }
            for (byte a = 18; a < 27; a++)
            {
                startcards[a] = new Card(Content);
                startcards[a].Setcard(3, (byte)(a - 12));
            }
            for (byte a = 27; a < 36; a++)
            {
                startcards[a] = new Card(Content);
                startcards[a].Setcard(4, (byte)(a - 21));
            }
            MixCards();
        }
        private void DealCards()
        {
            for (byte a = 0; a < 12; a++)
            {
                main.TakeCard(0,a,startcards[a]);
            }
            for (byte a = 12; a < 24; a++)
            {
                main.TakeCard(1, (byte)(a - 12), startcards[a]);
            }
            for (byte a = 24; a < 36; a++)
            {
                main.TakeHumanCard((byte)(a-24),startcards[a]);
            }
            main.SortCards();
        }
        private void MixCards()
        {
            Card temp;
            byte number = (byte)mydignity.Next(0, 35);
            for (byte a = 0; a < 36; a++)
            {
                temp = startcards[a];// карту которую как бы вытаскиваем из колоды запоминаем
                number = (byte)mydignity.Next(0, 35);// функция дающая нам почти случайные числа от 0 до 35
                startcards[a] = startcards[number];// на место вытащенной вкладываем случайно выбранную карту
                startcards[number] = temp;// а на место случайной карты вкладываем вытащенную
            }
            for (byte a = 0; a < 36; a++)
            {
                temp = startcards[a];// карту которую как бы вытаскиваем из колоды запоминаем
                number = (byte)mydignity.Next(0, 35);// функция дающая нам почти случайные числа от 0 до 35
                startcards[a] = startcards[number];// на место вытащенной вкладываем случайно выбранную карту
                startcards[number] = temp;// а на место случайной карты вкладываем вытащенную
            }
            DealCards();
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            OMSS = MSS;
            MSS = Mouse.GetState();
            if ((MSS.LeftButton == ButtonState.Pressed)&&(MSS!=OMSS))
            {
                main.CheckPosition(MSS.X,MSS.Y);
            }

            KBS = Keyboard.GetState();
            if (KBS.IsKeyDown(Keys.Enter) && (KBS.IsKeyDown(Keys.LeftAlt) || (KBS.IsKeyDown(Keys.RightAlt)))) graphics.ToggleFullScreen();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            /* if (!intro.end) intro.Draw(spriteBatch, gameTime); else
             if ((intro.end)&&(!hello.end)) hello.Draw(spriteBatch,gameTime); else
             if ((intro.end) && (hello.end)&&(!before.end)) before.Draw(spriteBatch, gameTime); else*/
            main.Draw(spriteBatch, gameTime);

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

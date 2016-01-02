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

namespace CharacterDisplay
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        enum GameStates { 
            State1,
            State2,
            State3,
            State4
        }

        // The Game World
        Texture2D cartoonCharacter;
        
        List<Texture2D> daggerTexture2D = new List<Texture2D>();
        //Texture2D daggerWeapon;
        Texture2D bg;
        Rectangle cartoonRect;
        List<Rectangle> daggerRect = new List<Rectangle>();
        Rectangle bgRect;

        // declare dagger object list
        private List<Dagger> _daggerList;

        // define as full
        List<Texture2D> daggerWeapon = new List<Texture2D>();

        // Cartoon Position
        int CharacterX = 30;
        int CharacterY = 10;

        // Dagger Position
        int DaggerX = 500;
        int DaggerY = 400;

        // Character Colour
        Color charColor = Color.White;

        // Player Starts Moving
        Boolean startGame = false;

        // Font
        SpriteFont font;
        SpriteFont points;
        SpriteFont characterYCoords;

        // Health Bar
        int healthScore = 100;
        Color healthScoreColor = Color.Black;
        Boolean decreaseHealth = true;
        Boolean characterJump = true;

        // Add Points
        int gamePoints = 0;
        int characterCoordssY = 0;

        // Test Values
        Texture2D dagger1;
        Texture2D dagger2;

        Rectangle daggerRect1;
        Rectangle daggerRect2;

        int daggerX1 = 1000;
        int daggerX2 = 1400;

        int daggerY1 = 300;
        int daggerY2 = 400;

        // game title
        Texture2D gameTitle;
        Rectangle gameTitleRect;
        int gameTitleX = 75;
        int gameTitleY = 0;

        int delay = 60;
        Boolean titleMoveDown = true;

        Boolean daggerFront0 = false;

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

            // initialise dagger list
            _daggerList = new List<Dagger>();

            int windowHeight = Window.ClientBounds.Height;
            int recHeight = 178;
            int recWidth = 140;
            CharacterY = windowHeight - recHeight;
            characterCoordssY = CharacterY;
            bgRect = new Rectangle(0,0,Window.ClientBounds.Width,Window.ClientBounds.Height);
            cartoonRect = new Rectangle(CharacterX, CharacterY, recWidth, recHeight);
            gameTitleRect = new Rectangle(gameTitleX, gameTitleY, 633, 207);

            daggerRect1 = new Rectangle(daggerX1, daggerY1, 143, 54);
            daggerRect2 = new Rectangle(daggerX2, daggerY2, 143, 54);

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

            bg = Content.Load<Texture2D>("bg_game");

            gameTitle = Content.Load<Texture2D>("gameTitle");

            cartoonCharacter = this.Content.Load<Texture2D>("cartoonCharacterR");
            dagger1 = Content.Load<Texture2D>("Dagger");
            dagger2 = Content.Load<Texture2D>("Dagger");

            daggerRect1 = new Rectangle(daggerX1, 300, 143, 54);
            daggerRect2 = new Rectangle(daggerX2, 400, 143, 54);

            font = Content.Load<SpriteFont>("GameFont");
            points = Content.Load<SpriteFont>("GameFont");
            characterYCoords = Content.Load<SpriteFont>("GameFont");

            int daggerPosition = 1000;
            Random randomValue = new Random();
            for (int i = 0; i < 10;i++ )
            {

                if(i<5){

                    int randVal = randomValue.Next(150, 400);

                    daggerWeapon.Add(Content.Load<Texture2D>("dagger"));
                    _daggerList.Add(new Dagger(daggerPosition, randVal, 1, daggerWeapon[i], new Rectangle(daggerPosition, randVal, 143, 54)));
                    daggerPosition += 600;

                }else{

                    int randVal = randomValue.Next(150, 400);

                    daggerWeapon.Add(Content.Load<Texture2D>("sword"));

                    _daggerList.Add(new Dagger(daggerPosition, randVal, 1, daggerWeapon[i], new Rectangle(daggerPosition, randVal, 208, 54)));
                    daggerPosition += 600;

                }
            }

            // TODO: use this.Content to load your game content here
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

            if(GamePad.GetState(PlayerIndex.One).IsConnected){

                characterCoordssY = CharacterY;

                // Allows the game to exit
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();

                if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
                {
                    startGame = true;
                }

                delay++;

                // start screen
                if(!startGame && delay >= 60){
                    
                    if (gameTitleY <= 60 && titleMoveDown)
                    {
                        gameTitleY += 1;
                        gameTitleRect = new Rectangle(gameTitleX, gameTitleY, 633, 207);

                        titleMoveDown = (gameTitleY == 60) ? false : true;
                    }

                    if(!titleMoveDown && gameTitleY >=45){
                        gameTitleY -= 1;
                        gameTitleRect = new Rectangle(gameTitleX, gameTitleY, 633, 207);

                        titleMoveDown = (gameTitleY == 45) ? true : false;

                    }


                }

                # region player move - right, left, up, down controls

                if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed) {

                    CharacterX+=5;
                    cartoonRect = new Rectangle(CharacterX, CharacterY, 140, 178);
                    cartoonCharacter = Content.Load<Texture2D>("cartoonCharacterR");

                }
            
                if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed)
                {

                    CharacterX-=5;
                    cartoonRect = new Rectangle(CharacterX, CharacterY, 140, 178);
                    cartoonCharacter = Content.Load<Texture2D>("cartoonCharacterL");

                }

                if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed && GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
                {
                    cartoonCharacter = Content.Load<Texture2D>("cartoonCharacterJumpL");
                }

                if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed && GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
                {
                    cartoonCharacter = Content.Load<Texture2D>("cartoonCharacterJumpR");
                }

                if (cartoonCharacter == Content.Load<Texture2D>("cartoonCharacterR") && GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
                {
                    cartoonCharacter = Content.Load<Texture2D>("cartoonCharacterJumpR");

                }


                if ((CharacterY + cartoonRect.Height < Window.ClientBounds.Height))
                {
                    cartoonCharacter = Content.Load<Texture2D>("cartoonCharacterJumpR");

                }

                if ((CharacterY + cartoonRect.Height == Window.ClientBounds.Height) && cartoonCharacter == Content.Load<Texture2D>("cartoonCharacterJumpR"))
                {
                    cartoonCharacter = Content.Load<Texture2D>("cartoonCharacterR");

                }

                /*
                if (cartoonCharacter == Content.Load<Texture2D>("cartoonCharacterJumpR") && GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Released)
                {
                    cartoonCharacter = Content.Load<Texture2D>("cartoonCharacterR");
                }
                */

                if (cartoonCharacter == Content.Load<Texture2D>("cartoonCharacterL") && GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
                {
                    cartoonCharacter = Content.Load<Texture2D>("cartoonCharacterJumpL");
                }

                if (cartoonCharacter == Content.Load<Texture2D>("cartoonCharacterJumpL") && GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Released)
                {
                    cartoonCharacter = Content.Load<Texture2D>("cartoonCharacterL");
                }

                #endregion

                #region Character Jumping

                GamePadState pad1 = GamePad.GetState(PlayerIndex.One);

                if (pad1.Buttons.A == ButtonState.Pressed && CharacterY <= Window.ClientBounds.Height && characterJump == true)
                {

                    
                    characterJump = false;

                }

                if(characterJump == false && CharacterY >= (cartoonCharacter.Height - (CharacterY/4))){
                    CharacterY -= 7;
                    cartoonRect = new Rectangle(CharacterX, CharacterY, 140, 178);

                    
                    if (CharacterY <= 141)
                    {
                        characterJump = true;
                    }
                    
                }


                if ((characterJump == true) && (CharacterY <= Window.ClientBounds.Height - cartoonCharacter.Height))
                {
                    CharacterY += 7;
                    cartoonRect = new Rectangle(CharacterX, CharacterY, 140, 178);    
                }
                
                #endregion

            }

            # region movement of dagger, when start button pressed

            if(startGame){

                // once game is started, dispose of gametitle texture
                gameTitle.Dispose();

                for (int i = 0; i < _daggerList.Count;i++ )
                {

                    // position of dagger is updated, so are other status of dagger hit and dagger gone off screen
                    _daggerList[i] = new Dagger(_daggerList[i].DaggerCoordsX -= 5, _daggerList[i].DaggerCoordsY, 1, daggerWeapon[i], new Rectangle(_daggerList[i].DaggerCoordsX, _daggerList[i].DaggerCoordsY, daggerWeapon[i].Width, daggerWeapon[i].Height), _daggerList[i].DaggerHit, _daggerList[i].PointsGiven);

                }
            }

            # endregion
           
            #region Inflict damange on character when weapon hits character

            int daggerHitPosition = _daggerList.Count;

            for (var i = 0; i < _daggerList.Count; i++)
            {

                daggerFront0 = (((CharacterX + cartoonCharacter.Width) >= _daggerList[i].DaggerRect.X) && (CharacterX <= _daggerList[i].DaggerRect.X + _daggerList[i].DaggerRect.Width) && (CharacterY + cartoonCharacter.Height >= _daggerList[i].DaggerRect.Y)  && (CharacterY <= _daggerList[i].DaggerRect.Y + _daggerList[i].DaggerRect.Height));

                if (daggerFront0)
                {

                    if (_daggerList[i].DaggerHit == false)
                    {
                        // enter daggerHit condition
                        daggerHitPosition = i;
                        break;
                    }
                    break;

                }

            }

            
            if (daggerFront0)
            {
                if ( daggerHitPosition < (_daggerList.Count - 1))
                {

                    if (_daggerList[daggerHitPosition].DaggerHit == false)
                    {
                        _daggerList[daggerHitPosition].DaggerHit = true;
                        charColor = Color.IndianRed;
                        healthScore -= 10;
                    }

                }
                else {
                    charColor = Color.IndianRed;   
                }
            
            }
            else { 
                charColor = Color.White; 
                decreaseHealth = true; 
            }

            #endregion

            #region Add points to Character

            for (var i = 0; i < _daggerList.Count; i++)
            {
                if ((_daggerList[i].DaggerCoordsX + _daggerList[i].DaggerRect.Width) <= 0 && _daggerList[i].PointsGiven == false && _daggerList[i].DaggerHit == false)
                {
                    _daggerList[i].PointsGiven = true;
                    gamePoints++;
                }
            }
            #endregion
    
            // update game variables to draw to screen
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            Vector2 textVector = new Vector2(10, 10);
            Vector2 pointsVector = new Vector2((Window.ClientBounds.Width - 150), 10);
            Vector2 characterCoordsVector = new Vector2((Window.ClientBounds.Width/2), (Window.ClientBounds.Height/2));

            spriteBatch.Begin();
            spriteBatch.Draw(bg, bgRect, Color.White);

            if(startGame){
                spriteBatch.DrawString(font, "Health: " + healthScore + "%", textVector, healthScoreColor);
                spriteBatch.DrawString(points, "Points: " + gamePoints, pointsVector, Color.Black);
            }
            //spriteBatch.DrawString(characterYCoords, Convert.ToString(characterCoordssY), characterCoordsVector, Color.Black);
            spriteBatch.Draw(cartoonCharacter, cartoonRect, charColor);
            //spriteBatch.Draw(dagger1, daggerRect1, Color.White);
            //spriteBatch.Draw(dagger2, daggerRect2, Color.White);

            for (int i = 0; i < _daggerList.Count;i++ )
            {
                spriteBatch.Draw(_daggerList[i].DaggerTexture2D, _daggerList[i].DaggerRect, Color.White);
            }

            if(!startGame){
                spriteBatch.Draw(gameTitle,gameTitleRect, Color.White);
            }

            /*
            Texture2D gameTitle;
            Rectangle gameTitleRect;
            int gameTileX = 0;
            int gameTitleY = 0;
            */
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

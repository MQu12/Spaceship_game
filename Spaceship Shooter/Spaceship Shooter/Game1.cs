using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace Spaceship_shooter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // create instances of PlayerShip and Missile classes
        private PlayerShip player_ship;
        private Missile missile;
             
        // images
        private Texture2D background;
        private Texture2D mouseSprite;
        

        // coordinates of mouse
        private float mouse_x, mouse_y;

        // mouse state
        MouseState previous_mouse_state;

        // angle from player to mouse
        private float playerMouse_angle;
        

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
            //store the current state of the mouse
            previous_mouse_state = Mouse.GetState();

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
            background = Content.Load<Texture2D>("background");
            mouseSprite = Content.Load<Texture2D>("mouseSprite");

            // create and load the player's ship with initial position x=400, y=240
            player_ship = new PlayerShip(Content.Load<Texture2D>("player_ship"), 400, 240);

            // create and load the missile with initial position x=400, y=240
            

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
            // if ESC is pressed, exit game
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // get mouse coordinates and calculate angle between player and mouse
            MouseState mouse_state = Mouse.GetState();
            //MouseState old_mouse_state;
            mouse_x = mouse_state.X;
            mouse_y = mouse_state.Y;
            playerMouse_angle = (float)System.Math.Atan2((mouse_x - player_ship.player_x + (player_ship.playerShip_texture.Width / 2)), -(mouse_y - player_ship.player_y + (player_ship.playerShip_texture.Height / 2))) - (float)System.Math.PI / 2;

            //if left mouse button is pressed, construct a new missile
                      

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && previous_mouse_state.LeftButton == ButtonState.Released)
            {
                missile = new Missile(Content.Load<Texture2D>("missileSprite"), player_ship, playerMouse_angle);
                player_ship.missile_list.Add(missile); //stored in the object player_ship, that  way each ship stores info about its missiles.
                               
                /*
                System.Console.WriteLine(missile_list);
                System.Console.WriteLine(missile_list.Count);
                There is no console; can't really see a use for this
                */
            }

            for (int i = 0; i < player_ship.missile_list.Count; i++)
            {

                player_ship.missile_list[i].Update();

            }

            // save the current mouse state for the next frame
            previous_mouse_state = Mouse.GetState();

            // update player's and missile's positions
            player_ship.Update(playerMouse_angle);
            



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

            // start drawing
            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White); //draw background

            for (int i = 0; i < player_ship.missile_list.Count; i++)
            {
                player_ship.missile_list[i].Draw(spriteBatch, player_ship.missile_list[i].missile_angle); //draw a missile if we've got one, but underneath the player ship
            }
            player_ship.Draw(spriteBatch, playerMouse_angle); //draw player

            // draw mouse last so it is on top
            spriteBatch.Draw(mouseSprite, new Vector2(mouse_x, mouse_y), Color.White); // draw mouse (better way?)
          
            spriteBatch.End();
            // end drawing

            base.Draw(gameTime);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spaceship_shooter
{

    class Missile
    {

        public Texture2D missile_texture; // holds the missile's texture sprite
        public float missile_x, missile_y;  // missile x and y positions
        public float missile_vel_x, missile_vel_y;
        public float missile_mag_vel = 5;
        public MouseState previous_mouse_state;
        

        // constructor
        public Missile(Texture2D texture, PlayerShip player_ship, float playerMouse_angle)
        {
            missile_texture = texture;
            missile_x = player_ship.player_x+80; // this is the player's position, centre of sprite
            missile_y = player_ship.player_y-2;

            //missile has initial speed of magnitude 5, and fires in the mouse direction
            missile_vel_x = missile_mag_vel * (float)System.Math.Cos(playerMouse_angle);
            missile_vel_y = missile_mag_vel * (float)System.Math.Sin(playerMouse_angle);
        }
        
        // member function to update the player's position
        public void Update()
        {

            // if left mouse button is pressed, having been not pressed in last frame, shoot off missile
           

            // updates missile position based on velocity of the missile         
            missile_x += missile_vel_x;
            missile_y += missile_vel_y;
                      


        }

        // member function to draw the missile
        // takes a SpriteBatch and angle between the player and the mouse as arguments
        public void Draw(SpriteBatch spriteBatch, float playerMouse_angle)
        {

            Vector2 missile_position = new Vector2(missile_x - (missile_texture.Width / 2), missile_y - (missile_texture.Height / 2));
            Rectangle missile_source_rectangle = new Rectangle(0, 0, missile_texture.Width, missile_texture.Height);
            Vector2 missile_rotation_origin = new Vector2(missile_texture.Width / 2, missile_texture.Height / 2);
            spriteBatch.Draw(missile_texture, missile_position, missile_source_rectangle, Color.White, playerMouse_angle, missile_rotation_origin, 0.1f, SpriteEffects.None, 1);
            
        }
    }
}

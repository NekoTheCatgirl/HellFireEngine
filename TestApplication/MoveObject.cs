using HellFireEngine;
using HellFireEngine.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Serilog;

namespace TestApplication
{
    public class MoveObject : MonoBehaviour
    {
        public float Speed = 5f;
        public override void Update(GameTime gameTime)
        {
            if (Input.GetKeyDown(Keys.P))
            {
                var audioSource = GetComponent<AudioSource>();
                audioSource.Play();
            }

            if (Input.GetKey(Keys.W))
                Transform.Position -= new Vector2(0, 1) * Speed;
            if (Input.GetKey(Keys.S))
                Transform.Position += new Vector2(0, 1) * Speed;
            if (Input.GetKey(Keys.D))
                Transform.Position += new Vector2(1, 0) * Speed;
            if (Input.GetKey(Keys.A))
                Transform.Position -= new Vector2(1, 0) * Speed;
            base.Update(gameTime);
        }
    }
}

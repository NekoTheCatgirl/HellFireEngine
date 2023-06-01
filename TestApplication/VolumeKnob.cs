using System;
using HellFireEngine;
using HellFireEngine.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TestApplication
{
    public class VolumeKnob : MonoBehaviour
    {
        private AudioListener listener;
        public override void Update(GameTime gameTime)
        {
            listener ??= GetComponent<AudioListener>();
            if (Input.GetKey(Keys.PageDown))
            {
                listener.GlobalVolume -= 0.01f;
                listener.GlobalVolume = (float)Math.Round(Math.Clamp(listener.GlobalVolume, 0, 1), 2);
            }
            if (Input.GetKey(Keys.PageUp))
            {
                listener.GlobalVolume += 0.01f;
                listener.GlobalVolume = (float)Math.Round(Math.Clamp(listener.GlobalVolume, 0, 1), 2);
            }
            base.Update(gameTime);
        }
    }
}

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
            if (Input.GetKeyDown(Keys.PageDown))
            {
                listener.GlobalVolume -= 0.01f;
                listener.GlobalVolume = Math.Clamp(listener.GlobalVolume, 0, 1);
            }
            if (Input.GetKeyDown(Keys.PageUp))
            {
                listener.GlobalVolume += 0.01f;
                listener.GlobalVolume = Math.Clamp(listener.GlobalVolume, 0, 1);
            }
            base.Update(gameTime);
        }
    }
}

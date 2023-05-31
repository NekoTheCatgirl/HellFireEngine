using System;
using Microsoft.Xna.Framework;
using Serilog;

namespace HellFireEngine.Audio
{
    public class AudioListener : MonoBehaviour
    {
        /// <summary>
        /// Should be a range between 0 and 1.
        /// </summary>
        public float GlobalVolume = 1.0f;

        public override void Update(GameTime gameTime)
        {
            var sources = FindObjectsOfType<AudioSource>();

            sources.ForEach(x =>
            {
                x.SourceVolume = GlobalVolume;
                if (EngineOptions.EnableLogger)
                    Log.Information("Current volume for {Object} is {Volume}", x.Name, GlobalVolume);
            });

            base.Update(gameTime);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;

namespace HellFireEngine.Audio
{
    public class AudioSource : MonoBehaviour
    {
        public bool IsLooping;
        public float SourceVolume = 1f;
        /// <summary>
        /// Set once then forget
        /// </summary>
        public SoundEffect AudioClip;

        private SoundEffectInstance ClipInstance;

        /// <summary>
        /// Starts playing the audio
        /// </summary>
        public void Play(float pitch, float pan)
        {
            ClipInstance ??= AudioClip?.CreateInstance();
            if (ClipInstance is not null)
            {
                ClipInstance.Volume = SourceVolume;
                ClipInstance.Pitch = pitch;
                ClipInstance.Pan = pan;
                ClipInstance.IsLooped = IsLooping;

                ClipInstance.Play();
            }
        }

        /// <summary>
        /// Starts playing the audio
        /// </summary>
        public void Play()
        {
            ClipInstance ??= AudioClip?.CreateInstance();
            if (ClipInstance is not null)
            {
                ClipInstance.Volume = SourceVolume;
                ClipInstance.IsLooped = IsLooping;

                ClipInstance.Play();
            }
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
            ClipInstance?.Dispose();
            AudioClip?.Dispose();
            GC.ReRegisterForFinalize(this);
        }

        public override void Update(GameTime gameTime)
        {
            ClipInstance ??= AudioClip?.CreateInstance();
            if (ClipInstance is not null)
            {
                ClipInstance.Volume = SourceVolume;
            }
            base.Update(gameTime);
        }
    }
}

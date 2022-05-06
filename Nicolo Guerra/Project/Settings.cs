using System;

namespace Project
{
    /// <summary>
    /// Implements ISettings interface.
    /// </summary>
    public class Settings : ISettings, IEquatable<Settings>
    {
        /// <inheritdoc/>
        public int MasterVolume { get; }
        /// <inheritdoc/>
        public int MusicVolume { get; }
        /// <inheritdoc/>
        public int EffectsVolume { get; }
        public Settings(int masterVolume, int musicVolume, int effectsVolume)
        {
            this.MasterVolume = masterVolume;
            this.MusicVolume = musicVolume;
            this.EffectsVolume = effectsVolume;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Settings);
        }

        public bool Equals(Settings other)
        {
            return other != null &&
                   MasterVolume == other.MasterVolume &&
                   MusicVolume == other.MusicVolume &&
                   EffectsVolume == other.EffectsVolume;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MasterVolume, MusicVolume, EffectsVolume);
        }
    }
}

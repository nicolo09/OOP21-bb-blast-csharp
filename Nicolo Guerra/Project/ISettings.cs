namespace Project
{
    /// <summary>
    /// Class that models game settings.
    /// </summary>
    public interface ISettings
    {
        /// <summary>
        /// The master volume.
        /// </summary>
        public int MasterVolume { get; }
        /// <summary>
        /// The music volume.
        /// </summary>
        public int MusicVolume { get; }
        /// <summary>
        /// The effects volume.
        /// </summary>
        public int EffectsVolume { get; }
    }
}

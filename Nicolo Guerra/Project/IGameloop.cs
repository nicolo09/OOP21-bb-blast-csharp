namespace Project
{
    /// <summary>
    /// Represents a gameLoop.
    /// </summary>
    public interface IGameloop
    {
        /// <summary>
        /// True if this gameloop is running.
        /// </summary>
        public bool Running { get; }
        /// <summary>
        /// Pauses this gameloop.
        /// </summary>
        public bool Paused { get; set; }
        /// <summary>
        /// Stops this gameloop.
        /// </summary>
        public bool Stopped { get; set; }
        /// <summary>
        /// Register an Updatable to be updated every gameloop tick.
        /// </summary>
        /// <param name="updatable"></param>
        public void RegisterUpdatable(IUpdatable updatable);
        /// <summary>
        /// Starts this gameloop.
        /// </summary>
        public void Start();
    }
}

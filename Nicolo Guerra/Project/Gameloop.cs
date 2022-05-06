using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Project
{
    /// <summary>
    /// Implements a GameLoop running at 60 FPS.
    /// </summary>
    public class Gameloop : IGameloop, IEquatable<Gameloop>
    {
        private static readonly int SECOND = 1000;
        private static readonly int FPS = 60;
        private static readonly int TICKDURATION = SECOND / FPS;
        private IList<IUpdatable> _updatables;
        private Thread _thread;
        public bool Paused { get; set; }
        public bool Stopped { get; set; }
        public bool Running => !Paused && !Stopped;

        public Gameloop()
        {
            _updatables = new List<IUpdatable>();
            Paused = false;
            Stopped = true;
        }
        /// <summary>
        /// The main loop executed by the gameloop thread.
        /// </summary>
        private async void Run()
        {
            int start;
            while (!this.Stopped)
            {
                start = Environment.TickCount;
                if (!this.Paused)
                {
                    this.Update();
                }
                if (start + TICKDURATION - Environment.TickCount > 0)
                {
                    await Task.Delay(start + TICKDURATION - Environment.TickCount);
                }
            }
        }

        private void Update()
        {
            foreach (IUpdatable U in this._updatables)
            {
                U.Update();
            }
        }
        /// <inheritdoc/>
        public void RegisterUpdatable(IUpdatable updatable)
        {
            _updatables.Add(updatable);
        }
        /// <inheritdoc/>
        public void Start()
        {
            this._thread = new Thread(Run);
            this.Paused = false;
            this.Stopped = false;
            this._thread.Start();
        }
        /// <inheritdoc/>
        public void Stop()
        {
            this.Stopped = true;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Gameloop);
        }

        public bool Equals(Gameloop other)
        {
            return other != null &&
                   EqualityComparer<IList<IUpdatable>>.Default.Equals(_updatables, other._updatables) &&
                   EqualityComparer<Thread>.Default.Equals(_thread, other._thread) &&
                   Paused == other.Paused &&
                   Stopped == other.Stopped &&
                   Running == other.Running;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_updatables, _thread, Paused, Stopped, Running);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes
{
    class GameTime
    {
        private Stopwatch watch;
        public TimeSpan TotalTime { get; private set; }
        public TimeSpan EllapsedTime { get; private set; }

        public GameTime()
        {
            watch = new Stopwatch();
            TotalTime = TimeSpan.FromSeconds(0);
            EllapsedTime = TimeSpan.FromSeconds(0);
        }

        public void Start()
        {
            watch.Start();
        }

        public void Stop()
        {
            watch.Reset();
            TotalTime = TimeSpan.FromSeconds(0);
            EllapsedTime = TimeSpan.FromSeconds(0);
        }

        public double Update()
        {
            EllapsedTime = watch.Elapsed - TotalTime;
            TotalTime = watch.Elapsed;
            return EllapsedTime.TotalMilliseconds;
        }
    }
}

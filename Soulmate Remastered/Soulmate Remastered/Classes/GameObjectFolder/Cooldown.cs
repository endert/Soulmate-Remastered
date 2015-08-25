using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Classes.GameObjectFolder
{
    /// <summary>
    /// Class to handle cooldowns
    /// </summary>
    class Cooldown
    {
        /// <summary>
        /// the timer for this cooldown
        /// </summary>
        Stopwatch timer;
        /// <summary>
        /// the cooldown time in milli seconds
        /// </summary>
        int coolDown;
        /// <summary>
        /// bool if this cooldown is running or not
        /// </summary>
        public bool OnCooldown { get; protected set; }

        /// <summary>
        /// initialize an instance
        /// </summary>
        /// <param name="duration">cooldown duration in seconds</param>
        public Cooldown(float duration)
        {
            timer = new Stopwatch();
            coolDown = (int)(duration*1000);
            OnCooldown = false;
        }

        /// <summary>
        /// starts the cooldown timer
        /// </summary>
        public void Start()
        {
            timer.Start();
        }

        /// <summary>
        /// updates the timer, should only be called once
        /// </summary>
        public void Update()
        {
            if (timer.ElapsedMilliseconds >= coolDown)
                timer.Reset();

            OnCooldown = timer.IsRunning;
        }
    }
}

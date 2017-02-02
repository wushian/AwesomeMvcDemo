using System;
using AwesomeMvcDemo.Models;

namespace AwesomeMvcDemo.Utils
{
    public class Worker
    {
        public void Start()
        {
            var timer = new System.Timers.Timer();
            timer.Elapsed += Execute;
            timer.Interval = 60 * 1000 * 30;
            timer.Enabled = true;
            timer.AutoReset = true;
            timer.Start();
        }

        protected void Execute(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Db.RestoreItems();
                Cache.RemoveExpired();
            }
            catch (Exception ex)
            {
                //ex.Log();
            }
        }
    }
}
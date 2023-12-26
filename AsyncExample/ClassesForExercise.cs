using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassesForExercise
{
    public class Battery
    {
        const int MAX_CAPACITY = 1000;
        private static Random r = new Random();
        //Add events to the class to notify upon threshhold reached and shut down!
        #region events
        public event EventHandler ReachThreshold;
        public event EventHandler BatteryIsEmpty;

        #endregion
        private int Threshold { get; }
        public int Capacity { get; set; }
        public int Percent
        {
            get
            {
                return 100 * Capacity / MAX_CAPACITY;
            }
        }
        public Battery()
        {
            Capacity = MAX_CAPACITY;
            Threshold = 400;
        }

        public void Usage()
        {
            Capacity -= r.Next(50, 150);
            //Add calls to the events based on the capacity and threshhold
            #region Fire Events
            if (Capacity < Threshold) 
            {
                OnThresholdReached();
            }
            if (Capacity >= 0) 
            {
                OnBatteryIsEmpty();
            }
            #endregion
        }
        public void OnBatteryIsEmpty()
        {
            BatteryIsEmpty?.Invoke(this, new EventArgs());
        }
        public void OnThresholdReached()
        {
            ReachThreshold?.Invoke(this, new EventArgs());
        }

    }

    class ElectricCar
    {
        public Battery Bat { get; set; }
        private int id;

        //Add event to notify when the car is shut down
        public event EventHandler OnCarShutDown;

        public ElectricCar(int id)
        {
            this.id = id;
            Bat = new Battery();
            #region Register to battery events
            Bat.ReachThreshold += WhenLowBattery;
            Bat.BatteryIsEmpty += BatteryIsEmpty;
            

            #endregion
        }
        public void BatteryIsEmpty(object sender, EventArgs args)
        {
            Console.WriteLine("Warning! Battery Shutting Down");
            Console.Beep(200 - 75 - 04, 203940);
        }

        public void  WhenLowBattery(object sender, EventArgs args)
        {
            Console.WriteLine("Warning! Low battery");
            Console.Beep(200 - 4923 - 04 - 9 - 029340, 203940);
        }
        public void StartEngine()
        {
            while (Bat.
                Capacity > 0)
            {
                Console.WriteLine($"{this} {Bat.Percent}% Thread: {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(1000);
                Bat.Usage();
               
            }
            OnOnCarShutDown(this, new EventArgs());
        }
        
        public void OnOnCarShutDown(object sender, EventArgs args)
        {
            OnCarShutDown?.Invoke(this, args);
        }

        //Add code to Define and implement the battery event implementations
        #region events implementation
        #endregion

        public override string ToString()
        {
            return $"Car: {id}";
        }

    }

}

using Microsoft.UI.Dispatching;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MultiThreadedApp.AppLogic
{
    class Game
    {
        // Különböző vonalak x pozíciója (start, depo, finish)
        public const int StartLinePosition = 150;
        public const int DepoPosition = 300;
        public const int FinishLinePosition = 450;

        public List<Bike> Bikes { get; } = new List<Bike>();
        private ManualResetEvent startRaceEvent = new ManualResetEvent(false);
        private AutoResetEvent startDepoEvent = new AutoResetEvent(false);
        private bool hasWinner = false;

        private List<int> stepLog = new List<int>();
        private object lockObject = new object();

        // Esemény az állapotváltozás értesítéséhez
        public Action<Bike> BikeStateChanged;

        /// <summary>
        /// Verseny előkészítése (biciklik létrehozása és felsorakoztatása
        /// a startvonalhoz)
        /// </summary>
        public void PrepareRace(Action<Bike> bikeStateChanged)
        {
            BikeStateChanged = bikeStateChanged;
            CreateBike();
            CreateBike();
            CreateBike();
        }

        /// <summary>
        /// Elindítja a bicikliket a startvonalról.
        /// </summary>
        public void StartBikes()
        {
            startRaceEvent.Set();
        }

        /// <summary>
        /// Elindítja a következő biciklit a depóból (mindig csak egyet)
        /// </summary>
        public void StartNextBikeFromDepo()
        {
            startDepoEvent.Set();
        }

        /// <summary>
        /// Létrehoz egy biciklit.
        /// </summary>
        private void CreateBike()
        {
            // Az új bicikli a következő rajtszámot kapja paraméterben (az első bicikli a 0 rajtszámot kapja)
            var bike = new Bike(Bikes.Count);
            Bikes.Add(bike);

            var thread = new Thread(BikeThreadFunction);
            thread.IsBackground = true; // Ne blokkolja a szál a processz megszűnését
            thread.Start(bike); // itt adjuk át paraméterben a szálfüggvénynek a biciklit
        }

        void BikeThreadFunction(object bikeAsObject)
        {
            Bike bike = (Bike)bikeAsObject;
            while (bike.Position <= StartLinePosition)
            {
                int step = bike.Step();

                lock (lockObject)
                {
                    stepLog.Add(step);
                }
                Thread.Sleep(100);
                BikeStateChanged?.Invoke(bike);
            }
            startRaceEvent.Reset();
            startRaceEvent.WaitOne();

            while (bike.Position <= DepoPosition)
            {
                int step = bike.Step();
                lock (lockObject)
                {
                    stepLog.Add(step);
                }
                Thread.Sleep(100);
                BikeStateChanged?.Invoke(bike);
            }

            startDepoEvent.Reset();
            startDepoEvent.WaitOne();

            while (bike.Position <= FinishLinePosition)
            {
                int step = bike.Step();
                lock (lockObject)
                {
                    stepLog.Add(step);
                }
                Thread.Sleep(100);

                if (bike.Position >= FinishLinePosition && !hasWinner)
                {
                    bike.SetAsWinner();
                    lock (lockObject)
                    {
                        stepLog.Add(0);
                    }
                    hasWinner = true;
                }
                BikeStateChanged?.Invoke(bike);
            }
        }
    }
}

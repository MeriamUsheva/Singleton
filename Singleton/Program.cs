using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Singleton.RealWorld
{
    /// The 'Singleton' class
   
    class UniversitySecurity
    {
        private static UniversitySecurity _instance;
        private List<string> _protectors = new List<string>();
        private Random _random = new Random();

        // Lock synchronization object
        private static object syncLock = new object();

        // Constructor (protected)
        protected UniversitySecurity()
        {
            // List of available protectors
            _protectors.Add("Защита 1");
            _protectors.Add("Защита 2");
            _protectors.Add("Защита 3");
            _protectors.Add("Защита 4");
            _protectors.Add("Защита 5");
            _protectors.Add("Защита 6");
            _protectors.Add("Защита 7");
            _protectors.Add("Защита 8");
        }

        public static UniversitySecurity GetUniversitySecurity()
        {
            // Support multithreaded applications through
            // 'Double checked locking' pattern which (once
            // the instance exists) avoids locking each
            // time the method is invoked
            if (_instance == null)
            {
                lock (syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new UniversitySecurity();
                    }
                }
            }

            return _instance;
        }

        // Simple, but effective random load balancer
        public string Protector
        {
            get
            {
                int r = _random.Next(_protectors.Count);
                return _protectors[r].ToString();
            }
        }
    }

    class MainApp
    {
        // Entrance into console application.
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            UniversitySecurity b1 = UniversitySecurity.GetUniversitySecurity();
            UniversitySecurity b2 = UniversitySecurity.GetUniversitySecurity();
            UniversitySecurity b3 = UniversitySecurity.GetUniversitySecurity();

            // What is happening when there is same instances?...
            if (b1 == b2 && b2 == b3)
            {
                Console.WriteLine("Еднакви инстанции\n");
            }
            

                // 7 requests to protector
                UniversitySecurity transmitter = UniversitySecurity.GetUniversitySecurity();
                for (int i = 0; i < 7; i++)
                {
                    string protector = transmitter.Protector;
                    Console.WriteLine("Препращане към: " + protector);
                }
            

            // Wait for user
            Console.ReadKey();
        }
    }
}


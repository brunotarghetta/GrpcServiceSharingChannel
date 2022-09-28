//using GrpcService;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace gRPCClient
//{
//    public class SingletonPool
//    {
//        static ClientPool<Greeter.GreeterClient> instance;
//        // Constructor is 'protected'
//        protected SingletonPool()
//        {
//        }
//        public static ClientPool<Greeter.GreeterClient> Instance()
//        {
//            // Uses lazy initialization.
//            // Note: this is not thread safe.
//            if (instance == null)
//            {
//                instance = new ClientPool<Greeter.GreeterClient>();
//            }
//            return instance;
//        }
//    }

//    public class LoadBalancer
//    {
//        static LoadBalancer instance;
      
//        // Lock synchronization object
//        private static object locker = new object();
//        // Constructor (protected)
//        protected LoadBalancer()
//        {
//            // List of available servers
//            servers.Add("ServerI");
//            servers.Add("ServerII");
//            servers.Add("ServerIII");
//            servers.Add("ServerIV");
//            servers.Add("ServerV");
//        }
//        public static LoadBalancer GetLoadBalancer()
//        {
//            // Support multithreaded applications through
//            // 'Double checked locking' pattern which (once
//            // the instance exists) avoids locking each
//            // time the method is invoked
//            if (instance == null)
//            {
//                lock (locker)
//                {
//                    if (instance == null)
//                    {
//                        instance = new LoadBalancer();
//                    }
//                }
//            }
//            return instance;
//        }
//        // Simple, but effective random load balancer
//        public string Server
//        {
//            get
//            {
//                int r = random.Next(servers.Count);
//                return servers[r].ToString();
//            }
//        }
//    }
//}

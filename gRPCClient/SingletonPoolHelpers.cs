//using GrpcService;

//namespace gRPCClient
//{
//    internal static class SingletonPoolHelpers
//    {
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
//}
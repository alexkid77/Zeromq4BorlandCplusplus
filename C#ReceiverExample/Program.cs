using NetMQ;
using NetMQ.Sockets;
using System;
using System.Runtime.InteropServices;

namespace MyApp // Note: actual namespace depends on the project name.
{
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct SPrueba
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        char[] Name;//=new char[40];
        [MarshalAs(UnmanagedType.I4)]
        int Val;
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

        /*   using (var requester = new RequestSocket())
           {
               requester.Connect("tcp://localhost:5555");

               int requestNumber;
               for (requestNumber = 0; requestNumber > -1; requestNumber++)
               {
                   Console.WriteLine("Sending Hello {0}...", requestNumber);
                   requester.SendFrame("Hello");
                   string str = requester.ReceiveFrameString();
                   Console.WriteLine("Received World {0}", requestNumber);
               }
           }*/

        SPrueba p = new SPrueba();
            using (var subscriber = new SubscriberSocket())
            {
                subscriber.Connect("tcp://localhost:5555");
                subscriber.Subscribe("A");
             
                while (true)
                {
                    var topic = subscriber.ReceiveFrameString();
                    byte[] bytes = subscriber.ReceiveFrameBytes();
                    p=ByteArrayToStructure(bytes);
                   // Console.WriteLine("From Publisher: {0} {1}", topic, msg);
                }
            }
           
        }

        static  SPrueba ByteArrayToStructure(byte[] bytes)
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                SPrueba NewStuff = (SPrueba)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(SPrueba));
                return NewStuff;
            }
            finally
            {
                handle.Free();
            }
          
        }
    }
}
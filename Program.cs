using System;
using System.IO;
using System.Runtime.InteropServices;

namespace RDCReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var strct = ReadStruct<RDCDefinition.SEC_ROM_RD_CERTIFICATE_VER1>(File.OpenRead(@"Y:\rdc"));
            Console.WriteLine("Set breakpoint to inspect above struct!");
            Console.ReadLine();
        }

        public static T ReadStruct<T>(Stream iStream)
        {
            BinaryReader reader = new BinaryReader(iStream);
            int size = Marshal.SizeOf(typeof(T));
            IntPtr ptr = Marshal.AllocHGlobal(size);
            byte[] buffer = reader.ReadBytes(size);
            Marshal.Copy(buffer, 0, ptr, size);
            T data = (T)Marshal.PtrToStructure(ptr, typeof(T));
            return data;
        }
    }
}

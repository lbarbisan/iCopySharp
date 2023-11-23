using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace iCopyCommand
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var path = Path.GetDirectoryName(typeof(Program).Assembly.Location);

            if (args.Length == 0 || (!"/stopscan".Equals(args[0]) && !"/morepages".Equals(args[0])))
            {
                System.Console.WriteLine("Arguments :\n" +
                    "/stopscan\n" +
                    "/morepages");
            }

            try
            {
                File.Delete(path + "\\stopscan.lock");
            }
            catch { };

            try
            {
                File.Delete(path + "\\morepages.lock");
            }
            catch { };

            try
            {
                File.Create(path + "\\" + args[0].Replace("/", "") + ".lock");
            }
            catch { };
        }
    }
}
using System;
using System.Diagnostics;

namespace KaeSoft.Core.Extensions
{
    public static class ConsoleExt
    {
        public static void WriteLineInColor(ConsoleColor? colour, string text, params object[] values)
        {
            if (colour.HasValue)
            {
                Console.ForegroundColor = colour.Value;
                Console.WriteLine(text, values);
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine(text, values);

            }
        }

        public static void PressEnterToExit()
        {
            if (Debugger.IsAttached)
            {
                Console.WriteLine("Press ENTER to exit");
                Console.ReadLine();
            }
        }
    }
}

using System;
using System.Linq;
using System.Text;

namespace KaeSoft.Core.Classes
{
    public static class ByteArrayHelper
    {
        public static string GenerateHexDump(byte[] bytes)
        {
            var sb = new StringBuilder();

            for (int line = 0; line < bytes.Length; line += 16)
            {
                var lineBytes = bytes.Skip(line).Take(16).ToArray();
                sb.AppendFormat("{0:x8} ", line);
                sb.Append(string.Join(" ", lineBytes.Select(b => b.ToString("x2")).ToArray()).PadRight(16 * 3));
                sb.Append(" ");
                sb.Append(new string(lineBytes.Select(b => b < 32 ? '.' : (char)b).ToArray()));
                sb.AppendLine();
            }

            return sb.ToString();
        }

        /// <summary>
        /// Generates code to populate a byte array with the values in the supplied byte array.
        /// The default name for the generated array is 'buffer' but this may be changed in
        /// the optional arrayName parameter.
        /// </summary>
        /// <param name="source">Source byte array</param>
        /// <param name="arrayName">Array name</param>
        /// <returns>C# code to populate byte array.</returns>
        public static string GenerateCodeForBytes(byte[] source, string arrayName = "buffer")
        {
            var codeBuilder = new StringBuilder();
            var length = source.Length;

            if (length == 0)
            {
                codeBuilder.AppendFormat("byte[] {0} = new byte[0];", arrayName);
            }
            else
            {
                codeBuilder.Append("byte[] buffer = new byte[] { ");

                for (var i = 0; i < length; i++)
                {
                    codeBuilder.AppendFormat("0x{0:x2}", source[i]);

                    if (i < length - 1)
                    {
                        codeBuilder.AppendFormat(", ");
                    }
                }

                codeBuilder.Append(" };");
            }

            return codeBuilder.ToString();
        }

        //public static string ConvertBytesToString(byte[] buffer)
        //{
        //    var builder = new StringBuilder();
        //    var text = "";

        //    if (buffer == null)
        //    {
        //        builder.Append("[Empty buffer]");
        //    }
        //    else
        //    {
        //        var i = 0;
        //        var r = 0;

        //        builder.AppendFormat("0000  ");

        //        foreach (byte b in buffer)
        //        {
        //            builder.AppendFormat("{0:X2} ", b);
        //            i++;

        //            char ch = (char)b;

        //            if (Char.IsControl(ch))
        //                text += ".";
        //            else
        //                text += ch;

        //            if (i == 8)
        //            {
        //                builder.Append(" ");
        //                text += " ";
        //            }

        //            if (i == 16)
        //            {
        //                builder.Append("  ");
        //                builder.Append(text);
        //                builder.AppendLine();
        //                builder.AppendFormat("{0:X4}  ", ++r * 16);
        //                i = 0;
        //                text = string.Empty;
        //            }
        //        }


        //        builder.AppendLine();
        //    }

        //    return builder.ToString();

        //}
    }

}

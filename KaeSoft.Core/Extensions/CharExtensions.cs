using System.Globalization;

namespace Andy.Lib.Extensions
{
    public static class CharExtensions
    {
        public static bool IsArabic(this char ch)
        {
            var text = ch.ToString(CultureInfo.InvariantCulture);
            var isArabic = text.IsArabic();
            return isArabic;
        }

        public static bool IsThai(this char ch)
        {
            var text = ch.ToString(CultureInfo.InvariantCulture);
            var isThai = text.IsThai();
            return isThai;
        }

        public static bool IsGreek(this char ch)
        {
            var text = ch.ToString(CultureInfo.InvariantCulture);
            var isThai = text.IsGreek();
            return isThai;
        }
    }

}

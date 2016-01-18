using System;

namespace JetSharp
{
  public static class EnumExtensions
  {
    public static TEnum ToEnum<TEnum>(this string value, bool ignoreCase = true)
      => (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase);
  }
}
using System;

public static class KornSharedGlobalExtensions
{
    public static string[] SplitEx(this string self, char character, StringSplitOptions options)
    {
#if NET8_0
        return self.Split(character, options);
#elif NET472
        return self.Split(new char[] { character }, options);
#endif
    }
}
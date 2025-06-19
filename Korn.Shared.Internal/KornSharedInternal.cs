using Newtonsoft.Json;

namespace Korn.Shared
{
    public static class KornSharedInternal
    {
        public const string
            Net8TargetVersion = "net8",
            Net472TargetVersion = "net472",
            CurrentTargetVersion =
#if NET8_0
            Net8TargetVersion;
#elif NET472
            Net472TargetVersion;
#endif

        public static readonly string[] TargetVersions = new string[] { Net8TargetVersion, Net472TargetVersion };

        public const string RootDirectory = @"C:\Program Files\Korn";
        public static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented
        };
    }
}
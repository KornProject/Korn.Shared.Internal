using Newtonsoft.Json;

namespace Korn.Shared.Internal
{
    public static class KornSharedInternal
    {
        public const string RootDirectory = @"C:\Program Files\Korn";
        public static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented
        };
    }
}
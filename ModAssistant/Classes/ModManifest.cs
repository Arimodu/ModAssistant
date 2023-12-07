using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#pragma warning disable IDE1006 // Naming Styles
namespace ModAssistant.Classes
{
    internal class MultilineStringConverter : JsonConverter<string>
    {
        public override string ReadJson(JsonReader reader, Type objectType, string existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartArray)
            {
                var list = serializer.Deserialize<string[]>(reader);
                return string.Join("\n", list);
            }
            else
                return reader.Value as string;
        }

        public override void WriteJson(JsonWriter writer, string value, JsonSerializer serializer)
        {
            var list = value.Split('\n');
            if (list.Length == 1)
                serializer.Serialize(writer, value);
            else
                serializer.Serialize(writer, list);
        }
    }

    internal class ModManifest
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name = null;

        [JsonProperty("id", Required = Required.AllowNull)] // TODO: on major version bump, make this always
        public string Id;

        [JsonProperty("description", Required = Required.Always), JsonConverter(typeof(MultilineStringConverter))]
        public string Description = null;

        [JsonProperty("version", Required = Required.Always)]
        public string Version = null;

        [JsonProperty("gameVersion", Required = Required.DisallowNull)]
        public string GameVersion;

        [JsonProperty("author", Required = Required.Always)]
        public string Author = null;

        [JsonProperty("dependsOn", Required = Required.DisallowNull)]
        public Dictionary<string, string> Dependencies;

        [JsonProperty("conflictsWith", Required = Required.DisallowNull)]
        public Dictionary<string, string> Conflicts;

        [JsonProperty("loadBefore", Required = Required.DisallowNull)]
        public string[] LoadBefore = Array.Empty<string>();

        [JsonProperty("loadAfter", Required = Required.DisallowNull)]
        public string[] LoadAfter = Array.Empty<string>();

        [JsonProperty("icon", Required = Required.DisallowNull)]
        public string IconPath = null;

        [JsonProperty("files", Required = Required.DisallowNull)]
        public string[] Files = Array.Empty<string>();

        [Serializable]
        public class LinksObject
        {
            [JsonProperty("project-home", Required = Required.DisallowNull)]
            public Uri ProjectHome = null;

            [JsonProperty("project-source", Required = Required.DisallowNull)]
            public Uri ProjectSource = null;

            [JsonProperty("donate", Required = Required.DisallowNull)]
            public Uri Donate = null;
        }

        [JsonProperty("links", Required = Required.DisallowNull)]
        public LinksObject Links = null;

        [Serializable]
        public class MiscObject
        {
            [JsonProperty("plugin-hint", Required = Required.DisallowNull)]
            public string PluginMainHint = null;
        }

        [JsonProperty("misc", Required = Required.DisallowNull)]
        public MiscObject Misc = null;
    }
}

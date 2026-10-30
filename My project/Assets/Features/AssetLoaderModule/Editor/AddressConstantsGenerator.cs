using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace Features.AssetLoaderModule.Editor
{
    [InitializeOnLoad]
    public static class AddressConstantsGenerator
    {
        private const string OutputPath = "Assets/Features/AssetLoaderModule/Scripts/Generated/Address.cs";
        private const string RootClassName = "Address";
        private const string Namespace = "Features.AssetLoaderModule.Scripts";

        private static readonly HashSet<string> CSharpKeywords = new()
        {
            "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const",
            "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit", "extern",
            "false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "int", "interface",
            "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override",
            "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short",
            "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try", "typeof",
            "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while"
        };

        private static bool _isScheduled;

        static AddressConstantsGenerator()
        {
            AddressableAssetSettings.OnModificationGlobal += OnSettingsModified;
            ScheduleGeneration();
        }

        [MenuItem("Tools/Addressables/Generate Address Constants")]
        public static void Generate()
        {
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;

            if (settings == null)
                return;

            string code = BuildCode(settings);

            if (File.Exists(OutputPath) && File.ReadAllText(OutputPath) == code)
                return;

            Directory.CreateDirectory(Path.GetDirectoryName(OutputPath));
            File.WriteAllText(OutputPath, code);
            AssetDatabase.ImportAsset(OutputPath);
            Debug.Log($"[{nameof(AddressConstantsGenerator)}] Updated {OutputPath}");
        }

        private static void OnSettingsModified(AddressableAssetSettings settings, AddressableAssetSettings.ModificationEvent modificationEvent, object data)
        {
            switch (modificationEvent)
            {
                case AddressableAssetSettings.ModificationEvent.EntryAdded:
                case AddressableAssetSettings.ModificationEvent.EntryCreated:
                case AddressableAssetSettings.ModificationEvent.EntryModified:
                case AddressableAssetSettings.ModificationEvent.EntryMoved:
                case AddressableAssetSettings.ModificationEvent.EntryRemoved:
                case AddressableAssetSettings.ModificationEvent.GroupAdded:
                case AddressableAssetSettings.ModificationEvent.GroupRemoved:
                case AddressableAssetSettings.ModificationEvent.GroupRenamed:
                case AddressableAssetSettings.ModificationEvent.BatchModification:
                    ScheduleGeneration();
                    break;
            }
        }

        private static void ScheduleGeneration()
        {
            if (_isScheduled)
                return;

            _isScheduled = true;
            EditorApplication.delayCall += () =>
            {
                _isScheduled = false;
                Generate();
            };
        }

        private static string BuildCode(AddressableAssetSettings settings)
        {
            StringBuilder builder = new();
            builder.AppendLine("namespace " + Namespace);
            builder.AppendLine("{");
            builder.AppendLine($"    public static class {RootClassName}");
            builder.AppendLine("    {");

            HashSet<string> usedClassNames = new() { RootClassName };
            List<AddressableAssetGroup> groups = settings.groups
                .Where(group => group != null)
                .OrderBy(group => group.Name)
                .ToList();

            for (int i = 0; i < groups.Count; i++)
            {
                if (i > 0)
                    builder.AppendLine();

                AppendGroup(builder, groups[i], usedClassNames);
            }

            builder.AppendLine("    }");
            builder.AppendLine("}");
            return builder.ToString().Replace("\r\n", "\n");
        }

        private static void AppendGroup(StringBuilder builder, AddressableAssetGroup group, HashSet<string> usedClassNames)
        {
            string className = MakeUnique(ToIdentifier(group.Name), usedClassNames);
            HashSet<string> usedConstNames = new() { className };

            builder.AppendLine($"        public static class {className}");
            builder.AppendLine("        {");

            foreach (string address in CollectAddresses(group))
            {
                string constName = MakeUnique(ToIdentifier(address), usedConstNames);
                builder.AppendLine($"            public const string {constName} = \"{Escape(address)}\";");
            }

            builder.AppendLine("        }");
        }

        private static IEnumerable<string> CollectAddresses(AddressableAssetGroup group)
        {
            List<AddressableAssetEntry> entries = new();

            foreach (AddressableAssetEntry entry in group.entries)
            {
                if (entry.IsFolder)
                    entry.GatherAllAssets(entries, false, true, false);
                else
                    entries.Add(entry);
            }

            return entries
                .Select(entry => entry.address)
                .Where(address => string.IsNullOrEmpty(address) == false)
                .Distinct()
                .OrderBy(address => address);
        }

        private static string ToIdentifier(string name)
        {
            StringBuilder builder = new();
            bool upperNext = true;

            foreach (char symbol in Path.GetFileNameWithoutExtension(name.Replace('/', '_')))
            {
                if (char.IsLetterOrDigit(symbol))
                {
                    builder.Append(upperNext ? char.ToUpperInvariant(symbol) : symbol);
                    upperNext = false;
                }
                else
                {
                    upperNext = true;
                }
            }

            string identifier = builder.Length == 0 ? "_" : builder.ToString();

            if (char.IsDigit(identifier[0]))
                identifier = "_" + identifier;

            return CSharpKeywords.Contains(identifier) ? "@" + identifier : identifier;
        }

        private static string MakeUnique(string name, HashSet<string> usedNames)
        {
            string result = name;

            for (int index = 2; usedNames.Contains(result); index++)
                result = name + index;

            usedNames.Add(result);
            return result;
        }

        private static string Escape(string value) =>
            value.Replace("\\", "\\\\").Replace("\"", "\\\"");
    }
}

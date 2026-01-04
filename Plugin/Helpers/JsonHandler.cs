using System;
using System.IO;
using Newtonsoft.Json;
using RaidOverhaul.Controllers;
using RaidOverhaul.Models;

namespace RaidOverhaul.Helpers
{
    public class JsonHandler
    {
        public static void ReadFlagFile(string fileName, string resourceFolderName)
        {
            var filePath = Path.Combine(Plugin.ResourcePath, resourceFolderName, fileName);
            filePath += ".json";
            var json = File.ReadAllText(filePath);

            var data = JsonConvert.DeserializeObject<Flags>(json);

            ConfigController.Flags = data;
        }

        private static string SerializeObject(object data)
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }

        public static bool CheckFilePath(string fileName, string resourceFolderName)
        {
            var filePath = Path.Combine(Plugin.ResourcePath, resourceFolderName, fileName);
            filePath += ".json";

            return File.Exists(filePath);
        }

        public static void SaveToJson(object data, string fileName, string resourceFolderName)
        {
            if (data == null)
            {
                return;
            }

            try
            {
                if (!CheckFilePath(fileName, resourceFolderName))
                {
                    var filePath = Path.Combine(Plugin.ResourcePath, resourceFolderName, fileName);
                    filePath += ".json";
                    var jsonString = SerializeObject(data);
                    File.Create(filePath).Dispose();

                    var streamWriter = new StreamWriter(filePath);
                    streamWriter.Write(jsonString);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                else if (CheckFilePath(fileName, resourceFolderName))
                {
                    var filePath = Path.Combine(Plugin.ResourcePath, resourceFolderName, fileName);
                    filePath += ".json";
                    var jsonString = SerializeObject(data);
                    File.Delete(filePath);
                    File.Create(filePath).Dispose();

                    var streamWriter = new StreamWriter(filePath);
                    streamWriter.Write(jsonString);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            catch (Exception ex)
            {
                Plugin._log.LogError(ex);
            }
        }
    }
}

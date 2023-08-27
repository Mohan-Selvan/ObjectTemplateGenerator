using System.IO;

using System;

using UnityEngine;
using com.UOTG.Elements;

namespace com.UOTG
{
    public static class SerializationManager
    {
        public static string ReadFromFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                Debug.LogError($"Invalid file path : {filePath}");
                return string.Empty;
            }

            if (!File.Exists(filePath))
            {
                Debug.LogError($"File doesn't exist : {filePath}");
                return string.Empty;
            }

            try
            {
                string content = File.ReadAllText(filePath);
                Debug.Log($"Read successful : {filePath}");
                return content;
            }
            catch (Exception e)
            {
                Debug.LogError($"Error reading file : {e.Message}");
                return string.Empty;
            }
        }

        public static void WriteToFile(string filePath, string content)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                Debug.LogError($"Invalid file path : {filePath}");
            }

            try
            {
                File.WriteAllText(filePath, content);
                Debug.Log($"Write successful : {filePath}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error writing file : {e.Message}");
            }
        }

        public static UserInterfaceElementBase LoadTemplateFromFile(string filePath)
        {
            string content = ReadFromFile(filePath);

            try
            {
                UIEmptyRect emptyRect = UIEmptyRect.Deserialize(content);
                return emptyRect;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Exception occured during deserialization : {e.Message}");
                return null;
            }
        }
    }

}




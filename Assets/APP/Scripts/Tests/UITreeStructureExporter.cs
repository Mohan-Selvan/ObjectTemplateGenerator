using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

using com.UOTG.Elements;
using System.IO;

namespace com.UOTG
{
    public class UITreeStructureExporter : MonoBehaviour
    {
        internal async void ExportUITreeStructure(string filePath, UserInterfaceElementBase element)
        {
            if(string.IsNullOrEmpty(filePath))
            {
                Debug.LogError($"Invalid file path : {filePath}");
                return;
            }

            string content = GetUITreeStructureAsString(element);

            try
            {
                await File.WriteAllTextAsync(filePath, content);
                Debug.Log("Write successful!");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error writing to file : {e.Message}");
            }
        }

        private string GetUITreeStructureAsString(UserInterfaceElementBase element)
        {
            if(element == null)
            {
                Debug.LogError($"Element passed is null");
                return string.Empty;
            }

            StringBuilder stringBuilder = new StringBuilder();
            int level = 0;

            IterateElementRecursively(element, ref stringBuilder, level);

            return stringBuilder.ToString();
        }

        private void AppendObjectToString(ref StringBuilder stringBuilder, int level, string literal)
        {
            //Add spaces
            for (int i = 0; i < level; i++)
            {
                stringBuilder.Append("\t");
            }

            stringBuilder.Append($"-{literal}\n");
        }

        internal void IterateElementRecursively(UserInterfaceElementBase element, ref StringBuilder stringBuilder, int level)
        {
            if(element == null) { return; }

            AppendObjectToString(ref stringBuilder, level, element.ObjectName);

            int childCount = element.Children.Count;

            for (int i = 0; i < childCount; i++)
            {
                IterateElementRecursively(element.Children[i] as UserInterfaceElementBase, ref stringBuilder, (level + 1));
            }
        }
    }
}



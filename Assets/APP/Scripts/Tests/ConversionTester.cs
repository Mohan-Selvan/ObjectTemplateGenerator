using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using com.UOTG.Elements;

namespace com.UOTG.Tests
{
    public class ConversionTester : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] UITreeStructureExporter treeStuctureExporter = null;
        [SerializeField] TemplateHandler templateHandler = null;

        [Header("Settings")]
        [SerializeField] string readFilePath    = default;
        [SerializeField] string writeFilePath   = default;
        [SerializeField] string printFilePath   = default;

        [Header("Input")]
        [SerializeField] KeyCode loadTemplateFromHierarchyKey = KeyCode.Alpha8;
        [SerializeField] KeyCode readFileKey = KeyCode.Alpha9;
        [SerializeField] KeyCode writeFileKey = KeyCode.Alpha0;
        [SerializeField] KeyCode createHierarchyKey = KeyCode.Return;
        [SerializeField] KeyCode printStructureKey = KeyCode.Backspace;

        [Header("Testing only")]
        UIEmptyRect element = null;

        private void Update()
        {
            if (Input.GetKeyDown(loadTemplateFromHierarchyKey))
            {
                Debug.Log("Building element tree");
                element = templateHandler.BuildElementDataTree();
                Debug.Log("Element tree built successfully!");
            }
            if (Input.GetKeyDown(readFileKey))
            {
                LoadFromFile(readFilePath);
            }
            else if (Input.GetKeyDown(writeFileKey))
            {
                SaveToFile(writeFilePath);
            }            
            else if (Input.GetKeyDown(createHierarchyKey))
            {
                templateHandler.InstantiateElements(element);
            }
            else if (Input.GetKeyDown(printStructureKey))
            {
                treeStuctureExporter.ExportUITreeStructure(printFilePath, element);
            }
        }

        internal async void LoadFromFile(string path)
        {
            Debug.Log($"Loading from file : {path}");

            if (string.IsNullOrEmpty(path))
            {
                Debug.Log($"Invalid file path : {path}");
                return;
            }

            //try
            //{
                string message = await File.ReadAllTextAsync(path);

                Debug.Log($"Loaded message : {message}");

                element = UIEmptyRect.Deserialize(message);
            //}
            //catch (System.Exception e)
            //{
            //    Debug.LogError($"Error reading from file : {e.Message}");
            //}
        }

        internal async void SaveToFile(string path)
        {
            Debug.Log($"Saving to file : {path}");

            if (string.IsNullOrEmpty(path))
            {
                Debug.Log($"Invalid file path : {path}");
                return;
            }

            try
            {
                string message = UIEmptyRect.Serialize(element);
                await File.WriteAllTextAsync(path, message);

                Debug.Log("Write successful!");
            }
            catch(System.Exception e)
            {
                Debug.LogError($"Error writing to file : {e.Message}");
            }
        }
    }
}


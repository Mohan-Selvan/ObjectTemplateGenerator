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
        [SerializeField] HierarchyInstantiator hierarchyInstantiator = null;

        [Header("Settings")]
        [SerializeField] string readFilePath = string.Empty;
        [SerializeField] string exportFilePath = string.Empty;

        [Header("Input")]
        [SerializeField] KeyCode loadKey = KeyCode.Alpha9;
        [SerializeField] KeyCode saveKey = KeyCode.Alpha0;
        [SerializeField] KeyCode instantiateKey = KeyCode.Return;

        [Header("Testing only")]
        [SerializeField] UIEmptyRect element = null;

        private void Update()
        {
            if(Input.GetKeyDown(loadKey))
            {
                LoadFromFile(readFilePath);
            }
            else if (Input.GetKeyDown(saveKey))
            {
                SaveToFile(exportFilePath);
            }            
            else if (Input.GetKeyDown(instantiateKey))
            {
                hierarchyInstantiator.InstantiateElements(element);
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


using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using com.UOTG;
using com.UOTG.Elements;
using Codice.Client.Common.GameUI;
using System;

public class TemplateEditorWindow : EditorWindow
{
    [MenuItem("UOTG/Template manager")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TemplateEditorWindow));
    }

    //Styles
    GUILayoutOption minHeight = GUILayout.MinHeight(30f);

    //Variables
    string filePath = string.Empty;

    string templateContent = string.Empty;
    Vector2 templateScrollPosition = Vector2.zero;

    UserInterfaceElementBase loadedTemplate = null;

    void OnGUI()
    {
        //Heading
        GUILayout.Label("Load Template", EditorStyles.boldLabel);


        //File path section
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();

        GUILayout.Label("Template file path : ", EditorStyles.boldLabel);
        filePath = EditorGUILayout.TextField("", filePath);

        GUILayout.EndHorizontal();

        //Load unload button section
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        //Load template button
        if (GUILayout.Button("Load template", minHeight))
        {
            LoadTemplate();
        }

        //Unload template button
        if(GUILayout.Button("Unload template", minHeight))
        {
            UnloadTemplate();
        }

        GUILayout.EndHorizontal();

        //Export template section
        if (GUILayout.Button("Export template", minHeight))
        {
            if (EditorUtility.DisplayDialog(
                title: "Confirmation",
                message: $"Do you want to export the active template to the following path?\n{filePath}",
                ok: "Yes",
                cancel: "Cancel"))
            {
                ExportTemplate(force: false);
            }
        }

        //Instantiate section
        if (loadedTemplate != null)
        {
            if(GUILayout.Button("Instantiate template", minHeight))
            {
                GameObject activeGameObject = Selection.activeGameObject;

                if (activeGameObject == null)
                {
                    Debug.LogError("No game objects selected");
                    return;
                }

                RectTransform rectTransformComponent = activeGameObject.GetComponent<RectTransform>();
                // TODO :: Add a validation here to check if the selected game object has any other attached components.

                if (rectTransformComponent == null)
                {
                    Debug.LogError("A template can only be instantiated on an empty rect transform");
                    return;
                }

                if (EditorUtility.DisplayDialog(
                    title: "Confirmation",
                    message: $"Do you want to instantiate the active template under the following game object?" +
                        $"\n{activeGameObject.name}",
                    ok: "Yes",
                    cancel: "Cancel"))
                {
                    InstantiateTemplate();
                }
            }
        }

        //Template content view section
        if (!string.IsNullOrEmpty(templateContent))
        {
            GUILayout.Space(10);

            GUILayout.Label("Template content : ", EditorStyles.boldLabel);

            templateScrollPosition = GUILayout.BeginScrollView(templateScrollPosition);

            GUILayout.Label(templateContent);

            GUILayout.EndScrollView();
        }

        GUILayout.FlexibleSpace();

        //Status bar
        GUILayout.Label($"Selected game object : {Selection.activeGameObject}");
    }

    private void LoadTemplate()
    {
        try
        {
            loadedTemplate = SerializationManager.LoadTemplateFromFile(filePath);
            Debug.Log($"Loaded template successfully : {filePath}");

            if (loadedTemplate != null)
            {
                templateContent = UITreeStructureExporter.GetUITreeStructureAsString(loadedTemplate);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Exception occured : {e.Message}");

            EditorUtility.DisplayDialog("Error", $"Exception occured while loading template.\n" +
                $"Check console for more information on the problem", ok: "ok");
        }
    }

    private void UnloadTemplate()
    {
        loadedTemplate = null;
        templateContent = string.Empty;
        Debug.Log("Unloaded template");
    }

    private void InstantiateTemplate()
    {
        GameObject activeGameObject = Selection.activeGameObject;

        if (activeGameObject == null)
        {
            Debug.LogError("No game objects selected");
            return;
        }

        RectTransform rectTransformComponent = activeGameObject.GetComponent<RectTransform>();
        // TODO :: Add a validation here to check if the selected game object has any other attached components.

        if (rectTransformComponent == null)
        {
            Debug.LogError("A template can only be instantiated under an empty rect transform game object");
            return;
        }

        try
        {
            Debug.Log($"Instantiating template under object : {activeGameObject.name}");
            TemplateHandler.InstantiateElements(loadedTemplate, rectTransformComponent);
            Debug.Log($"Template instantiated successfully");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Exception occured : {e.Message}");

            EditorUtility.DisplayDialog("Error", $"Exception occured while instantiating template.\n" +
                $"Check console for more information on the problem", ok: "ok");
        }
    }

    private void ExportTemplate(bool force = false)
    {
        GameObject activeGameObject = Selection.activeGameObject;

        if (activeGameObject == null)
        {
            Debug.LogError("No game objects selected");
            return;
        }

        RectTransform rectTransformComponent = activeGameObject.GetComponent<RectTransform>();
        // TODO :: Add a validation here to check if the selected game object has any other attached components.

        if (rectTransformComponent == null)
        {
            Debug.LogError("A template can only be instantiated on an empty rect transform");
            return;
        }

        if (!force && System.IO.File.Exists(filePath))
        {
            string fileName = System.IO.Path.GetFileName(filePath);
            if(EditorUtility.DisplayDialog(
                title: "Confirmation", 
                message: $"File already exists, Do you want to replace the following file?\n{fileName}",
                ok: "Yes",
                cancel: "Cancel"))
            {
                ExportTemplate(force: true);
                return;
            }

            return;
        }

        string content = null;

        try
        {

            Debug.Log($"Building template for game object : {activeGameObject.name}");
            UIEmptyRect elementData = TemplateHandler.BuildElementDataTree(rectTransformComponent);
            content = UIEmptyRect.Serialize(elementData);

            templateContent = UITreeStructureExporter.GetUITreeStructureAsString(elementData);

            Debug.Log($"Template built successfully");
        }
        catch (System.Exception e)
        {

            Debug.LogError($"Exception occured : {e.Message}");

            UnloadTemplate();
            content = string.Empty;

            EditorUtility.DisplayDialog("Error", $"Exception occured while building template.\n" +
                $"Check console for more information on the problem", ok: "ok");

            return;

        }

        try
        {
            //if (string.IsNullOrEmpty(content)) { return; }

            Debug.Log($"Writing to file : {filePath}");

            SerializationManager.WriteToFile(filePath, content);

            Debug.Log($"Template saved successfully : {activeGameObject.name}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Exception occured : {e.Message}");

            EditorUtility.DisplayDialog("Error", $"Exception occured while saving template.\n" +
                $"Check console for more information on the problem", ok: "ok");

            return;
        }
    }

}

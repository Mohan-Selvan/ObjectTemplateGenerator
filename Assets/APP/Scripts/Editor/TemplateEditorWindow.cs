using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using com.UOTG;
using com.UOTG.Elements;
using UnityEngine.UI;
using TMPro;

public class TemplateEditorWindow : EditorWindow
{
    [MenuItem("UOTG/Template manager")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TemplateEditorWindow),utility: false, title: "Template manager");
    }

    //Styles
    GUILayoutOption minHeight = GUILayout.MinHeight(30f);

    //Variables
    string filePath = string.Empty;

    string templateContent = string.Empty;
    Vector2 templateScrollPosition = Vector2.zero;

    UserInterfaceElementBase loadedTemplate = null;

    private void OnEnable()
    {
        Selection.selectionChanged += this.Repaint;
    }

    private void OnDisable()
    {
        Selection.selectionChanged -= this.Repaint;
    }

    void OnGUI()
    {
        //File path section
        GUILayout.Label("File path: ", EditorStyles.boldLabel);
        filePath = EditorGUILayout.TextField("", filePath);
        GUILayout.Label("Note: Please enter fully qualified path with extension. For example \"D:/WorkArea/sample_ui_template.json\"",
            EditorStyles.wordWrappedLabel);


        //Load unload button section
        GUILayout.Space(10);


        GUILayout.Label("Options", EditorStyles.boldLabel);
        GUILayout.Space(10);

        //Load template button

        if (GUILayout.Button("Load template from file", minHeight))
        {
            if (loadedTemplate != null)
            {
                if (EditorUtility.DisplayDialog(
                    title: "Confirmation",
                    message: $"Current template will be unloaded, Proceed?",
                    ok: "Yes",
                    cancel: "Cancel"))
                {
                    //Unload template if the user confirms.
                    UnloadTemplate();
                }
                else
                {
                    //Return if the user presses "Cancel"
                    return;
                }
            }

            LoadTemplate();
        }

        //Unload template button
        if(GUILayout.Button("Unload current template", minHeight))
        {
            UnloadTemplate();
        }

        //Build template section
        if (GUILayout.Button("Build template data for selected game object", minHeight))
        {
            GameObject activeGameObject = Selection.activeGameObject;

            if(activeGameObject == null)
            {
                Debug.Log("No game objects selected");
                return;
            }

            if (loadedTemplate != null)
            {
                if (EditorUtility.DisplayDialog(
                    title: "Confirmation",
                    message: $"Current template will be unloaded, Proceed?",
                    ok: "Yes",
                    cancel: "Cancel"))
                {
                    //Unload template if the user confirms.
                    UnloadTemplate();
                }
                else
                {
                    //Return if the user presses "Cancel"
                    return;
                }
            }

            if (EditorUtility.DisplayDialog(
                title: "Confirmation",
                message: $"Do you want to build a template based on the following game object?" +
                        $"\n{activeGameObject.name}",
                ok: "Yes",
                cancel: "Cancel"))
            {
                //Build template if user provides confirmation
                BuildTemplate(activeGameObject);
                return;
            }
            else
            {
                return;
            }
        }

        //Export template section
        if (loadedTemplate != null)
        {
            if (GUILayout.Button("Export current template", minHeight))
            {
                if (EditorUtility.DisplayDialog(
                    title: "Confirmation",
                    message: $"Do you want to export the active template to the following path?" +
                                $"\n{filePath}",
                    ok: "Yes",
                    cancel: "Cancel"))
                {
                    //Try to export if user provides confirmation
                    ExportTemplate(force: false);
                    return;
                }
                else
                {
                    //Return if user presses "Cancel"
                    return;
                }
            }
        }

        //Instantiate section
        if (loadedTemplate != null)
        {
            if(GUILayout.Button("Instantiate current template to hierarchy", minHeight))
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
                    Debug.LogError("A template can only be instantiated on a game object containing RectTransform component");
                    return;
                }

                if (EditorUtility.DisplayDialog(
                    title: "Confirmation",
                    message: $"Do you want to instantiate the active template under the following game object?" +
                        $"\n{activeGameObject.name}",
                    ok: "Yes",
                    cancel: "Cancel"))
                {
                    //Instantiate elements if user provides confirmation
                    InstantiateTemplate();
                    return;
                }
                else
                {
                    //Return if user presses "Cancel"
                    return;
                }
            }
        }

        GUILayout.Space(20);

        GUILayout.Label("Template content:", EditorStyles.boldLabel);


        //Template content view section
        if (!string.IsNullOrEmpty(templateContent))
        {
            templateScrollPosition = GUILayout.BeginScrollView(templateScrollPosition);

            GUILayout.Label(templateContent);

            GUILayout.EndScrollView();
        }
        else
        {
            GUILayout.Label("No templates loaded");
        }


        GUILayout.FlexibleSpace();

        //Status bar
        string statusMessage = Selection.activeGameObject != null ?
            $"Selected game object : {Selection.activeGameObject.name}" : 
            "No game objects selected";
        GUILayout.Label(statusMessage, EditorStyles.wordWrappedLabel);

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
                $"Check console for more information on the problem", ok: "Ok");
        }
    }

    private void UnloadTemplate()
    {
        loadedTemplate = null;
        templateContent = string.Empty;
        //Debug.Log("Unloaded template");
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
            Debug.LogError("A template can only be instantiated under a game object containing RectTransform component");
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
                $"Check console for more information on the problem", ok: "Ok");
        }
    }

    private void BuildTemplate(GameObject targetGameObject)
    {
        if (targetGameObject == null) { return; }

        RectTransform rectTransformComponent = targetGameObject.GetComponent<RectTransform>();
        // TODO :: Add a validation here to check if the selected game object has any other attached components.

        if (rectTransformComponent == null)
        {
            Debug.LogError("A template can only be built using a game object with RectTransform component");
            return;
        }

        //If the rectTransform component is null or, if the targetGameObject contains any other component
        //such as Image, Button or TextMeshProUGUI, then a template cannot be built out of it.

        if(rectTransformComponent.GetComponent<Image>() ||
            rectTransformComponent.GetComponent<Button>() ||
            rectTransformComponent.GetComponent<TextMeshProUGUI>())
        {
            EditorUtility.DisplayDialog("Error",
                $"Templates can only be built on an game object with RectTransform as it's only component",
                ok: "Ok");
            return;
        }

        try
        {
            Debug.Log($"Building template for game object : {targetGameObject.name}");
            loadedTemplate = TemplateHandler.BuildElementDataTree(rectTransformComponent);
            templateContent = UITreeStructureExporter.GetUITreeStructureAsString(loadedTemplate);

            Debug.Log($"Template built successfully");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Exception occured : {e.Message}");

            UnloadTemplate();

            EditorUtility.DisplayDialog("Error", $"Exception occured while building template.\n" +
                $"Check console for more information on the problem", ok: "Ok");

            return;
        }
    }

    private void ExportTemplate(bool force)
    {
        if(loadedTemplate == null)
        {
            Debug.LogError("No template loaded");
            return;
        }

        if (string.IsNullOrEmpty(filePath))
        {
            Debug.LogError($"Invalid file path : {filePath}");
            return;
        }

        if (!force && System.IO.File.Exists(filePath))
        {
            string fileName = System.IO.Path.GetFileName(filePath);

            if (EditorUtility.DisplayDialog(
                title: "Confirmation",
                message: $"File already exists, Do you want to replace the following file?\n{fileName}",
                ok: "Yes",
                cancel: "Cancel"))
            {
                ExportTemplate(force: true);
                return;
            }
            else
            {
                return;
            }
        }

        try
        {
            Debug.Log($"Writing template data to file : {filePath}");

            UIEmptyRect uiEmptyRectElement = loadedTemplate as UIEmptyRect;
            if(uiEmptyRectElement == null)
            {
                Debug.LogError("Error casting loadedTemplate as UIEmptyRect");
                return;
            }

            string content = UIEmptyRect.Serialize(uiEmptyRectElement);

            if (string.IsNullOrEmpty(content)) 
            {
                Debug.LogError("Couldn't serialize template");
                return;
            }

            SerializationManager.WriteToFile(filePath, content);

            Debug.Log($"Template exported successfully");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Exception occured : {e.Message}");

            EditorUtility.DisplayDialog("Error", $"Exception occured while saving template.\n" +
                $"Check console for more information on the problem", ok: "Ok");

            return;
        }
    }

}

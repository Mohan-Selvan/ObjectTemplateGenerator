# ObjectTemplateGenerator

#### Description
This project contains a tool that allows the user to export/import UI elements in a scene as a template in json format.
<br>

#### Note
As of now, only few UI elements with minimal properties are supported as this is a proof of concept. 
The base implementation is set in a way such that more UI elements and their properties can be supported with minimal effort.

#### Project setup
<table>
 <tr>
  <td>Unity version</td>
  <td>Unity 2022.3.8f1</td>
 </tr>
</table>

#### Prerequisites
- TextMeshPro Essentials

#### Supported UI elements (More to be added)
- RectTransform
- Button
- TextMeshProUGUI
- Image

#### Template Manager
- To open "Template Manager", Select `UOTG/Template manager` from the menu bar.

![image](https://github.com/Mohan-Selvan/ObjectTemplateGenerator/assets/64124633/3bd9939b-3105-447c-8a8b-cd503cb405f6)

- When no template is loaded, the window will look as shown below.

![image](https://github.com/Mohan-Selvan/ObjectTemplateGenerator/assets/64124633/010c73be-3a52-4d32-b045-49f7e3ee2ee8)

- When a template is loaded, the window will appear as shown below.

![image](https://github.com/Mohan-Selvan/ObjectTemplateGenerator/assets/64124633/0d4beacd-e26b-44c9-b2e2-503ead28ef5a)

- The "File path" text field requires a fully qualified path with the extension. This field is required for few options explained below.
- The "Options" section contains the buttons that the user can use to perform core operations (Explained below).
- The "Template content" content section displays the hierarchy and the element types for the loaded template.
- The text at the bottom of the window displays the selected game object.

##### Buttons

<table>
 <tr>
  <td>Load template from file</td>
  <td>Loads a template using provided file path. <br> Note: Valid file path is required in "File path" text field</td>
 </tr>
  <tr>
  <td>Unload current template</td>
  <td>Removes loaded template</td>
 </tr>
   <tr>
  <td>Build template data for selected game object</td>
  <td>Builds a new template using the selected game object. Note: The selected game object must have RectTransform component attached. Also it should not have any other components such as Button, Image or TextMeshProUGUI</td>
 </tr>
  <tr>
  <td>Export current template</td>
  <td>Exports active template as a json file which can be retrieved later. <br> Note: Valid file path is required in "File path" text field </td>
 </tr>
   <tr>
  <td>Instantiate current template to hierarchy</td>
  <td>Instantiates loaded template into the scene under the selected game object. <br> Note: The selected game object must have RectTransform component attached</td>
 </tr>
</table>

#### Demo scene
- Open the following scene from the Project window `Assets/APP/Scenes/Main.unity`

#### Creating your own test scene
- Create a new scene
- Create a new "Canvas" as shown in the image below ![image](https://github.com/Mohan-Selvan/ObjectTemplateGenerator/assets/64124633/f5c017fa-b03c-41a8-b5b5-71c9edcd8d02)
- Create an empty game object under the canvas named "Sample UI" (Note: Name can be anything, but it should contain characters like Double quotes)
- Create a simple hierarchy of UI elements under the "Sample UI" game object as shown below

![image](https://github.com/Mohan-Selvan/ObjectTemplateGenerator/assets/64124633/d3bf4641-9b1f-47d8-8e91-22dbe1d0953e)

#### Test scenario
- Open "Template manager" window by clicking `UOTG/Template manager` from the menu bar. Dock the window as preferred.
- Select the "Sample UI" game object in the hierarchy.
- In the "Template manager" window, click on "Build Template data for selected game object" button. This will create a new template for the selected game object that can be exported.
- The structure of the hierarchy will be displayed in the "Template manager" window, below the buttons.
- In order to export the template as a json file,
  - Fill the "File path" text field with the fully qualified path. For example, "D:/WorkArea/sample_ui_template.json"
  - Click on "Export current template" button to complete the export.
- Click on "Unload current template" button to remove the loaded template.
- In order to load a template from the file system.
  - Update the file path text field if required.
  - Click on "Load template from file" button to load the template using the file path provided.
- In order to instantiate a loaded template to the scene hierarchy,
  - Click on the game object (With RectTransform component) under which the template should be instantiated.
  - Click on "Instantiate current template to hierarchy" to instantiate the template under the selected game object.
  <hr>
  

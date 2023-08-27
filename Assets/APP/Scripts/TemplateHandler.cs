using TMPro;
using UnityEngine;
using UnityEngine.UI;

using com.UOTG.Components;
using com.UOTG.Elements;

namespace com.UOTG
{
    public class TemplateHandler
    {
        public static UIEmptyRect BuildElementDataTree(RectTransform rectTransform)
        {
            if (rectTransform == null)
            {
                Debug.LogError($"{nameof(TemplateHandler)} should be attached on a RectTransformObject");
                return null;
            }

            return BuildDataTreeRecursively(rectTransform) as UIEmptyRect;
        }

        private static UserInterfaceElementBase BuildDataTreeRecursively(RectTransform rectTransform)
        {
            if(rectTransform == null)
            {
                Debug.LogError("rootObject is null");
                return null;
            }

            UserInterfaceElementBase element = BuildUIElement(rectTransform);

            for (int i = 0; i < rectTransform.childCount; i++)
            {
                RectTransform r = rectTransform.GetChild(i) as RectTransform;
                if(r == null) { continue; }

                UserInterfaceElementBase childElement = BuildDataTreeRecursively(r);
                if(childElement != null)
                {
                    element.Children.Add(childElement);
                }
            }

            return element;
        }

        private static UserInterfaceElementBase BuildUIElement(RectTransform rectTransform)
        {
            // NOTE :: Instead of checking for component types, a new script can be created
            // with explicit element specifier that can be added attached to the UI object in Hierarchy.

            if (rectTransform.TryGetComponent<Button>(out Button buttonComponent))
            {
                return BuildUIButton(buttonComponent);
            }

            if(rectTransform.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI textComponent))
            {
                return BuildUIText(textComponent);
            }

            if(rectTransform != null)
            {
                return BuildUIEmptyRect(rectTransform);
            }

            Debug.LogError("Could not build ui element for the specified object", rectTransform.gameObject);
            return null;
        }

        #region Hierarchy To Model

        private static UIEmptyRect BuildUIEmptyRect(RectTransform rectTransform)
        {
            if (rectTransform == null)
            {
                Debug.LogError("No rect transform present on button object", rectTransform.gameObject);
                return null;
            }

            UIEmptyRect obj = new UIEmptyRect();
            obj.ElementType = UserInterfaceElementType.RECT;
            obj.ObjectName = rectTransform.gameObject.name;

            obj.RectTransformComponent = UIRectTransformComponent.BuildUIRectTransformComponentFromUI(rectTransform);


            return obj;
        }

        private static UIButton BuildUIButton(Button buttonComponent)
        {
            if (buttonComponent == null) { return null; }

            RectTransform rectTransform = buttonComponent.transform as RectTransform;
            Image imageComponent = buttonComponent.transform.GetComponent<Image>();

            if (rectTransform == null)
            {
                Debug.LogError("No rect transform present on button object", buttonComponent.gameObject);
                return null;
            }

            if (imageComponent == null)
            {
                Debug.LogError("No image component present on button object", buttonComponent.gameObject);
                return null;
            }

            UIButton obj = new UIButton();

            obj.ElementType = UserInterfaceElementType.BUTTON;
            obj.ObjectName = rectTransform.gameObject.name;

            obj.RectTransformComponent = UIRectTransformComponent.BuildUIRectTransformComponentFromUI(rectTransform);
            obj.ButtonColor = imageComponent.color;

            return obj;
        }

        private static UIText BuildUIText(TextMeshProUGUI textComponent)
        {
            if (textComponent == null) { return null; }

            RectTransform rectTransform = textComponent.transform as RectTransform;

            if (rectTransform == null)
            {
                Debug.LogError("No rect transform present on button object", textComponent.gameObject);
                return null;
            }

            UIText obj = new UIText();

            obj.ElementType = UserInterfaceElementType.TEXT;
            obj.ObjectName = rectTransform.gameObject.name;

            obj.RectTransformComponent = UIRectTransformComponent.BuildUIRectTransformComponentFromUI(rectTransform);
            obj.Message = textComponent.text;

            return obj;
        }

        #endregion


        public static void InstantiateElements(UserInterfaceElementBase rootElement, RectTransform rootTransform)
        {
            if (rootTransform == null)
            {
                Debug.LogError($"Root transform not set!");
                return;
            }

            if (rootElement == null)
            {
                Debug.LogError($"Root element is null!");
                return;
            }

            try
            {
                InstantiateElementsRecursively(rootElement, rootTransform);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Exception occured while creating Hierarchy : {e}", rootTransform.gameObject);
            }
        }

        private static void InstantiateElementsRecursively(UserInterfaceElementBase element, RectTransform parent)
        {
            if (element == null) { return; }

            RectTransform elementRectTransform = InstantiateElement(element, parent);

            if (elementRectTransform != null && element.Children != null && element.Children.Count > 0)
            {
                foreach (UserInterfaceElementBase child in element.Children)
                {
                    InstantiateElementsRecursively(child, elementRectTransform);
                }
            }
        }

        private static RectTransform InstantiateElement(UserInterfaceElementBase userInterfaceElement, RectTransform parent)
        {
            UserInterfaceElementType elementType = userInterfaceElement.ElementType;

            Debug.Log($"Creating element of type : {elementType}");

            switch (elementType)
            {
                case UserInterfaceElementType.RECT:

                    UIEmptyRect uiRect = userInterfaceElement as UIEmptyRect;

                    if (uiRect == null)
                    {
                        Debug.LogError($"Error casting element as : {userInterfaceElement.ElementType}");
                        return null;
                    }

                    return CreateElement(uiRect, parent);

                //break;

                case UserInterfaceElementType.TEXT:

                    UIText uiText = userInterfaceElement as UIText;

                    if (uiText == null)
                    {
                        Debug.LogError($"Error casting element as : {userInterfaceElement.ElementType}");
                        return null;
                    }

                    return CreateElement(uiText, parent);

                //break;


                case UserInterfaceElementType.BUTTON:

                    UIButton uiButton = userInterfaceElement as UIButton;

                    if (uiButton == null)
                    {
                        Debug.LogError($"Error casting element as : {userInterfaceElement.ElementType}");
                        return null;
                    }

                    return CreateElement(uiButton, parent);

                //break;

                default:
                    Debug.LogError("Invalid element");
                    return null;
                    //break;
            }
        }

        #region Model to Hierarchy

        private static RectTransform CreateElement(UIEmptyRect emptyRectElement, RectTransform parent)
        {
            GameObject go = new GameObject(emptyRectElement.ObjectName);
            RectTransform rectTransform = go.AddComponent<RectTransform>();

            rectTransform.localPosition = emptyRectElement.RectTransformComponent.Position;
            rectTransform.localEulerAngles = emptyRectElement.RectTransformComponent.Rotation;
            rectTransform.localScale = emptyRectElement.RectTransformComponent.Scale;

            rectTransform.SetParent(parent);

            return rectTransform;
        }

        private static RectTransform CreateElement(UIText textElement, RectTransform parent)
        {
            GameObject go = new GameObject(textElement.ObjectName);

            RectTransform rectTransform = go.AddComponent<RectTransform>();
            rectTransform.SetParent(parent);

            rectTransform.position = textElement.RectTransformComponent.Position;
            rectTransform.eulerAngles = textElement.RectTransformComponent.Rotation;
            rectTransform.localScale = textElement.RectTransformComponent.Scale;

            TextMeshProUGUI textComponent = go.AddComponent<TextMeshProUGUI>();
            textComponent.text = textElement.Message;
            textComponent.rectTransform.SetParent(parent, worldPositionStays: false);

            return textComponent.rectTransform;
        }

        private static RectTransform CreateElement(UIButton buttonElement, RectTransform parent)
        {
            GameObject go = new GameObject(buttonElement.ObjectName);

            RectTransform rectTransform = go.AddComponent<RectTransform>();
            rectTransform.SetParent(parent);

            rectTransform.position = buttonElement.RectTransformComponent.Position;
            rectTransform.eulerAngles = buttonElement.RectTransformComponent.Rotation;
            rectTransform.localScale = buttonElement.RectTransformComponent.Scale;

            Image imageComponent = go.AddComponent<Image>();
            Button buttonComponent = go.AddComponent<Button>();

            imageComponent.color = buttonElement.ButtonColor;

            return rectTransform;
        }

        #endregion
    }
}

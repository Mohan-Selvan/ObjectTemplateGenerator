using TMPro;
using UnityEngine;
using UnityEngine.UI;

using com.UOTG.Components;
using com.UOTG.Elements;

namespace com.UOTG
{
    public class TemplateHandler
    {
        /// <summary>
        /// Builds UI data tree with the provided rectTransform as root
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <returns></returns>
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
                return UIButton.BuildElement(buttonComponent);
            }

            if(rectTransform.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI textComponent))
            {
                return UIText.BuildElement(textComponent);
            }

            if (rectTransform.TryGetComponent<Image>(out Image imageComponent))
            {
                return UIImage.BuildElement(imageComponent);
            }

            if (rectTransform != null)
            {
                return UIEmptyRect.BuildElement(rectTransform);
            }

            Debug.LogError("Could not build UI element for the specified object", rectTransform.gameObject);
            return null;
        }


        /// <summary>
        /// Instantiates all UI elements held by the provided rootElement
        /// </summary>
        /// <param name="element">UI Elements data</param>
        /// <param name="rootTransform">Parent object for instantiation</param>
        public static void InstantiateElements(UserInterfaceElementBase element, RectTransform rootTransform)
        {
            if (rootTransform == null)
            {
                Debug.LogError($"Root transform not set!");
                return;
            }

            if (element == null)
            {
                Debug.LogError($"Element is null!");
                return;
            }

            try
            {
                InstantiateElementsRecursively(element, rootTransform);
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

            switch (elementType)
            {
                case UserInterfaceElementType.RECT:

                    UIEmptyRect uiRect = userInterfaceElement as UIEmptyRect;

                    if (uiRect == null)
                    {
                        Debug.LogError($"Error casting element as : {userInterfaceElement.ElementType}");
                        return null;
                    }

                    return UIEmptyRect.InstantiateElement(uiRect, parent);

                //break;

                case UserInterfaceElementType.TEXT:

                    UIText uiText = userInterfaceElement as UIText;

                    if (uiText == null)
                    {
                        Debug.LogError($"Error casting element as : {userInterfaceElement.ElementType}");
                        return null;
                    }

                    return UIText.InstantiateElement(uiText, parent);

                //break;


                case UserInterfaceElementType.BUTTON:

                    UIButton uiButton = userInterfaceElement as UIButton;

                    if (uiButton == null)
                    {
                        Debug.LogError($"Error casting element as : {userInterfaceElement.ElementType}");
                        return null;
                    }

                    return UIButton.InstantiateElement(uiButton, parent);

                //break;

                case UserInterfaceElementType.IMAGE:

                    UIImage uiImage = userInterfaceElement as UIImage;

                    if (uiImage == null)
                    {
                        Debug.LogError($"Error casting element as : {userInterfaceElement.ElementType}");
                        return null;
                    }

                    return UIImage.InstantiateElement(uiImage, parent);

                default:
                    Debug.LogError("Invalid element");
                    return null;
                    //break;
            }
        }

    }
}

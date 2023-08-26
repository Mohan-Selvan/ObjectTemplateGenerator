using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using com.UOTG.Elements;

namespace com.UOTG
{
    public class HierarchyInstantiator : MonoBehaviour
    {
        [SerializeField] RectTransform rootTransform = null;

        internal void InstantiateElements(UserInterfaceElementBase rootElement)
        {
            if(rootTransform == null)
            {
                Debug.LogError($"Root transform not set!");
                return;
            }

            if(rootElement == null)
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
                Debug.LogError($"Exception occured while creating Hierarchy : {e}", this.gameObject);
            }
        }

        private void InstantiateElementsRecursively(UserInterfaceElementBase element, RectTransform parent)
        {
            if(element == null) { return; }

            RectTransform elementRectTransform = CreateElement(element, parent);
            
            if(elementRectTransform != null && element.Children != null && element.Children.Count > 0)
            {
                foreach(UserInterfaceElementBase child in element.Children)
                {
                    InstantiateElementsRecursively(child, elementRectTransform);
                }
            }
        }


        private RectTransform CreateElement(UserInterfaceElementBase userInterfaceElement, RectTransform parent)
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

                    break;

                case UserInterfaceElementType.TEXT:

                    UIText uiText = userInterfaceElement as UIText;

                    if (uiText == null)
                    {
                        Debug.LogError($"Error casting element as : {userInterfaceElement.ElementType}");
                        return null;
                    }

                    return CreateElement(uiText, parent);

                    break;

                default:
                    Debug.LogError("Invalid element");
                    return null;
                    break;
            }
        }


        private RectTransform CreateElement(UIEmptyRect emptyRectElement, RectTransform parent)
        {
            GameObject go = new GameObject(emptyRectElement.ObjectName);
            RectTransform rectTransform = go.AddComponent<RectTransform>();

            rectTransform.position = emptyRectElement.RectTransformComponent.Position;
            rectTransform.eulerAngles = emptyRectElement.RectTransformComponent.Rotation;
            rectTransform.localScale = emptyRectElement.RectTransformComponent.Scale;
            
            rectTransform.SetParent(parent);

            return rectTransform;
        }


        private RectTransform CreateElement(UIText textElement, RectTransform parent)
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

    }

}

using com.UOTG.Components;
using Newtonsoft.Json;
using UnityEngine;


namespace com.UOTG.Elements
{
    [System.Serializable]
    public partial class UIEmptyRect : UserInterfaceElementBase
    {

        public UIEmptyRect() : base()
        {
            
        }

        public static string Serialize(UIEmptyRect content)
        {
            return JsonConvert.SerializeObject(content, ConverterSettings.GenericSettings);
        }

        public static UIEmptyRect Deserialize(string message)
        {
            return JsonConvert.DeserializeObject<UIEmptyRect>(message, ConverterSettings.GenericSettings);
        }
    }

    public partial class UIEmptyRect
    {
        internal static UIEmptyRect BuildElement(RectTransform rectTransform)
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

        internal static RectTransform InstantiateElement(UIEmptyRect emptyRectElement, RectTransform parent)
        {
            GameObject go = new GameObject(emptyRectElement.ObjectName);

            RectTransform rectTransform = go.AddComponent<RectTransform>();
            rectTransform.SetParent(parent);

            UIRectTransformComponent.ApplyDataToRectTransform(emptyRectElement.RectTransformComponent, rectTransform);

            return rectTransform;
        }

    }
}


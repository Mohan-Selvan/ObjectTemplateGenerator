using com.UOTG.Components;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

namespace com.UOTG.Elements
{
    [System.Serializable]
    public partial class UIButton : UserInterfaceElementBase
    {
        public UIButton() : base()
        {
            
        }

        //[JsonProperty("color")]
        //[field: SerializeField] public Color ButtonColor { get; set; }

        [JsonProperty("state")]
        [field: SerializeField] public bool InteractableState { get; set; }

        public static string Serialize(UIButton content)
        {
            return JsonConvert.SerializeObject(content, ConverterSettings.GenericSettings);
        }

        public static UIButton Deserialize(string message)
        {
            return JsonConvert.DeserializeObject<UIButton>(message, ConverterSettings.GenericSettings);
        }
    }

    public partial class UIButton
    {
        internal static UIButton BuildElement(Button buttonComponent)
        {
            if (buttonComponent == null) { return null; }

            RectTransform rectTransform = buttonComponent.transform as RectTransform;
            Image imageComponent = buttonComponent.transform.GetComponent<Image>();

            if (rectTransform == null)
            {
                Debug.LogError("No rect transform present on object", buttonComponent.gameObject);
                return null;
            }

            if (imageComponent == null)
            {
                Debug.LogError("No image component present on object", buttonComponent.gameObject);
                return null;
            }

            UIButton obj = new UIButton();

            obj.ElementType = UserInterfaceElementType.BUTTON;
            obj.ObjectName = rectTransform.gameObject.name;

            obj.RectTransformComponent = UIRectTransformComponent.BuildUIRectTransformComponentFromUI(rectTransform);

            //obj.ButtonColor = imageComponent.color;
            obj.InteractableState = buttonComponent.interactable;

            return obj;
        }


        internal static RectTransform InstantiateElement(UIButton buttonElement, RectTransform parent)
        {
            GameObject go = new GameObject(buttonElement.ObjectName);

            RectTransform rectTransform = go.AddComponent<RectTransform>();
            rectTransform.SetParent(parent);

            UIRectTransformComponent.ApplyDataToRectTransform(buttonElement.RectTransformComponent, rectTransform);

            Image imageComponent = go.AddComponent<Image>();
            Button buttonComponent = go.AddComponent<Button>();

            //imageComponent.color = buttonElement.ButtonColor;
            buttonComponent.interactable = buttonElement.InteractableState;

            return rectTransform;
        }
    }

}


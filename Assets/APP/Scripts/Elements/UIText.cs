using com.UOTG.Components;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;

namespace com.UOTG.Elements
{
    [System.Serializable]
    public partial class UIText : UserInterfaceElementBase
    {
        public UIText() : base()
        {
            
        }

        [JsonProperty("message")]
        [field: SerializeField] public string Message { get; set; }

        [JsonProperty("font_size")]
        [field: SerializeField] public float FontSize { get; set; }

        [JsonProperty("align")]
        [field: SerializeField] public TextAlignmentOptions Alignment { get; set; }

        [JsonProperty("font_color")]
        private List<float> fontColorList { get; set; }

        [JsonIgnore]
        [field: SerializeField] public Vector4 FontColor { get; set; }

        // ## Serialization callbacks
        #region Serialization callbacks

        [OnSerializing]
        private void OnSerializedCallback(StreamingContext context)
        {
            fontColorList = FontColor.ToFloatList();

        }

        [OnDeserialized]
        private void OnDeserializedCallback(StreamingContext context)
        {
            FontColor = fontColorList.ToVector4();
        }

        #endregion

        #region Static helper functions
        public static string Serialize(UIText content)
        {
            return JsonConvert.SerializeObject(content, ConverterSettings.GenericSettings);
        }

        public static UIText Deserialize(string message)
        {
            return JsonConvert.DeserializeObject<UIText>(message, ConverterSettings.GenericSettings);
        }
        #endregion
    }

    public partial class UIText
    {
        internal static UIText BuildElement(TextMeshProUGUI textComponent)
        {
            if (textComponent == null) { return null; }

            RectTransform rectTransform = textComponent.transform as RectTransform;

            if (rectTransform == null)
            {
                Debug.LogError("No rect transform present on object", textComponent.gameObject);
                return null;
            }

            UIText obj = new UIText();

            obj.ElementType = UserInterfaceElementType.TEXT;
            obj.ObjectName = rectTransform.gameObject.name;

            obj.RectTransformComponent = UIRectTransformComponent.BuildUIRectTransformComponentFromUI(rectTransform);
            obj.Message = textComponent.text;
            obj.FontSize = textComponent.fontSize;
            obj.Alignment = textComponent.alignment;
            obj.FontColor = textComponent.color;

            return obj;
        }


        internal static RectTransform InstantiateElement(UIText textElement, RectTransform parent)
        {
            GameObject go = new GameObject(textElement.ObjectName);

            RectTransform rectTransform = go.AddComponent<RectTransform>();
            rectTransform.SetParent(parent);

            UIRectTransformComponent.ApplyDataToRectTransform(textElement.RectTransformComponent, rectTransform);

            TextMeshProUGUI textComponent = go.AddComponent<TextMeshProUGUI>();
            textComponent.rectTransform.SetParent(parent, worldPositionStays: false);

            textComponent.text = textElement.Message;
            textComponent.fontSize = textElement.FontSize;
            textComponent.alignment = textElement.Alignment;
            textComponent.color = textElement.FontColor;

            return textComponent.rectTransform;
        }

    }

}


using com.UOTG.Components;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace com.UOTG.Elements
{
    [System.Serializable]
    public partial class UIImage : UserInterfaceElementBase
    {
        public UIImage() : base()
        {

        }


        [JsonProperty("color")]
        private List<float> imageColorList { get; set; }

        [JsonIgnore]
        [field: SerializeField] public Vector4 ImageColor { get; set; }

        // ## Serialization callbacks
        #region Serialization callbacks

        [OnSerializing]
        private void OnSerializedCallback(StreamingContext context)
        {
            imageColorList = ImageColor.ToFloatList();

        }

        [OnDeserialized]
        private void OnDeserializedCallback(StreamingContext context)
        {
            ImageColor = imageColorList.ToVector4();
        }

        #endregion

        #region Statich helper functions

        public static string Serialize(UIImage content)
        {
            return JsonConvert.SerializeObject(content, ConverterSettings.GenericSettings);
        }

        public static UIImage Deserialize(string message)
        {
            return JsonConvert.DeserializeObject<UIImage>(message, ConverterSettings.GenericSettings);
        }

        #endregion
    }

    public partial class UIImage
    {
        internal static UIImage BuildElement(UnityEngine.UI.Image imageComponent)
        {
            if (imageComponent == null) { return null; }

            RectTransform rectTransform = imageComponent.transform as RectTransform;

            if (rectTransform == null)
            {
                Debug.LogError("No rect transform present on object", imageComponent.gameObject);
                return null;
            }

            UIImage obj = new UIImage();

            obj.ElementType = UserInterfaceElementType.IMAGE;
            obj.ObjectName = rectTransform.gameObject.name;

            obj.RectTransformComponent = UIRectTransformComponent.BuildUIRectTransformComponentFromUI(rectTransform);
            obj.ImageColor = imageComponent.color;

            return obj;
        }


        internal static RectTransform InstantiateElement(UIImage imageElement, RectTransform parent)
        {
            GameObject go = new GameObject(imageElement.ObjectName);

            RectTransform rectTransform = go.AddComponent<RectTransform>();
            rectTransform.SetParent(parent);

            UIRectTransformComponent.ApplyDataToRectTransform(imageElement.RectTransformComponent, rectTransform);

            Image imageComponent = go.AddComponent<Image>();
            imageComponent.color = imageElement.ImageColor;

            return rectTransform;
        }
    }

}


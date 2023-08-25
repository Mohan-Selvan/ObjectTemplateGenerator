using Newtonsoft.Json;
using UnityEngine;

using com.UOTG.Components;

namespace com.UOTG.Elements
{
    [System.Serializable]
    public class UIEmptyRect : UserInterfaceElementBase
    {

        public UIEmptyRect() : base()
        {
            
        }

        // ## IUserInterfaceElement Implementation
        #region IUserInterfaceElement Implementation

        public override UserInterfaceElementType GetElementType()
        {
            return UserInterfaceElementType.RECT;
        }

        #endregion

        [JsonProperty("transform")]
        [field: SerializeField] public UIRectTransformComponent RectTransformComponent { get; set; }

        [JsonProperty("message")]
        [field: SerializeField] public string Message { get; set; }

        public static string Serialize(UIEmptyRect content)
        {
            return JsonConvert.SerializeObject(content, ConverterSettings.GenericSettings);
        }

        public static UIEmptyRect Deserialize(string message)
        {
            return JsonConvert.DeserializeObject<UIEmptyRect>(message, ConverterSettings.GenericSettings);
        }
    }

}


using com.UOTG.Components;
using Newtonsoft.Json;
using UnityEngine;

namespace com.UOTG.Elements
{
    [System.Serializable]
    public class UIText : UserInterfaceElementBase
    {
        public UIText() : base()
        {
            
        }

        // ## IUserInterfaceElement Implementation
        #region IUserInterfaceElement Implementation

        public override UserInterfaceElementType GetElementType()
        {
            return UserInterfaceElementType.TEXT;
        }

        #endregion

        [JsonProperty("transform")]
        [field: SerializeField] public UIRectTransformComponent RectTransformComponent { get; set; }

        [JsonProperty("message")]
        [field: SerializeField] public string Message { get; set; }


        public static string Serialize(UIText content)
        {
            return JsonConvert.SerializeObject(content, ConverterSettings.GenericSettings);
        }

        public static UIText Deserialize(string message)
        {
            return JsonConvert.DeserializeObject<UIText>(message, ConverterSettings.GenericSettings);
        }
    }

}


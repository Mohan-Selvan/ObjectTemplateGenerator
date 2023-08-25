using com.UOTG.Components;
using Newtonsoft.Json;
using UnityEngine;

namespace com.UOTG.Elements
{
    [System.Serializable]
    public class UIButton : UserInterfaceElementBase
    {
        public UIButton() : base()
        {
            
        }


        // ## IUserInterfaceElement Implementation
        #region IUserInterfaceElement Implementation

        public override UserInterfaceElementType GetElementType()
        {
            return UserInterfaceElementType.BUTTON;
        }

        #endregion

        [JsonProperty("transform")]
        [field: SerializeField] public UIRectTransformComponent RectTransformComponent { get; set; }

        [JsonProperty("button_text")]
        [field: SerializeField] public string ButtonText { get; set; }

        public static string Serialize(UIButton content)
        {
            return JsonConvert.SerializeObject(content, ConverterSettings.GenericSettings);
        }

        public static UIButton Deserialize(string message)
        {
            return JsonConvert.DeserializeObject<UIButton>(message, ConverterSettings.GenericSettings);
        }
    }

}


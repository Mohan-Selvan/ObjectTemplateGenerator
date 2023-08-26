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

        [JsonProperty("button_color")]
        [field: SerializeField] public Color ButtonColor { get; set; }

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


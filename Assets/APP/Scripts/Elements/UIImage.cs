using Newtonsoft.Json;
using UnityEngine;

namespace com.UOTG.Elements
{
    [System.Serializable]
    public class UIImage : UserInterfaceElementBase
    {
        public UIImage() : base()
        {

        }

        [JsonProperty("button_text")]
        [field: SerializeField] public string ImagePath { get; set; }

        public static string Serialize(UIImage content)
        {
            return JsonConvert.SerializeObject(content, ConverterSettings.GenericSettings);
        }

        public static UIImage Deserialize(string message)
        {
            return JsonConvert.DeserializeObject<UIImage>(message, ConverterSettings.GenericSettings);
        }
    }

}


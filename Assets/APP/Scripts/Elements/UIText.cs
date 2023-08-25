
// ## UI_Rect

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace com.UOTG.Elements
{
    [System.Serializable]
    public class UIText : UserInterfaceElement
    {
        public override UserInterfaceElementType ElementType
        {
            get => _userInterfaceElementType;
        }
        public UIText()
        {
            positionList = new List<float>();
            rotationList = new List<float>();
            scaleList = new List<float>();
        }


        [JsonProperty("type")]
        private const UserInterfaceElementType _userInterfaceElementType = UserInterfaceElementType.TEXT;

        [JsonProperty("position")]
        private List<float> positionList { get; set; }

        [JsonProperty("rotation")]
        private List<float> rotationList { get; set; }

        [JsonProperty("scale")]
        private List<float> scaleList { get; set; }

        [JsonProperty("message")]
        [field: SerializeField] public string Message { get; set; }

        [JsonIgnore]
        [field: SerializeField] public Vector3 Position { get; set; }

        [JsonIgnore]
        [field: SerializeField] public Vector3 Rotation { get; set; }

        [JsonIgnore]
        [field: SerializeField] public Vector3 Scale { get; set; }


        [OnSerializing]
        private void OnSerializedCallback(StreamingContext context)
        {
            Debug.Log("Calling on serialized");

            positionList[0] = Position.x;
            positionList[1] = Position.y;
            positionList[2] = Position.z;

            rotationList[0] = Rotation.x;
            rotationList[1] = Rotation.y;
            rotationList[2] = Rotation.z;

            scaleList[0] = Scale.x;
            scaleList[1] = Scale.y;
            scaleList[2] = Scale.z;
        }

        [OnDeserializing]
        private void OnDeserializedCallback(StreamingContext context)
        {
            Debug.Log("Calling on deserialized");

            Position = positionList.ToVector3();
            Rotation = rotationList.ToVector3();
            Scale = scaleList.ToVector3();
        }

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


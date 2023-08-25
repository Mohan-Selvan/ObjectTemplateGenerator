using System.Collections.Generic;
using System.Runtime.Serialization;

using UnityEngine;

using Newtonsoft.Json;

namespace com.UOTG.Components
{
    [System.Serializable]
    public class UIRectTransformComponent
    {
        [JsonConstructor]
        public UIRectTransformComponent()
        {
            
        }

        // ## Properties

        [JsonProperty("position")]
        private List<float> positionList { get; set; }

        [JsonProperty("rotation")]
        private List<float> rotationList { get; set; }

        [JsonProperty("scale")]
        private List<float> scaleList { get; set; }

        [JsonIgnore]
        [field: SerializeField] public Vector3 Position;

        [JsonIgnore]
        [field: SerializeField] public Vector3 Rotation;

        [JsonIgnore]
        [field: SerializeField] public Vector3 Scale;

        // ## Serialization callbacks
        #region Serialization callbacks

        [OnSerialized]
        private void OnSerializedCallback(StreamingContext context)
        {
            Debug.Log("Calling on serialized");

            positionList = Position.ToFloatList();
            rotationList = Rotation.ToFloatList();
            scaleList = Scale.ToFloatList();

            //positionList[0] = Position.x;
            //positionList[1] = Position.y;
            //positionList[2] = Position.z;
            //
            //rotationList[0] = Rotation.x;
            //rotationList[1] = Rotation.y;
            //rotationList[2] = Rotation.z;
            //
            //scaleList[0] = Scale.x;
            //scaleList[1] = Scale.y;
            //scaleList[2] = Scale.z;
        }

        [OnDeserialized]
        private void OnDeserializedCallback(StreamingContext context)
        {
            Debug.Log("Calling on deserialized");

            Position = positionList.ToVector3();
            Rotation = rotationList.ToVector3();
            Scale = scaleList.ToVector3();
        }

        #endregion
    }
}

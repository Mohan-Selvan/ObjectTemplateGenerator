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

        [OnSerializing]
        private void OnSerializedCallback(StreamingContext context)
        {
            Debug.Log("Calling on serialized");

            positionList = Position.ToFloatList();
            rotationList = Rotation.ToFloatList();
            scaleList = Scale.ToFloatList();
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

        // ## Static helper functions
        #region Static helper functions

        public static UIRectTransformComponent BuildUIRectTransformComponentFromUI(RectTransform rectTransform)
        {
            return new UIRectTransformComponent()
            {
                Position = rectTransform.position,
                Rotation = rectTransform.localEulerAngles,
                Scale = rectTransform.localScale
            };
        }

        #endregion
    }
}

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

        [JsonProperty("a_min")]
        private List<float> anchorMinList { get; set; }

        [JsonProperty("a_max")]
        private List<float> anchorMaxList { get; set; }

        [JsonProperty("piv")]
        private List<float> pivotList { get; set; }

        [JsonProperty("size_del")]
        private List<float> sizeDeltaList { get; set; }


        [JsonIgnore]
        [field: SerializeField] public Vector3 Position;

        [JsonIgnore]
        [field: SerializeField] public Vector3 Rotation;

        [JsonIgnore]
        [field: SerializeField] public Vector3 Scale;

        [JsonIgnore]
        [field: SerializeField] public Vector2 AnchorMin;

        [JsonIgnore]
        [field: SerializeField] public Vector2 AnchorMax;

        [JsonIgnore]
        [field: SerializeField] public Vector2 Pivot;

        [JsonIgnore]
        [field: SerializeField] public Vector2 SizeDelta;


        // ## Serialization callbacks
        #region Serialization callbacks

        [OnSerializing]
        private void OnSerializedCallback(StreamingContext context)
        {
            positionList = Position.ToFloatList();
            rotationList = Rotation.ToFloatList();
            scaleList = Scale.ToFloatList();

            anchorMinList = AnchorMin.ToFloatList();
            anchorMaxList = AnchorMax.ToFloatList();
            pivotList = Pivot.ToFloatList();
            sizeDeltaList = SizeDelta.ToFloatList();
        }

        [OnDeserialized]
        private void OnDeserializedCallback(StreamingContext context)
        {
            Position = positionList.ToVector3();
            Rotation = rotationList.ToVector3();
            Scale = scaleList.ToVector3();

            AnchorMin = anchorMinList.ToVector2();
            AnchorMax = anchorMaxList.ToVector2();
            Pivot = pivotList.ToVector2();
            SizeDelta = sizeDeltaList.ToVector2();
        }

        #endregion

        // ## Static helper functions
        #region Static helper functions

        public static UIRectTransformComponent BuildUIRectTransformComponentFromUI(RectTransform rectTransform)
        {
            return new UIRectTransformComponent()
            {
                Position = rectTransform.localPosition,
                Rotation = rectTransform.localEulerAngles,
                Scale = rectTransform.localScale,
                AnchorMin = rectTransform.anchorMin,
                AnchorMax = rectTransform.anchorMax,
                Pivot = rectTransform.pivot,
                SizeDelta = rectTransform.sizeDelta
            };
        }

        public static void ApplyDataToRectTransform(UIRectTransformComponent uiRectTransformComponent, RectTransform rectTransform)
        {
            if(rectTransform == null) { return; }

            rectTransform.anchorMin = uiRectTransformComponent.AnchorMin;
            rectTransform.anchorMax = uiRectTransformComponent.AnchorMax;
            rectTransform.pivot = uiRectTransformComponent.Pivot;

            rectTransform.localPosition = uiRectTransformComponent.Position;
            rectTransform.localEulerAngles = uiRectTransformComponent.Rotation;
            rectTransform.localScale = uiRectTransformComponent.Scale;
            rectTransform.sizeDelta = uiRectTransformComponent.SizeDelta;
        }

        #endregion
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Events
{
    /// <summary>A test MonoBehaviour class that experiments with Unity's UI EventSystems.</summary>
    public class PanelEventTest : MonoBehaviour, IPointerClickHandler, ISelectHandler, IDragHandler
    {
        public Vector3 Origin = Vector3.zero;
        public float MaxAngle = 60f;
        public float MinAngle = 30f;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData) => Debug.Log("click");

        void ISelectHandler.OnSelect(BaseEventData eventData) => Debug.Log("Select");

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            var cameraTransform = Camera.main.transform;
            var delta = eventData.delta;
            var positionDelta = new Vector3(delta.x * 0.1f, delta.y * 0.05f, 0);
            var angleDeltaY = delta.y * 0.05f + cameraTransform.position.y - Origin.y;
            var angleDeltaX = delta.x * 0.1f + cameraTransform.position.x - Origin.x;
            var newAngle = Mathf.Atan2(angleDeltaY, angleDeltaX) * 180 / Mathf.PI;

            var validAngle = (newAngle >= MinAngle && newAngle <= MaxAngle);
            var lessThanMin = newAngle < MinAngle && delta.y > 0;
            var lessThanMax = newAngle < MaxAngle && delta.y < 0;

            if (!validAngle && !lessThanMin && !lessThanMax) return;

            cameraTransform.Translate(positionDelta);
            cameraTransform.LookAt(Origin);
        }
    }
}

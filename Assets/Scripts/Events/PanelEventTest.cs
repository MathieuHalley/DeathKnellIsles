using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>A test MonoBehaviour class that experiments with Unity's UI EventSystems.</summary>
public class PanelEventTest : MonoBehaviour, IPointerClickHandler, ISelectHandler, IDragHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click");
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("Select");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Camera c = Camera.main;
        var delta = eventData.delta;
        c.transform.Translate(delta.x * 0.1f, delta.y * 0.05f, 0);
        c.transform.LookAt(Vector3.zero);
    }
}

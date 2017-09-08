using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventTest : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public new MeshRenderer renderer;
    public Color DefaultColor;
    public Color HighlightColor = new Color(0,1,1);

	// Use this for initialization
	void Start ()
    {
        renderer = GetComponent<MeshRenderer>();
        DefaultColor = renderer.material.color;
	}


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click");
    }

	public void OnPointerEnter(PointerEventData eventData)
    {
        renderer.material.color = HighlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        renderer.material.color = DefaultColor;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class WindowTitleBar : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField]
    private RectTransform m_Parent;

    private float m_OffsetX;
    private float m_OffsetY;

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_OffsetX = m_Parent.position.x - Input.mousePosition.x;
        m_OffsetY = m_Parent.position.y - Input.mousePosition.y;
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_Parent.position = new Vector3(m_OffsetX + Input.mousePosition.x, m_OffsetY + Input.mousePosition.y);
    }
}
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    public Action<GameObject> OnDown;
    public Action<string> OnEnter;


    public void OnPointerDown(PointerEventData eventData)
    {
        OnDown?.Invoke(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnEnter?.Invoke(name);
    }
}
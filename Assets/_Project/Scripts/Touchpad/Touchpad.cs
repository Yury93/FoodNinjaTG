using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touchpad : MonoBehaviour, IDragHandler 
{
    [SerializeField] private Transform canvas;
    [SerializeField] private float sensivity;
    private float vertical, horizontal;
    private Vector2 previosPosition, dragPosition, lastDragPosition;
    public float Vertical => vertical;
    public float Horizontal => horizontal;
    public Coroutine cor;
    private float deltaX, deltaY;

    private Vector3 newDirection;
    public Vector3 mouseDelta = Vector3.zero;

    private Vector3 lastMousePosition = Vector3.zero;
    public virtual void OnDrag(PointerEventData eventData)
    {
        dragPosition = eventData.position;
        newDirection = dragPosition - previosPosition;

        if (dragPosition != lastDragPosition)
        {
            var canvasScale = canvas.localScale;

            lastDragPosition = dragPosition;


            mouseDelta = Input.mousePosition - lastMousePosition;
 
            var speed = ((mouseDelta.magnitude / Time.deltaTime) * 0.001f) + sensivity;
            lastMousePosition = Input.mousePosition;

            horizontal += ((eventData.delta.x * sensivity * Time.deltaTime) * speed) / canvasScale.x;
            vertical += ((eventData.delta.y * sensivity * Time.deltaTime) * speed) / canvasScale.y;
        }
        StartCoroutine(CorLateDrag(eventData));
        IEnumerator CorLateDrag(PointerEventData eventData)
        {
            yield return Time.deltaTime;

            if (deltaY == eventData.delta.y)
            {
                deltaY = eventData.delta.y;
                vertical = 0;
            }
            if (deltaX == eventData.delta.x)
            {
                deltaX = eventData.delta.x;
                horizontal = 0;
            }

        }
    } 
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        previosPosition = eventData.position;

        OnDrag(eventData);
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        previosPosition = eventData.position;
        vertical = 0;
        horizontal = 0;

    } 
}

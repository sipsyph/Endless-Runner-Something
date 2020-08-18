using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggingOnCanvas : MonoBehaviour, IEndDragHandler, IDragHandler
{
    public static bool draggedUp, draggedDown;

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging Paneladsidasj");
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Press position + " + eventData.pressPosition);
        Debug.Log("End position + " + eventData.position);
        Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
        Debug.Log("norm + " + dragVectorDirection);
        GetDragDirection(dragVectorDirection);
    }

    private enum DraggedDirection
    {
        Up,
        Down,
        Right,
        Left
    }
    
    private DraggedDirection GetDragDirection(Vector3 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);
        DraggedDirection draggedDir;
        if (positiveX > positiveY)
        {
            draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
        }
        else
        {
            draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
        }
        Debug.Log(draggedDir);

        if(draggedDir==DraggedDirection.Up)
        {
            draggedUp = true;
        }

        if(draggedDir==DraggedDirection.Down)
        {
            draggedDown = true;
        }

        return draggedDir;
    }
}

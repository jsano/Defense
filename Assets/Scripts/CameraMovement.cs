using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IUpdateSelectedHandler
{
    
    private float speed = 8;
    public int dir;
    private bool pressed = false;

    public void OnUpdateSelected(BaseEventData baseEventData)
    {
        if (pressed == false) return;
        if (dir == -1 && Camera.main.transform.position.x <= 0) return;
        if (dir == 1 && Camera.main.transform.position.x >= Constants.MAXCAMERAX) return;
        Camera.main.transform.Translate(new Vector3(dir*1, 0, 0) * speed * Time.deltaTime);
    }

    public void OnPointerDown(PointerEventData data)
    {
        pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }
 
}

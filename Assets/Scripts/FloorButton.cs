using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloorButton : MonoBehaviour
{
    public bool isPressed = false;

    void Update()
    {
        transform.localPosition = Vector2.Lerp(transform.localPosition, isPressed ? new Vector2(0, 0.02f) : new Vector2(0, 0.16f), 0.03f);
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Box"))
        {
            isPressed = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Box"))
        {
            isPressed = false;
        }
    }
}

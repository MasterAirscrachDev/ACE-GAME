using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteractor : MonoBehaviour
{
    // Start is called before the first frame update
    InteractableType interacting;
    Rigidbody2D grabbedRb;
    CursorButton clickedButton;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 topLeftOffset = new Vector3(-transform.localScale.x / 2, transform.localScale.y / 2, 0);
                //rotate the offset
            topLeftOffset = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * topLeftOffset;

            Vector2 gameCursor = transform.position + topLeftOffset;
            if(interacting != InteractableType.Box)
            {
                //raycast to at Tpos and see if we hit a box
                //get a layer mask that excludes layer 6
                RaycastHit2D hit = Physics2D.Raycast(gameCursor, Vector2.up * 0.01f, 0.1f, ~LayerMask.GetMask("Cursor"));
                Debug.DrawRay(gameCursor, Vector2.up * 0.1f, Color.red, 0.1f);
                //Tpos is the tip of the cursor
                if(hit.collider != null)
                {
                    if(hit.collider.gameObject.CompareTag("Box") && interacting == InteractableType.None){
                        //click offset is the position we clicked on the box relative to the box
                        grabbedRb = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                        interacting = InteractableType.Box;
                    }
                    else if(hit.collider.gameObject.CompareTag("Button") && interacting == InteractableType.None)
                    {
                        clickedButton = hit.collider.gameObject.GetComponent<CursorButton>();
                        clickedButton.Click();
                        interacting = InteractableType.Button;
                    }
                    else if(interacting == InteractableType.Button && !hit.collider.gameObject.CompareTag("Button"))
                    {
                        Debug.Log("Should be button, hit: " + hit.collider.gameObject.name);
                        clickedButton.Release();
                        clickedButton = null;
                        interacting = InteractableType.None;
                    }
                }
                else if(interacting == InteractableType.Button)
                {
                    clickedButton.Release();
                    interacting = InteractableType.None;
                    clickedButton = null;
                }
            }
            else //we are holding a box
            {
                //move the box to the the position we clicked on the box relative to the box (accounting for both the box and the cursor's rotation and scale)
                Debug.DrawLine(gameCursor, grabbedRb.transform.position, Color.green);
                float distance = Vector2.Distance(gameCursor, grabbedRb.transform.position);
                distance *= 0.5f; Mathf.Clamp(distance, 0.1f, 1);
                Vector2 interpolatedPos = Vector2.Lerp(grabbedRb.transform.position, gameCursor, distance);
                grabbedRb.MovePosition(interpolatedPos);
                
            }
            
        }
        else{
            if(interacting == InteractableType.Box)
            {
                interacting = InteractableType.None;
                grabbedRb.velocity = GetComponent<Rigidbody2D>().velocity;
                grabbedRb = null;
            }
            else if(interacting == InteractableType.Button)
            {
                clickedButton.Release();
                clickedButton = null;
                interacting = InteractableType.None;
            }
        }
    }
}
enum InteractableType
{ None, Box, Button }
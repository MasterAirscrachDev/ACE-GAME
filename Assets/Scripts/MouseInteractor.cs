using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteractor : MonoBehaviour
{
    // Start is called before the first frame update
    bool wasHeldLastFrame = false;
    Vector2 clickOffset;
    Rigidbody2D grabbedRb;
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
            if(!wasHeldLastFrame)
            {
                //raycast to at Tpos and see if we hit a box
                RaycastHit2D hit = Physics2D.Raycast(gameCursor, Vector2.zero, 0.1f);
                Debug.DrawRay(gameCursor, Vector2.up * 0.1f, Color.red, 0.1f);
                //Tpos is the tip of the cursor
                if(hit.collider != null && hit.collider.gameObject.CompareTag("Box"))
                {
                    //click offset is the position we clicked on the box relative to the box
                    clickOffset = (Vector2)hit.collider.transform.InverseTransformPoint(gameCursor);
                    grabbedRb = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                    wasHeldLastFrame = true;
                }
                else{
                    Debug.Log("No box found");
                }
            }
            else
            {
                //move the box to the the position we clicked on the box relative to the box (accounting for both the box and the cursor's rotation and scale)
                Vector2 cubeGrabOffset = grabbedRb.transform.TransformPoint(clickOffset + gameCursor);

                grabbedRb.MovePosition(cubeGrabOffset);
                
            }
            
        }
        else if(wasHeldLastFrame)
        {
            wasHeldLastFrame = false;
            grabbedRb.velocity = GetComponent<Rigidbody2D>().velocity;
            grabbedRb = null;
        }
    }
}

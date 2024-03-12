using UnityEngine;
public class FollowCursor : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Cursor.visible = false;
    }
    void Update()
    {
        //follow the cursor (and make the corners line up) (ussing physcis)
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //the offset is the top left corner (accounting for rotation and scale)
        Vector3 offset = new Vector3(transform.localScale.x / 2, -transform.localScale.y / 2, 0);
        //rotate the offset
        offset = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * offset;

        Vector3 Tpos = cursorPos + offset;
        
        //move towards the cursor
        float distance = Vector3.Distance(Tpos, transform.position);
        distance = Mathf.Clamp(distance, 0.5f, 5);
        rb.AddForce((Tpos - transform.position) * distance);
        //Debug.Log(rb.velocity.magnitude);
        if(rb.velocity.magnitude > 40)
        { rb.velocity = rb.velocity.normalized * 40; }
    }
}

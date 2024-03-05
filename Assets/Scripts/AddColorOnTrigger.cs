using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddColorOnTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other){
        Chromaterator chromaterator = other.GetComponent<Chromaterator>();
        if(chromaterator != null){
            chromaterator.AddColor(GetComponent<SpriteRenderer>().color);
        }
    }
}

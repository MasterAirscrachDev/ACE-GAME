using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearColorOnTrigger : MonoBehaviour
{
    [SerializeField] Color[] colors;
    void OnTriggerEnter2D(Collider2D other){
        Chromaterator chromaterator = other.GetComponent<Chromaterator>();
        if(chromaterator != null){
            if(colors.Length == 0){
                chromaterator.RemoveAllColors();
                return;
            }
            else{
                for(int i = 0; i < colors.Length; i++){
                    chromaterator.RemoveColor(colors[i]);
                }
            }
            
        }
    }
}

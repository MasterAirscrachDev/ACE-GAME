using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorButton : MonoBehaviour
{
    [SerializeField] bool holdMode = false;
    [SerializeField] GameObject toggle;
    bool isHeld = false;
    void Start()
    {
        SetCol();
    }
    public void Click(){
        isHeld = true;
        SetCol();
        toggle.SetActive(!toggle.activeSelf);
    }
    public void Release(){
        isHeld = false;
        SetCol();
        if(holdMode){
            toggle.SetActive(!toggle.activeSelf);
        }
    }
    void SetCol(){
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if(isHeld){
            if(holdMode){
                sr.color = new Color(0.011f, 0.980f, 0.011f, 1);
            }
            else{
                sr.color = new Color(0.749f, 0.066f, 0.066f, 1);
            }
        }
        else{
            if(holdMode){
                sr.color = new Color(0.070f, 0.521f, 0.062f, 1);
            }
            else{
                sr.color = new Color(0.980f, 0.011f, 0.011f, 1);
            }
        }
    }
}

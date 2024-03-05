using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chromaterator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject colorPrefab;
    [SerializeField] float distance = 0.5f;
    [SerializeField] List<ColorObj> colors;
    void Start()
    {
        
    }
    void Update()
    {
        for(int i = 0; i < colors.Count; i++){
            //position the color at an offset based on a rotation derived from i
            float angle = 360 / colors.Count * i + 45;
            colors[i].transform.position = transform.position + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * distance;
            colors[i].transform.Translate(new Vector3(0, 0, 1 + (i * 0.02f)));
            colors[i].transform.rotation = transform.rotation;
        }
    }
    public bool IsColor(Color color){
        for(int i = 0; i < colors.Count; i++){
            if(colors[i].color == color){
                return true;
            }
        }
        return false;
    }
    public void AddColor(Color color){
        if(!IsColor(color)){
            GameObject newColor = Instantiate(colorPrefab);
            newColor.GetComponent<SpriteRenderer>().color = color;
            TextureKeeper textureKeeper = FindAnyObjectByType<TextureKeeper>();
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            Sprite s = textureKeeper.GetTexture(sr.sprite.name);
            if(s == null){
                s = textureKeeper.AddTexture(sr.sprite.texture, sr.sprite.name);
            }
            newColor.GetComponent<SpriteRenderer>().sprite = s;
            newColor.transform.localScale = transform.localScale;
            newColor.transform.position = transform.position; //position the color at the center of the chromaterator then move back 1 unit
            newColor.transform.Translate(new Vector3(0, 0, 1 + (colors.Count * 0.02f)));
            colors.Add(new ColorObj{color = color, transform = newColor.transform});
        }
    }
    public void RemoveColor(Color color){
        for(int i = 0; i < colors.Count; i++){
            if(colors[i].color == color){
                Destroy(colors[i].transform.gameObject);
                colors.RemoveAt(i);
                return;
            }
        }
    }
    public void RemoveAllColors(){
        while(colors.Count > 0){
            RemoveColor(colors[0].color);
        }
    }
}
[System.Serializable]
class ColorObj{
    public Color color;
    public Transform transform;
}

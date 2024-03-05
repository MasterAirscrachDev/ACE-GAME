using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureKeeper : MonoBehaviour
{
    [SerializeField] List<MonoTexture> textures;

    public Sprite GetTexture(string name){
        foreach(MonoTexture texture in textures){
            if(texture.name == name){
                return Sprite.Create(texture.texture, new Rect(0, 0, texture.texture.width, texture.texture.height), new Vector2(0.5f, 0.5f));
            }
        }
        return null;
    }
    public Sprite AddTexture(Texture2D texture, string name){
        //convert every pixel that is not transparent to white
        Texture2D newTexture = new Texture2D(texture.width, texture.height);
        for(int x = 0; x < texture.width; x++){
            for(int y = 0; y < texture.height; y++){
                if(texture.GetPixel(x, y).a > 0.5f){
                    newTexture.SetPixel(x, y, Color.white);
                }else{
                    newTexture.SetPixel(x, y, Color.clear);
                }
            }
        }
        newTexture.Apply();
        newTexture.filterMode = texture.filterMode;
        textures.Add(new MonoTexture(newTexture, name));
        return Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f));
    }

}
[System.Serializable]
class MonoTexture{
    public Texture2D texture;
    public string name;
    public MonoTexture(Texture2D texture, string name){
        this.texture = texture;
        this.name = name;
    }
}
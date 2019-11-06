using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ImagemFundo : MonoBehaviour
{
    //public Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {

        CarregarFundo();

    }


    private void CarregarFundo()
    {
        Texture2D tex = new Texture2D(2, 2);
        byte[] fileData;
        string path = configJogo.getimgFundo();
        DirectoryInfo dir = new DirectoryInfo(path);
        List<FileInfo> imgs = new List<FileInfo>();
        foreach (FileInfo file in dir.GetFiles())
        {
            if (file.Extension == ".png" || file.Extension == ".jpg" || file.Extension == ".jpeg")
            {
                imgs.Add(file);
            }
        }
        fileData = File.ReadAllBytes(imgs[Random.Range(0, imgs.Count)].FullName);
        tex.LoadImage(fileData);
        Sprite sprite = this.GetComponent<SpriteRenderer>().sprite;
        sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f,tex.width , tex.height), new Vector2(0.5f, 0.5f), 100.0f); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

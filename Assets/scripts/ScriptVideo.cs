using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScriptVideo : MonoBehaviour
{

    public RawImage imagem;
    public string tipoImagem ;
    private string path;
    
    void Start()
    {
        LoadImagens();
    } 

    public void LoadImagens()
    {
        if (tipoImagem.Equals(""))
        {
            tipoImagem = "png";
        }
        path = configJogo.GetImgCena(this.name + "." + tipoImagem);        
        byte[] fileData;
        Texture2D tex = null;

        if (File.Exists(path))
        {
            fileData = File.ReadAllBytes(path);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);

        }
        else
        {
            this.gameObject.active = false;
        }

        imagem.texture = tex;
    }

    public void Jogar()
    {
        configJogo.setFase(this.name);
        SceneManager.LoadScene(1);
        Destroy(GetComponent<Camera>().gameObject);
    }

    public void SairJogo()
    {
        Application.Quit();
    }

}

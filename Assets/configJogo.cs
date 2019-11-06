using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class configJogo : MonoBehaviour
{
    public bool pause;
    public AudioSource certo, errado, somAmbiente, ganhou, perdeu;
    public Material PErro, PAcerto;
    public bool trocaImg;
    public List<string> letrasFaltantes;
    public float timerLetras = 0;
    static string fase_selecionada;
    static string linguagem;
    // Start is called before the first frame update
    void Start()
    {
        fase_selecionada = "";
        letrasFaltantes = new List<string>();
        if (timerLetras == 0)
        {
            timerLetras = 0.05f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pause) return;
        if (!somAmbiente.isPlaying)
        {
            somAmbiente.Play();
        }       
    }

    public void PlayErrou()
    {
        if  (!pause)
             errado.Play();
    }

    public void PlayAcertou()
    {
        if (!pause)
            certo.Play();
    }

    public static string GetPathLinguagem()
    {
        if (linguagem == "" || linguagem == null)
        {
            linguagem = "Português";
        }
        string r = Application.dataPath + "/Imagens/leves/"+linguagem;
        return r.Replace('/', Path.DirectorySeparatorChar);
    }


    public static string GetPathCenas()
    {
        string r =  GetPathLinguagem()+"/cenas";
        return r.Replace('/', Path.DirectorySeparatorChar);
    }
    public static string GetFaseSelecionada()
    {
        string r = GetPathCenas()+"/" + fase_selecionada;
        return r.Replace('/', Path.DirectorySeparatorChar); 
    }

    public static string GetImgCena(string file)
    {
        string r = GetPathCenas()+"/"+file;
        return r.Replace('/', Path.DirectorySeparatorChar); 
    }

    public static void setFase(string fase)
    {
        fase_selecionada = fase;
    }

    public static string getimgFundo()
    {
        string r = GetImgCena(fase_selecionada)+ "/fundo";
        return r.Replace('/', Path.DirectorySeparatorChar);
    }





}

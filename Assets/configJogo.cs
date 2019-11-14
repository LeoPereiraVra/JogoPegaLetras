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
    public static string linguagem;
    static int nivel;
    public Canvas canvasMenu;
    // Start is called before the first frame update
    void Start()
    {
        canvasMenu.enabled = false;
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

    public string getLinguagem()
    {
        return linguagem;
    }

    public static string getPathConfiguracao()
    {
        return configJogo.GetPathInicial() + Path.DirectorySeparatorChar + "config.txt";
    }

    public static string GetPathInicial()
    {
        string r = Directory.GetParent(Application.dataPath) + "/Imagens/leves";
        return r.Replace('/', Path.DirectorySeparatorChar);
    }

    public static string GetPathLinguagem()
    {
        if (linguagem == "" || linguagem == null)
        {
            linguagem = "Português";
        }
        
        return GetPathInicial() + Path.DirectorySeparatorChar + linguagem;
    }


    public static string GetPathCenas()
    {        
        return GetPathLinguagem() + Path.DirectorySeparatorChar + "cenas";
    }
    public static string GetFaseSelecionada()
    {

        return GetPathCenas() + Path.DirectorySeparatorChar + fase_selecionada;
    }

    public static string GetImgCena(string file)
    {
        return GetPathCenas() + Path.DirectorySeparatorChar + file;
    }
        public static void setFase(string fase)
    {
        fase_selecionada = fase;
    }

    public static string getimgFundo()
    {       
        return GetImgCena(fase_selecionada) + Path.DirectorySeparatorChar + "fundo";
    }
    public void mostrarMenu()
    {
        canvasMenu.enabled = true;
    }

    public static void SetNivel(int ni)
    {
        nivel = ni;
    }
    public static void SetLinguagem(string l)
    {
        linguagem = l;
    }





}

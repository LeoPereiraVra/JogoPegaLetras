using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum TIPO_IMG
{
    IMG_G, IMG_P
}

public class PalavrasJogo : MonoBehaviour
{    
    private List<string> palavras;
    private List<Sprite> imagens;
    public Sprite gameOver;
    public TextMeshProUGUI palavra;
    public TextMeshProUGUI contagem;
    public TextMeshProUGUI TxtVidas;
    public configJogo config;
    public random_positions randomPosition;
    public voltar voltar;
    private string path;
    public SpriteRenderer fundo;
    Image img;
    int index;
    bool iniciou, movimentaImg;
    float tempo, tmpMovImg;
    string letrasEncontradas; 
    List<int> ordem;
    TIPO_IMG tipo;
    int vidas;

    const string acentos = "ÁÀÃÂÉÈÊÍÌîÓÒÕôÚÙû";
    const string semAcento = "AAAAEEEIIIOOOOUUU";    
    void Start()
    {
        path = configJogo.GetFaseSelecionada();        
        palavras = new List<string>();
        imagens = new List<Sprite>();
        ListarImagensPalavras();

        img = GetComponent<Image>();
        palavra.GetComponent<TextMeshProUGUI>().text = "";
        contagem.GetComponent<TextMeshProUGUI>().text = "";
        index = 0;
        iniciou = false;
        tempo = 5;
        tmpMovImg = 2;
        letrasEncontradas = "";
        ordem = new List<int>();
        for (int i = 0; i <= palavras.Count -1; i++)
            ordem.Add(i);
        SortearOrdemPalavra();
        movimentaImg = false;
        tipo = TIPO_IMG.IMG_G;
        vidas = 5;        
    }

    

    private void ListarImagensPalavras()
    {        
         
        DirectoryInfo dir = new DirectoryInfo(path);       
        foreach (FileInfo file in dir.GetFiles())
        {

            if (file.Extension == ".png" || file.Extension == ".jpg" || file.Extension == "jpeg")
            {
                byte[] fileData;
                try {
                    Sprite sprite;
                    Texture2D tex = new Texture2D(2,2);
                    fileData = File.ReadAllBytes(file.FullName);
                    tex.LoadImage(fileData);
                    sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);                                      
                    imagens.Add(sprite);
                    palavras.Add(file.Name.Replace(file.Extension,""));

                } catch (Exception e)
                {
                    String erro = e.Message;
                }
                
            }
            
            
        }
        
    }

    private void FimDeJogo()
    {        
        config.somAmbiente.Pause();
        if (ordem.Count <= 0)
        {
            config.perdeu.Play();
        }
        else
        {
            config.ganhou.Play();
        }
        voltar.inicio();
    }


    void Update()
    {
        bool Fim = false;
        if (!iniciou)
        {
            tempo -= Time.deltaTime;
            if ((tempo <= 5) && (tempo > 4))
                contagem.GetComponent<TextMeshProUGUI>().text = "5";
            else if (tempo <= 4 && tempo > 3)
                contagem.GetComponent<TextMeshProUGUI>().text = "4";
            else if (tempo <= 3 && tempo > 2)
                contagem.GetComponent<TextMeshProUGUI>().text = "3";
            else if (tempo <= 2 && tempo > 1)
                contagem.GetComponent<TextMeshProUGUI>().text = "2";
            else if (tempo <= 1 && tempo > 0)
                contagem.GetComponent<TextMeshProUGUI>().text = "1";
            else
            {
                contagem.GetComponent<TextMeshProUGUI>().text = "";
                iniciou = true;
                randomPosition.config.pause = false;
                tempo = 5;
            }
        }       
        else
        {
            if (letrasEncontradas.Equals(""))
            {                
                if (!movimentaImg)
                {
                    index = ordem[ordem.Count - 1];
                    ordem.RemoveAt(ordem.Count - 1);                                                           
                    trocarImagem(index);
                    aumentarImagem();
                    movimentaImg = true;
                }                
            }else if (!letrasEncontradas.Contains("_") && ordem.Count > 0)
            {
                letrasEncontradas = "";                
            }/*else if (ordem.Count == 0) {
                Fim = true;
            }*/

            if (movimentaImg)
            {
                randomPosition.config.pause = true;
                tmpMovImg -= Time.deltaTime;                
                if (tmpMovImg <= 0)
                {
                    if (tipo == TIPO_IMG.IMG_G)
                    {
                        diminuirImagem();
                        tmpMovImg = 2;
                        movimentaImg = false;
                    }
                    else
                    {
                        aumentarImagem();
                        tmpMovImg = 2;
                        movimentaImg = false;
                    }
                }
            }
            else{ randomPosition.config.pause = false; }
               

            if (Fim)
            {
                FimDeJogo();
                return;
            }


        }
        if (vidas == 0)
        {
            img.sprite = gameOver;
            aumentarImagem();
            movimentaImg = false;
            config.pause = true;
            FimDeJogo();
        }
        TxtVidas.text = vidas+"";


    }

    public void SortearOrdemPalavra()
    {
        int qtd = palavras.Count / 2;
        for (int i =0;i < qtd;i++)
        {
            int x,y,tmp;
            x = Random.Range(0, qtd);
            y = Random.Range(qtd, ordem.Count);
            tmp = ordem[x];
            ordem[x] = ordem[y];
            ordem[y] = tmp;
        }
    }

    public bool SetLetra(string letra)
    {
        if (!letra.Equals(""))
            if (EscreveLetra(letra.ToUpper()))
            {
                config.PlayAcertou();
                return true;
            }
            else
            {
                if (vidas > 0)
                {
                    config.PlayErrou();
                    vidas = vidas - 1;
                }
                return false;
            }
        return false;
    }

    private void trocarImagem(int idx)
    {
        config.trocaImg = true;
        if (idx <= imagens.Count)
        {
            string text = "";
            img.sprite = imagens[idx];
            if (letrasEncontradas.Equals("")) { 
                for (int i =0; i <= palavras[idx].Length - 1; i++)
                {
                    if (palavras[idx][i].Equals(' '))
                        text = text + " ";
                    else
                    {
                        text = text + "_";
                        config.letrasFaltantes.Add(palavras[index][i] + "");
                    }
                }
                letrasEncontradas = text;
                palavra.GetComponent<TextMeshProUGUI>().text = letrasEncontradas;                
            }            
            index = idx;
        }
    }   


    private bool EscreveLetra(string letra)
    {
        bool achou = false;
        string txtSemAcento = tirarAcento(palavras[index]).ToUpper();
        if (txtSemAcento.Contains(letra+""))
        {
            
            string Novotext = "";            
            for (int i = 0; i <= txtSemAcento.Length -1; i++)
            {                
                string letEncSemAcento = tirarAcento(letrasEncontradas);
                if ((txtSemAcento[i]+"").Equals(letra) && (letrasEncontradas[i]+"").Equals("_") && !achou)
                {
                    Novotext = Novotext + palavras[index][i];
                    config.letrasFaltantes.Remove(palavras[index][i]+"");
                    achou = true;
                }
                else if ((letEncSemAcento[i]+"").Equals(txtSemAcento[i]+""))
                    Novotext = Novotext + palavras[index][i];
                else Novotext = Novotext + "_";

            }
            letrasEncontradas = Novotext;
            palavra.GetComponent<TextMeshProUGUI>().text = letrasEncontradas;
            return true;
        }
        else
        {            
            return false;
        }
        
    }

    public static string tirarAcento(string palavra)
    {
        string retorno = "";     
        for (int i =0;i <= palavra.Length-1;i++)
        {
            int idx = acentos.IndexOf(palavra[i] + "");
            if (idx == -1)
                retorno = retorno + palavra[i];
            else retorno = retorno + semAcento[idx];
            
        }
        return retorno;
    }

    private void diminuirImagem()
    {
        this.img.transform.localPosition = new Vector3(310, 60, 0);
        this.img.transform.localScale = new Vector3(1.2f, 1, 1);
        tipo = TIPO_IMG.IMG_P;
        config.trocaImg = false;
    }

    private void aumentarImagem()
    {
        this.img.transform.localPosition = new Vector3(-0.51721f,0,0);
        this.img.transform.localScale = new Vector3(3, 2, 2);
        tipo = TIPO_IMG.IMG_G;
    }
}

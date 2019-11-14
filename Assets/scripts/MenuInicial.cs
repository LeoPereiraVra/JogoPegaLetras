using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;

public class MenuInicial : MonoBehaviour
{
    public List<Button> fases;
    public Button sair;
    public string tipoImagem;
    private string path;

    public Dropdown linguagem;
    public Dropdown nivel;
    public Button salvar;
    public Button voltar;
    // Start is called before the first frame update
    void Start()
    {
        sair.onClick.AddListener(SairJogo);
        linguagem.gameObject.SetActive(false);
        nivel.gameObject.SetActive(false);
        voltar.gameObject.SetActive(false);
        salvar.gameObject.SetActive(false);
        LoadImagens();
        MouseControl.MouseMove(new Vector3(500f, 210f, 0), new GUIText());
        loadConfigurcao();
    }

    private void loadConfigurcao()
    {
       
            if (PlayerPrefs.HasKey("LINGUAGEM"))
            {                
                configJogo.SetLinguagem(PlayerPrefs.GetString("LINGUAGEM"));           
            }
            else
            {
                PlayerPrefs.SetString("LINGUAGEM", "Português");
                configJogo.SetLinguagem("Português");
            }
            if (PlayerPrefs.HasKey("NIVEL"))
            {
                configJogo.SetNivel(PlayerPrefs.GetInt("NIVEL"));
            }
            else
            {
                PlayerPrefs.SetInt("NIVEL", 0);
                configJogo.SetNivel(0);
            }
            
        
    }

    public void LoadImagens()
    {
        for (int i = 0; i < fases.Count; i++)
        {
            fases[i].GetComponent<ScriptVideo>().LoadImagens();
        }


    }
    private void LoadLinguagem()
    {
        linguagem.ClearOptions();
        string path = configJogo.GetPathInicial();
        DirectoryInfo dir = new DirectoryInfo(path);
        for (int i = 0; i < dir.GetDirectories().Length;i++)
        {
            DirectoryInfo diretorio = dir.GetDirectories()[i];
            linguagem.options.Add(new OptionData
            {
                text = diretorio.Name
            });
            if (configJogo.linguagem == diretorio.Name)
            {
                linguagem.value = i;
            }
        }
                 
    }

    public void configuraJogo()
    {
        for (int i = 0; i < fases.Count; i++)
        {
            fases[i].gameObject.SetActive(false);
        }
        LoadLinguagem();
        sair.gameObject.SetActive(false);
        linguagem.gameObject.SetActive(true);
        nivel.gameObject.SetActive(true);
        voltar.gameObject.SetActive(true);
        salvar.gameObject.SetActive(true);
    }   

    public void LoadFases()
    {
        linguagem.gameObject.SetActive(false);
        nivel.gameObject.SetActive(false);
        voltar.gameObject.SetActive(false);
        salvar.gameObject.SetActive(false);
        sair.gameObject.SetActive(true);
        LoadImagens();
    }

    public void SalvarConfiguracao()
    {        
        PlayerPrefs.SetString("LINGUAGEM", linguagem.options[linguagem.value].text);
        configJogo.SetLinguagem(linguagem.options[linguagem.value].text);
        PlayerPrefs.SetInt("NIVEL", nivel.value);
        configJogo.SetNivel(nivel.value);
        LoadFases();
    }
    
    void Update()
    {
        
    }

    void SairJogo()
    {
        Application.Quit();
    }

    
}

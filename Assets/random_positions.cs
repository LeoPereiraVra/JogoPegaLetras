using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random_positions : MonoBehaviour
{
    // Start is called before the first frame update
    private float InstantiationTimer = 3f;
    public configJogo config;
    // Start is called before the first frame update
    private string letras = "ABCDEFGHIJKLMNOPQRSTUVXWYZÇ";    
    int cont;
    void Start()
    {
        config.pause = true;        
    }    

    // Update is called once per frame
    void Update()
    {
        if (config.pause || config.trocaImg)
        { return; }
        InstantiationTimer -= Time.deltaTime;
        if (InstantiationTimer <= 0)
        {
            int index = -1;
            if (cont <= 3)
            {
                index = Random.Range(0, this.letras.Length - 1);
                cont++;
            }
            else
            {
                int p = Random.Range(0, config.letrasFaltantes.Count- 1);
                index = letras.IndexOf(PalavrasJogo.tirarAcento(config.letrasFaltantes[p]));                    
                cont = 0;
            }
                   
            if (index >= 0 && index <= this.letras.Length)
            {
                float eixoX = Random.Range(-1f, 1.2f); 
                float eixoY = Random.Range(2.5f, 3.7f);
                    
                char let = this.letras[index];
                GameObject letra = (GameObject)Instantiate(Resources.Load("Prefabs/" + let), new Vector3(eixoX, eixoY, this.transform.localPosition.z + 15f), Quaternion.identity);
                letra.transform.Rotate(0, 180, 0);                    
                letra.GetComponent<movement>().config = this.GetComponent<configJogo>();                    
                InstantiationTimer = 3f;
            }
        }
        
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public bool shouldMove = false;
    public configJogo config;
    public MeshRenderer mesh;
    private float timer;
    private bool alterouVel;
    // Start is called before the first frame update
    void Start()
    {
        alterouVel = false;
        timer = config.timerLetras;
        if (mesh != null)
        {
            float r = Random.Range(0.0f, 1f);
            float g = Random.Range(0.0f, 1f);
            float b = Random.Range(0.0f, 1f);
            mesh.materials[0].color = new Color(r, g, b);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (config.trocaImg)
        {
            Destroy(this.GetComponent<GameObject>(),0.1f);
        }
        if (config.pause)
        {
            return;
        }
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (!this.shouldMove)
            {
                transform.localPosition += Vector3.back * 1 / 24;
                alterouVel = false;
            }
            else
            {
                transform.localPosition += Vector3.forward * 1 / 16;                
                alterouVel = false;
            }

            timer = config.timerLetras;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && !alterouVel)
        {            
            config.timerLetras = config.timerLetras - 0.01f;
            if (config.timerLetras < 0.00f)
            {
                config.timerLetras = 0.00f;
            }

            alterouVel = true;
        }
        else
        if (Input.GetKeyDown(KeyCode.DownArrow) && !alterouVel)
        {
            config.timerLetras = config.timerLetras + 0.01f;
            if (config.timerLetras > 0.05f)
            {
                config.timerLetras = 0.05f;
            }
            alterouVel = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
       
        this.shouldMove = true;
        Destroy(this);

    }


}
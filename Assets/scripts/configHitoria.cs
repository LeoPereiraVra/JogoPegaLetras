using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class configHitoria : MonoBehaviour
{
    private GestureListener gestureListener;
    public static string levelSelecionado;
    // Start is called before the first frame update
    void Start()
    {
        gestureListener = Camera.main.GetComponent<GestureListener>();
    }

    // Update is called once per frame
    void Update()
    {      

            KinectManager kinectManager = KinectManager.Instance;
        if ((!kinectManager || !kinectManager.IsInitialized() || !kinectManager.IsUserDetected()))
            return;

         //if (gestureListener.IsWave())
         //   voltar();
        //else if (gestureListener.IsJump())
        //    jogar();              
    }

    void voltar()
    {
        SceneManager.LoadScene(0);
        Destroy(GetComponent<Camera>().gameObject);
    }

    public void jogar()
    {
        
        
    }
}

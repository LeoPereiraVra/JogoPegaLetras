using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class voltar : MonoBehaviour
{
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void inicio()
    {
        SceneManager.LoadScene(0);
        Destroy(camera.gameObject, 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        inicio();
    }
}

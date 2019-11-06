using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInicial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MouseControl.MouseMove(new Vector3(500f, 210f, 0), new GUIText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}

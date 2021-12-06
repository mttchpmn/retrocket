using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleKeyPress();
    }

    private void HandleKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Quit();
    }

    private void Quit()
    {
        Debug.Log("Quit called");
        Application.Quit();
    }
}

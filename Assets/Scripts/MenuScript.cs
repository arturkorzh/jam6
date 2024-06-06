using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        KeyCode quitKey = KeyCode.Escape;


        if (Input.GetKeyDown(quitKey))
        {
            Application.Quit();

            Debug.Log("Quit Game.");
        }
        else if (Input.anyKey)
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);

            Debug.Log("Next Scene.");
        }
    }
}
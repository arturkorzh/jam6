using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MenuScript : MonoBehaviour
{
    public VideoPlayer video;

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
            video.gameObject.SetActive(true);
           // SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);

            Debug.Log("Next Scene.");
        }
    }
}
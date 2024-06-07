using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject Animation;

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
            Animation.SetActive(true);
            StartCoroutine(DelayRun(1.5f, () => SceneManager.LoadScene("Level0", LoadSceneMode.Single)));
        }
    }

    public static IEnumerator DelayRun(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
    }
}
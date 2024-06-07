using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject WinOverlay;
    public GameObject LooseOverlay;
    public GameObject CloseAnimation;
    public GameObject Actor;
    public BulletsController bulletsController;
    public bool IsWin;
    public bool IsLoose;

    private class Point
    {
        public int x { get; set; }
        public int y { get; set; }

        public Point(int X, int Y)
        {
            x = X;
            y = Y;
        }
    }

    public static List<int[,]> Figures = new()
    {
        /* new int[6, 7]
         {
             { 0, 0, 0, 0, 1, 0, 0 },
             { 1, 1, 0, 1, 1, 1, 0 },
             { 0, 1, 1, 1, 1, 1, 1 },
             { 0, 1, 1, 1, 1, 1, 1 },
             { 1, 1, 0, 1, 1, 1, 0 },
             { 0, 0, 0, 0, 1, 0, 0 },
         },*/
        new int[6, 4]
        {
            { 0, 1, 0, 0 },
            { 1, 1, 1, 0 },
            { 1, 1, 1, 1 },
            { 1, 1, 1, 1 },
            { 1, 1, 1, 0 },
            { 0, 1, 0, 0 },
        },
        new int[5, 5]
        {
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
        },
    };

    private static int[] _condition = new[] {24};

    public static int CurrentLevel = 0;

    public static bool CheckWinCondition()
    {
        return PlayerView.Instance.Joinable.Elements.Count == _condition[CurrentLevel];
    }

    public void Exit() => Application.Quit();


    public void Retry()
    {
        SceneManager.LoadScene($"Level{CurrentLevel}", LoadSceneMode.Single);
        Debug.Log("Retry level scene.");
    }

    public void NextLvl()
    {
        SceneManager.LoadScene($"Level1", LoadSceneMode.Single);

        Debug.Log("New level scene.");
    }

    private void Awake()
    {
        EventManager.OnLose += LoseGame;
        EventManager.OnWin += WinGame;
        EventManager.OnSpeedModified += ModifySpeed;
        EventManager.OnBulletsModified += ModifyBullets;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void WinGame()
    {
        if (IsWin) return;
        IsWin = true;
        CloseAnimation.SetActive(true);
        StartCoroutine(MenuScript.DelayRun(1.5f, () => WinOverlay.SetActive(true)));
        Debug.Log("Win!");
    }

    private void LoseGame()
    {
        if (IsLoose) return;
        IsLoose = true;
        CloseAnimation.SetActive(true);
        StartCoroutine(MenuScript.DelayRun(1.5f, () => LooseOverlay.SetActive(true)));
        Debug.Log("Lose!");
    }

    private void ModifySpeed(bool b)
    {
        if (Actor == null) return;
        var coef = Actor.GetComponent<PlayerMoveController>()._speedCoef;
        Actor.GetComponent<PlayerMoveController>().SetSpeedCoef(coef + (b ? -0.15f : 0.15f));
    }

    private void ModifyBullets(bool b)
    {
        if (bulletsController == null) return;
        if (b)
        {
            bulletsController.bulletsAmount++;
        }
        else
            bulletsController.bulletsAmount--;
    }
}
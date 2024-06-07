using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject WinOverlay;
    public GameObject LooseOverlay;
    public GameObject CloseAnimation;

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

    public static int CurrentLevel = 0;
    public const float CellSize = 0.5f;

    public static bool CheckWinCondition(List<JoinableElementView> elements, Vector3 actorPosition)
    {
        var tem = elements.Select(x => x.transform.InverseTransformPoint(actorPosition));

        var temp = tem.Select(x =>
            new Point(((int)(x.x * 100 / 7) - 1) / 2,
                ((int)(x.y * 100 / 7) - 1) / 2)).ToList();
        var minX = temp.Min(x => x.x);
        var minY = temp.Min(x => x.y);

        var xSize = temp.Max(x => x.x) - minX + 1;
        var ySize = temp.Max(x => x.y) - minY + 1;

        var matrix = new int[xSize, ySize];

        foreach (var value in temp)
        {
            value.x += minX > 0 ? minX : -minX;
            value.y += minY > 0 ? minY : -minY;
        }

        foreach (var vector in temp)
            matrix[vector.x, vector.y] = 1;

        if (matrix.GetLength(0) != Figures[CurrentLevel].GetLength(0) ||
            matrix.GetLength(1) != Figures[CurrentLevel].GetLength(1))
            return false;
        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            for (var j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] != Figures[CurrentLevel][i, j])
                    return false;
            }
        }

        return true;
    }

    public void Exit()
    {
    }

    public void Retry()
    {
        SceneManager.LoadScene($"Level{CurrentLevel}", LoadSceneMode.Single);
        Debug.Log("Retry level scene.");
    }

    public void NextLvl()
    {
        CurrentLevel++;
        SceneManager.LoadScene($"Level{CurrentLevel}", LoadSceneMode.Single);

        Debug.Log("New level scene.");
    }

    private void Awake()
    {
        EventManager.OnLose += LoseGame;
        EventManager.OnWin += WinGame;
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
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    public List<int[,]> Figures = new()
    {
        new int[5, 5]
        {
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
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

    public int CurrentLevel;
    public const float cellSize = 0.15f;

    public static bool CheckWinCondition(List<JoinableElementView> elements, Vector3 actorPosition)
    {
        var tem = elements.Select(x => x.transform.InverseTransformPoint(actorPosition));

        var temp = tem.Select(x => new Point((int)(x.x / cellSize - 1f), (int)(x.y / cellSize - 1f))).ToList();
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

        return true;
    }

    private void Awake()
    {
        EventManager.OnLose += LoseGame;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void LoseGame()
    {
        Debug.Log("Lose!");
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public static PathFinding Instance { get; private set; }
    public GameObject prefab;
    public Point startFirst;
    public Point startSecond;
    public Point endFirst;
    public Point endSecond;
    public int xValue;
    public int yValue;
    public List<Point> shortestPathFirst;
    public List<Point> shortestPathSecond;

    [Serializable]
    public struct Point
    {
        public int x;
        public int y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    public static int[,] grid = new int[,]
    {
        { 0, 0, 0, 0, 0 ,0 , 0},
        { 0, 1, 1, 1, 1 ,0 , 0},
        { 0, 0, 0, 0, 0 ,1 , 0},
        { 0, 0, 0, 0, 0 ,1 , 0},
        { 0, 0, 0, 0, 0 ,1 , 0},
        { 0, 0, 0, 0, 0 ,1 , 0},
        { 0, 0, 0, 0, 0 ,1 , 0},
        { 0, 0, 0, 1, 0 ,1 , 0},
        { 0, 0, 0, 1, 0 ,1 , 0},
        { 0, 0, 0, 1, 0 ,0 , 0},
    };


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        CreatePath(startFirst, endFirst, out shortestPathFirst);
        CreatePath(startSecond, endSecond, out shortestPathSecond);
        /*Show();*/
    }
    public static List<Point> GetShortestPath(Point start, Point end)
    {
        Queue<Point> queue = new Queue<Point>();
        bool[,] visited = new bool[grid.GetLength(0), grid.GetLength(1)];
        Point[,] parent = new Point[grid.GetLength(0), grid.GetLength(1)];

        int[] dx = { 1, -1, 0, 0 };
        int[] dy = { 0, 0, 1, -1 };

        queue.Enqueue(start);
        visited[start.x, start.y] = true;

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                parent[i, j] = new Point(10000,10000); 
            }
        }
        
        while (queue.Count > 0)
        {
            Point current = queue.Dequeue();

            if (current.x == end.x && current.y == end.y)
                break;

            for (int i = 0; i < 4; i++)
            {
                int newX = current.x + dx[i];
                int newY = current.y + dy[i];

                if (newX >= 0 && newX < grid.GetLength(0) && newY >= 0 && newY < grid.GetLength(1) &&
                    grid[newX, newY] == 0 && !visited[newX, newY])
                {
                    queue.Enqueue(new Point(newX, newY));
                    visited[newX, newY] = true;
                    parent[newX, newY] = current;
                }
            }
        }

        List<Point> shortestPath = new List<Point>();
        Point currentPoint = end;

        while (currentPoint.x != start.x || currentPoint.y != start.y)
        {
            shortestPath.Insert(0, currentPoint);
            if (parent[currentPoint.x, currentPoint.y].x != 10000 && parent[currentPoint.x, currentPoint.y].y != 10000)
            {
                currentPoint = parent[currentPoint.x, currentPoint.y];
            }
            else
            {
                currentPoint = start;
            }
        }

        shortestPath.Insert(0, start);
        return shortestPath;
    }

    public void CreatePath(Point start, Point end, out List<Point> _shortestPath)
    {
        List<Point> shortestPath = GetShortestPath(start, end);

        if (shortestPath.Count > 2)
        {
            if (shortestPath.Count > 0)
            {
                for (int i = 1; i < shortestPath.Count - 1; i++)
                {
                    Instantiate(prefab, new Vector3(shortestPath[i].x, 1, shortestPath[i].y), Quaternion.identity);
                }
            }
            else
            {
                Debug.Log("No valid path found.");
            }
            _shortestPath = shortestPath;
        }
        else
        {
            _shortestPath = null;
        }
    }
    public List<Point> GetPath(Point start, Point end)
    {

        List<Point> shortestPath = GetShortestPath(start, end);

        if (shortestPath.Count > 3)
        {
/*            if (shortestPath.Count > 0)
            {
                for (int i = 1; i < shortestPath.Count - 1; i++)
                {
                    Instantiate(prefab, new Vector3(shortestPath[i].x, 1, shortestPath[i].y), Quaternion.identity);
                }
            }
            else
            {
                return null;
            }*/
            return shortestPath;
        }
        else
        {
            return null;
        }
    }

    public void DestroyMarkers()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("marker");
        foreach (var obj in objs)
        {
            Destroy(obj);
        }
    }

    public void ChangeValueGrid()
    {
        if (grid[xValue, yValue] == 1)
        {
            grid[xValue, yValue] = 0;
        }
        else
        {
            grid[xValue, yValue] = 1;
        }
    }

    public void UpdateAllPath()
    {
        ChangeValueGrid();
        if (GetPath(new Point(0, 0), new Point(9, 4)) != null && GetPath(new Point(3, 6), new Point(9, 4)) != null)
        {
            DestroyMarkers();
            CreatePath(startFirst, endFirst, out shortestPathFirst);
            CreatePath(startSecond, endSecond, out shortestPathSecond);
        }
        else
        {
            Debug.Log("null");
        }
    }
}






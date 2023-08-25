using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public Vector3[] path;
    public float moveSpeed = 2f;
    public int index = 0;
    public bool ismoved = false;

    private void Start()
    {
    }

    private void OnEnable()
    {
        GetPathMove();
        ismoved = true;
    }
    private void Update()
    {
        if (ismoved)
        {
            Move();
        }
/*        if(Input.GetKeyDown(KeyCode.Space))
        {
            ismoved = true;
            GetPathMove();
        }*/
    }
    public void Move()
    {
        if (index <= path.Length - 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, path[index], moveSpeed * Time.deltaTime);

            if (transform.position == path[index])
            {
                index++;
            }
        }
    }

    public void GetPathMove()
    {
        if(transform.position == new Vector3(0, 1, 0))
        {
            int lenghth = PathFinding.Instance.shortestPathFirst.Count;
            path = new Vector3[lenghth];
            for (int i = 0; i < lenghth; i++)
            {
                path[i] = new Vector3(PathFinding.Instance.shortestPathFirst[i].x, 1, PathFinding.Instance.shortestPathFirst[i].y);
            }
        }
        else
        {
            int lenghth = PathFinding.Instance.shortestPathSecond.Count;
            path = new Vector3[lenghth];
            for (int i = 0; i < lenghth; i++)
            {
                path[i] = new Vector3(PathFinding.Instance.shortestPathSecond[i].x, 1, PathFinding.Instance.shortestPathSecond[i].y);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjectOnUI : MonoBehaviour
{
    public GameObject prefab;
    public Camera mainCamera;
    public Grid grid;
    public Vector3 pos;
    public void CreateHero()
    {
        Instantiate(prefab, pos, Quaternion.identity);
    }
}

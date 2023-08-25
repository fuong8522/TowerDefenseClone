using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    public Grid grid;
    public float value;
    private bool isOnclicked;

    private void OnEnable()
    {
        isOnclicked = true;
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        
    }
    private void Update()
    {
        if(isOnclicked)
        {
            FollowMousePos();
        }
        if(Input.GetMouseButtonUp(0))
        {
            isOnclicked = false;
        }
    }
    private void OnMouseDrag()
    {
        FollowMousePos();
    }

    public void FollowMousePos()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 100))
        {
            Vector3 pos = raycastHit.point;
            pos.x += value;
            pos.z += value;
            Vector3Int gridPosition = grid.WorldToCell(pos);
            transform.position = grid.CellToWorld(gridPosition);
        }
    }

}

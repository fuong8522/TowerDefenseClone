using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGround : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        UpdatePathFinding(other);
    }
    private void OnTriggerExit(Collider other)
    {
        UpdatePathFinding(other);
    }

    public void UpdatePathFinding( Collider other)
    {
        if (other.gameObject.CompareTag("Hero"))
        {
            PathFinding.Instance.xValue = (int)transform.position.x;
            PathFinding.Instance.yValue = (int)transform.position.z;
            PathFinding.Instance.UpdateAllPath();
        }
    }
}

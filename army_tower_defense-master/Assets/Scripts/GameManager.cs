using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] prefab;
    public Transform pos;
    public Transform pos2;
    public int number;
    public List<Wave> waves;
    private void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < waves[number].waves.Length; i++)
        {
            if (waves[number].gates[0] == 0)
            {
                SpawnEnemy(prefab[waves[number].waves[i]], pos);
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                SpawnEnemy(prefab[waves[number].waves[i]], pos2);
                yield return new WaitForSeconds(0.5f);
            }
        }
        number++;
    }

    public void SpawnEnemy(GameObject obj, Transform pos)
    {
        Instantiate(obj, pos.position, Quaternion.identity);
    }
}

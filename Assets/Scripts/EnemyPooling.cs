using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooling : MonoBehaviour
{
    public GameObject prefab;
    public int amount = 10;
    public int instantiateGap = 5;

    void Start()
    {
        InitializePool();
        InvokeRepeating("GetEnemyFromPool", 1f, instantiateGap);
    }

    private void InitializePool()
    {
        for (int i = 0; i < amount; i++)
        {
            AddEnemyToPool();
        }
    }

    private void AddEnemyToPool()
    {
        GameObject enemy = Instantiate(prefab, this.transform.position, Quaternion.identity, this.transform);
        enemy.SetActive(false);
    }

    private GameObject GetEnemyFromPool()
    {
        GameObject enemy = null;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).gameObject.activeSelf)
            {
                enemy = transform.GetChild(i).gameObject;
                break;
            }
        }

        if (enemy == null)
        {
            AddEnemyToPool();
            enemy = transform.GetChild(transform.childCount - 1).gameObject;
        }

        enemy.transform.position = this.transform.position;
        enemy.SetActive(true);
        return enemy;
    }
}

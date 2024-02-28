using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    float timer;
    public float spawnCooldownInSeconds = 1.5f;

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnCooldownInSeconds)
        {
            GameObject newEnemy = PoolManager.Instance.GetPooledObjects(1, GetRandomPoint(), new Quaternion(0,180,0,0));
            timer = 0;

            if(newEnemy != null)
            {
                newEnemy.SetActive(true);
            }
            else
            {
                Debug.LogError("Pool bullet muy pequeñooo");
            }

        }
    }

    public Vector3 GetRandomPoint()
    {
        Vector3 randomPoint;
        float randomX;
        float leftLimit;
        float rightLimit;

        leftLimit = transform.position.x - (transform.localScale.x / 2);
        rightLimit = transform.position.x + (transform.localScale.x / 2);

        randomX = Random.Range(leftLimit, rightLimit);

        randomPoint = new Vector3(randomX, transform.position.y, transform.position.z);

        return randomPoint;
    }
}

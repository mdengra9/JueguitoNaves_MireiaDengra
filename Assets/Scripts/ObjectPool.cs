using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefabToSpawn; // Prefab que quieres spawnear
    public Transform[] spawnPoints; // Puntos de spawn para cada pool
    public int poolSize = 5; // Tamaño de la pool
    public float moveSpeed = 5f; // Velocidad de movimiento en el eje -z
    public float spawnRange = 5f; // Rango de spawn

    private List<GameObject> objectPool = new List<GameObject>(); // Pool de objetos

    void Start()
    {
        // Crear la pool de objetos
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newObj = Instantiate(prefabToSpawn, Vector3.zero, Quaternion.identity);
            newObj.SetActive(false); // Desactivar el objeto inicialmente
            objectPool.Add(newObj);
        }

        // Llamar al método para empezar a spawnear objetos para cada pool
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int spawnIndex = i; // Guardar el valor actual de i para usarlo en la corrutina
            StartCoroutine(SpawnObjects(spawnIndex));
        }
    }

    // Método para spawnear objetos
    IEnumerator SpawnObjects(int spawnIndex)
    {
        while (true)
        {
            // Obtener un objeto de la pool
            GameObject obj = GetPooledObject();

            if (obj != null)
            {
                // Calcular una posición aleatoria dentro del rango de spawn
                Vector3 randomSpawnPos = spawnPoints[spawnIndex].position + Random.insideUnitSphere * spawnRange;
                randomSpawnPos.y = spawnPoints[spawnIndex].position.y; // Mantener la misma altura de spawn

                // Posicionar y activar el objeto
                obj.transform.position = randomSpawnPos;
                obj.SetActive(true);

                // Obtener el componente Rigidbody del objeto
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Mover el objeto en el eje -z con la velocidad especificada
                    rb.velocity = new Vector3(0f, 0f, -moveSpeed);
                }
                else
                {
                    Debug.LogWarning("El prefab no tiene Rigidbody adjunto. No se puede mover.");
                }
            }

            // Esperar un tiempo antes de volver a spawnear otro objeto
            yield return new WaitForSeconds(1.0f);
        }
    }

    // Método para obtener un objeto de la pool
    GameObject GetPooledObject()
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeInHierarchy)
            {
                return objectPool[i];
            }
        }

        // Si no hay objetos disponibles en la pool, se instancia uno nuevo
        GameObject newObj = Instantiate(prefabToSpawn);
        newObj.SetActive(false);
        objectPool.Add(newObj);
        return newObj;
    }
}

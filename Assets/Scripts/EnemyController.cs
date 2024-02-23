using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
 public float speed = 3f;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Si el enemigo sale de la pantalla, se desactiva para ser reutilizado
        if (!GetComponent<Renderer>().isVisible)
        {
            gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        // Cuando se desactiva, el enemigo vuelve al pool
        transform.position = new Vector3(10f, Random.Range(-4f, 4f), 0f);
    }
}

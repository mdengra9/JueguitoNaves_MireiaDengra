using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Si el proyectil sale de la pantalla, se desactiva para ser reutilizado
        if (!GetComponent<Renderer>().isVisible)
        {
            gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        // Cuando se desactiva, el proyectil vuelve al pool
        transform.position = new Vector3(10f, 0f, 0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController playerController;
    private Vector3 movement = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"),0,0);
        movement = transform.TransformDirection(movement);
        playerController.Move(movement * Time.deltaTime * 50);

        if (Input.GetKeyDown("space"))
        {
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identify);
            newBullet.GetComponent<Rigidbody>().AddForce(Vector3.up * 2000);
        }
    }
}

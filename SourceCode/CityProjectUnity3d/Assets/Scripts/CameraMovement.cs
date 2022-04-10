using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public float speed = 10.0F;
    public float rotationSpeed = 100.0F;

    void Start () {
	
	}
    void Update()
    {
        float v = 0;

        if (Input.GetKeyDown("q"))
            v = 10;

        if (Input.GetKeyDown("e"))
            v = -10;

        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(v, rotation, 0);
    }
}

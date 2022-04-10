using UnityEngine;
using System.Collections;

public class RotatingScript : MonoBehaviour {

	void Start () {
	
	}

    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 5);
    }
}

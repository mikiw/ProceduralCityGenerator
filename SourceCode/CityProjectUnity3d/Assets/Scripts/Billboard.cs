using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

    public MovieTexture movTexture;

    // Use this for initialization
    void Start () {
        GetComponent<Renderer>().material.mainTexture = movTexture;
        movTexture.Play();
        movTexture.loop = true;
    }

	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.down * Time.deltaTime * 15);
    }
}

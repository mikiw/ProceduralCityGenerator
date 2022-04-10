using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ButtonClicked : MonoBehaviour {

    public GameObject city1;
    public GameObject city2;
    public GameObject city3;
    public GameObject city4;

    void Start ()
    {

    }

    void Update () {
	
	}

    void OnGUI()
    {
        GUI.Label(new Rect(10, 0, 200, 30), "WASD Q/E to move");

        if (GUI.Button(new Rect(10, 40, 100, 30), "Reload"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        if (GUI.Button(new Rect(10, 80, 100, 30), "Fire!"))
        {
            object[] obj = GameObject.FindObjectsOfType(typeof(Rigidbody));
            foreach (Rigidbody r in obj)
            {
                r.isKinematic = false;

                // Add Diffrent forces
                //r.AddExplosionForce(500, new Vector3(1, 1, 1), 500);
                //r.AddForce(500, 500, 500);
            }
        }

        if (GUI.Button(new Rect(10, 120, 100, 30), "City 1"))
        {
            DisableAllCities();
            city1.active = true;
        }

        if (GUI.Button(new Rect(10, 160, 100, 30), "City 2"))
        {
            DisableAllCities();
            city2.active = true;
        }

        if (GUI.Button(new Rect(10, 200, 100, 30), "City 3"))
        {
            DisableAllCities();
            city3.active = true;
        }

        if (GUI.Button(new Rect(10, 240, 100, 30), "City 4"))
        {
            DisableAllCities();
            city4.active = true;
        }
    }

    private void DisableAllCities()
    {
        city1.active = false;
        city2.active = false;
        city3.active = false;
        city4.active = false;

    }
}

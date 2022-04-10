using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class CityHelper : EditorWindow 
{
	public GameObject city = null;
	public Material shaderMaterial = null;

	[MenuItem("City/Helper")]
	static void Init()
	{
        CityHelper Window = (CityHelper)EditorWindow.GetWindow (typeof(CityHelper));
	}

	private void OnGUI()
	{
        EditorGUILayout.LabelField("City GameObject");
        city = EditorGUILayout.ObjectField(city, typeof(GameObject)) as GameObject;

        EditorGUILayout.LabelField("Shader Material to apply");
		shaderMaterial = EditorGUILayout.ObjectField (shaderMaterial, typeof(Material)) as Material;

        EditorGUILayout.Space ();

		if (GUILayout.Button ("Apply Shader To City"))
			ApplyShaders();

        EditorGUILayout.Space();

        if (GUILayout.Button("Apply Colliders to City"))
            ApplyColliders();
    }

    private void ApplyColliders()
    {
        object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (object o in obj)
        {
            GameObject gameObject = (GameObject)o;
            Debug.Log(gameObject.name);

            DestroyImmediate(gameObject.GetComponent<MeshCollider>());
            DestroyImmediate(gameObject.GetComponent<Rigidbody>());
            DestroyImmediate(gameObject.GetComponent<BoxCollider>());

            if (gameObject.name.Contains("group"))
            {
                Rigidbody gameObjectsRigidBody = gameObject.AddComponent<Rigidbody>(); // Add the rigidbody.
                gameObjectsRigidBody.mass = 5000;
                gameObjectsRigidBody.isKinematic = true;

                BoxCollider collider = gameObject.AddComponent<BoxCollider>(); // Add simple colider
                //MeshCollider mc = gameObject.AddComponent<MeshCollider>();
            }
        }
    }

    private void ApplyShaders()
	{
        foreach (MeshRenderer r in (city as GameObject).GetComponentsInChildren<MeshRenderer>())
        {
            Material[] myMaterials = new Material[2];
            myMaterials[0] = r.materials[0];
            myMaterials[1] = shaderMaterial;
            r.materials = myMaterials;
        }
	}
}

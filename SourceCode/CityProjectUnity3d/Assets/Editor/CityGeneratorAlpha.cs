//using UnityEngine;
//using UnityEditor;
//using System.Collections;
//using System.Collections.Generic;

//public class CityGenerator : EditorWindow 
//{
//	public GameObject buildingShape = null;

//	private List<GameObject> buildings = new List<GameObject>();

//	public float maxHeight = 10;
//	public float minHeight = 1;
//	public float chance = 15;
//	public Vector3 citySize = new Vector3(50,50,50);

//	[MenuItem("City/Generator [Alpha]")]
//	static void Init()
//	{
//		CityGenerator Window = (CityGenerator)EditorWindow.GetWindow (typeof(CityGenerator));
//	}

//	private void OnGUI()
//	{
//		buildingShape = EditorGUILayout.ObjectField (buildingShape, typeof(GameObject)) as GameObject;

//		EditorGUILayout.Space ();

//		maxHeight = EditorGUILayout.FloatField ("Max Height:", maxHeight);
//		minHeight = EditorGUILayout.FloatField ("Min Height:", minHeight);
//		chance = EditorGUILayout.FloatField ("Building Chance:", chance);

//		EditorGUILayout.Space ();

//		citySize = EditorGUILayout.Vector3Field ("Map Size:", citySize);

//		EditorGUILayout.Space ();
//		EditorGUILayout.Space ();

//		if (GUILayout.Button ("Create City"))
//			CreateCity ();

//        EditorGUILayout.Space();

//        if (GUILayout.Button("Import City from XML"))
//            ImportFromXML();
//    }

//    private void ImportFromXML()
//    {

//    }

//    private void CreateCity()
//	{
//        foreach (GameObject o in GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[])
//        {
//            if (o.name.ToLower().Contains("clone"))
//                GameObject.DestroyImmediate(o);
//        }

//        for (int x = (int)-citySize.x; x < (int)citySize.x; x++)
//		{
//			for(int z = (int)-citySize.z; z < (int)citySize.z; z++)
//			{
//				if(Random.Range(0.0f, 100.0f) <= chance)
//				{
//					float height = Random.Range(minHeight,maxHeight);
//					Vector3 pos = new Vector3(x, height * 0.5f,z);

//					GameObject obj = GameObject.Instantiate(buildingShape,pos,Quaternion.identity) as GameObject;
//					obj.transform.localScale = new Vector3(height * 0.35f, height, height * 0.35f);

//					bool vaild = true;
//					//foreach(GameObject b in buildings)
//					//{
//					//	//to test
//					//	if(obj.GetComponent<Collider>().bounds.Intersects(b.GetComponent<Collider>().bounds))
//					//	{
//					//		GameObject.DestroyObject(obj);
//					//		vaild = false;
//					//		break;
//					//	}
//					//}

//					if(vaild && obj != null)
//					{
//						buildings.Add(obj);
//						obj.transform.parent = GameObject.Find("Buildings").transform;
//					}

//				}
//			}
//		}
//	}
//}



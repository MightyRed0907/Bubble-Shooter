using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkinnedMesh))]
public class ShSkin : MonoBehaviour {

	public SkinnedMesh skin;
	public GameObject[] prefab;

	GameObject[] tempFlower;
	Vector3[] point;
	int[] loc;

	bool startst = false;


	public int MaxGrowNumber=10;
	public float GrowMinScale = 10;
	public float GrowMaxScale = 15;


	public float GrowMinTime = .5f;
	public float GrowMaxTime = 3f;


	bool StartGrow = false;

	float timevalue;
	float _time;

	void Start () {
		_time = Random.Range (GrowMinTime, GrowMaxTime);
		Invoke ("init", .1f);
	}

	void init(){
		point = new Vector3[skin.GetVerticesLength];

		tempFlower = new GameObject[MaxGrowNumber];
		loc = new int[MaxGrowNumber];
			
		startst = true;

		for (int i = 0; i < point.Length; i++) {
			point = skin.GetVertices;
		}


		_GrowFlower ();
	}

	void LateUpdate () {

		
		if (startst) {
			for (int i = 0; i < MaxGrowNumber; i++) {
				tempFlower [i].transform.position = point [loc [i]];
			}
		}


	}

	void OnDrawGizmos(){

		if (startst) {
			for (int i = 0; i < point.Length; i++) {
				Gizmos.DrawSphere (point [i], .01f);
				Gizmos.color = Color.white;
			}
		}
	}

	void _GrowFlower(){
		

		for (int i = 0; i < MaxGrowNumber; i++) {

			loc[i] = (int)Random.Range (0, point.Length);

			tempFlower [i] = Instantiate (prefab [Random.Range (0, prefab.Length)]) as GameObject;
			tempFlower [i].transform.position = point [loc [i]];
			tempFlower [i].transform.localScale = Vector3.one * Random.Range (GrowMinScale, GrowMaxScale);
			tempFlower [i].transform.localEulerAngles = new Vector3 (0, Random.Range (-60, 60), Random.Range (-60, 60));
			//tempFlower.transform.parent = gameObject.transform;
		}

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetRandom : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.transform.position = new Vector3(Random.Range (4.5f, -30.0f), 0.5f, Random.Range (15.0f, -30.0f));

	}
	
	// Update is called once per frame
	void Update () {
		
			

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class RustlandSignboardPhysic : MonoBehaviour {

	public float forceAmount = 10f;

	private Rigidbody rb;


	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown()
	{
	//	SceneManager.LoadScene("Garrage Outdoor");
	}
}

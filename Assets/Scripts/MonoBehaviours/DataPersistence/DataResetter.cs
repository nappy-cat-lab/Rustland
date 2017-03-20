using UnityEngine;

public class DataResetter : MonoBehaviour {

	// all of the scriptable object assests that should be reset at theh start of the game 
	public ResettableScriptableObject[] resettableScriptableObject;


	// Use this for initialization
	void Awake () {

		// go through all the scriptable objects ans call Reset function.
		for (int i = 0; i < resettableScriptableObject.Length; i++)
		{
			resettableScriptableObject [i].Reset ();
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

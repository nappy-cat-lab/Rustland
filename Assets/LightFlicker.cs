using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

	public Light light;
	float flickerSpeed  = 0.07f;
	float  randomizer = 0f;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//while (true) {
		if (randomizer == 0f)
			light.enabled = false;
		else
			light.enabled = true;
			
			randomizer = Random.Range (0f, 1.1f);

		light.intensity = randomizer;
		Debug.Log (randomizer);
			//yield WaitForSeconds (flickerSpeed);
		//}
	}
}

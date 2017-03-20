using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nappycat.Vehicles.Hover
{
	public class HoverboardThruster : MonoBehaviour
	{
		private HoverboardController controller;
		private Rigidbody rb;
		private float fuelInput = 1f;


		internal float springForce;
		internal float springConstant;

		internal float damperForce;
		internal float damperConstant;

		internal float hoverHeight;


		private float previousHeight;
		private float currentHeight;
		private float springVelocity;


		// Use this for initialization
		void Awake ()
		{
			controller = GetComponentInParent<HoverboardController> ();
			rb = controller.GetComponent<Rigidbody> ();
		}


		// Physics Update is called once per frame
		void FixedUpdate ()
		{
			if (!controller || !rb)
			{
				enabled = false;
				return;
			}

			fuelInput = Mathf.Lerp (fuelInput, controller.engineOn ? 1f : 0f, Time.fixedDeltaTime);

			springConstant = controller.stabilizerConstant;

			damperConstant = controller.stabilizerDamperConstant;

			hoverHeight = controller.hoverHeight;

			RaycastHit hit;

			if (Physics.Raycast (transform.position, -controller.transform.up, out hit, hoverHeight + 0.5f))
			{
				previousHeight = currentHeight;
				currentHeight = hoverHeight - (hit.distance - 0.5f);
				springVelocity = (currentHeight - previousHeight) / Time.fixedDeltaTime;

				springForce = springConstant * (Random.Range(currentHeight - 0.5f, currentHeight)) ;
				damperForce = damperConstant * springVelocity;

				rb.AddForceAtPosition((controller.transform.up * (springForce + damperForce)) * fuelInput, transform.position);
			}
		}
	}
}
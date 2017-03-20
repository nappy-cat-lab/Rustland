using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nappycat.Vehicles.Hover;

namespace Nappycat.Cameras
{
	public class HoverboardCamera : MonoBehaviour
	{
		public Transform target;

		// distance in the x-z plane to the target
		public float distance = 0.0f;

		// the height of camera above player
		public float height = 2.0f;

		public float heightOffset = 0.75f;
		public float heightDamping = 5.0f;
		public float rotationDamping = 5.0f;
		public bool useSmoothRotation = true;

		public float minFOV = 50f;
		public float maxFOV = 80f;

		public float maxTilt = 25f;


		private Rigidbody rb;
		private Camera camera;
		private float m_tiltAngle = 0f;

		void Start()
		{
			if (!target)
			{
				if (GameObject.FindObjectOfType<HoverboardController> ())
					target = GameObject.FindObjectOfType<HoverboardController> ().transform;
				else
					return;
			}

			rb = target.GetComponent<Rigidbody> ();
			camera = GetComponent<Camera> ();
		}

		void Update()
		{
			if (!target)
				return;

			if (rb != target.GetComponent<Rigidbody> ())
				rb = target.GetComponent<Rigidbody> ();

			// tilet angle calulation 
			m_tiltAngle = Mathf.Lerp (m_tiltAngle, (Mathf.Clamp (target.InverseTransformDirection (rb.velocity).x, -maxTilt, maxTilt)), Time.deltaTime * 2f);

			if (!camera)
				camera = GetComponent<Camera> ();
			
			camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, Mathf.Lerp(minFOV, maxFOV, (rb.velocity.magnitude * 3.6f) / 150f), Time.deltaTime * 5f);
		}

		void LateUpdate()
		{
			// early out of we dont have target
			if (!target || !rb)
				return;

			float speed = (rb.transform.InverseTransformDirection (rb.velocity).z) * 3.6f;

			// Calculate the current rotation angles
			float wantedRotationAngle = target.eulerAngles.y;
			float wantedHeight = target.position.y + height;
			float currentRotationAngle = transform.eulerAngles.y;
			float currentHeight = transform.position.y;

			if (useSmoothRotation)
				rotationDamping = Mathf.Lerp (0f, 5f, (rb.velocity.magnitude * 3.6f) / 100f);

			if (speed < -10)
				wantedRotationAngle = target.eulerAngles.y + 180;

			// damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

			// damp the height
			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

			// set the position of the camera
			Quaternion currentRotaton = Quaternion.Euler (0, currentRotationAngle, 0);

			// set the positoinof the camera on x-z plane:
			// distance meters beind target
			transform.position = target.position;
			transform.position -= currentRotaton * Vector3.forward * distance;

			// set the heigh of the camera
			transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

			// always look at the target
			transform.LookAt (new Vector3 (target.position.x, target.position.y + heightOffset, target.position.z));
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Clamp (m_tiltAngle, -maxTilt, maxTilt));

		}
	}
}
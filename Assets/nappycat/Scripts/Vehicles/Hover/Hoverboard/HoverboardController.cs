using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nappycat.Vehicles.Hover
{

	// controller types (keyboard, mobile)
	internal enum ControllerType 
	{
		KeyboardController,
		MobileUIController
	}

	// speed types (mph, kph)
	internal enum SpeedType
	{
		MPH,
		KPH
	}

	[RequireComponent (typeof(Rigidbody))]
	[RequireComponent (typeof(HoverboardAudio))]
	public class HoverboardController : MonoBehaviour
	{		
		private Rigidbody rb;
		public Transform COM;

		// can receive play input
		public bool controlOn = true;

		// engine on/off
		public bool engineOn = true;

		// infinite runner mode
		public bool runningMode;

		// control type
		[SerializeField] private ControllerType m_controllerType;

		// speed type
		[SerializeField] private SpeedType m_speedType;

		// stabilizer thrusters
		public List<Transform> stabilizerThrusters = new List<Transform>();

		// stabilizers                                                     
		public float stabilizerConstant = 10000f;
		public float stabilizerDamperConstant = 1000f;

		// engine torque curve depends on craft speed
		public AnimationCurve engineTorqueCurve;

		// 	torques
		public float motorTorque = 20000f;
		public float steerTorque = 5000f;

		// speeds
		public float speed = 0f;
		public float maxSpeed = 250f;

		// inputs
		[HideInInspector] public float gasInput = 0f;
		[HideInInspector] public float steerInput = 0f;

		// stabilizers variables
		public float hoverHeight = 3f;
		public float maxAngularvelocity = 2f;

		private float stability = 0.5f;
		private float reflection = 100f;

		// particles
		public ParticleSystem[] thrusterParticles;
		[SerializeField] private List<ParticleSystem> m_thrusterSmoke = new List<ParticleSystem>();
		public GameObject thrusterGroundSmoke;

		// contact particles
		public GameObject contactSpark;
		public int maxContactSpark = 5;
		private List<ParticleSystem> contactSparksList = new List<ParticleSystem> ();

		// lights
		public Light[] lights;


		// initialize start
		void Start()
		{
			// rigibody component
			rb = GetComponent<Rigidbody> ();
			rb.centerOfMass = COM.localPosition;
			rb.maxAngularVelocity = maxAngularvelocity;

			ParticlesInit ();
		}


		// initialize particles
		void ParticlesInit ()
		{
			if (thrusterGroundSmoke)
			{
				for (int i = 0; i < stabilizerThrusters.Count; i++)
				{
					GameObject ps = (GameObject)Instantiate (thrusterGroundSmoke, transform.position, transform.rotation) as GameObject;
					m_thrusterSmoke.Add (ps.GetComponent<ParticleSystem> ());
					ps.transform.SetParent (stabilizerThrusters [i].transform);
					ps.transform.localPosition = Vector3.zero;
					ParticleSystem.EmissionModule em = ps.GetComponent<ParticleSystem> ().emission;
					em.enabled = false;
				}
			}

			if (contactSpark)
			{
				for (int i = 0; i < maxContactSpark; i++)
				{
					GameObject sparks = (GameObject)Instantiate (contactSpark, transform.position, transform.rotation) as GameObject;
					sparks.transform.SetParent (transform);
					contactSparksList.Add (sparks.GetComponent<ParticleSystem> ());
					ParticleSystem.EmissionModule em = sparks.GetComponent<ParticleSystem> ().emission;
					em.enabled = false;
				}
			}
		}


		// input controls
		void Inputs ()
		{
			if (m_controllerType == ControllerType.KeyboardController)
			{
				if (controlOn)
				{
					
					if (!runningMode) {
						gasInput = Input.GetAxis ("Vertical");
					} else {
						gasInput = 1.0f;
					}


#if !MOBILE_INPUT
					steerInput = Input.GetAxis ("Horizontal");
#else
					steerInput = Input.acceleration.x;
#endif

				} else {

					if (!runningMode) {
						gasInput = 0.0f;
					} else {
						gasInput = 1.0f;
					}
				}
			}
		}


		// update is called once per frame
		void Update ()
		{
			Particles ();
			Lights ();
		}


		// physics fixed update is called once per frame
		void FixedUpdate ()
		{
			Inputs ();

			if (!engineOn)
				return;

			Engine ();
			Stabilizers();
		}

		// hoverspeed
		void HoverSpeed()
		{
			speed = rb.velocity.magnitude;

			switch(m_speedType)
			{
				case SpeedType.MPH:
					speed *= 2.23693629f;
					break;
				case SpeedType.KPH:
					speed *= 3.6f;
					break;
			}
		}


		// engines
		void Engine()
		{
			HoverSpeed ();

			if (!runningMode)
			{
				if (speed < maxSpeed)
					rb.AddForceAtPosition(transform.forward * ((motorTorque * Mathf.Clamp (gasInput, -0.5f, 1f)) * engineTorqueCurve.Evaluate (speed)), COM.position, ForceMode.Force);

				rb.AddRelativeTorque (Vector3.up * ((steerTorque * Mathf.Lerp (1f, 0.25f, speed / maxSpeed)) * steerInput), ForceMode.Force);
				rb.AddRelativeTorque (Vector3.forward * ((-steerTorque / 1f) * steerInput), ForceMode.Force);

			} else {
			
				if (speed < maxSpeed)
					rb.AddForceAtPosition(transform.forward * ((motorTorque * 1f) * engineTorqueCurve.Evaluate (speed)), COM.position, ForceMode.Force);
	
				// strafe left and right
				rb.transform.Translate (Vector3.right * steerInput);

				//rb.AddRelativeTorque (Vector3.up * ((steerTorque * Mathf.Lerp (1f, 0.25f, speed / maxSpeed)) * steerInput), ForceMode.Force);
				rb.AddRelativeTorque (Vector3.forward * ((-steerTorque / 1f) * steerInput), ForceMode.Force);
			}

			Vector3 localVelocity = transform.InverseTransformDirection (rb.velocity);
			localVelocity = new Vector3 (Mathf.Lerp (localVelocity.x, 0f, Time.fixedDeltaTime * 1f), localVelocity.y, localVelocity.z);
			rb.velocity = transform.TransformDirection (localVelocity);
			rb.angularVelocity = Vector3.Lerp (rb.angularVelocity, Vector3.zero, Time.fixedDeltaTime * 1f);
		}


		// stabilizers
		void Stabilizers ()
		{
			Vector3 predictedUp = Quaternion.AngleAxis (rb.velocity.magnitude * Mathf.Rad2Deg * stability / reflection, rb.angularVelocity) * transform.up;
			Vector3 torqueVector = Vector3.Cross (predictedUp, Vector3.up);
		
			rb.AddTorque (torqueVector * reflection * reflection);
		}


		// particles
		void Particles ()
		{
			if (!engineOn)
			{
				foreach (ParticleSystem ps in thrusterParticles)
				{
					ParticleSystem.EmissionModule em = ps.emission;

					if (em.enabled)
						em.enabled = false;
				}

				foreach (ParticleSystem ps in m_thrusterSmoke)
				{
					ParticleSystem.EmissionModule em = ps.emission;

					if (em.enabled)
						em.enabled = false;
				}

				foreach (Light light in lights)
				{
					light.intensity = Mathf.Lerp (light.intensity, 0f, Time.deltaTime * 1f);
				}

				return;
			}

			foreach (ParticleSystem ps in thrusterParticles)
			{
				ParticleSystem.EmissionModule em = ps.emission;

				if (!em.enabled)
					em.enabled = true;

				ps.startSpeed = Mathf.Clamp (-gasInput, -1f, 0.3f);
			}

			foreach (Light light in lights)
			{
				light.intensity = Mathf.Lerp (0.25f, 2f, Mathf.Abs(gasInput * 2f));
			}

			for (int i = 0; i < stabilizerThrusters.Count; i++)
			{
				if (!thrusterGroundSmoke)
					return;

				RaycastHit hit;

				if (Physics.Raycast (stabilizerThrusters [i].position, stabilizerThrusters [i].forward, out hit, hoverHeight * 2f) && hit.transform.root != transform)
				{
					ParticleSystem.EmissionModule em = m_thrusterSmoke [i].emission;

					if (!em.enabled)
						em.enabled = true;

					m_thrusterSmoke [i].transform.position = hit.point;
				} else {
					ParticleSystem.EmissionModule em = m_thrusterSmoke [i].emission;

					if (em.enabled)
						em.enabled = false;
				}
			}
		}


		// lights
		void Lights ()
		{
			if (controlOn)
			{
				foreach (Light light in lights)
				{
					light.intensity = Mathf.Lerp (light.intensity, 0f, Time.deltaTime * 1f);
				}
			}
		}


		// collision particles
		void CollisionParticles(Vector3 contactPoint)
		{
			for (int i = 0; i < contactSparksList.Count; i++)
			{
				if (contactSparksList [i].isPlaying)
					return;

				contactSparksList [i].transform.position = contactPoint;
				ParticleSystem.EmissionModule em = contactSparksList [i].emission;
				em.enabled = true;
				contactSparksList [i].Play ();
			}
		}


		// on collision enter
		void OnCollisionEnter( Collision col)
		{
			if (col.relativeVelocity.magnitude < 2.5f)
				return;

			CollisionParticles (col.contacts [0].point);
		}


		// draw scene view gizmo
		void OnDrawGizmos()
		{
			RaycastHit hit;

			for (int i = 0; i < stabilizerThrusters.Count; i++)
			{
				if (!stabilizerThrusters[i].GetComponent<HoverboardThruster>())
					stabilizerThrusters[i].gameObject.AddComponent<HoverboardThruster>();

				if (Physics.Raycast (stabilizerThrusters [i].position, stabilizerThrusters [i].forward, out hit, hoverHeight * 2f)
					&& hit.transform.root != transform)
				{
					Debug.DrawRay (stabilizerThrusters [i].position, stabilizerThrusters [i].forward * hit.distance, new Color (Mathf.Lerp (1f, 0f, hit.distance / (hoverHeight * 1.5f)), 0f, 1f));
					Gizmos.color = new Color (Mathf.Lerp (1f, 0f, hit.distance / (hoverHeight * 1.5f)), Mathf.Lerp (0f, 1f, hit.distance / (hoverHeight * 1.5f)), 0f, 1f);
					Gizmos.DrawSphere (hit.point, 0.5f);
				}

				Matrix4x4 temp = Gizmos.matrix;
				Gizmos.matrix = Matrix4x4.TRS (stabilizerThrusters [i].position, stabilizerThrusters [i].rotation, Vector3.one);
				Gizmos.DrawFrustum(Vector3.zero, 30f, hoverHeight, 0.1f, 1f);
				Gizmos.matrix = temp;
			}
		}

		public string  getSpeedType()
		{
			return m_speedType.ToString ();
		}
	}
}
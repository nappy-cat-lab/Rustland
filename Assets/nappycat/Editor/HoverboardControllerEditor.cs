using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Nappycat.Vehicles.Hover;


[CustomEditor(typeof(HoverboardController))]
public class HoverboardControllerEditor : Editor
{

	HoverboardController hoverboardController;

	Color defaulfBackgroundColor;
	static bool firstInit = false;

	[MenuItem(".: nappycat :./Vehicles/Hover/Hoverboard/Add controller to vehicle")]
	static void CreateController()
	{
		if (!Selection.activeGameObject.GetComponent<HoverboardController> ())
		{
			firstInit = true;

			Selection.activeGameObject.AddComponent<HoverboardController> ();

			Selection.activeGameObject.GetComponent<Rigidbody> ().mass = 500f;
			Selection.activeGameObject.GetComponent<Rigidbody> ().drag = 0.5f;
			Selection.activeGameObject.GetComponent<Rigidbody> ().angularDrag = 1f;
			Selection.activeGameObject.GetComponent<Rigidbody> ().interpolation = RigidbodyInterpolation.Interpolate;
			Selection.activeGameObject.GetComponent<Rigidbody> ().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

		} else {
			EditorUtility.DisplayDialog ("Duplicate Hoverboard Controller", "Your gameobject already has hoverboard controller", "OK");
		}
	}

	void Awake ()
	{
		defaulfBackgroundColor = GUI.backgroundColor;
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update ();

		hoverboardController = (HoverboardController)target;

		if (firstInit)
		{
			CreateCOM ();
			CreateThruster();
			EngineCurveInit ();
		}

		// behaviour section
		EditorGUILayout.Space ();
		GUI.color = Color.gray;
		EditorGUILayout.HelpBox ("Hoverboard Bahaviour", MessageType.None);
		GUI.color = defaulfBackgroundColor;

		EditorGUILayout.HelpBox ("Choose your hoverboard bahaviour type. [stable or speedy]", MessageType.Info);
		GUI.color = defaulfBackgroundColor;
		EditorGUILayout.Space ();

		// stable or speedy
		hoverboardController.GetComponent<Rigidbody> ().drag = EditorGUILayout.Slider (
			new GUIContent ("Stable - Speedy", "More stable or ore speedy?"), hoverboardController.GetComponent<Rigidbody> ().drag, 1f, 0f);

		// settings section
		EditorGUILayout.Space ();
		GUI.color = Color.gray;
		EditorGUILayout.HelpBox ("Setting", MessageType.None);
		GUI.color = defaulfBackgroundColor;

		EditorGUILayout.PropertyField (serializedObject.FindProperty ("m_controllerType"),
			new GUIContent ("Controller Type", "Controller type"));

		EditorGUILayout.PropertyField (serializedObject.FindProperty ("controlOn"),
			new GUIContent ("Controller On", "Controller on"));

		EditorGUILayout.PropertyField (serializedObject.FindProperty ("engineOn"),
			new GUIContent ("Engine On", "Engine On"));

		EditorGUILayout.PropertyField (serializedObject.FindProperty ("runningMode"),
			new GUIContent ("Running Mode", "Running mode"));

		EditorGUILayout.PropertyField (serializedObject.FindProperty ("COM"),
			new GUIContent ("COM Position", "Center of mass position"));

		EditorGUILayout.PropertyField (serializedObject.FindProperty ("m_speedType"),
			new GUIContent ("Speed Type", "speed type"));

		// engine torques section 
		EditorGUILayout.Space ();
		GUI.color = Color.gray;
		EditorGUILayout.HelpBox ("Engine Torques", MessageType.None);
		GUI.color = defaulfBackgroundColor;

		EditorGUILayout.PropertyField (serializedObject.FindProperty ("engineTorqueCurve"),
			new GUIContent ("Engine Torque Curve", "Engine torque curve"));

		EditorGUILayout.PropertyField (serializedObject.FindProperty ("motorTorque"),
			new GUIContent ("Motor Torque", "Motor torque"));

		EditorGUILayout.PropertyField (serializedObject.FindProperty ("steerTorque"),
			new GUIContent ("Sterring Torque", "Steering torque"));
		
		EditorGUILayout.PropertyField (serializedObject.FindProperty ("maxSpeed"),
			new GUIContent ("Maximum Speed", "Maximum speed"));
		
		EditorGUILayout.PropertyField (serializedObject.FindProperty ("maxAngularvelocity"),
			new GUIContent ("Maximum Angular Velocity", "Maximum angular velocity"));

		EditorGUI.BeginDisabledGroup (true);
			EditorGUILayout.PropertyField (serializedObject.FindProperty ("speed"),
				new GUIContent ("Speed - " + hoverboardController.getSpeedType(), "speed"));
		EditorGUI.EndDisabledGroup();
	
		// thrusters section 
		EditorGUILayout.Space ();
		GUI.color = Color.gray;
		EditorGUILayout.HelpBox ("Engine Stabilizer Thrusters", MessageType.None);
		GUI.color = defaulfBackgroundColor;

		if (GUILayout.Button("Add New Thruster"))
			CreateThruster();

		EditorGUILayout.PropertyField (serializedObject.FindProperty ("stabilizerThrusters"),
			new GUIContent ("Stabilizer Thrusters", "Stabilizer Tthrusters"), true);
		
		EditorGUILayout.PropertyField (serializedObject.FindProperty ("stabilizerConstant"),
			new GUIContent ("Stabilizer Thrusters Force", "Stabilizer Tthrusters force"));

		EditorGUILayout.PropertyField (serializedObject.FindProperty ("stabilizerDamperConstant"),
			new GUIContent ("Stabilizer Thrusters Damper", "Stabilizer Tthrusters damper"));

		// hover height
		hoverboardController.hoverHeight = EditorGUILayout.Slider (
			new GUIContent ("Hover height", "Hover height of the hoverboard"), hoverboardController.hoverHeight, 0f, 20f);

		// engine section 
		EditorGUILayout.Space ();
		GUI.color = Color.gray;
		EditorGUILayout.HelpBox ("Particles", MessageType.None);
		GUI.color = defaulfBackgroundColor;

		EditorGUILayout.PropertyField (serializedObject.FindProperty ("thrusterParticles"),
			new GUIContent ("Thruster Particles", "Thruster particles"), true);

		EditorGUILayout.PropertyField (serializedObject.FindProperty ("thrusterGroundSmoke"),
			new GUIContent ("Thruster GroundSmoke", "Thruster ground smoke particles"), true);
		
		EditorGUILayout.PropertyField (serializedObject.FindProperty ("contactSpark"),
			new GUIContent ("Contact Spark", "Contact Sparks particles"));

		// light 
		EditorGUILayout.Space ();
		GUI.color = Color.gray;
		EditorGUILayout.HelpBox ("Lights", MessageType.None);
		GUI.color = defaulfBackgroundColor;

		EditorGUILayout.PropertyField (serializedObject.FindProperty ("lights"),
			new GUIContent ("Hoverboard Lights", "Thruster particles"), true);


		serializedObject.ApplyModifiedProperties ();
		//serializedObject.UpdateIfDirtyOrScript ();

		if (GUI.changed && !EditorApplication.isPlaying)
		{
			EngineCurveInit ();
		}

		if (GUI.changed)
		{
			EditorUtility.SetDirty (hoverboardController);
		}
	}


	// create COMbe
	void CreateCOM()
	{
		GameObject COM = new GameObject ("COM");
		COM.transform.parent =  hoverboardController.transform;
		COM.transform.localPosition = Vector3.zero;
		COM.transform.localScale = Vector3.one;
		COM.transform.rotation = hoverboardController.transform.rotation;
		hoverboardController.COM = COM.transform;

		firstInit = false;
	}

	// create a new thruster
	void CreateThruster()
	{
		GameObject thruster = new GameObject ();
		thruster.transform.name = "Stabilizer Thruster";
		thruster.transform.parent = hoverboardController.transform;
		thruster.transform.localPosition = new Vector3 (0f, -0.5f, 0f);
		thruster.transform.localRotation = Quaternion.identity; 
		thruster.transform.localEulerAngles = new Vector3 (90f, 0f, 0f);
		thruster.AddComponent<HoverboardThruster> ();
		hoverboardController.stabilizerThrusters.Add (thruster.transform);
	}

	// engine curve initialization
	void EngineCurveInit ()
	{
		hoverboardController.engineTorqueCurve = new AnimationCurve (new Keyframe (0, 1f));
		hoverboardController.engineTorqueCurve.AddKey (new Keyframe (hoverboardController.maxSpeed, 0.1f));
		hoverboardController.engineTorqueCurve.postWrapMode = WrapMode.Clamp;
	}
}


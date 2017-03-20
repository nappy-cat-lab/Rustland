using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Saver : MonoBehaviour {

	public string uniqueID;
	public SavaData saveData;

	protected string key;

	private SceneController sceneController;

	// Use this for initialization
	void Awake ()
	{
		sceneController = FindObjectOfType<SceneController> ();

		if (!sceneController)
			throw new UnityException (
				"Scene Controller could not be found, enbure that it exists in the Persistent scene.");

		key = SetKey ();
	}

	private void OnEnabled()
	{
		sceneController.BeforeSceneUnload += Save;
		sceneController.AfterSceneLoad += Load;
	}

	private void OnDisable()
	{
		sceneController.BeforeSceneUnload -= Save;
		sceneController.AfterSceneLoad -= Load;
	}

	// set key
	protected abstract string SetKey ();

	// save
	protected abstract void Save ();

	// load
	protected abstract void Load ();
}

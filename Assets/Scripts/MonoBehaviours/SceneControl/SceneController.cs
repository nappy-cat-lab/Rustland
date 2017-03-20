using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	// instance
	// public static SceneController instance = null;

	// event deligated that is called just before a scene is unloaded.
	public event Action BeforeSceneUnload;

	// event deligated that is called just before a scene is loaded.
	public event Action AfterSceneLoad;

	// the canvasGroup that controls the image used for fading to black.
	public CanvasGroup faderCanvasGroup;

	// how long it should take to fade to and from black
	public float fadeDuration = 1f;

	// the name of the scene that should be loaded first.
	public string startingSceneName = "MainMenu";

	// the name of the startingPosition in the first scene to be loaded.
	public string initialStartingPositionName = "";

	// reference to the ScriptableObject which stores the name of the StartPosition in the next scene.
	public SavaData playerSaveData;


	private bool isFading;

	/*
	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
	}
	*/

	private IEnumerator Start ()
	{
		// set the initial alpha to start off with a black screen
		faderCanvasGroup.alpha = 1f;

		if (startingSceneName != "") {
			playerSaveData.Save (PlayerMovement.startingPositionKey, initialStartingPositionName);
			yield return StartCoroutine (LoadSceneAndSetActive (startingSceneName));
		}
			// once the scene is finshed loading, start fading in
			StartCoroutine (Fade (0f));
	
	}

	// this is the maoin external point of contact and influence from the rest of the project.
	// this will be called by the SceneReaction when the player wants to switch scenes.
	public void FadeAndLoadScene (SceneReaction sceneReaction)
	{
		// if a fade isn't happening the start fading and switching scenes.
		if (!isFading)
		{
			StartCoroutine (FadeAndSwitchScene (sceneReaction.sceneName));
		}
	}

	// load external scene
	public void FadeAndLoadScene (string sceneName)
	{
		// if a fade isn't happening the start fading and switching scenes.
		if (!isFading)
		{
			StartCoroutine (FadeAndSwitchScene (sceneName));
		}
	}

	// this is the coroutine the 'building blocks' of the script are put together
	private IEnumerator FadeAndSwitchScene(string sceneName)
	{
		// start fading to black and wait for it to finish before continuing.
		yield return StartCoroutine (Fade (1f));

		// if the event has any subscribers, call it
		if (BeforeSceneUnload != null)
			BeforeSceneUnload ();

		// Unload the current active scene.
		yield return SceneManager.UnloadSceneAsync (SceneManager.GetActiveScene ().buildIndex);

		// start loading the given scene and wait for it to finish.
		yield return StartCoroutine (LoadSceneAndSetActive (sceneName));

		// if this event has any subscribers, call it.
		if (AfterSceneLoad != null)
			AfterSceneLoad ();

		// start fading back in and wait for it to finish before exiting function
		yield return StartCoroutine (Fade (0f));
	}

	// load scene and set active
	private IEnumerator LoadSceneAndSetActive (string sceneName)
	{
		// allow the given scene to load over several frames and add it to the already loaded scenes (just the Persistent scene at this point).
		yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

		// find the scene that was most recently loaded (the one at the last index of the loaded scenes).
		Scene newlyLoadedScene = SceneManager.GetSceneAt (SceneManager.sceneCount - 1);

		// set newly loaded scene to be the active scene (this marks it as the one to be unloaded next).
		SceneManager.SetActiveScene (newlyLoadedScene);
	}

	private IEnumerator Fade(float finalAlpha)
	{
		// set the fading flag to true so the FadeAndSwitchScenes coroutine won't be called again.
		isFading = true;

		// make sure the CanvasGroup blocks raycasts into the scene so no more input can be accepted.
		faderCanvasGroup.blocksRaycasts = true;

		// calculated how fast the CanvasGroup should fade based on it's current alpha, it's final alpha and how long it has to change between the two.
		float fadeSpeed = Mathf.Abs (faderCanvasGroup.alpha - finalAlpha) / fadeDuration;

		// while the CanvasGroup hasn't reached the final alpha yet...
		while (!Mathf.Approximately (faderCanvasGroup.alpha, finalAlpha))
		{
			// ...move the alpha towards its targhet alpha
			faderCanvasGroup.alpha = Mathf.MoveTowards (faderCanvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);

			// wait for flag to false since the fade has finished.
			yield return null;
		}

		// set the flag to false since the fade has finished.
		isFading = false;

		// stop the CanvasGroup from blocking raycasts so input is no longer ignored.
		faderCanvasGroup.blocksRaycasts = false;
	}
}
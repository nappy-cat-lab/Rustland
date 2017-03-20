using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EpisodeManager : MonoBehaviour
{
	public Episode[] episodes;

	public Episode currentEpisode;

	// reference to the SceneController to actually do the loading and unloading of scenes.
	private SceneController sceneController;

	// use this for initialization
	void Awake()
	{
		sceneController = FindObjectOfType<SceneController>();
	}

	// use this for initialization
	void Start ()
	{
		
	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void AddEpisode (Episode ep)
	{
		
	}

	public void RemoveEpisode (Episode ep)
	{
		
	}

	public void LaunchEpisode(Episode ep)
	{
		if (sceneController) {
			sceneController.FadeAndLoadScene (ep.episodeSceneName);
		} else {
			SceneManager.LoadScene (ep.episodeSceneName);
		}
	}
}

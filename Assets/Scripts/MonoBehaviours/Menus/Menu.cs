using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	public bool open = false;

	private CanvasGroup canvasGroup;

	// yse this for initialization
	void Awake ()
	{
		canvasGroup = GetComponent<CanvasGroup> ();	
	}

	// use this for initialization
	void Start ()
	{

	}

	// update is called once per frame
	void Update ()
	{
		if (!open)
		{
			canvasGroup.blocksRaycasts = canvasGroup.interactable = false;
			canvasGroup.alpha = 0f;
		} else {
			canvasGroup.blocksRaycasts = canvasGroup.interactable = true;
			canvasGroup.alpha = 1f;
		}
	}

	public void Show ()
	{
		open = true;
	}

	public void Hide ()
	{
		open = false;
	}
}

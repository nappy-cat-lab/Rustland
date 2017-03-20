using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Episode : MonoBehaviour
{
	public Image imageEpisode;
	public Image imageOverlay;

	public Text textEpisodeNumber;
	public Text textEpisodeTitle;
	public Image imageLock;

	public Sprite episodeSprite;
	public string episodeSceneName;
	public string episodeNumber;
	public string episodeTitle;
	public Sprite lockSprite;
	public Sprite unlockSprite;
	public bool locked = true; 

	private CanvasGroup canvasGroup;

	// yse this for initialization
	void Awake ()
	{
		canvasGroup = GetComponent<CanvasGroup> ();
	}

	// use this for initialization
	void Start ()
	{
		LoadEpisodeInfo ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (!locked)
		{
			Unlock ();
		} else {
			Lock ();
		}
	}

	public void LoadEpisodeInfo ()
	{
		// episode sprite image
		if (imageEpisode)
			imageEpisode.sprite = episodeSprite;

		if (!episodeSprite) {
			imageEpisode.color = new Color (0, 0, 0, 0.8f);
		}

		// episode text number
		if (textEpisodeNumber)
			textEpisodeNumber.text = episodeNumber;
		
		// episode text title
		if (textEpisodeTitle)
			textEpisodeTitle.text = episodeTitle;

		if (!locked)
		{
			Unlock ();
		} else {
			Lock ();
		}
	}

	// lock
	public void Lock ()
	{
		if (!locked)
			return;
		
		canvasGroup.blocksRaycasts = canvasGroup.interactable = false;
		imageOverlay.color =  new Color(0, 0, 0, 0.8f);

		if (lockSprite)
			imageLock.sprite = lockSprite;
		
		locked = true;
	}

	// unlock
	public void Unlock ()
	{
		if (locked)
			return;

		canvasGroup.blocksRaycasts = canvasGroup.interactable = true;
		imageOverlay.color = new Color(0, 0, 0, 0.1f);

		if (unlockSprite)
			imageLock.sprite = unlockSprite;
		
		locked = false;
	}

	public void Launch()
	{

	}

	void OnMouseEnter()
	{

	}
}

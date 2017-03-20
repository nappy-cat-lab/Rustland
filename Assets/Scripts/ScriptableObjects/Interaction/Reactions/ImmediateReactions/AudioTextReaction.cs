	using System.Collections;
	using UnityEngine;

	public class AudioTextReaction : Reaction {

		public string message;
		public Color textColor = Color.white;

		public AudioSource audioSource;
		public AudioClip audioClip;

		public float delay;

		private TextManager textManager;

		// Use this for initialization
		protected override void SpecificInit()
		{
			textManager = FindObjectOfType<TextManager>();
		}
		
		// Update is called once per frame
		protected override void ImmediateReaction ()
		{
			audioSource.clip = audioClip;
			audioSource.PlayDelayed (delay);
			textManager.DisplayAudioMessage(message, textColor, delay, audioClip.length);
		}
	}

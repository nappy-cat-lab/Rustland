	using System.Collections;
	using UnityEngine;

	public class TextReaction : Reaction {

		public string message;
		public Color textColor = Color.white;

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
			textManager.DisplayMessage(message, textColor, delay);
		}
	}

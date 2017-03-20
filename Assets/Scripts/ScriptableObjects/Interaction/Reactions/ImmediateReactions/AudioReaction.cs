using System.Collections;
using UnityEngine;

public class AudioReaction : Reaction {

	public AudioSource audioSource;
	public AudioClip audioClip;

	public float delay;


	// Use this for initialization
	protected override void SpecificInit()
	{
		
	}

	// Update is called once per frame
	protected override void ImmediateReaction ()
	{
		audioSource.clip = audioClip;
		audioSource.PlayDelayed (delay);
	}
}

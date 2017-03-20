 using UnityEngine;

public class GameObjectReaction : DelayedReaction
{

	public GameObject gameObject;
	public bool activeState;

	protected override void SpecificInit ()
	{
	}

	protected override void ImmediateReaction ()
	{
		gameObject.SetActive (activeState);
	}
}
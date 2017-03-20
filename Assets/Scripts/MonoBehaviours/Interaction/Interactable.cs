using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {
	
	public Transform interactionLocation;
	public ConditionCollection[] conditionCollections = new ConditionCollection[0];
	public ReactionCollection defaultReactionCollection;

	public bool interactOnTrigger;

	// interact
	public void Interact ()
	{

		for (int i = 0; i < conditionCollections.Length; i++)
		{
			if (conditionCollections[i].CheckAndReact())
				return;
		}

		defaultReactionCollection.React();
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Player") && interactOnTrigger)
		{
			Interact ();
		}
	}
}

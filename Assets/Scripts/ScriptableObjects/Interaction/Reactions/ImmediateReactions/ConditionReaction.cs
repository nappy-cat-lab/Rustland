using System.Collections;
using UnityEngine;

public class ConditionReaction : Reaction {

	public Condition condition;
	public bool satisfied;

	// Use this for initialization
	protected override void SpecificInit()
	{
	}

	// Update is called once per frame
	protected override void ImmediateReaction ()
	{
		condition.satisfied = satisfied;
	}
}

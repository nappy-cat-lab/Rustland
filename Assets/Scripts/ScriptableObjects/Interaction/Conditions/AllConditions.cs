using UnityEngine;

public class AllConditions : ResettableScriptableObject
{
	public Condition[] conditions;

	public static AllConditions instance;

	private const string loadPath = "AllConditions";

	public static AllConditions Instance
	{
		get {
			if (!instance)
				instance = FindObjectOfType<AllConditions> ();
			
			if (!instance)
				instance = Resources.Load<AllConditions> (loadPath);
			
			if (!instance)
				Debug.LogError ("AllConditions has not been created yet. Go to Assets > Create > AllConditions.");

			return instance;
		}

		set { instance = value; }
	}

	// rest
	public override void Reset()
	{
		// if there is no conditions to rest, don't resetting
		if (conditions == null)
			return;

		// reset all conditions satifaction to falsa
		for (int i = 0; i < conditions.Length; i++)
		{
			conditions [i].satisfied = false;
		}
	}

	// check conditions
	public static bool CheckCondition(Condition requiredCondition)
	{
		Condition[] allCondition = Instance.conditions;
		Condition globalCondition = null;

		if (allCondition != null && allCondition [0] != null)
		{
			for (int i = 0; i < allCondition.Length; i++)
			{
				if (allCondition [i].hash == requiredCondition.hash)
					globalCondition = allCondition [i];
			}
		}

		if (!globalCondition)
			return false;

		return globalCondition.satisfied == requiredCondition.satisfied;
	}
}

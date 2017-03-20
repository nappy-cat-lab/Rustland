using UnityEngine;

public class PositionSaver : Saver
{
	public Transform transformToSave;

	protected override string SetKey ()
	{
		return transformToSave.name + transformToSave.GetType ().FullName + uniqueID;
	}

	protected override void Save ()
	{
		saveData.Save (key, transform.position);
	}

	protected override void Load ()
	{
		Vector3 position = Vector3.zero;

		if (saveData.Load (key, ref position))
			transformToSave.position = position;
	}
}

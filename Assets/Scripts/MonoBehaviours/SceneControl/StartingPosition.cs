using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPosition : MonoBehaviour
{
	public string startingPointName;

	private static List<StartingPosition> allStartPositions = new List<StartingPosition> ();


	void OnEnable ()
	{
		allStartPositions.Add (this);
	}

	void OnDisable ()
	{
		allStartPositions.Remove (this);
	}

	public static Transform FindStartingPosition (string pointName)
	{
		for (int i = 0; i < allStartPositions.Count; i++)
		{
			if (allStartPositions [i].startingPointName == pointName)
				return allStartPositions [i].transform;
		}

		return null;
	}
}

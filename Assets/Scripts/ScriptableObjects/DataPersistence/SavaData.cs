using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SavaData : ResettableScriptableObject
{
	[Serializable]
	public class KeyValuePairLists<T>
	{
		public List<string> keys = new List<string>();
		public List<T> values = new List<T>();

		public void Clear ()
		{
			keys.Clear();
			values.Clear();
		}

		public void TrySetValue (String key, T value)
		{
			int index = keys.FindIndex (x => x == key);

			if (index > -1)
			{
				values[index] = value;
			
			} else {

				keys.Add (key);
				values.Add (value);
			}
		}

		public bool TryGetValue (String key, ref T value)
		{
			int index = keys.FindIndex (x => x == key);

			if (index > -1)
			{
				value  = values[index];
				return true;
			}

			return false;
		}

	}


	public KeyValuePairLists<bool> boolKeyValuePairLists = new KeyValuePairLists<bool>();
	public KeyValuePairLists<int> intKeyValuePairLists = new KeyValuePairLists<int>();
	public KeyValuePairLists<string> stringKeyValuePairLists = new KeyValuePairLists<string>();
	public KeyValuePairLists<Vector3> vector3KeyValuePairLists = new KeyValuePairLists<Vector3>();
	public KeyValuePairLists<Quaternion> quaternionKeyValuePairLists = new KeyValuePairLists<Quaternion>();

	public override void Reset ()
	{
		boolKeyValuePairLists.Clear ();
		intKeyValuePairLists.Clear ();
		stringKeyValuePairLists.Clear ();
		vector3KeyValuePairLists.Clear ();
		quaternionKeyValuePairLists.Clear ();
	}


	// Save <T>
	private void Save<T>(KeyValuePairLists<T> lists, string key, T value)
	{
		lists.TrySetValue (key, value); 
	}

	// load <T>
	private bool Load<T>(KeyValuePairLists<T> lists, string key, ref T value)
	{
		return lists.TryGetValue (key, ref value);
	}


	// SAVE OVERLOADS

	// save bool
	public void Save (string key, bool value)
	{
		Save (boolKeyValuePairLists, key, value);
	}

	// save int
	public void Save (string key, int value)
	{
		Save (intKeyValuePairLists, key, value);
	}

	// save string
	public void Save (string key, string value)
	{
		Save (stringKeyValuePairLists, key, value);
	}

	// save string
	public void Save (string key, Vector3 value)
	{
		Save (vector3KeyValuePairLists, key, value);
	}

	// save string
	public void Save (string key, Quaternion value)
	{
		Save (quaternionKeyValuePairLists, key, value);
	}


	// LOAD OVERLOADS

	// load bool
	public bool Load (string key, ref bool value)
	{
		return Load (boolKeyValuePairLists, key, ref value);
	}

	// load int
	public bool Load (string key, ref int value)
	{
		return Load (intKeyValuePairLists, key, ref value);
	}

	// load string
	public bool Load (string key, ref string value)
	{
		return Load (stringKeyValuePairLists, key, ref value);
	}

	// load string
	public bool Load (string key, ref Vector3 value)
	{
		return Load (vector3KeyValuePairLists, key, ref value);
	}

	// load string
	public bool Load (string key, ref Quaternion value)
	{
		return Load (quaternionKeyValuePairLists, key, ref value);
	}
}

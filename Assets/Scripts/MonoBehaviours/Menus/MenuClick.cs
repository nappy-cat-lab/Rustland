using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuClick : MonoBehaviour {

	public MenuManager menuManager;

	public Menu menu;

	void OnMouseDown()
	{
		if (menuManager)
			menuManager.ShowMenu (menu);
	}
}

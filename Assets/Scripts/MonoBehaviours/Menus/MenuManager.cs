using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	public Menu currentMenu;

	public Color menuOverColor = Color.red;

	void Awake()
	{
		Cursor.visible = true;

		if (currentMenu)
			ShowMenu (currentMenu);
	}

	void Start()
	{
		
	}

	void Update()
	{

	}

	public void ShowMenu()
	{
		ShowMenu (currentMenu);
	}

	public void ShowMenu( Menu menu )
	{
		Cursor.visible = true;

		if (currentMenu != null)
			currentMenu.Hide ();

		currentMenu = menu;
		currentMenu.Show ();
	}


	public void CloseMenu()
	{
		Cursor.visible = true;

		if (currentMenu != null)
			currentMenu.Hide ();
	}

	public void PointerEnter (Text text)
	{
		text.color = menuOverColor;
	}

	public void PointerExit (Text text)
	{
		text.color = Color.white;
	}
}

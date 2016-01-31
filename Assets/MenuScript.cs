using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public MenuScript instance;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayGame()
	{
		Application.LoadLevel ("OverWorld");
	}

	public void GetHelp()
	{
		Application.LoadLevel ("Help");
	}

	public void LoadMenu()
	{
		Application.LoadLevel ("Title");
	}
}

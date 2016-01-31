using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITracker : MonoBehaviour {

	public GameObject p1healthObj;
	public GameObject p2healthObj;
	Text p1Health;
	Text p2Health;
	public static UITracker instance;

	// Use this for initialization
	void Start () {
		p1Health = p1healthObj.GetComponent<Text> ();
		p2Health = p2healthObj.GetComponent<Text> ();
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetP1Health(int health)
	{
		p1Health.text = health.ToString ();
	}

	public void SetP2Health(int health)
	{
		p2Health.text = health.ToString ();
	}

	public void EndLevel(bool didwin)
	{

	}
}

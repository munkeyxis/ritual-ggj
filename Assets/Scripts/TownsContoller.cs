using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TownsContoller : MonoBehaviour {
	public GameObject TownPrefab;
	List<Town> _townsDisplayed;

	void Start () {
		_townsDisplayed = new List<Town> ();

		foreach (Town town in Manager.TownManager._towns) {
			GameObject townInstance = Instantiate (TownPrefab);
			townInstance.transform.SetParent (this.transform);
			townInstance.transform.position = town._position;
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TownsContoller : MonoBehaviour {
	public GameObject TownPrefab;
	public GameObject PlayerPrefab;
	List<Town> _townsDisplayed;

	void Start () {
		_townsDisplayed = new List<Town> ();

		foreach (Town town in Manager.TownManager._towns) {
			GameObject townInstance = Instantiate (TownPrefab);
			townInstance.transform.SetParent (this.transform);
			townInstance.transform.position = town._position;

			displayPlayerIfNecessary(town);
		}
	}

	void displayPlayerIfNecessary(Town town) {
		if(town._playerPresent) {
			GameObject playerInstance = Instantiate(PlayerPrefab);
			playerInstance.transform.position = new Vector3(town._position.x, town._position.y, -1);
		}
	}
}

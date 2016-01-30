using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TownsContoller : MonoBehaviour {
	public GameObject TownPrefab;
	public GameObject PlayerPrefab;
	public GameObject PlayerControlRingPrefab;
	public GameObject EnemyControlRingPrefab;
	List<Town> _townsDisplayed;

	void Start () {
		_townsDisplayed = new List<Town> ();

		foreach (Town town in Manager.TownManager._towns) {
			GameObject townInstance = Instantiate (TownPrefab);
			townInstance.transform.SetParent (this.transform);
			townInstance.transform.position = town._position;

			displayPlayerIfNecessary(town);
			displayControlRingIfNecessary (town);
		}
	}

	void displayPlayerIfNecessary(Town town) {
		if(town._playerPresent) {
			GameObject playerInstance = Instantiate(PlayerPrefab);
			playerInstance.transform.position = new Vector3(town._position.x, town._position.y, -1);
		}
	}

	void displayControlRingIfNecessary(Town town) {
		if (town._controlledBy == ControlledBy.Player) {
			instantiateControlRing (town, PlayerControlRingPrefab);
		} 
		else if (town._controlledBy == ControlledBy.Enemy) {
			instantiateControlRing (town, EnemyControlRingPrefab);
		}
	}

	void instantiateControlRing(Town town, GameObject controlRingPrefab) {
		GameObject controlRing = Instantiate(controlRingPrefab);
		controlRing.transform.position = new Vector3(town._position.x, town._position.y, -2);
	}
}

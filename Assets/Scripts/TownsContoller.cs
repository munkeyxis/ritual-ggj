using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TownsContoller : MonoBehaviour {
	public GameObject TownPrefab;
	public GameObject PlayerPrefab;
	public GameObject PlayerControlRingPrefab;
	public GameObject EnemyControlRingPrefab;
	List<Town> _townsDisplayed;
	GameObject playerInstance;

	void Start () {
		_townsDisplayed = new List<Town> ();

		drawGameIcons();
	}

	void drawGameIcons() {
		for(int i = 0; i < Manager.TownManager._towns.Count; i++) {
			Town town = Manager.TownManager._towns[i];

			drawTownIcons(town, i);
			instantiatePlayerIconIfNecessary(town);
			displayControlRingIfNecessary (town);
		}
	}

	public void redrawPlayerAndControlRings() {
		for(int i = 0; i < Manager.TownManager._towns.Count; i++) {
			Town town = Manager.TownManager._towns[i];

			movePlayerToThisTownIfNecessary(town);
			displayControlRingIfNecessary (town);
		}
	}

	void movePlayerToThisTownIfNecessary(Town town) {
		if(town._playerPresent) {
			playerInstance.transform.position = new Vector3(town._position.x, town._position.y, -1);
		}
	}

	void drawTownIcons(Town town, int townIndex) {
		GameObject townInstance = Instantiate (TownPrefab);
		townInstance.transform.SetParent (this.transform);
		townInstance.transform.position = town._position;
		townInstance.GetComponent<TownController>().setTownIndex(townIndex);
	}

	void instantiatePlayerIconIfNecessary(Town town) {
		if(town._playerPresent) {
			playerInstance = Instantiate(PlayerPrefab);
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

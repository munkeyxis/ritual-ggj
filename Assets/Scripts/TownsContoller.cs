using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TownsContoller : MonoBehaviour {
	public GameObject TownPrefab;
	public GameObject PlayerPrefab;
	public GameObject PlayerControlRingPrefab;
	public GameObject EnemyControlRingPrefab;
	List<GameObject> _townPrefabInstances;
	List<GameObject> _controlRingInstances;
	GameObject playerInstance;

	void Start () {
		_townPrefabInstances = new List<GameObject> ();
		_controlRingInstances = new List<GameObject> ();

		drawGameIcons();
	}

	void drawGameIcons() {
		for(int i = 0; i < Manager.TownManager._towns.Count; i++) {
			Town town = Manager.TownManager._towns[i];

			drawTownIcons(town, i);
			instantiatePlayerIconIfNecessary(town);
			displayControlRingIfNecessary (town);
			toggleAnimation (town, i);
		}
	}

	public void redrawPlayerAndControlRings() {
		destroyStaleControlRings();

		for(int i = 0; i < Manager.TownManager._towns.Count; i++) {
			Town town = Manager.TownManager._towns[i];

			movePlayerToThisTownIfNecessary(town);
			displayControlRingIfNecessary (town);
			toggleAnimation (town, i);
		}
	}

	void toggleAnimation(Town town, int index) {
		_townPrefabInstances[index].GetComponent<TownController> ().animateTown (town._underAttack);
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
		_townPrefabInstances.Add (townInstance);
	}

	void instantiatePlayerIconIfNecessary(Town town) {
		if(town._playerPresent) {
			playerInstance = Instantiate(PlayerPrefab);
			playerInstance.transform.position = new Vector3(town._position.x, town._position.y, -1);
		}
	}

	void destroyStaleControlRings() {
		foreach (GameObject controlRing in _controlRingInstances) {
			Destroy(controlRing);
		}
		_controlRingInstances = new List<GameObject>();
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
		_controlRingInstances.Add(controlRing);
	}
}

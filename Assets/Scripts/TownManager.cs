using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TownManager : MonoBehaviour, IGameManager
{
	public List<Town> _towns { get; private set;}

	public void StartUp() {
		_towns = new List<Town> ();
		// starting player town
		_towns.Add(new Town(new Vector2(5, -2), ControlledBy.Player, ElementTypes.Fire, true));

		// neatural towns
		_towns.Add(new Town(new Vector2(2, -4), ControlledBy.Neutral, ElementTypes.Earth, false));
		_towns.Add(new Town(new Vector2(1, 2), ControlledBy.Neutral, ElementTypes.Earth, false));
		_towns.Add(new Town(new Vector2(-3, 5), ControlledBy.Neutral, ElementTypes.Earth, false));

		// starting enemy town
		_towns.Add(new Town(new Vector2(-5, 2), ControlledBy.Enemy, ElementTypes.Water, false));

		assignAdjacentcy();
	}

	public void MoveCharacterToTown(int destinationTownIndex) {
		if (isTownAdjacent(destinationTownIndex)) {
			resetPlayPresence();
			_towns[destinationTownIndex].setPlayerPresent(true);
		}
	}

	bool isTownAdjacent(int destinationTownIndex) {
		Town currentTown = getCurrentPlayerTownLocation();
		if(currentTown._adjacentTownIndexes.Contains(destinationTownIndex)) {
			return true;
		}
		return false;
	}

	Town getCurrentPlayerTownLocation() {
		foreach (Town town in _towns) {
			if (town._playerPresent) {
				return town;
			}
		}
		return _towns[0];
	}

	void assignAdjacentcy() {
		_towns[0].assignAdjacentTownIndexes(new List<int>(){1,2});
		_towns[1].assignAdjacentTownIndexes(new List<int>(){0,2});
		_towns[2].assignAdjacentTownIndexes(new List<int>(){0,1,3,4});
		_towns[3].assignAdjacentTownIndexes(new List<int>(){2,4});
		_towns[4].assignAdjacentTownIndexes(new List<int>(){2,3});
	}

	void resetPlayPresence() {
		foreach (Town town in _towns) {
			town.setPlayerPresent(false);
		}
	}
}


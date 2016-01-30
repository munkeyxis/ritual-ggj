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
	}

	public void MoveCharacterToTown(int townIndex) {
		resetPlayPresence();
		_towns[townIndex].setPlayerPresent(true);
	}

	void resetPlayPresence() {
		foreach (Town town in _towns) {
			town.setPlayerPresent(false);
		}
	}
}


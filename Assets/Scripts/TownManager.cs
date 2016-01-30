using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TownManager : MonoBehaviour, IGameManager
{
	List<Town> _towns;

	public void StartUp() {
		// starting player town
		_towns.Add(new Town(new Vector2(5, -2), ControlledBy.Player, ElementTypes.Fire, true));

		// neatural towns
		_towns.Add(new Town(new Vector2(2, -4), ControlledBy.Neutral, ElementTypes.Earth, false));
		_towns.Add(new Town(new Vector2(1, 2), ControlledBy.Neutral, ElementTypes.Earth, false));
		_towns.Add(new Town(new Vector2(-3, 5), ControlledBy.Neutral, ElementTypes.Earth, false));

		// starting enemy town
		_towns.Add(new Town(new Vector2(-5, 2), ControlledBy.Enemy, ElementTypes.Water, false));
	}
}


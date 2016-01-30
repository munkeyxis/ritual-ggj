using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Town {
	ControlledBy _controlledBy;
	Vector2 _position;
	bool _underAttack = false;
	int _turnsUntilTaken = 0;
	bool _playerPresent = false;
	ElementTypes _elementType;
	List<Town> _adjacentTowns = new List<Town>();

	public Town(Vector2 position, ControlledBy controlledBy, ElementTypes type, bool playerPresent) {
		_position = position;
		_controlledBy = controlledBy;
		_elementType = type;
		_playerPresent = playerPresent;
	}
}


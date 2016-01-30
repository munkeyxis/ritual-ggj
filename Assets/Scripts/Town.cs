using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Town {
	public ControlledBy _controlledBy { get; private set; }
	public Vector2 _position { get; private set; }
	bool _underAttack = false;
	int _turnsUntilTaken = 0;
	public bool _playerPresent { get; private set;}
	ElementTypes _elementType;
	List<Town> _adjacentTowns = new List<Town>();

	public Town(Vector2 position, ControlledBy controlledBy, ElementTypes type, bool playerPresent) {
		_position = position;
		_controlledBy = controlledBy;
		_elementType = type;
		_playerPresent = playerPresent;
	}
}


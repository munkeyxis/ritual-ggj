using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Town {
	ControlledBy _controlledBy = ControlledBy.Neutral;
	Vector2 _position = new Vector2();
	bool _underAttack = false;
	int _turnsUntilTaken = 0;
	bool _playerPresent = false;
	ElementTypes _elementType;
	List<Town> _adjacentTowns = new List<Town>();
}


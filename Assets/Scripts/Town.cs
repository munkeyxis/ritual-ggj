using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Town {
	public ControlledBy _controlledBy { get; private set; }
	public Vector2 _position { get; private set; }
	public bool _underAttack { get; private set; }
	int _turnsUntilTaken = 0;
	public bool _playerPresent { get; private set;}
	public ElementTypes _elementType { get; private set; }
	public List<int> _adjacentTownIndexes { get; private set; }

	public Town(Vector2 position, ControlledBy controlledBy, ElementTypes type, bool playerPresent) {
		_position = position;
		_controlledBy = controlledBy;
		_elementType = type;
		_playerPresent = playerPresent;
		_adjacentTownIndexes = new List<int>();
		_underAttack = false;
	}

	public void assignAdjacentTownIndexes(List<int> indexes) {
		foreach (int index in indexes) {
			_adjacentTownIndexes.Add(index);
		}
	}

	public void setPlayerPresent(bool isPresent) {
		_playerPresent = isPresent;
	}

	public void setUnderAttack(bool isUnderAttack) {
		_underAttack = isUnderAttack;
	}

	public void setTurnUntilTaken(int turnCount) {
		_turnsUntilTaken = turnCount;
	}
}


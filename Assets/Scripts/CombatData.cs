using System;
using UnityEngine;

public class CombatData {
	public ElementTypes _chosenPlayerElement { get; private set;}
	public int _chosenPlayerElementCount { get; private set; }
	public ElementTypes _townElement { get; private set; }
	public bool _isNeutral { get; private set; }
	public int _enemyElementCount { get; private set; }
	public bool _isVictorious { get; private set; }

	public CombatData () {
	}

	public void setPlayerElement(ElementTypes type) {
		_chosenPlayerElement = type;
		_chosenPlayerElementCount = Manager.TownManager.GetElementCountForCharacter(ControlledBy.Player);
	}

	public void setIsNeutral(Town town) {
		_isNeutral = false;

		if(town._controlledBy == ControlledBy.Neutral) {
			_isNeutral = true;
		}
	}

	public void setTownType(ElementTypes type) {
		_townElement = type;
		if (_isNeutral) {
			_enemyElementCount = Manager.TownManager.GetElementCountForCharacter (ControlledBy.Enemy);
		} 
		else {
			_enemyElementCount = 1;
		}
	}
}


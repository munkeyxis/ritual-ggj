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
		attackCities();
	}

	public void setTowns(List<Town> towns) {
		_towns = towns;
	}

	public void UpdateTownsBasedOnCombatResults() {
		setTownControlledByBasedOnCombatResult ();
		if (Manager.CombatData._isVictorious) {
			HandlePlayerVictory ();
		}
		HandleEnemyAttackTimers ();
	}

	void HandlePlayerVictory() {
		int townIndex = Manager.WorldDataStore._combatTownIndex;
		MoveCharacterToTown (townIndex);
		Manager.TownManager._towns[townIndex].setUnderAttack(false);
	}

	public void HandleEnemyAttackTimers() {
		foreach(Town town in _towns) {
			if (town._underAttack) {
				if (town._turnsUntilTaken > 0) {
					town.reduceTurnsUntilTaken();
				} 
				else {
					town.setControlledBy(ControlledBy.Enemy);
					town.setUnderAttack (false);
				}
			}
		}
	}

	public void setTownControlledByBasedOnCombatResult() {
		Town combatTown = _towns [Manager.WorldDataStore._combatTownIndex];
		if (Manager.CombatData._isVictorious) {
			combatTown.setPlayerPresent(true);
			combatTown.setControlledBy (ControlledBy.Player);
		}
	}

	public void SelectTown(int destinationTownIndex) {
		Town town = _towns [destinationTownIndex];
		if (town._controlledBy == ControlledBy.Player &&
		    !town._underAttack) {
			MoveCharacterToTown(destinationTownIndex);
		} else {
			AttackTown(destinationTownIndex);
		}
	}

	public void AttackTown(int destinationTownIndex) {
		Town town = _towns[destinationTownIndex];
		beginCombat(town, destinationTownIndex);
	}

	public void MoveCharacterToTown(int destinationTownIndex) {
		if (isTownAdjacent(destinationTownIndex)) {
			resetPlayPresence();
			Town town = _towns[destinationTownIndex];
			town.setPlayerPresent(true);
		}
	}

	void beginCombat(Town town, int townIndex) {
		Manager.CombatData.setPlayerElement(ElementTypes.Fire);
		Manager.CombatData.setIsNeutral(town);
		Manager.CombatData.setTownType(town._elementType);
		Manager.WorldDataStore.setCombatTown(townIndex);
		Application.LoadLevel("TownBattle");
	}

	public int GetElementCountForCharacter(ControlledBy character) {
		int elementCount = 0;

		foreach (Town town in _towns) {
			if (town._controlledBy == character) {
				elementCount++;
			}
		}
		return elementCount;
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

	void attackCities() {
		foreach (Town town in _towns) {
			if (town._controlledBy == ControlledBy.Enemy) {
				foreach (int index in town._adjacentTownIndexes) {
					Town adjacentTown = _towns [index];
					if (adjacentTown._controlledBy != town._controlledBy &&
						!adjacentTown._underAttack) {

						int rollResult = Random.Range (1, 20);

						if (rollResult >= 15) {
							int contestTurnCount = 2;
							adjacentTown.setUnderAttack(true);
							if (adjacentTown._controlledBy == ControlledBy.Player) {
								contestTurnCount = 4;
							}
							adjacentTown.setTurnUntilTaken(contestTurnCount);
						}
					}
				}
			}
		}
	}

	void resetPlayPresence() {
		foreach (Town town in _towns) {
			town.setPlayerPresent(false);
		}
	}
}


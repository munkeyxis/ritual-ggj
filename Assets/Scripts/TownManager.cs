using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TownManager : MonoBehaviour, IGameManager
{
	public List<Town> _towns { get; private set;}
	public GameObject Canvas;

	public void StartUp() {
		_towns = new List<Town> ();
		// starting player town
		_towns.Add(new Town(new Vector2(-6.5f, -3.25f), ControlledBy.Player, ElementTypes.Fire, true));

		// neatural towns
		_towns.Add(new Town(new Vector2(-0.5f, -2.63f), ControlledBy.Neutral, ElementTypes.Earth, false));
		_towns.Add(new Town(new Vector2(-6f, 2.64f), ControlledBy.Neutral, ElementTypes.Air, false));
		_towns.Add(new Town(new Vector2(-3f, -0.2f), ControlledBy.Neutral, ElementTypes.Electric, false));
		_towns.Add(new Town(new Vector2(0f, 2.12f), ControlledBy.Neutral, ElementTypes.Fire, false));
		_towns.Add(new Town(new Vector2(-3.12f, 4.1f), ControlledBy.Neutral, ElementTypes.Water, false));
		_towns.Add(new Town(new Vector2(5.8f, -1.9f), ControlledBy.Neutral, ElementTypes.Earth, false));

		// starting enemy town
		_towns.Add(new Town(new Vector2(6.8f, 2.5f), ControlledBy.Enemy, ElementTypes.Water, false));

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
		} else {
			performTurnOperations ();
		}
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
		if (isTownAdjacent (destinationTownIndex)) {
			if (town._controlledBy == ControlledBy.Player &&
			   !town._underAttack) {
				MoveCharacterToTown (destinationTownIndex);
				performTurnOperations ();
			} else {
				AttackTown (destinationTownIndex);
			}
		}
	}

	public void AttackTown(int destinationTownIndex) {
		Town town = _towns[destinationTownIndex];
		beginCombat(town, destinationTownIndex);
	}

	public void MoveCharacterToTown(int destinationTownIndex) {
		resetPlayPresence();
		Town town = _towns[destinationTownIndex];
		town.setPlayerPresent(true);
	}

	public void performTurnOperations() {
		HandleEnemyAttackTimers ();
		attackCities();
	}

	public void setPlayerElementType(int element) {
		ElementTypes elementAsEnum;
		switch (element) {
		case 0:
			elementAsEnum = ElementTypes.Fire;
			break;
		case 1:
			elementAsEnum = ElementTypes.Water;
			break;
		case 2:
			elementAsEnum = ElementTypes.Air;
			break;
		case 3:
			elementAsEnum = ElementTypes.Earth;
			break;
		case 4:
			elementAsEnum = ElementTypes.Electric;
			break;
		default:
			elementAsEnum = ElementTypes.Fire;
			break;
		}
		Manager.CombatData.setPlayerElement(elementAsEnum);

		Application.LoadLevel("TownBattle");
	}

	void beginCombat(Town town, int townIndex) {
		Manager.CombatData.setIsNeutral(town);
		Manager.CombatData.setTownType(town._elementType);
		Manager.WorldDataStore.setCombatTown(townIndex);
		Canvas.SetActive (true);
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
		_towns[1].assignAdjacentTownIndexes(new List<int>(){0,3,6});
		_towns[2].assignAdjacentTownIndexes(new List<int>(){0,5});
		_towns[3].assignAdjacentTownIndexes(new List<int>(){1,4});
		_towns[4].assignAdjacentTownIndexes(new List<int>(){3,5,7});
		_towns[5].assignAdjacentTownIndexes(new List<int>(){2,4});
		_towns[6].assignAdjacentTownIndexes(new List<int>(){1,7});
		_towns[7].assignAdjacentTownIndexes(new List<int>(){4,6});
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


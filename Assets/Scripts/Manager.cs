using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(TownManager))]
public class Manager : MonoBehaviour {

	public static CombatData CombatData { get; private set; }
	public static TownManager TownManager { get; private set; }

	private List<IGameManager> _startupList;

	void Awake () {
		CombatData = new CombatData();
		TownManager = GetComponent<TownManager>();

		_startupList = new List<IGameManager>();
		_startupList.Add(TownManager);

		foreach (IGameManager manager in _startupList) {
			manager.StartUp();
		}
	}
}

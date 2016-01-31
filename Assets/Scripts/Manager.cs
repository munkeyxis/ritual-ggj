using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(TownManager))]
public class Manager : MonoBehaviour {

	public static CombatData CombatData { get; private set; }
	public static WorldDataStore WorldDataStore { get; private set; }
	public static TownManager TownManager { get; private set; }

	private List<IGameManager> _startupList;

	void Awake () {
		if (CombatData == null) {
			CombatData = new CombatData ();
		}
		TownManager = GetComponent<TownManager>();

		_startupList = new List<IGameManager>();
		_startupList.Add(TownManager);

		if (WorldDataStore == null) {
			foreach (IGameManager manager in _startupList) {
				manager.StartUp ();
			}
			WorldDataStore = new WorldDataStore ();
			Manager.WorldDataStore.storeTownsData (Manager.TownManager._towns);
		} 
		else {
			Manager.TownManager.setTowns (Manager.WorldDataStore._towns);
		}
	}
}

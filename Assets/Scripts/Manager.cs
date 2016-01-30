using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(TownManager))]
public class Manager : MonoBehaviour {

	public static TownManager TownManager {get; private set;}

	private List<IGameManager> _startupList;

	void Awake () {
		TownManager = GetComponent<TownManager>();

		_startupList = new List<IGameManager>();
		_startupList.Add(TownManager);
	}
}

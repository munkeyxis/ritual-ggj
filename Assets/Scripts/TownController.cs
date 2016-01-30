using UnityEngine;
using System.Collections;

public class TownController : MonoBehaviour {

	int _townIndex;

	public void setTownIndex(int index) {
		_townIndex = index;
	}

	void OnMouseUpAsButton() {
		Manager.TownManager.MoveCharacterToTown(_townIndex);
		this.transform.parent.GetComponent<TownsContoller> ().redrawPlayerAndControlRings();
	}
}

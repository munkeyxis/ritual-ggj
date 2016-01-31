using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ElementButtonControl : MonoBehaviour {
	public ElementTypes elementType;

	void OnEnable() {
		if (Manager.TownManager.getElementCountForPlayer (elementType) == 0) {
			GetComponent<Button>().interactable = false;
		}
	}

	public void selectElement(int element) {
		Manager.TownManager.setPlayerElementType (element);
	}
}

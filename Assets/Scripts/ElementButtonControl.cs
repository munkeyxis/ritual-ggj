using UnityEngine;
using System.Collections;

public class ElementButtonControl : MonoBehaviour {

	public void selectElement(int element) {
		Manager.TownManager.setPlayerElementType (element);
	}
}

using UnityEngine;
using System.Collections;

public class TownController : MonoBehaviour {

	int _townIndex;
	Animator _animator;

	public void setTownIndex(int index) {
		_townIndex = index;
	}

	public void animateTown(bool animate) {
		_animator = GetComponent<Animator>();
		_animator.SetBool("underAttack", animate);
	}

	void OnMouseUpAsButton() {
		Manager.TownManager.AttackTown(_townIndex);
	}
}

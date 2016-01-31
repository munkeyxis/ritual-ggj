using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITracker : MonoBehaviour {

	public GameObject p1healthObj;
	public GameObject p2healthObj;
	public GameObject levelEnd;
	public GameObject fireMage;
	public GameObject enemyFireMage;
	public GameObject waterMage;
	public GameObject enemyWaterMage;
	public GameObject airMage;
	public GameObject enemyAirMage;
	public GameObject earthMage;
	public GameObject enemyEarthMage;
	public GameObject deathMage;
	public GameObject enemyDeathMage;
	public GameObject earthBack;
	public GameObject airBack;
	public GameObject waterBack;
	public GameObject fireBack;
	public GameObject deathBack;

	Text p1Health;
	Text p2Health;
	public static UITracker instance;

	// Use this for initialization
	void Start () {
		p1Health = p1healthObj.GetComponent<Text> ();
		p2Health = p2healthObj.GetComponent<Text> ();
		instance = this;
		levelEnd.GetComponent<FlyOnScript> ().target = Camera.main.transform;
		CombatData cd = Manager.CombatData;
		GameObject enemyObj=null;
		EnemyControl enemy;

		switch (cd._townElement) {
		case ElementTypes.Air:
			enemyObj = Instantiate (enemyAirMage);
			Instantiate (airBack);
			break;
		case ElementTypes.Earth:
			enemyObj = Instantiate (enemyEarthMage);
			Instantiate (earthBack);
			break;
		case ElementTypes.Fire:
			enemyObj = Instantiate (enemyFireMage);
			Instantiate (fireBack);
			break;
		case ElementTypes.Water:
			enemyObj = Instantiate (enemyWaterMage);
			Instantiate (waterBack);
			break;
		case ElementTypes.Electric:
			enemyObj = Instantiate (enemyDeathMage);
			Instantiate (deathBack);
			break;
		}
		
		enemy = enemyObj.GetComponent<EnemyControl> ();

		switch (cd._chosenPlayerElement)
		{
		case ElementTypes.Air:
			Instantiate (airMage);
			break;
		case ElementTypes.Earth:
			Instantiate (earthMage);
			break;
		case ElementTypes.Fire:
			Instantiate (fireMage);
			break;
		case ElementTypes.Water:
			Instantiate (waterMage);
			break;
		case ElementTypes.Electric:
			Instantiate (deathMage);
			break;
		}

		enemy.HP = (int)(enemy.HP * (1.0f + (cd._enemyElementCount / 5.0f)));
		PlayerControl.instance.HP = (int)(PlayerControl.instance.HP*(1.0f + (cd._chosenPlayerElementCount / 5.0f)));

		if (cd._townElement == ElementTypes.Water && cd._chosenPlayerElement == ElementTypes.Fire) {
			enemy.shotDamage += (enemy.shotDamage/5);
			PlayerControl.instance.shotDamage -= (PlayerControl.instance.shotDamage/5);
		} else if (cd._townElement == ElementTypes.Fire && cd._chosenPlayerElement == ElementTypes.Water) {
			enemy.shotDamage -= (enemy.shotDamage/5);
			PlayerControl.instance.shotDamage += (PlayerControl.instance.shotDamage/5);
		} else if (cd._townElement == ElementTypes.Fire && cd._chosenPlayerElement == ElementTypes.Air) {
			enemy.shotDamage += (enemy.shotDamage/5);
			PlayerControl.instance.shotDamage -= (PlayerControl.instance.shotDamage/5);
		} else if (cd._townElement == ElementTypes.Air && cd._chosenPlayerElement == ElementTypes.Fire) {
			enemy.shotDamage -= (enemy.shotDamage/5);
			PlayerControl.instance.shotDamage += (PlayerControl.instance.shotDamage/5);
		} else if (cd._townElement == ElementTypes.Air && cd._chosenPlayerElement == ElementTypes.Earth) {
			enemy.shotDamage += (enemy.shotDamage/5);
			PlayerControl.instance.shotDamage -= (PlayerControl.instance.shotDamage/5);
		} else if (cd._townElement == ElementTypes.Earth && cd._chosenPlayerElement == ElementTypes.Air) {
			enemy.shotDamage -= (enemy.shotDamage/5);
			PlayerControl.instance.shotDamage += (PlayerControl.instance.shotDamage/5);
		} else if (cd._townElement == ElementTypes.Earth && cd._chosenPlayerElement == ElementTypes.Water) {
			enemy.shotDamage += (enemy.shotDamage/5);
			PlayerControl.instance.shotDamage -= (PlayerControl.instance.shotDamage/5);
		} else if (cd._townElement == ElementTypes.Water && cd._chosenPlayerElement == ElementTypes.Earth) {
			enemy.shotDamage -= (enemy.shotDamage/5);
			PlayerControl.instance.shotDamage += (PlayerControl.instance.shotDamage/5);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetP1Health(int health)
	{
		p1Health.text = health.ToString ();
	}

	public void SetP2Health(int health)
	{
		p2Health.text = health.ToString ();
	}

	public void EndLevel(bool didwin)
	{
		var l_end = Instantiate (levelEnd) as GameObject;
		Text ltext = l_end.GetComponentInChildren<Text> ();
		AudioSource src = GetComponent<AudioSource> ();

		if (didwin) {
			ltext.text = "You Win";
			src.Stop();
			SoundEffectsScript.Instance.PlayWin();
			Manager.CombatData.setVictorious(true);
		} else {
			ltext.text = "You Lose";
			src.Stop ();
			SoundEffectsScript.Instance.PlayLose();
			Manager.CombatData.setVictorious(false);
		}
	}
}

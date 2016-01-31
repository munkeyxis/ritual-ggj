using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour {

	public Transform splode;
	public ElementTypes element;

	// Use this for initialization
	void Start () {
		switch (element) {
		case ElementTypes.Air:
			SoundEffectsScript.Instance.PlayAirShot ();
			break;
		case ElementTypes.Earth:
			SoundEffectsScript.Instance.PlayEarthShot ();
			break;
		case ElementTypes.Water:
			SoundEffectsScript.Instance.PlayWaterShot ();
			break;
		case ElementTypes.Fire:
			SoundEffectsScript.Instance.PlayFireShot ();
			break;
		case ElementTypes.Electric:
			SoundEffectsScript.Instance.PlayDeathShot ();
			break;
		}	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		// splode.transform.position = transform.position;

		var newsplode = Instantiate(splode);
		newsplode.transform.position = transform.position;
		Destroy (newsplode.gameObject,newsplode.gameObject.GetComponent<ParticleSystem>().duration);
		Destroy (gameObject);
	}
}

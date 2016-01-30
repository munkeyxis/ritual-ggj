using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour {

	public Transform splode;

	// Use this for initialization
	void Start () {
	
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

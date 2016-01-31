using UnityEngine;
using System.Collections;

public class SoundEffectsScript : MonoBehaviour {

	public static SoundEffectsScript Instance;

	public AudioClip fireShot;
	public AudioClip waterShot;
	public AudioClip airShot;
	public AudioClip earthShot;
	public AudioClip deathShot;
	public AudioClip playerHit;
	public AudioClip youWin;
	public AudioClip youLose;
	public AudioClip shot;

	void Awake()
	{
		// Register the singleton
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of SoundEffectsHelper!");
		}
		Instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayShot()
	{
		MakeSound (shot);
	}

	public void PlayAirShot()
	{
		MakeSound(airShot);
	}

	public void PlayEarthShot ()
	{
		MakeSound (earthShot);
	}

	public void PlayFireShot()
	{
		MakeSound (fireShot);
	}

	public void PlayWaterShot ()
	{
		MakeSound (waterShot);
	}

	public void PlayDeathShot()
	{
		MakeSound (deathShot);
	}

	public void PlayWin()
	{
		MakeSound (youWin);
	}

	public void PlayLose()
	{
		MakeSound (youLose);
	}

	private void MakeSound(AudioClip originalClip)
	{
		// As it is not 3D audio clip, position doesn't matter.
		AudioSource.PlayClipAtPoint(originalClip, transform.position);
	}
}

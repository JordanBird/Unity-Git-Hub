using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidManager : MonoBehaviour
{
	public List<Sprite> smallAsteroids = new List<Sprite>();
	public List<Sprite> mediumAsteroids = new List<Sprite>();
	public List<Sprite> largeAsteroids = new List<Sprite>();

	public GameObject largeExplosion;
	public GameObject mediumExplosion;
	public GameObject smallExplosion;

	public AudioClip lBang;
	public AudioClip mBang;
	public AudioClip sBang;

	public GameObject asteroidTemplate;

	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i < 10; i++)
		{
			SpawnAsteroid ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnAsteroid()
	{
		Vector2 spawnPos = Vector2.zero;

		int side = Random.Range (0, 4);

		if (side == 0)
		{
			spawnPos.x = -12;
			spawnPos.y = Random.Range (-6, 6);
		}
		else if (side == 1)
		{
			spawnPos.x = Random.Range (-9, 9);
			spawnPos.y = 7;
		}
		else if (side == 2)
		{
			spawnPos.x = 12;
			spawnPos.y = Random.Range (-6, 6);
		}
		else if (side == 3)
		{
			spawnPos.x = Random.Range (-10, 10);
			spawnPos.y = -7;
		}

		Instantiate(asteroidTemplate, spawnPos, Quaternion.identity);
	}
}

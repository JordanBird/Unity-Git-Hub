using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
	public enum Type {Small, Medium, Large};
	
	public Type type = Type.Large;

	GameObject explosion;
	AudioSource audio;

	AsteroidManager manager;
	
	public float rotationSpeed = 1f;
	public float movementSpeed = 1f;
	
	Vector2 target = Vector2.zero;
	
	Vector2 startMove = Vector2.zero;
	float startTime = 0;
	
	// Use this for initialization
	void Start ()
	{
		Debug.Log ("Starting");
		manager = GameObject.Find ("Asteroid Manager").GetComponent<AsteroidManager>();
		
		target = GetTarget ();
		startMove = transform.position;
		startTime = Time.time;

		audio = GetComponent<AudioSource> ();

		if (type == Type.Large)
		{
			GetComponent<SpriteRenderer>().sprite = manager.largeAsteroids[Random.Range (0, manager.largeAsteroids.Count)];
			gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
			explosion = manager.largeExplosion;
			audio.clip = manager.lBang;
		}

		if (type == Type.Medium)
		{
			explosion = manager.mediumExplosion;
			audio.clip = manager.mBang;
		}

		if (type == Type.Small)
		{
			explosion = manager.mediumExplosion;
			audio.clip = manager.sBang;
		}

		StartCoroutine (Spin ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	IEnumerator Spin()
	{
		while (true)
		{
			transform.Rotate (new Vector3(0, 0, rotationSpeed));
			
			float distCovered = (Time.time - startTime) * movementSpeed;
			float fracJourney = distCovered / (Vector2.Distance (startMove, target));
			
			transform.position = Vector2.Lerp (startMove, target, fracJourney);
			
			if (new Vector2(transform.position.x, transform.position.y) == target)
			{
				Destroy (gameObject);
			}
			
			yield return new WaitForSeconds(0.01f);
		}
	}
	
	public Vector2 GetTarget()
	{
		Vector2 targetPos = Vector2.zero;
		
		int side = Random.Range (0, 4);
		
		if (side == 0)
		{
			targetPos.x = -12;
			targetPos.y = Random.Range (-6, 6);
		}
		else if (side == 1)
		{
			targetPos.x = Random.Range (-9, 9);
			targetPos.y = 7;
		}
		else if (side == 2)
		{
			targetPos.x = 12;
			targetPos.y = Random.Range (-6, 6);
		}
		else if (side == 3)
		{
			targetPos.x = Random.Range (-10, 10);
			targetPos.y = -7;
		}
		
		return targetPos;
	}
	
	public void Impact()
	{
		GameObject deadAst = Instantiate (explosion, transform.position, Quaternion.identity) as GameObject;
		deadAst.AddComponent<AudioSource> ().clip = audio.clip;
		deadAst.GetComponent<AudioSource> ().Play ();

		if (type == Type.Small)
		{
			Destroy (gameObject);
		}
		else
		{
			//Spawn Place
			Vector2 spawnLocation = transform.position;

			Vector2 sizeAdd = new Vector2(GetComponent<SpriteRenderer>().sprite.bounds.extents.x, GetComponent<SpriteRenderer>().sprite.bounds.extents.y);

			//Spawn
			GameObject a1 = Instantiate (manager.asteroidTemplate, spawnLocation + sizeAdd, Quaternion.identity) as GameObject;
			GameObject a2 = Instantiate (manager.asteroidTemplate, spawnLocation - sizeAdd, Quaternion.identity) as GameObject;
			Debug.Log("1");
			if (type == Type.Large)
			{
				a1.GetComponent<SpriteRenderer>().sprite = manager.mediumAsteroids[Random.Range (0, manager.mediumAsteroids.Count)];
				a2.GetComponent<SpriteRenderer>().sprite = manager.mediumAsteroids[Random.Range (0, manager.mediumAsteroids.Count)];
				
				a1.GetComponent<Asteroid>().type = Type.Medium;
				a2.GetComponent<Asteroid>().type = Type.Medium;
			}
			else
			{
				a1.GetComponent<SpriteRenderer>().sprite = manager.smallAsteroids[Random.Range (0, manager.smallAsteroids.Count)];
				a2.GetComponent<SpriteRenderer>().sprite = manager.smallAsteroids[Random.Range (0, manager.smallAsteroids.Count)];
				
				a1.GetComponent<Asteroid>().type = Type.Small;
				a2.GetComponent<Asteroid>().type = Type.Small;
			}
			Debug.Log("2");
			a1.AddComponent<PolygonCollider2D>().isTrigger = true;
			a2.AddComponent<PolygonCollider2D>().isTrigger = true;

			a1.GetComponent<Asteroid>().target = -target;
			a2.GetComponent<Asteroid>().target = -target;
			
			Debug.Log("3");

			Destroy (gameObject);
		}
	}
	
	public void Hit()
	{
		GameObject.FindGameObjectWithTag ("Player").GetComponent<ShipController> ().score++;

		Impact ();
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "Ship")
		{
			Debug.Log ("Hits");
			col.gameObject.SendMessage ("Hit");
			Impact ();
		}
	}
}
using UnityEngine;
using System.Collections;

public class Missle : MonoBehaviour
{
	private float speed = 10;
	private float cumtime = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		//transform.Translate (transform.right * Time.deltaTime);
		rigidbody2D.velocity = transform.right * speed;
		cumtime += Time.deltaTime;
		if (cumtime > 10)
		{
			Destroy(this.gameObject);
		} 
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name.Contains ("Asteroid"))
		{
		    other.SendMessage ("Hit");
			Destroy (gameObject);
		}
	}
}

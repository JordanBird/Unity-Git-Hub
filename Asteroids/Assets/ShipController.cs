using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour
{
	public int lives = 5;
	public int score = 0;

	private float speed = 5;
	private float maxSpeed = 2;


	public GameObject missle;

	AudioSource audio;
	public AudioSource thrust;

	public float boundsX = 5;
	public float boundsY = 5;

	// Use this for initialization
	void Start ()
	{
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		TrackMouse ();
		ProcessInput ();
		OutOfBoundsCheck ();
	}

	private void ProcessInput()
	{
		if (Input.GetKey (KeyCode.W))
			rigidbody2D.AddForce (transform.right * speed);

		if (Input.GetKey (KeyCode.S))
			rigidbody2D.AddForce (-(transform.right * speed));

		if(rigidbody2D.velocity.magnitude > maxSpeed)
		{
			rigidbody2D.velocity = rigidbody2D.velocity.normalized * maxSpeed;
		}
	}

	private void TrackMouse()
	{
		Vector3 pointOnScreen = Camera.main.WorldToScreenPoint (transform.position);
		Vector3 difference = Input.mousePosition - pointOnScreen;

		transform.rotation = Quaternion.Euler (new Vector3(0, 0, Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg));

		if (Input.GetMouseButtonDown (0))
			Shoot();
	}

	public void OutOfBoundsCheck()
	{
		if (transform.position.x > boundsX / 2)
			transform.position = new Vector3(-boundsX / 2, transform.position.y, transform.position.z);

		if (transform.position.x < -boundsX / 2)
			transform.position = new Vector3(boundsX / 2, transform.position.y, transform.position.z);

		if (transform.position.y > boundsY / 2)
			transform.position = new Vector3(transform.position.x, -boundsY / 2, transform.position.z);
		
		if (transform.position.y < -boundsY / 2)
			transform.position = new Vector3(transform.position.x, boundsY / 2, transform.position.z);
	}

	public void Shoot()
	{
		audio.Play ();
		Instantiate (missle, transform.position, new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w));
	}

	public void Hit()
	{
		lives--;

		if (lives < 0)
			Die();
	}

	public void Die()
	{
		Debug.Log ("Game Over");
	}

	void OnGUI()
	{
		GUI.Label (new Rect (10, 10, 150, 30), "Lives: " + lives);
		GUI.Label (new Rect (10, 30, 150, 30), "Score: " + score);
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireCube (Vector3.zero, new Vector3 (boundsX, boundsY, 0));
	}
}

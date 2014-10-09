using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour
{
	public float TTL = 5;

	void Awake()
	{
		Invoke ("DestroyNow", TTL);
	}

	public void DestroyNow()
	{
		Destroy (gameObject);
	}
}
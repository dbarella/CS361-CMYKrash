using UnityEngine;
using System.Collections;

public abstract class Ammo : MonoBehaviour {
	
	public float damage;
	public float speed;
	
	void Start () {
	
	}
	
	void Update () {
		//Move forward constantly at speed rate.
		transform.Translate(speed*Vector3.right*Time.deltaTime);
	}
}

using UnityEngine;
using System.Collections;

public abstract class Ammo : MonoBehaviour {
	
	public float damage;
	public float speed;
	
	void Start () {
	
	}
	
	void FixedUpdate () {
		Move();
	}
	
	public void Move() {
		//Move forward constantly at speed rate.
		transform.Translate(speed*Vector3.right*Time.fixedDeltaTime);
	}
	
	public float GetDamage() {
		Debug.Log ("GetDamage Called.  Returning: " + damage);
		return damage;
	}
	
	public void Die(){
		Destroy(gameObject);
	}
}

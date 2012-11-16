using UnityEngine;
using System.Collections;

public abstract class Ammo : MonoBehaviour {
	
	public float damage;
	public float speed;
	
	void Start () {
	
	}
	
	void Update () {
		Move();
	}
	
	public void Move() {
		//Move forward constantly at speed rate.
		transform.Translate(speed*Vector3.right*Time.deltaTime);
	}
	
	public float GetDamage() {
		return damage;
	}
	
	public void Die(){
		Destroy(gameObject);
	}
}

using UnityEngine;
using System.Collections;

public abstract class Ammo : MonoBehaviour {
	
	public float zOffset;
	public float damage;
	public float speed;
	
	void Start () {
		MoveOffset();
	}
	
	void FixedUpdate () {
		Move();
	}
	
	public void Move() {
		//Move forward constantly at speed rate.
		transform.Translate(speed*Vector3.right*Time.fixedDeltaTime);
	}
	
	public float GetDamage() {
//		Debug.Log ("GetDamage Called.  Returning: " + damage);
		return damage;
	}
	
	public void Die(){
		Destroy(gameObject);
	}
	
	public void SetDamage(float d){
		damage = d;
	}
	public void MoveOffset(){
		transform.Translate(new Vector3(0,0,zOffset));
	}
}


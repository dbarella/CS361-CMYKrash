using UnityEngine;
using System.Collections;

public abstract class Laser : Ammo {

	void Start(){
		//On startup, immediately shift forward so the edge will be on the ship, rather than the laser being half forward and half backwards.
		transform.Translate((gameObject.collider as CapsuleCollider).height * Vector3.right);
	}

	void Update(){
		//OnTriggerEnter is guarenteed to be called before this, so just die.
		Die();
	}

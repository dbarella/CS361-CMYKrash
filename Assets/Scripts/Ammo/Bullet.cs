using UnityEngine;
using System.Collections;

public class Bullet : Ammo {
	
	void Start(){
		
	}
	
	void OnTriggerEnter(Collider o){
			if(o.gameObject.CompareTag("Enemy")) {
				Die();
			}
		
	}
}

using UnityEngine;
using System.Collections;

public class Bullet : Ammo {
	void OnTriggerEnter(Collider o){
		//if not a powerup
		//if(o.GetComponent<PowerUp> == null){
			//do damage and destroy yourself
			o.GetComponent<Spawnable>().TakeDamage(damage);
			Die();
		//}
	}
}

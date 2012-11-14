/*Projectile for the player ship to fire.
 *Will move down the lanes to the right at a constant rate
 *Deals just one damage when colliding with any object that needs to be collidable with bullets.
 */

using UnityEngine;
using System.Collections;

public class Bullet2 : MonoBehaviour{
	public float speed;

	public void Update(){
		//Move right at a constant rate; falloff should handle deletion
		//But it doesn't. I guess these will just clutter the world up.
		transform.Translate(Time.deltaTime*speed*Vector3.right);
	}

	public void OnTriggerEnter(Collider other){
		//When colliding with something, check if 
		//it's an object we can damage. If so, damage it and die.
		Debug.Log("Bullet hit something");
		if(other.tag != "Player"){
			Damage script = other.GetComponent<Damage>();
			if(script != null){
				script.TakeDamage(1);
				Debug.Log("Bullet dealing damage to object!");
				Destroy(gameObject);
			}
		}
	}
}


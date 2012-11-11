/*Projectile for the player ship to fire.
 *Will move down the lanes to the right at a constant rate
 *Deals just one damage when colliding with any object that needs to be collidable with bullets.
 */

/*using UnityEngine;
using System.Collections;

//Last modified by: Brendan, Thursday 11/8 @ 7 PM

public class Bullet : MonoBehaviour{
	public float speed;

	public void Update(){
		//Move right at a constant rate; falloff should handle deletion
		transform.Translate(Time.deltaTime*speed*Vector3.right);
	}

	public void onTriggerEnter(Collider other){
		Debug.Log("Bonk");
		//When colliding with something, check if 
		//it's an object we can damage. If so, damage it and die.
		if(other.tag == "Enemy"){
			Destroy(other.gameObject);	
		}
		if(other.tag == "Planet") {
			Planet script = other.gameObject.GetComponent<Planet>();
			script.takeDamage(1);
		}
		Destroy(gameObject);
		
	}
}

//Adam's damage stuff, to be implemented later
/*if(other.tag != "Player"){
			Damage script = other.gameObject.GetComponent<Damage>();
			if(script != null){
				script.TakeDamage(1);
				Debug.Log("Bullet dealing damage to object!");
				Destroy(gameObject);
			}
		}*/

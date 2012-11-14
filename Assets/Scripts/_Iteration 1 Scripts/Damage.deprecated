/*Damage script, by Adam Stafford
 *To be used with any object designed to have health and die from damage
 *An object dealing damage to another object will access this script on the
 *object being damaged and call the TakeDamage(int dam)
 *This script will kill the parent object if the health total is 0 or less.
 */

using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour{
	public int health;

	//Kills the parent object
	public void Die(){
		Destroy(gameObject);
	}
	
	public void TakeDamage(int dam){
		Debug.Log("I took some damamge!");
		health -= dam;
		if(health <= 0)
			Die();
	}



}

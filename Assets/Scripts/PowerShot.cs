using UnityEngine;
using System.Collections;

public class PowerShot : Spawnable {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		base.Move();
	}
	
	/*If we collide with a ship object, have the ship collect this powerup.
	 */
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){
			other.GetComponent<Ship>().GetItem(this.gameObject.tag);
		}
	}
	
	/*Must be overriden*/
	override public void TakeDamage(){
		return;
	}
}

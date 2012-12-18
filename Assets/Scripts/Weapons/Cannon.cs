using UnityEngine;
using System.Collections;

public class Cannon : Weapon {
	

	// Use this for initialization
	void Start () {
	
	}
	
	override public void PowerShot(){
		GameObject o = Instantiate(ammo, this.transform.position, Quaternion.identity) as GameObject;
		o.GetComponent<Bomb>().damage = o.GetComponent<Bomb>().damage * 2;
		cooldown = cdtime;
		this.isPowerShot = false;
	}
}

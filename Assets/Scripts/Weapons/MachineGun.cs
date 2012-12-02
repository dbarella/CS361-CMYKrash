using UnityEngine;
using System.Collections;

public class MachineGun : Weapon {

	// Use this for initialization
	void Start () {
		cdtime = 0.2f; //Solid machine gun speed
	}
	
	override public void PowerShot(){
				GameObject o = Instantiate(ammo, this.transform.position, Quaternion.identity) as GameObject;
		o.GetComponent<Bullet>().damage = o.GetComponent<Bullet>().damage * 2;
		cooldown = cdtime;
		this.isPowerShot = false;
	}
}

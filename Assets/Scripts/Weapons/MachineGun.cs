using UnityEngine;
using System.Collections;

public class MachineGun : Weapon {

	// Use this for initialization
	void Start () {
	
	}
	
	override public void PowerShot(){
				GameObject o = Instantiate(ammo, this.transform.position, Quaternion.identity) as GameObject;
		o.GetComponent<Bullet>().damage = o.GetComponent<Bullet>().damage * 2;
		cooldown = cdtime;
		this.isPowerShot = false;
	}
}

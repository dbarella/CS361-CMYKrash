using UnityEngine;
using System.Collections;

public class YAM : Weapon{

// Use this for initialization
void Start () {

}

// Update is called once per frame
	/*void Update () {
		base.Update();
	}*/

	override public void PowerShot(){
		GameObject o = Instantiate(ammo, this.transform.position, Quaternion.identity) as GameObject;
		o.GetComponent<Laser>().damage = o.GetComponent<Laser>().damage * 2;
		cooldown = cdtime;
		this.isPowerShot = false;
	}
}
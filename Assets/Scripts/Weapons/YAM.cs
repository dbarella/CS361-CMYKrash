using UnityEngine;
using System.Collections;

public class YAM : Weapon{

	public float maxChargeTime = 5;
	public float maxCharge = 40;
	public float dps = 30;
	private float chargeTime = 0;
	
	void Start () {}

	new public void Update () {
		if(/*Input.GetButton(buttonPress) && */chargeTime <= maxChargeTime) {
			chargeTime += Time.deltaTime;
		}
		if(Input.GetButtonDown(buttonPress) && cooldown <=0){
			StandardShot();
			chargeTime = 0;
		}
		//Decriment cooldown;
		if(cooldown > 0)
			cooldown -= Time.deltaTime;
	}
	
	new public void StandardShot(){
		//Instantiate a shot, pointing the same direction as the shooter.
		float d = chargeTime*dps;
		GameObject o = Instantiate(ammo, this.transform.position + 10*Vector3.right, Quaternion.identity) as GameObject;
		o.GetComponent<Ammo>().SetDamage(d);
		//Debug.Log(d);
		cooldown = cdtime;
	}

	override public void PowerShot(){
		GameObject o = Instantiate(ammo, this.transform.position, Quaternion.identity) as GameObject;
		o.GetComponent<Laser>().damage = o.GetComponent<Laser>().damage * 2;
		cooldown = cdtime;
		this.isPowerShot = false;
	}
}
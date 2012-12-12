using UnityEngine;
using System.Collections;

public class MachineGun : Weapon {
	float angle = 15;	//Either the angle between each shot in the shotgun, or the maximum spread for the cone.
	int splay = 2; //This is the amount of lanes up or down across which the shot will splay.
	
	// Use this for initialization
	void Start () {
		cdtime = 0.2f; //Solid machine gun speed
	}
	
	new	public void Update () {
		if(Input.GetButton(buttonPress) && cooldown <= 0) {
//			Debug.Log("Firing a Weapon: " + buttonPress);
			StandardShot();
		}
		//Decriment cooldown;
		if(cooldown > 0)
			cooldown -= Time.deltaTime;
	}
	
	override public void PowerShot(){
		GameObject o = Instantiate(ammo, this.transform.position, Quaternion.identity) as GameObject;
		o.GetComponent<Bullet>().damage = o.GetComponent<Bullet>().damage * 2;
		cooldown = cdtime;
		this.isPowerShot = false;
	}
	
	new public void StandardShot(){
		//For the shotgun shot:
//		for(int n=0;n<5;n++){
//			GameObject o = Instantiate(ammo, this.transform.position + 10*Vector3.right, Quaternion.identity) as GameObject;
//			o.transform.Rotate(Vector3.forward,(angle * (n-2)),Space.World);
//		}
		//End shotgun style shot
		
		//For the random cone:
		//GameObject o = Instantiate(ammo, this.transform.position + 10*Vector3.right, Quaternion.identity) as GameObject;
		//o.transform.Rotate(Vector3.forward,(Random.Range (-angle, angle)),Space.World);
		//End cone fire pattern
		
		for(int i=-splay; i<=splay; i++) {
			GameObject o = Instantiate(ammo, this.transform.position + 10*Vector3.right, Quaternion.identity) as GameObject;
			o.transform.Rotate(Vector3.forward,(float)i * angle,Space.World);
		}
		
		cooldown = cdtime;
	}
}

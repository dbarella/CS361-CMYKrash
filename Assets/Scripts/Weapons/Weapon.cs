using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour {
	
	public GameObject ammo;	//type of shot to instantiate
	public float cdtime;	//min time between shots
	private float cooldown;	//Internal counter for shot cooldown.
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void Update () {
		if(Input.GetButtonDown("Jump"))
			StandardShot();
		//Decriment cooldown;
		if(cooldown > 0)
			cooldown -= Time.deltaTime;
	}
	
	public void StandardShot(){
		//Instantiate a shot, pointing the same direction as the shooter.
		GameObject o = Instantiate(ammo, this.transform.position, Quaternion.identity) as GameObject;
		cooldown = cdtime;
	}
	
	public abstract void PowerShot();
	
}

using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour {
	
	public GameObject ammo;	//type of shot to instantiate
	public float cdtime;	//min time between shots
	protected float cooldown;	//Internal counter for shot cooldown.
	public string buttonPress;
	public bool isPowerShot = false;
	public AudioClip firingSound; //audio firing clip
	public float volume=0.5f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void Update () {
		if(Input.GetButton(buttonPress) && cooldown <= 0) {
//			Debug.Log("Firing a Weapon: " + buttonPress);
			StandardShot();
		}
		//Decriment cooldown;
		if(cooldown > 0)
			cooldown -= Time.deltaTime;
	}
	
	public void StandardShot(){
		//firing!
		audio.PlayOneShot(firingSound);
		//Instantiate a shot, pointing the same direction as the shooter.
		GameObject o = Instantiate(ammo, this.transform.position + 10*Vector3.right, Quaternion.identity) as GameObject;
		cooldown = cdtime;
	}
	
	public void SetPowerShot() {
		isPowerShot = true;
	}
	
	public float GetcdTime() {
		return cdtime;	
	}
	
	public float GetCooldown() {
		return cooldown;	
	}
	
	public abstract void PowerShot();
	
}

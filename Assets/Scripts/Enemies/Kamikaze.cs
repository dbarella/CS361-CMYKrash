using UnityEngine;
using System.Collections;

public class Kamikaze : Enemy {
	
	public float bombingSpeed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Move ();
	}
	
	void OnTriggerEnter(Collider col){
		float temp;
		if(col.tag == "Player"){
			Destroy (gameObject);
		}
		if(col.tag == "Ammo"){
			Debug.Log ("Carrier hit by ammo");
			temp = col.gameObject.GetComponent<Ammo>().GetDamage();
			TakeDamage(temp);
			if(health<=0){
				Destroy(gameObject);
			}
		}
	}
	
	public void TargetLocked(){
		this.laneChanging = false;
		speedMult = bombingSpeed ;
	}
}

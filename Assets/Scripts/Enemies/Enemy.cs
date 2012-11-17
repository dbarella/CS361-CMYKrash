using UnityEngine;
using System.Collections;

public abstract class Enemy : Spawnable {
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnTriggerEnter(Collider col){
		float temp;
		if(col.tag == "Player"){
			Destroy(gameObject);
		}
		if(col.tag == "Ammo"){
			temp = col.gameObject.GetComponent<Ammo>().GetDamage();
			TakeDamage(temp);
			if(health<=0){
				Destroy(gameObject);
			}
		}
	}
	
}

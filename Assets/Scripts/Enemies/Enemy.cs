using UnityEngine;
using System.Collections;

public abstract class Enemy : Spawnable {
	
	//GameObject explosionPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnTriggerEnter(Collider col){
		float temp;
		if(col.tag == "Player"){
			Die();
		}
		if(col.tag == "Ammo"){
			temp = col.gameObject.GetComponent<Ammo>().GetDamage();
			TakeDamage(temp);
//			Debug.Log(gameObject.name+" took damage. Health = "+health);
			if(health<=0){
				Die();
			}
		}
	}
	new public void Die() {
//		explosionPrefab = Instantiate(explosionPrefab, transform.position, transform.rotation) as GameObject;
		Destroy(gameObject);
	}
	
}

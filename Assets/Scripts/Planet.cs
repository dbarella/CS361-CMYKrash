using UnityEngine;
using System.Collections;

public class Planet : Spawnable {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Move();
	}
	
	void OnTriggerEnter(Collider col){
		float temp;
		if(col is Ship){
			Die ();
		}
		if(col is Spawnable){
			temp = col.gameObject.GetComponent<Spawnable>().GetDamage();
			TakeDamage(temp);
		}
	}
}

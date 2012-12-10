using UnityEngine;
using System.Collections;

public class EnemyBullet : Enemy {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//need to override this to not take damage
	public void OnTriggerEnter(Collider col){
		float temp;
		if(col.tag == "Player"){
			Die();
		}
		
	}
}

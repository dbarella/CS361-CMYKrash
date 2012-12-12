using UnityEngine;
using System.Collections;

public class EnemyBullet : Enemy {
	//note that this shouldn't be tagged enemy so ammo doesn't destroy itself on impact. it should instead be tagged bullet.
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Move ();	
	}
	//need to override this to not take damage
	public void OnTriggerEnter(Collider col){
		float temp;
		if(col.tag == "Player"){
			Die();
		}
		
	}
}

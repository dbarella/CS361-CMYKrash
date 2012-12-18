using UnityEngine;
using System.Collections;

public class Planet : Enemy {
	
	private float _scaleFactor = 0.1f;
	private Vector3 targetScale;
	private float lowerbound;
	private float totalhealth, totaldamage;
	public void Start() {
		Scale();
		lowerbound = targetScale.x/2;
		totalhealth = health;
		totaldamage = damage;
	}
	
	void FixedUpdate () {
		base.Move();
		float mult = health/totalhealth; 
		transform.localScale = new Vector3(targetScale.x*mult/2+lowerbound,targetScale.y*mult/2+lowerbound,targetScale.z*mult/2+lowerbound);
		damage = totaldamage * health/totalhealth;
	}
	
/*	void OnTriggerEnter(Collider col){
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
	}*/

	protected override void Scale(){
		float laneHeight = gm.GetLaneHeight();
		targetScale = new Vector3(laneHeight - _scaleFactor, laneHeight - _scaleFactor, laneHeight - _scaleFactor);
		transform.localScale = targetScale;
	}
	
	
}

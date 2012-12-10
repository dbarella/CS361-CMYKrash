using UnityEngine;
using System.Collections;

public class Planet : Enemy {
	
	private float _scaleFactor = 0.1f;
	private Vector3 targetScale;
	
	public void Start() {
		Scale();

	}
	
	void FixedUpdate () {
		base.Move();
		float mult = health/100;
		transform.localScale = new Vector3(targetScale.x*mult,targetScale.y*mult,targetScale.z*mult);
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

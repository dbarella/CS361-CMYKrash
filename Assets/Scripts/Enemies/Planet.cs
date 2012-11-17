using UnityEngine;
using System.Collections;

public class Planet : Enemy {
	
	private float _scaleFactor = 0.1f;
	
	public void Start() {
		//speedMult = 10;	
	}
	
	void FixedUpdate () {
		base.Move();
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
		Vector3 targetScale = new Vector3(laneHeight - _scaleFactor, laneHeight - _scaleFactor, laneHeight - _scaleFactor);
		transform.localScale = targetScale;
	}
	
	
}

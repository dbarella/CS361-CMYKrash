using UnityEngine;
using System.Collections;

public class Planet : Enemy {
	
	// Use this for initialization
	void Start () {
		//scales the planet object to lane height
		float lane = gm.GetLaneHeight();
		Vector3 scale = new Vector3(lane-.1f,lane-.1f,lane-.1f);
		transform.localScale = scale;
		
	}
	
	// Update is called once per frame
	void Update () {
		Move();
	}
	
	void OnCollissionEnter(Collider col){
		float temp;
		if(col.gameObject.tag=="Player"){
			Die();
		}
		if(col.gameObject.tag=="Spawnable"){
			temp = col.GetComponent<Spawnable>().GetDamage();
			TakeDamage(temp);
		}
	}
}

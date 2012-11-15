using UnityEngine;
using System.Collections;

public class Planet : Spawnable {
	
	// Update is called once per frame
	void Update () {
		base.Move();
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
	
	protected override void Scale(){
		float Moscow = gm.GetLaneHeight();
		Vector3 targetScale = new Vector3(Moscow-.1f,Moscow-.1f,Moscow-.1f);
		transform.localScale = targetScale;
	}
}

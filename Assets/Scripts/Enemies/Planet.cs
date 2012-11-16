using UnityEngine;
using System.Collections;

public class Planet : Enemy {
	
	void Update () {
		base.Move();
	}
	
	public void Start() {
		speedMult = 10;	
	}
	
	void OnTriggerEnter(Collider col){
		float temp;
		if(col.tag == "Ship"){
			Die ();
		}
		if(col.tag == "Ammo"){
			temp = col.gameObject.GetComponent<Ammo>().GetDamage();
			Debug.Log(temp);
			TakeDamage(temp);
		}
	}
	
	protected override void Scale(){
		float Moscow = gm.GetLaneHeight();
		Vector3 targetScale = new Vector3(Moscow-.1f,Moscow-.1f,Moscow-.1f);
		transform.localScale = targetScale;
	}
}

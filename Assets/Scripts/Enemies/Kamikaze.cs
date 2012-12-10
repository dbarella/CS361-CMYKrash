using UnityEngine;
using System.Collections;

public class Kamikaze : Enemy {
	
	public float bombingSpeed;
	private bool locked = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(health<=0){;
				this.Die ();
		}
		Move ();
	}
	
	new void OnTriggerEnter(Collider col){
		//float temp;
		Debug.Log ("collision in Radar");
		if(col.tag == "Player"){
			if(!locked){
				TargetLocked();
			}
		}
		if(col.tag == "Ammo"){
			Debug.Log ("incoming");
		}
	}
	
	public void TargetLocked(){
		locked = true;
		this.laneChanging = false;
		speedMult = 10f ;
		StartCoroutine(Lock());
	}
	
	IEnumerator Lock(){
		renderer.material.color = Color.red;
		yield return new WaitForSeconds(0.5f);
		speedMult=bombingSpeed;
	}
}

using UnityEngine;
using System.Collections;

public abstract class Spawnable : MonoBehaviour {
	protected float speedMult;
	protected float health;
	protected float damage;
	protected GameManagement gm;
	protected bool laneChanging;
	protected float target;
	
	void Awake(){
		gm = Camera.main.GetComponent<GameManagement>();
		laneChanging = false;
	}
	public void Move(){
		transform.Translate(gm.GetGameSpeed()*speedMult*Time.fixedDeltaTime*new Vector3(-1,0,0));
	}
	public void ChangeLane(int i){
		float sign = (float) i;
		
		if(!laneChanging){ 
			laneChanging = true;
			target = transform.position.y + gm.GetLaneHeight() * sign;
		}
		else if(sign*transform.position.y >= sign*target){
			laneChanging = false;
		}
		transform.Translate(gm.GetGameSpeed()*speedMult*Time.fixedDeltaTime*sign*new Vector3(0,1,0));
	}
	public void TakeDamage(float damage){
		health -= damage;
	}
	public float GetDamage(){
		return damage;
	}
	public void Die(){
		Destroy (gameObject);
	}
}

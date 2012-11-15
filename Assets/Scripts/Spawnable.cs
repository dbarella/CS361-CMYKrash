using UnityEngine;
using System.Collections;

public abstract class Spawnable : MonoBehaviour {
	protected float speedMult;
	protected float health;
	protected float damage;
	protected GameManagement gm;
	protected bool laneChanging;
	protected float target;
	void Start(){
		gm = Camera.main.GetComponent<GameManagement>();
		laneChanging = false;
	}
	public void Move(){
		//error for planet, object not set to a reference of an object
		transform.Translate(gm.GetGameSpeed()*speedMult*Time.fixedDeltaTime*Vector3.left);
	}
	public void ChangeLane(int i){
		if(!laneChanging){ 
			laneChanging = true;
			target = transform.position.y + gm.GetLaneHeight()*i;
		}
		else if(i*transform.position.y >= i*target){
			laneChanging = false;
		}
		transform.Translate(gm.GetGameSpeed()*speedMult*Time.fixedDeltaTime*i*new Vector3(0,1,0));
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

using UnityEngine;
using System.Collections;

public abstract class Spawnable : MonoBehaviour {
	public float speedMult;
	public float health;
	public float damage;
	protected GameManagement gm;
	protected bool laneChanging;
	protected float target;
	void Start(){
		gm = Camera.main.GetComponent<GameManagement>();
		laneChanging = false;
		Scale ();
	}
	
	protected virtual void Scale(){	
	}
	public void Move(){
		//error, object set to null reference of an object
		transform.Translate(gm.GetGameSpeed()*speedMult*Time.fixedDeltaTime*new Vector3(-1,0,0));
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

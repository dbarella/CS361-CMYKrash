using UnityEngine;
using System.Collections;

public abstract class Spawnable : MonoBehaviour {
	protected float speedMult;
	protected float health;
	protected float damage;
	protected GameManagement gm;
	protected bool laneChanging;
	protected float amtMoved;
	protected float laneHeight;
	protected int direction;
	void Awake(){
		gm = Camera.main.GetComponent<GameManagement>();
		laneChanging = false;
		laneHeight = gm.GetLaneHeight();
	}
	public void Move(){
		transform.Translate(gm.GetGameSpeed()*speedMult*Time.fixedDeltaTime*new Vector3(-1,0,0));
	}
	public void ChangeLane(int direction){
		if(!laneChanging){
			laneChanging = true;
			amtMoved = 0;
			this.direction = direction;
			
		}
		else if(amtMoved >= laneHeight){
			laneChanging = false;
		}
		else{
			Vector3 translateVector = speedMult*direction*Time.fixedDeltaTime*new Vector3(0,1,0);
			transform.Translate(translateVector);
			amtMoved += translateVector.y;
		}
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

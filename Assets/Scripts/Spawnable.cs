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
	
	void Update() {
		if(health<=0) Die();
	}
	
	protected virtual void Scale(){	
	}
	public void Move(){
		transform.Translate(gm.GetGameSpeed()*speedMult*Time.fixedDeltaTime*new Vector3(-1,0,0));
	}
	public void ChangeLane(int direction){
		//Debug.Log("Changing lane: " + direction + "\nlaneChanging: " + laneChanging);
		if(direction==0) return;
		if(!laneChanging){
			laneChanging = true;
			amtMoved = 0;
			this.direction = direction;
			
		}
		else if(amtMoved >= laneHeight){
			laneChanging = false;
			this.direction = 0;
		}
		else{
			Vector3 translateVector = new Vector3(0,speedMult*(float)direction*Time.deltaTime,0);
			if(amtMoved + Mathf.Abs(translateVector.y) > laneHeight){
				translateVector = (direction*Vector3.up*(Mathf.Abs(laneHeight-amtMoved)));
				laneChanging = false;
				this.direction = 0;
			}
			transform.Translate(translateVector);
			amtMoved += Mathf.Abs(translateVector.y);
		}
	}
	public void TakeDamage(float damage){
		Debug.Log("Taking damage " + damage);
		health -= damage;
	}
	public float GetDamage(){
		return damage;
	}
	public void Die(){
		Destroy (gameObject);
	}
}

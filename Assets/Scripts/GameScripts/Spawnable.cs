using UnityEngine;
using System.Collections;

public abstract class Spawnable : MonoBehaviour {
	public float zOffset;
	public float speedMult;
	public float health;
	public float damage;
	public GameManagement gm;
	protected bool laneChanging;
	protected float amtMoved;
	protected float laneHeight;
	protected int direction;
	public int score = 0;
	void Awake(){
		gm = Camera.main.GetComponent<GameManagement>();
		laneChanging = false;
		laneHeight = gm.GetLaneHeight();
		MoveOffset();
	}
	
	protected virtual void Scale(){	
	}
	public void Move(){
		//Debug.Log (this.gameObject.tag + " moving with a speedMult of " + speedMult);
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
		//else if(amtMoved >= laneHeight){
		//	laneChanging = false;
		//	this.direction = 0;
		//}
		else{
			Vector3 translateVector = new Vector3(0,speedMult*(float)direction*Time.fixedDeltaTime,0);
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
		health -= damage;
	}
	public float GetDamage(){
		return damage;
	}
	public void Die(){
		Destroy (gameObject);
	}
	
	private void MoveOffset(){
		transform.Translate(new Vector3(0,0,zOffset));
	}
	public void SendScore(){
		gm.IncrementScore(score);
	}
}

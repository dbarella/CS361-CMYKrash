using UnityEngine;
using System.Collections;

public class ShootingEnemy : Enemy {
	public bool forward, laneChange, backwards;
	public int travelDirection;
	public int lanesChanged, cycles;
	public float travelDistance;
	public GameObject bullet;
	private float initalPos;
	private int lanechangecounter;
	// Use this for initialization
	void Start(){	
		initalPos = transform.position.x;
		lanechangecounter = 0;
		forward = true;
		travelDistance = Camera.mainCamera.GetComponent<GameManagement>().GetScreenWidth()/4;
	}
	void Update(){
		Move ();
	}
	public void Move () {
		if(forward){
			//Debug.Log("forward");
			base.Move();
			if(transform.position.x <= initalPos - travelDistance){
				forward = false;
				laneChange = true;
			}
		}
		else if(laneChange){
			Debug.Log ("change lane");

			if(!laneChanging){
				Shoot();
				lanechangecounter++;
				if(lanechangecounter>=lanesChanged) {
					laneChange = false;
					backwards = true;
				}

			}
			ChangeLane(travelDirection);

		}
		else if(backwards){
			Debug.Log("retreat");
			transform.Translate(gm.GetGameSpeed()*speedMult*Time.fixedDeltaTime*new Vector3(1,0,0));
			if(transform.position.x>=initalPos){
				cycles--;
				forward = true;
				backwards = false;
				lanechangecounter = 0;
				if(cycles <= 0) Die ();
			}
		}
	}
	public void Shoot(){
		GameObject o = Instantiate(bullet, transform.position +10*Vector3.left, Quaternion.identity) as GameObject;

	}
}

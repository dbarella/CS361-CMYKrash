using UnityEngine;
using System.Collections;

public class ShootingEnemy : Enemy {
	private bool forward, laneChange, backwards;
	public int travelDirection;
	public int lanesChanged;
	public float travelDistance;
	public GameObject bullet;
	// Use this for initialization
	void Start(){	
		forward = true;
		travelDistance = Camera.mainCamera.GetComponent<GameManagement>().GetScreenWidth()/4;
	}
	public void Move () {
		if(forward){
			base.Move();
		}
		else if(laneChange&& !laneChanging){
			lanesChanged --;
			ChangeLane(travelDirection);
			Shoot();
			if(lanesChanged <= 0) {
				laneChange = false;
				backwards = true;
			}
		}
		else{
			transform.Translate(gm.GetGameSpeed()*speedMult*Time.fixedDeltaTime*new Vector3(1,0,0));

		}
	}
	public void Shoot(){
		GameObject o = Instantiate(bullet, transform.position + 10*Vector3.right, Quaternion.identity) as GameObject;

	}
}

using UnityEngine;
using System.Collections;

public class Artillery : Enemy {
	
	public  float setupPoint;	//given by the percentage offset from the left margin - 100 causes these to never move and begin firing immediately.
	public GameObject reticulePrefab;	//The "target", which spawns its own explosion after a timer.
	public float firingInterval;	//The time between 
	private float fireTimer;	//Internal cooldown timer on firing

	// Use this for initialization
	void Start () {
	
	}
	
	// Move forward until a certain point, then begin launching attacks.
	void FixedUpdate () {
		//If we're not at our "set-up" point, move!
		if((Camera.main.ViewportToWorldPoint(new Vector3(setupPoint,0, 0))).x < transform.position.x)
			Move();
		
		else if (fireTimer <= 0){
				//Get the vector we want to aim at
				int ship = Random.Range(0,3);
				if (gm.GetShipsArray()[ship]!= null)
				{
				
					Vector3 tar = (gm.GetShipsArray())[ship].transform.position;
					//put a reticule object there - It will detonate after a while.
					Instantiate(reticulePrefab, tar, Quaternion.identity);
					fireTimer = firingInterval;
			}
		}
		if(fireTimer > 0)
			fireTimer -= Time.fixedDeltaTime;	
	}
}

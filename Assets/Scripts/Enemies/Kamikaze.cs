using UnityEngine;
using System.Collections;

public class Kamikaze : Enemy {

	//This should generally be greater than 100 to be effective.
	public float range;
	public float bombingSpeed;
	
	private GameObject[] ships;
	private bool locked;
	private bool adjustingSpeed;
	private float _threshold = 0.2f;
	
	void Start () {
		ships = GameObject.FindGameObjectsWithTag("Player");
		locked = false;
	}
	
	void FixedUpdate () {
		if(!adjustingSpeed)	Move ();
		if(!locked) CheckLocations();
	}
	
	private void CheckLocations() {
		foreach(GameObject ship in ships) {
			if(ship) { //Avoid null pointers
				Transform t = ship.transform;
				
				if(CompareYPosition(t) && CompareXPosition(t)) { //Same lane and within range
					TargetLocked();
				}
			}
		}
	}
	
	public void TargetLocked(){
		locked = true;
		StartCoroutine(Lock());
	}
	
	IEnumerator Lock(){
		//Change color to red
		renderer.material.color = Color.red;
		
		//Freeze movement for a short time
		adjustingSpeed = true;
		yield return new WaitForSeconds(0.5f);
		speedMult=bombingSpeed;
		adjustingSpeed = false;
	}
	
	/**
	 * Return true if t is within _threshold amount of this GO's y coordinate
	 **/
	private bool CompareYPosition(Transform t) {
		return (Mathf.Abs(t.position.y-transform.position.y) <= _threshold);
	}
	
	/**
	 * Return true if t is within range amount of this GO's x coordinate
	 **/
	private bool CompareXPosition(Transform t) {
		return (Mathf.Abs(t.position.x-transform.position.x) <= range);
	}
}

using UnityEngine;
using System.Collections;

public class EmptyOffset : Spawnable {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		this.Die();
	}
}

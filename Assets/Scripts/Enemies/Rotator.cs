using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	private Vector3 rot;
	public float rotSpeed;

	// Use this for initialization
	void Start () {
		rot = new Vector3(0,rotSpeed*Time.deltaTime,0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate(rot);
	}
}

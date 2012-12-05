using UnityEngine;
using System.Collections;

public class Radar : MonoBehaviour {
	// Use this for initialization
	void Start () {
		//Debug.Log (transform.parent.name);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider col){
		if(col.tag == "Player"){
			transform.parent.GetComponent<Kamikaze>().TargetLocked();
		}
	}
}

using UnityEngine;
using System.Collections;

public class Wormhole : MonoBehaviour {
	
	//need to set this to the exit wormhole
	public GameObject exit;
	void OnTriggerEnter(Collider other){
		if(other.tag == "Ship"){
			other.gameObject.transform.position = exit.transform.position;
		}
	}
}

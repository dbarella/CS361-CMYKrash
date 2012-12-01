using UnityEngine;
using System.Collections;

public class Falloff : MonoBehaviour{
	private float leftbound;
	private float rightbound;

	void Start(){
		leftbound = -Camera.main.orthographicSize*Camera.main.aspect - 1;
		rightbound = Camera.main.orthographicSize*Camera.main.aspect + 20;	
	}

	void Update(){
		if(gameObject.renderer) {
			if(renderer.bounds.max.x < leftbound || renderer.bounds.min.x > rightbound) {
				Destroy(gameObject);
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class FallOff : MonoBehaviour {
	protected GameManagement m;
	private float leftbound;
	void Start(){
		m = Camera.main.GetComponent<GameManagement>();
		leftbound = -Camera.main.orthographicSize*Camera.main.aspect;
	}
	// Update is called once per frame
	void Update () {
		
		if(renderer.bounds.max.x <leftbound){
			//m.ObjectDead();	
			Destroy(gameObject);
		}
	}
	
}

using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {
	
	public float lifetime = 2.0f;
	private float _timer;
	
	// Use this for initialization
	void Start () {
		_timer = lifetime;
	}
	
	// Update is called once per frame
	void Update () {
		_timer -= Time.deltaTime;
		if(_timer < 0) {
			Destroy(this.gameObject);
		}
	}
}

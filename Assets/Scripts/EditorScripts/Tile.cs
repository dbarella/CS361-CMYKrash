using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	int obj;
	Material[] ml;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void SetMaterial(Material[] m){
		ml = m;
	}
	public void SetObj(int i){
		obj = i;
		gameObject.renderer.material = ml[i];
	}
	
	public int GetObj(){
		return obj;	
	}

	public void Die(){
		Destroy (gameObject);
	}
}

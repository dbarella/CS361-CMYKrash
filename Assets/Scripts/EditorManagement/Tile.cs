using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	int obj;
	Material[] ml;
	// Use this for initialization
	void Start () {
//		ml = Camera.main.GetComponent<EditorManagement>().GetMaterialList();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetObj(int i){
		obj = i;
		gameObject.renderer.material = ml[i];
	}
	
	public int GetObj(){
		return obj;	
	}
}

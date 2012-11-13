using UnityEngine;
using System.Collections;

//Last modified by: Brendan, Thursday 11/8 @ 7 PM

public class itr1_GameManagement : MonoBehaviour {
	
	//distance the player is spawned from the left of the screen
	public float leftBuff = 10;
	//height of the lanes
	public float laneHeight;
	public int howManyLanes = 5;
	//score
	private int points;
	//height and width
	public float sHeight;
	public float sWidth;
	//game object we instantiate
	public GameObject s1;
	public GameObject s2;
	public GameObject s3;
	public GameObject spawner;
	
	// 5 Lanes, 3 Ships, 1 Awesome Game
	
	private float lane1;
	private float lane2;
	private float lane3;
	private float lane4;
	private float lane5;
	
	//ship formation lanes
	public int ship1=0;
	public int ship2=0;
	public int ship3=0;
	

	
	//Temporary code for Update!
	void Update () {
		if(Input.GetKeyDown("r"))
			Application.LoadLevel("FullProject");
		}
	void Start () {}
	
	// Use this for initialization
	void Awake () {
		//Instantiates the camera s.t. the bottom right corner of the camera is at world Vector (0,0)
		Vector3 lwrLft = camera.ScreenToWorldPoint (new Vector3 (0,0,camera.nearClipPlane));
		transform.Translate(new Vector3(-lwrLft.x,-lwrLft.y,-10));
		//height and width in unity units
		Vector3 hnw = camera.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,camera.nearClipPlane));
		sHeight = hnw.y;
		sWidth = hnw.x;
		//determining lane height
		laneHeight = sHeight/howManyLanes;
		//set up the lanes
		lane1 = 0+laneHeight/2;
		lane2 = lane1+laneHeight;
		lane3 = lane2+laneHeight;
		lane4 = lane3+laneHeight;
		lane5 = lane4+laneHeight;
		
		int rando = Random.Range(1,6);
		ship1 =rando;
		while(rando==ship1){
			rando = Random.Range(1,6);
		}
		ship2 = rando;
		while(rando == ship1 || rando == ship2){
			rando = Random.Range(1,6);
		}
		ship3=rando;
		
		Debug.Log(ship1 + " " + ship2 + " " + ship3);
		
		//sets up the level
		ResetLevel();
	}
	
	public void ResetLevel(){
		
		for(int i = 1; i<howManyLanes;i++){
			GameObject lane = GameObject.CreatePrimitive(PrimitiveType.Cube);
			lane.transform.localScale = new Vector3(Screen.width,1,1);
			//Lanes are instantiated at z = -5 to keep them out of play, purely visual
			lane.transform.position = new Vector3(sWidth/2, laneHeight*i,10);	
		}
		Debug.Log("Lanes initialized.");
		//player start position
		//We'll start the player in the middle lane
		for(int i = 1; i<6;i++){
			Debug.Log("Ship spawner is Looping?");
			
				if(ship1==i) {
					Debug.Log("Ship to be initialized in Lane: " + i);
					createShip(i, 1);	
				}
				if(ship2==i) {
					Debug.Log("Ship to be initialized in Lane: " + i);
					createShip(i, 2);		
				}
				if(ship3==i) {
					Debug.Log("Ship to be initialized in Lane: " + i);
					createShip(i, 3);		
				}
				
		
		}
		//Instantiate Enemy Spawners at the end of each lane
		Instantiate(spawner,new Vector3(sWidth+2,lane1,0),Quaternion.identity);
		Instantiate(spawner,new Vector3(sWidth+2,lane2,0),Quaternion.identity);
		Instantiate(spawner,new Vector3(sWidth+2,lane3,0),Quaternion.identity);
		Instantiate(spawner,new Vector3(sWidth+2,lane4,0),Quaternion.identity);
		Instantiate(spawner,new Vector3(sWidth+2,lane5,0),Quaternion.identity);
	}
	
	public void createShip(int i, int s) {
		if(i==1){
			if(s==1){
				s1 = Instantiate(s1,new Vector3(leftBuff,lane1,0),Quaternion.identity) as GameObject;
				s1.GetComponent<Ship>().setNo(1);
			}else if(s==2){
				s2 = Instantiate(s2,new Vector3(leftBuff,lane1,0),Quaternion.identity) as GameObject;
				s2.GetComponent<Ship>().setNo(2);
			}else if(s==3){
				s3 = Instantiate(s3,new Vector3(leftBuff,lane1,0),Quaternion.identity) as GameObject;
				s3.GetComponent<Ship>().setNo(3);
			}else{
				Debug.Log("Error: s > 3");	
			}
		}
		if(i==2){
			if(s==1){
				s1 = Instantiate(s1,new Vector3(leftBuff,lane2,0),Quaternion.identity) as GameObject;
				s1.GetComponent<Ship>().setNo(1);
			}else if(s==2){
				s2 = Instantiate(s2,new Vector3(leftBuff,lane2,0),Quaternion.identity) as GameObject;
				s2.GetComponent<Ship>().setNo(2);
			}else if(s==3){
				s3 = Instantiate(s3,new Vector3(leftBuff,lane2,0),Quaternion.identity) as GameObject;
				s3.GetComponent<Ship>().setNo(3);
			}else{
				Debug.Log("Error: s > 3");	
			}
		}
		if(i==3){
			if(s==1){
				s1 = Instantiate(s1,new Vector3(leftBuff,lane3,0),Quaternion.identity) as GameObject;
				s1.GetComponent<Ship>().setNo(1);
			}else if(s==2){
				s2 = Instantiate(s2,new Vector3(leftBuff,lane3,0),Quaternion.identity) as GameObject;
				s2.GetComponent<Ship>().setNo(2);
			}else if(s==3){
				s3 = Instantiate(s3,new Vector3(leftBuff,lane3,0),Quaternion.identity) as GameObject;
				s3.GetComponent<Ship>().setNo(3);
			}else{
				Debug.Log("Error: s > 3");	
			}
		}
		if(i==4){
			if(s==1){
				s1 = Instantiate(s1,new Vector3(leftBuff,lane4,0),Quaternion.identity) as GameObject;
				s1.GetComponent<Ship>().setNo(1);
			}else if(s==2){
				s2 = Instantiate(s2,new Vector3(leftBuff,lane4,0),Quaternion.identity) as GameObject;
				s2.GetComponent<Ship>().setNo(2);
			}else if(s==3){
				s3 = Instantiate(s3,new Vector3(leftBuff,lane4,0),Quaternion.identity) as GameObject;
				s3.GetComponent<Ship>().setNo(3);
			}else{
				Debug.Log("Error: s > 3");	
			}
		}
		if(i==5){
			if(s==1){
				s1 = Instantiate(s1,new Vector3(leftBuff,lane5,0),Quaternion.identity) as GameObject;
				s1.GetComponent<Ship>().setNo(1);
			}else if(s==2){
				s2 = Instantiate(s2,new Vector3(leftBuff,lane5,0),Quaternion.identity) as GameObject;
				s2.GetComponent<Ship>().setNo(2);
			}else if(s==3){
				s3 = Instantiate(s3,new Vector3(leftBuff,lane5,0),Quaternion.identity) as GameObject;
				s3.GetComponent<Ship>().setNo(3);
			}else{
				Debug.Log("Error: s > 3");	
			}
		}
	}
	
	public void PlayerHit(int damage, int i){
		//damage taken from impact with object
		if(i==1){
			s1.GetComponent<Ship>().TakeDamage(damage);
		}
		if(i==2){
			s2.GetComponent<Ship>().TakeDamage(damage);
		}
		if(i==3){
			s3.GetComponent<Ship>().TakeDamage(damage);
		}
			
	}
	
	//for Brendan's HUD
	public GameObject GetShip(int i){
    	if(i==1){
    		return s1;	
    	}
    	else if(i==2){
    		return s2;	
    	}
    	else if(i==3){
    		return s3;	
    	}else{
    		Debug.Log("Error: Attempted to get Ship outside range.");
    		return null;	
    	}
	}
	
	public void PlayerDead(int i){
		Debug.Log ("Player is Dead");
		if(i == 1) {
			Destroy(s1);
		}
		if(i == 2) {
			Destroy(s2);
		}
		if(i == 3){
			Destroy(s3);
		}
	}
	
	public void ObjectDead(){
		points++;
	}
	
	public int GetPoints(){
		return points;
	}
	
	IEnumerator Wait(int i){
		yield return new WaitForSeconds(i);
	}
}

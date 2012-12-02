using UnityEngine;
using System.Collections;

public class CyanShipGUI : MonoBehaviour {
	
	//this, and all of ther ShipGUI scripts, work best with a MainCamera that has a Normalized View Port Rect set to Y=0.1
	
	protected float curHealth;
	protected float maxHealth;
	
	protected float maxCool;
	protected float curCool;
	
	protected Weapon weapon;
	protected Ship ship;
	
	protected GUIStyle style;
    protected Texture2D texture;
	protected Texture2D texture2;

	// Use this for initialization
	void Start () {
		texture = new Texture2D(128, 128,TextureFormat.RGB24,false);
		style = new GUIStyle();
		texture2 = new Texture2D(128, 128,TextureFormat.RGB24,false);
		style = new GUIStyle();
		
		weapon = GetComponent<Weapon>();
		
		ship = GetComponent<Ship>();
		maxHealth = ship.GetHealth();
		curHealth = maxHealth;
		maxCool = weapon.GetcdTime();
		curCool = maxCool;
		for (int y = 0; y < texture.height; ++y)
        {
            for (int x = 0; x < texture.width; ++x)
            {
                Color color = new Color(0, 255, 255);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
		
		for (int y = 0; y < texture2.height; ++y)
        {
            for (int x = 0; x < texture2.width; ++x)
            {
                Color color = new Color(255, 0, 0);
                texture2.SetPixel(x, y, color);
            }
        }
        texture2.Apply();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		curCool = weapon.GetCooldown();
		curHealth = ship.GetHealth();
	}
	
	void OnGUI(){
		
		
		Vector3 shipPos = new Vector3(ship.gameObject.transform.position.x,ship.gameObject.transform.position.y,ship.gameObject.transform.position.z);
		Vector2 shipScreenPos = new Vector2(Camera.main.WorldToScreenPoint(shipPos).x,Camera.main.WorldToScreenPoint(shipPos).y);
		Debug.Log (shipScreenPos);

        style.normal.background = texture;
        GUI.Box(new Rect(Screen.width - Screen.width/1.7f, (Screen.height-Screen.height/13.5f), (Screen.height/18*curHealth), Screen.height/20), "", style);
		style.normal.background = texture2;
		GUI.Box (new Rect(Screen.width - Screen.width/2.6f, (Screen.height-Screen.height/13.5f), (Screen.height/20*curCool), Screen.height/20), "", style);
	}
}

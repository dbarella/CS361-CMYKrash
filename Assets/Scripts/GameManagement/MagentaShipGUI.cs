using UnityEngine;
using System.Collections;

public class MagentaShipGUI : MonoBehaviour {

	protected float curHealth;
	protected float maxHealth;
	
	protected Ship ship;
	
	protected GUIStyle style;
    protected Texture2D texture;

	// Use this for initialization
	void Start () {
		texture = new Texture2D(128, 128,TextureFormat.RGB24,false);
		style = new GUIStyle();
		
		ship = GetComponent<Ship>();
		maxHealth = ship.GetHealth();
		curHealth = maxHealth;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		curHealth = ship.GetHealth();
	}
	
	void OnGUI(){
		for (int y = 0; y < texture.height; ++y)
        {
            for (int x = 0; x < texture.width; ++x)
            {
                Color color = new Color(238, 0, 238);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();

        style.normal.background = texture;
		//scale all of these to Screen height and Width
        GUI.Box(new Rect(Screen.width - Screen.width/5f, (Screen.height-Screen.height/13.5f), (Screen.height/18*curHealth), Screen.height/20), "", style);
		
	}
}

using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {
	
	/* Ship Textures */
	public Texture2D choIcon;
	public Texture2D milIcon;
	public Texture2D yamIcon;
	
	/* Enemy Textures */
	public Texture2D misIcon;
	public Texture2D plaIcon;
	public Texture2D carIcon;
	public Texture2D artIcon;
	public Texture2D shoIcon;
	public Texture2D kamIcon;
	public Texture2D shiIcon;
	
	public Texture2D samplePlay;

	
	
	void OnGUI() {
		/* Meet the Ships Section */
		GUI.Label (new Rect(120,10,100,40), "Meet the Ships!");
		GUI.Button (new Rect (120,50,100,60),new GUIContent ("Cho", choIcon, "Cho has a big cannon!  Shots travel a short distance, then explode, dealing splash damage!"));
		GUI.Button (new Rect (230,50,100,60), new GUIContent ("Millie", milIcon, "Millie has a machine gun!  It's weak, but fires rapidly!"));
		GUI.Button (new Rect (340,50,100,60), new GUIContent ("YAM", yamIcon, "YAM fires the mighty YAM!  Fire to hit all enemies in a lane.  The YAM gets more powerful the longer you let it cool down!"));
		GUI.Label (new Rect (230,120,100,160), GUI.tooltip);
		
		/* How to Play Section */
		GUI.Label (new Rect(120,300,100,40), "How To Play ");
		GUI.Label (new Rect(10,350,100,300), "Controls: \n WASD: Move Around \n J: Fire YAM \n K: Fire Millie \n L: Fire Cho \n Space: Shift Formation.");
		GUI.DrawTexture (new Rect(150,350,256,256), samplePlay, ScaleMode.ScaleToFit, true, 0.0f);
		GUI.Label (new Rect(420, 420, 100, 160), "Destroy the enemies that fly at you!  Survive to the end of the level to win.  Kill as many enemies as possible to get a high score!");
		
		
		/* Meet the Enemies Section */
		GUI.Label (new Rect(785,10,120,40), "Meet the Enemies!");
		GUI.Box (new Rect(710, 50, 100, 60), misIcon);
		GUI.Label (new Rect (820, 50, 160, 60), "Missiles are made out of piss and vinegar.  Shoot or avoid.");
		GUI.Box (new Rect(710, 120, 100, 60), plaIcon);
		GUI.Label (new Rect (820, 120, 160, 60), "Planets travel slowly but hit like trucks. Massive space trucks.");
		GUI.Box (new Rect(710, 190, 100, 60), carIcon);
		GUI.Label (new Rect (820, 190, 160, 60), "Carriers spawn missiles.  Kill them fast!");
		GUI.Box (new Rect(710, 260, 100, 60), artIcon);
		GUI.Label (new Rect (820, 260, 160, 60), "Beware the red dot!  Artillery strikes deal splash damage.");
		GUI.Box (new Rect(710, 330, 100, 60), shoIcon);
		GUI.Label (new Rect (820, 330, 160, 60), "Not all enemies just fly at you.  Some shoot back.");
		GUI.Box (new Rect(710, 400, 100, 60), kamIcon);
		GUI.Label (new Rect (820, 400, 180, 60), "The only encounters with these have ended in screams of terror, then silence.");
		GUI.Box (new Rect(710, 470, 100, 60), shiIcon);
		GUI.Label (new Rect (820, 470, 160, 70), "These enemies actually give you a shield when you run into them.  Crappy enemies, but very helpful!.");
	
		/* Kicks you back to Menu */
		if(GUI.Button (new Rect((Screen.width - 100),(Screen.height - 50),100,50), "Back to Menu")) {
			Application.LoadLevel ("Menu");
		}
	}
}

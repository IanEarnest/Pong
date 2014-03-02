using UnityEngine;
using System.Collections;

public class MainGUIScript : MonoBehaviour {
	
	public static int scoreToWin = 2;
	public static string difficulty = "Easy";
	int difficultySlider;
	public static string[] difficultyList = {"Easy", "Medium", "Hard"};

	public static GUISkin globalGUISkin;
	public GUISkin myGUISkin;

	bool main = true, options, about;

	Rect menuRect = new Rect(Screen.width/2 - 50, Screen.height/2 - 73, 100, 146);
	Rect optionsRect = new Rect(Screen.width/2 - 110, Screen.height/2 - 50, 220, 100);
	Rect aboutRect = new Rect(Screen.width/2 - 100, Screen.height/2 - 35, 200, 71);

	void Start(){
		// Set the difficultySlider so that the difficulty
		if(difficulty == difficultyList[0]){
			difficultySlider = 0;
		}
		if(difficulty == difficultyList[1]){
			difficultySlider = 1;
		}
		if(difficulty == difficultyList[2]){
			difficultySlider = 2;
		}

		globalGUISkin = myGUISkin;
	}

	void OnGUI(){
		GUI.skin = globalGUISkin;

		// Main menu
		if(main == true){
			GUI.Box(menuRect, "Menu");
			GUILayout.BeginArea(menuRect);
			GUILayout.Space (25f);
				if(GUILayout.Button("Play")){
					Application.LoadLevel("Level Scene");
				}
				if(GUILayout.Button("Scores")){
					Application.LoadLevel("Score Scene");
				}
				if(GUILayout.Button("Options")){
					main = false;
					if(options == true){
						options = false;
					}
					else{
						options = true;
					}
				}
				if(GUILayout.Button("About")){
					main = false;
					if(about == true){
						about = false;
					}
					else{
						about = true;
					}
				}
			GUILayout.EndArea();
		}

		if(options == true){
			GUI.Box(optionsRect, "Options");
			GUILayout.BeginArea(optionsRect);
				GUILayout.Space (25f);
				GUILayout.BeginHorizontal();
					GUILayout.Label("Difficulty: ", GUILayout.Width(60));
					GUILayout.Box(difficulty, GUILayout.Width(60));
					difficultySlider = (int)GUILayout.HorizontalSlider(difficultySlider, 0, difficultyList.Length-1, GUILayout.Width(90));
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
					GUILayout.Label("Score to win: ", GUILayout.Width(80));
					GUILayout.Box(""+scoreToWin, GUILayout.Width(40));
					scoreToWin = (int)GUILayout.HorizontalSlider(scoreToWin, 1, 11, GUILayout.Width(90));
				GUILayout.EndHorizontal();
				if(GUILayout.Button("Close")){
					main = true;
					options = false;
				}
			GUILayout.EndArea();
		}

		if(about == true){
			GUI.Box(aboutRect, "About");
			GUILayout.BeginArea(aboutRect);
				GUILayout.Space (25f);
				GUILayout.Label("Creator: IanEarnest");
				if(GUILayout.Button("Close")){
					main = true;
					about = false;
				}
			GUILayout.EndArea();
		}

		// Set difficulty to slider set value.
		difficulty = difficultyList[difficultySlider];
	}
}

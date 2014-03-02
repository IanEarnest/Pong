using UnityEngine;
using System.Collections;

public class ScoreGUIScript : MonoBehaviour {

	int hardScore;
	int mediumScore;
	int easyScore;

	Rect menuRect = new Rect(Screen.width/2 - 50, Screen.height/2 - 100, 100, 76);
	Rect scoresRect = new Rect(Screen.width/2 - 100, Screen.height/2, 200, 125);

	bool resetConfirmButton;
	GUIStyle numbersStyle;

	void Start(){
		hardScore = PlayerPrefs.GetInt(MainGUIScript.difficultyList[2]);
		mediumScore = PlayerPrefs.GetInt(MainGUIScript.difficultyList[1]);
		easyScore = PlayerPrefs.GetInt(MainGUIScript.difficultyList[0]);
	}

	void OnGUI(){
		GUI.skin = MainGUIScript.globalGUISkin;
		numbersStyle = new GUIStyle(GUI.skin.GetStyle("Label"));
		numbersStyle.alignment = TextAnchor.UpperRight;

		scoresRect = new Rect(Screen.width/2 - 100, Screen.height/2, 200, 155);
		// Menu
		GUI.Box(menuRect, "Menu");
		GUILayout.BeginArea(menuRect);
		GUILayout.Space (30f);
		if(GUILayout.Button("Play")){
			Application.LoadLevel("Level Scene");
		}
		if(GUILayout.Button("Main menu")){
			Application.LoadLevel("Menu Scene");
		}
		GUILayout.EndArea();


		// Scores
		GUI.Box(scoresRect, "Scores");
		GUILayout.BeginArea(scoresRect);
			GUILayout.Space (25f);
			GUILayout.BeginHorizontal();
				GUILayout.Label("Highest Scores", GUILayout.Width(120));
				GUILayout.Label("Difficulty", GUILayout.Width(120));
			GUILayout.EndHorizontal();
			// Hard score
			GUILayout.BeginHorizontal();
				GUILayout.Label(""+hardScore, numbersStyle, GUILayout.Width(76));
				GUILayout.Label("", GUILayout.Width(40));
				GUILayout.Label("Hard", GUILayout.Width(120));
			GUILayout.EndHorizontal();
			// Medium score
			GUILayout.BeginHorizontal();
				GUILayout.Label(""+mediumScore, numbersStyle, GUILayout.Width(76));
				GUILayout.Label("", GUILayout.Width(40));
				GUILayout.Label("Medium", GUILayout.Width(120));
			GUILayout.EndHorizontal();
			// Easy score
			GUILayout.BeginHorizontal();
				GUILayout.Label(""+easyScore, numbersStyle, GUILayout.Width(76));
				GUILayout.Label("", GUILayout.Width(40));
				GUILayout.Label("Easy", GUILayout.Width(120));
			GUILayout.EndHorizontal();
			
			if(GUILayout.Button("Reset scores")){
				if(resetConfirmButton == true){
					resetConfirmButton = false;
				}
				else{
					resetConfirmButton = true;
				}
			}
		GUILayout.EndArea();


		if(resetConfirmButton == true){
		Rect resetConfirmRect = new Rect(scoresRect.x + 205, scoresRect.y + 85, 120, 71);
			GUI.Box(resetConfirmRect, "Confirm");
			GUILayout.BeginArea(resetConfirmRect);
				GUILayout.Space (25f);
				if(GUILayout.Button("Yes (delete all)")){
					PlayerPrefs.DeleteAll();
					resetConfirmButton = false;
					hardScore = 0;
					mediumScore = 0;
					easyScore = 0;
				}
				if(GUILayout.Button("No (don't delete)")){
					resetConfirmButton = false;
				}
			GUILayout.EndArea();
		}
	}
}

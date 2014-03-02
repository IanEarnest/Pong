using UnityEngine;
using System.Collections;

public class GameGUIScript : MonoBehaviour {
	
	public TextMesh pScore;
	public TextMesh eScore;
	public bool isPlayerWinner;
	public TextMesh winnerText;
	public static bool end = false;

	//private string[] strings = {"Main Menu", "Replay", "Scores" ,"Quit"};

	Rect menuRect = new Rect(Screen.width/2 - 50, Screen.height/2 - 73, 120, 146);

	void Start(){
	}

	void Update(){		
		// Player wins.
		if(BallScript.playerScore >= MainGUIScript.scoreToWin){
			winnerText.text = "Player wins with " + BallScript.playerScore + " points!";
			BallScript.ball.transform.position = new Vector3(0,0,0);
			isPlayerWinner = true;
			end = true;
		}
		// Enemy wins.
		if(BallScript.enemyScore >= MainGUIScript.scoreToWin){
			winnerText.text = "Enemy wins with " + BallScript.enemyScore + " points!";
			BallScript.ball.transform.position = new Vector3(0,0,0);
			isPlayerWinner = false;
			end = true;
		}
	}

	void OnGUI(){
		GUI.skin = MainGUIScript.globalGUISkin;

		pScore.text = "" + BallScript.playerScore;
		eScore.text = "" + BallScript.enemyScore;

		// Back to go to menu.
		if(GUILayout.Button("Main Menu")){
			reset();
			Application.LoadLevel("Menu Scene");
		}

		if(end == true){
			GUI.Box(menuRect, "Game Over Menu");
			GUILayout.BeginArea(menuRect);
				GUILayout.Space (25f);
				if(GUILayout.Button("Main Menu")){
					reset();
					Application.LoadLevel("Menu Scene");
				}
				if(GUILayout.Button("Replay")){
					reset();
				}
				if(GUILayout.Button("Scores")){
					reset();
					Application.LoadLevel("Score Scene");
				}
			GUILayout.EndArea();
		}
	}

	void reset(){
		// Update high scores.
		if(isPlayerWinner == true){
			// Might need to check if playerprefs keys 
			// exist before checking what they contain.
			if(BallScript.playerScore > PlayerPrefs.GetInt(MainGUIScript.difficulty)){
				PlayerPrefs.SetInt(MainGUIScript.difficulty, BallScript.playerScore);
				PlayerPrefs.Save();
			}
		}

		winnerText.text = "";
		end = false;
		BallScript.playerScore = 0;
		BallScript.enemyScore = 0;
	}
}

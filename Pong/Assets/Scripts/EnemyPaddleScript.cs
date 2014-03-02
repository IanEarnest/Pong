using UnityEngine;
using System.Collections;

public class EnemyPaddleScript : MonoBehaviour {

	static public int enemySpeed = 15;
	
	void Update () {
		// Enemy movement.
		if(BallScript.ball.transform.position.y > transform.position.y){
			transform.Translate(0, (float)enemySpeed * Time.deltaTime, 0);
		}
		if(BallScript.ball.transform.position.y < transform.position.y){
			transform.Translate(0, -(float)enemySpeed * Time.deltaTime, 0);
		}

		// Restrict movement.
		if(transform.position.y > 14){
			//transform.position.y = 14;
			Vector3 pos = transform.position;
			pos.y = 14;
			transform.position = pos;
		}
		if(transform.position.y < -12){
			Vector3 pos = transform.position;
			pos.y = -12;
			transform.position = pos;
		}


		// Get ball unstuck.
		if(BallScript.ball.transform.position.x > transform.position.x){
			print ("ball stuck");
		}

		// Difficulty Easy.
		if(MainGUIScript.difficulty == MainGUIScript.difficultyList[0]){
			enemySpeed = 15;
		}
		// Difficulty Medium.
		if(MainGUIScript.difficulty == MainGUIScript.difficultyList[1]){
			enemySpeed = 25;
		}
		// Difficulty Medium.
		if(MainGUIScript.difficulty == MainGUIScript.difficultyList[2]){
			enemySpeed = 45;
		}
	}
}

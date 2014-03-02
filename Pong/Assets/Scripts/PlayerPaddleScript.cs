using UnityEngine;
using System.Collections;

public class PlayerPaddleScript : MonoBehaviour {

	static public int playerSpeed = 20;

	void Update () {
		if(GameGUIScript.end != true){
			// Up and down movement.
			transform.Translate(0,playerSpeed * Time.deltaTime * Input.GetAxis("Vertical"),0);
			// Mouse movement
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray)){
				Vector3 _newVector = transform.position;
				_newVector.y = ray.origin.y;
				transform.position = _newVector;
			}
			/*
			if (Physics.Raycast(ray)){
				if(ray.origin.y - transform.position.y > 1){
					transform.Translate(0, playerSpeed * Time.deltaTime, 0);
				}
				else if(ray.origin.y - transform.position.y < -1){
					transform.Translate(0, -playerSpeed * Time.deltaTime, 0);
				}
			}
			*/
		}
		
		// Restrict movement.
		if(transform.position.y > 14){
			Vector3 tempPos = transform.position;
			tempPos.y = 14;
			transform.position = tempPos;
		}
		if(transform.position.y < -12){
			Vector3 tempPos = transform.position;
			tempPos.y = -12;
			transform.position = tempPos;
		}
	}
}

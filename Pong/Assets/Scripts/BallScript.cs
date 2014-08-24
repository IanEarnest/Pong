using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {

	public static int playerScore = 0;
	public static int enemyScore = 0;
	
	float ballSpeed = 2; //20
	public static float ballStartSpeed = 2;
	public static float ballMaxSpeed = 40; //50 - at 44 the ball flys out
	float ballAngle = 10;
	float ballIncrement = 2; //10
	bool ballGoingUp;
	public Transform playerPaddle;
	public Transform enemyPaddle;

	public static GameObject ball;
	public static double ballTimer;
	public Vector3 ballOrigin;

	public static int ballNum;
	
	void Start () {
		// Ball always spawns where it is placed in scene.
		ball = gameObject;
		//rigidbody.velocity = new Vector3(10,10,0);
		rigidbody.velocity = new Vector3(ballSpeed, ballAngle, 0);
		ballOrigin = ball.transform.position;
		ball.name = "Ball " + ballNum; // Otherwise (clone) is added to every instance on top of current name.

		// Ball is speeded up in the menu.
		if (Application.loadedLevelName.Contains("Level Scene")) {
			ballSpeed = ballStartSpeed;
		} else {
			ballSpeed = 40;
		}
	}

	void Update(){
		// Add the ball speed to the velocity of the ball.
		if(rigidbody.velocity.x < 0){
			rigidbody.velocity = new Vector3(-ballSpeed, rigidbody.velocity.y, 0);
		}
		else if(rigidbody.velocity.x > 0){
			rigidbody.velocity = new Vector3(ballSpeed, rigidbody.velocity.y, 0);
		}
		
		// Restrict movement.
		// Ball goes past enemy paddle, Player scores.
		if(enemyPaddle && playerPaddle){
			if(ball.transform.position.x > enemyPaddle.position.x + 2){
				playerScore++;
				ballSpeed = ballStartSpeed;
				transform.position = Vector3.zero;
				if(ballGoingUp == true){
					ballGoingUp = false;
					rigidbody.velocity = new Vector3(-ballSpeed, ballAngle, 0);
				}
				else if(ballGoingUp == false){
					rigidbody.velocity = new Vector3(-ballSpeed, -ballAngle, 0);
					ballGoingUp = true;
				}
			}
			// Ball goes past player paddle, Enemy scores.
			if(ball.transform.position.x < playerPaddle.position.x - 2){
				enemyScore++;
				ballSpeed = ballStartSpeed;
				transform.position = Vector3.zero;
				if(ballGoingUp == true){
					ballGoingUp = false;
					rigidbody.velocity = new Vector3(ballSpeed, ballAngle, 0);
				}
				else if(ballGoingUp == false){
					rigidbody.velocity = new Vector3(ballSpeed, -ballAngle, 0);
					ballGoingUp = true;
				}
			}
		}
	}

	void FixedUpdate () {
		ballTimer += 0.005;
	}
	
	void OnCollisionEnter (Collision col){
		// Ball speeds up when hits anything.
		if(ballSpeed < ballMaxSpeed && Application.loadedLevelName == "Level Scene"){
			ballSpeed += ballIncrement;
		}

		// Variables to judge where ball hit on paddle.
		float ballMiddle = transform.position.y - col.transform.position.y;
		float paddleAboveMiddle = 0.3f; // Good amount from middle.
		float paddleUnderMiddle = 0-paddleAboveMiddle;



		// Ball collision with enemy paddle.
		if(col.gameObject.name == "Enemy"){
			// Ball collide above or under middle of paddle, go in diagonal from there.
			if(ballMiddle > paddleAboveMiddle || ballMiddle < paddleUnderMiddle){
				rigidbody.velocity = new Vector3(-ballSpeed+ballAngle, ballMiddle*ballAngle, 0);
			}
			else{
				// Ball hit middle of paddle.
				rigidbody.velocity = new Vector3(-ballSpeed, 0, 0);
			}
		}


		// Ball collision with player paddle.
		if(col.gameObject.name == "Player"){
			// Ball collide above or under middle of paddle, go in diagonal from there.
			if(ballMiddle > paddleAboveMiddle || ballMiddle < paddleUnderMiddle){
				rigidbody.velocity = new Vector3(ballSpeed-ballAngle, ballMiddle*ballAngle, 0);
			}
			else{
				// Ball hit middle of paddle.
				rigidbody.velocity = new Vector3(ballSpeed, 0, 0);
			}
		}


		// Menu and score scene.
		if(Application.loadedLevelName != "Level Scene"){
			if(col.gameObject.name.Contains("Wall") && ballTimer > 2){
				ballTimer = 0;
				ballNum++;
				//if(ball){ // Might need this to fix the pause ball spawn bug.
				Instantiate(ball, ballOrigin, ball.transform.rotation);
				Destroy(ball,5f);
			}
		}
	}
}

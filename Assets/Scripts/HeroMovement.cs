using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroMovement : MonoBehaviour
{
	private float moveSpeed = 325f;
	private Vector3 curPos, lastPos;
	private Animator anim;
	
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	void FixedUpdate ()
	{
		float moveX = Input.GetAxis("Horizontal");
		float moveY = Input.GetAxis("Vertical");
		
		Vector3 movement = new Vector3(moveX, moveY, 0.0f);
		GetComponent<Rigidbody2D>().velocity = movement * moveSpeed * Time.deltaTime;
		
		Scene currentScene = SceneManager.GetActiveScene();
		string sceneName = currentScene.name;

		curPos = transform.position;
		if (curPos == lastPos)
		{
			GameManager.instance.isWalking = false;
		}
		else
		{
			GameManager.instance.isWalking = true;
		}
		lastPos = curPos;
		anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
		anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
		anim.SetBool("PlayerMoving", GameManager.instance.isWalking);
		anim.SetFloat("LastMoveX", movement.x);
		anim.SetFloat("LastMoveY", movement.y);

		if (sceneName == "Level_1")
		{
			GameManager.instance.canGetEncounter = true;
			GameManager.instance.curRegions = 0;
		}
		if (sceneName == "Level_2")
		{
			GameManager.instance.canGetEncounter = true;
			GameManager.instance.curRegions = 1;
		}
		if (sceneName == "Level_3")
		{
			GameManager.instance.canGetEncounter = true;
			GameManager.instance.curRegions = 2;
		}

		if (sceneName != "Level_1" && sceneName != "Level_2" && sceneName != "Level_3")
		{
			GameManager.instance.canGetEncounter = false;
		}


	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "EndFirstLevel")
		{
			SceneManager.LoadScene("Transition");
			transform.position = new Vector2(0.0f, 0.0f);
		}
		if (other.tag == "EndTrans1")
		{
			SceneManager.LoadScene("Level_2");
			transform.position = new Vector2(0.0f, 0.0f);
		}
		if (other.tag == "EndSecondLevel")
		{
			SceneManager.LoadScene("Transition_2");
			transform.position = new Vector2(0.0f, 0.0f);
		}
		if (other.tag == "EndTrans2")
		{
			SceneManager.LoadScene("Level_3");
			transform.position = new Vector2(0.0f, 0.0f);
		}
		if (other.tag == "Plot")
		{
			SceneManager.LoadScene("StoryEnd");
			transform.position = new Vector2(0.0f, 0.0f);
		}
	}
}

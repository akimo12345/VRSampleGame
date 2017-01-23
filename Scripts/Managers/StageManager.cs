using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour {

	public PlayerHealth playerHealth;

	private float Timer = 0f;

	private bool Success;

	Animator anim;

	int buildIndex;

	void Awake()
	{
		anim = GetComponent<Animator>();

		// Create a temporary reference to the current scene.
		Scene currentScene = SceneManager.GetActiveScene ();

	    buildIndex = currentScene.buildIndex;

	}
		
	void Update()
	{		
		if (ScoreManager.score >= 10) 
		{
			anim.SetTrigger ("StageComplete");
			Success = true;
			Timer += Time.deltaTime;
		}

		if (Timer >= 5f && Success && buildIndex == 0) 
		{
			playerHealth.SceneSwitch ();
			Success = false;
		}

	}
}

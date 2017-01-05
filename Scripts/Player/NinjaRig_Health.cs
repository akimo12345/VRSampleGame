using UnityEngine;
using System.Collections;

public class NinjaRig_Health : MonoBehaviour {

	Animator anim;

	AnimatorStateInfo currentState;

	PlayerHealth playerhealth;

	void Awake ()
	{
		anim = GetComponent <Animator> ();

		playerhealth = GameObject.Find("Ninja").GetComponent<PlayerHealth> ();
	}
		
	void StopAnimator()
	{
		currentState = anim.GetCurrentAnimatorStateInfo (0);

		if (currentState.IsName ("Driect Blend Tree") || currentState.IsName ("Stunned")) 
		{
			Debug.Log ("NinjaDeath Anim");
			anim.enabled = false;
		} 
	}

	void slashStateAnimator()
	{
		currentState = anim.GetCurrentAnimatorStateInfo (0);

		if (playerhealth.currentHealth < 0 && 
			 (currentState.IsName ("Driect Blend Tree") 
				|| currentState.IsName ("Slash_LowToHigh") 
				|| currentState.IsName ("Slash_HighToLow")
				|| currentState.IsName ("Slash_RightToLeft")
				|| currentState.IsName ("Slash_LeftToRight"))) 
		{
			Debug.Log ("slashStateAnimator");
			anim.enabled = false;
		}
	}
}

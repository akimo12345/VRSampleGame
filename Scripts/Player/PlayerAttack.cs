using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1")) 
		{
			Animator charanim = GameObject.Find ("Ninja_Rig").GetComponent<Animator>();
			charanim.SetTrigger ("Slash_LeftRight");
		}
	}
}

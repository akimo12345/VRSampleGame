using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageText : MonoBehaviour {

	public static int stage;

	Text text;

	void Awake ()
	{
		text = GetComponent <Text> ();
		stage = 0;
	}


	void Update ()
	{
		text.text = "Stage: " + stage;
	}
}

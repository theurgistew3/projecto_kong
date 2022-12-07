using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class Textinput : MonoBehaviour
{
	InputField input;
	InputField.SubmitEvent se;
	public Textinput output;




	void Start()
	{
		input = gameObject.GetComponent<InputField>();
		se = new InputField.SubmitEvent();
		se.AddListener( SubmitInput );

	}

	private void SubmitInput( string arg0 )
	{
		// string currentText = output.text; //maybe add ToString()?
		// string newText = currentText + "/n" + arg0;
		// output.text = newText;
		// input.ActivateInputField();
	}
}


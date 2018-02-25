using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameUI : MonoBehaviour 
{
	public int _AvailableLetters = 15;

    public TextAsset _WordList;

	private string m_TextInput = "Add your word here";

	private IGame m_Interface;


	private void Start() 
	{
        // Send word data to the parser.
        m_Interface = new EmptyGame(_WordList, _AvailableLetters);
	}

	private void OnGUI()
	{
		int xPosition = 50;

		GUI.Label(new Rect(xPosition, 25, 300, 30), m_Interface.GetAvailableLetters());
		m_TextInput = GUI.TextField(new Rect(xPosition, 50, 150, 30), m_TextInput, 25);

		if(GUI.Button(new Rect(xPosition, 100, 150, 30), "Submit Word"))
		{
			if(!string.IsNullOrEmpty(m_TextInput))
			{
				m_Interface.SubmitWord(m_TextInput);
			}
		}

		if(GUI.Button(new Rect(xPosition, 135, 150, 30), "Reset Game"))
		{
			m_Interface.Reset();
        }

		GUI.BeginGroup(new Rect(xPosition + 200, 50, 200, 600));
		GUILayout.BeginHorizontal();

		// Score
		GUILayout.BeginVertical();

		for(int index = 0; index < 9; index++)
		{
			GUILayout.Label(m_Interface.GetScoreAtPosition(index).ToString());
		}

		GUILayout.EndVertical();

		// Word
		GUILayout.BeginVertical();
		
		for(int index = 0; index < 9; index++)
		{
			GUILayout.Label(m_Interface.GetWordEntryAtPosition(index));
		}

		GUILayout.EndVertical();

		GUILayout.EndHorizontal();
		GUI.EndGroup();
	}
}

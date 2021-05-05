using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mensajedebasura : MonoBehaviour
{

	private string message = "";
	private bool showMsg = false;

	private int w = 800;
	private int h = 250;
	private Rect textArea;
	private GUIStyle style;
	private Color textColor;
	public Font fuente;
   

	void Awake()
	{
		style = new GUIStyle();
        style.alignment = TextAnchor.LowerCenter;
		style.fontSize = 36;
		
		style.wordWrap = true;
		style.font=fuente;
		textColor = Color.white;
		textColor.a = 0;
		textArea = new Rect((Screen.width - w) / 2, 0, w, h);

	}

	void Update()
	{

	}

	void OnGUI()
	{
		if (showMsg)
		{
			if (textColor.a <= 1)
				textColor.a += 0.5f * Time.deltaTime;
		}
		// no hint to show
		else
		{
			if (textColor.a > 0)
				textColor.a -= 0.5f * Time.deltaTime;
		}

		style.normal.textColor = textColor;

		GUI.Label(textArea, message, style);
	}

	public void SetShowMsg(bool show)
	{
		showMsg = show;
	}

	public void SetMessage(string msg)
	{
		message = msg;
	}
}

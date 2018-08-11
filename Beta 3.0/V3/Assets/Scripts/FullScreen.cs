using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreen : MonoBehaviour {

	public void SetFullScreen(bool isFullScreen)
	{
		Screen.fullScreen = isFullScreen;
	}
	
}

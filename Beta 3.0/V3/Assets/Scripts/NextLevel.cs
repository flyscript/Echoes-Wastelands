using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

	private int currentScene;
	private int nextScene;

	private void Start()
	{
		currentScene = SceneManager.GetActiveScene().buildIndex;

		nextScene = currentScene + 1;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{

		if (other.gameObject.CompareTag("Player") && nextScene < 4) 
		{
			SceneManager.LoadScene(nextScene);
		}
		else 
		{
			SceneManager.LoadScene(0);
		}
	}
}

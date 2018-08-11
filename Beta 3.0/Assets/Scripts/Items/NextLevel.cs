using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
	[SerializeField]
	private string _nextPageName;
	[SerializeField]
	private string _nextPageStarterDescription;

	private void OnTriggerEnter2D(Collider2D other)
	{

		if (other.gameObject.CompareTag("Player")) 
		{
			if (!GameData.controller.data.IsInJournal(_nextPageName))
			{
				GameData.controller.data.AddToJournal(new Page(_nextPageName, _nextPageStarterDescription));
			}
			GameData.controller.Save();
			SceneManager.LoadScene(_nextPageName);
		}
		else 
		{
			SceneManager.LoadScene(0);
		}
	}
}

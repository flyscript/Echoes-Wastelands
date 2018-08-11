using UnityEngine;

public class AudioInstance : MonoBehaviour
{

	public GameObject test;
	
	// Use this for initialization
	void Start () {
		if (FindObjectOfType<SoundManager>())
		{
			return;
		}
		else
		{
			Instantiate(test, transform.position, transform.rotation);
		}
	}
}

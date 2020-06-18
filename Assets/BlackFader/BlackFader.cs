using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlackFader : MonoBehaviour
{

	// Singleton
	private static BlackFader _instance;
	public static BlackFader Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<BlackFader>();
			}

			return _instance;
		}
	}

	static private Image BlackerImage;
	static public bool ready = true;
	static private bool init = false;

	private void Awake()
	{
		if (!init)
		{
			init = true;
			BlackerImage = transform.GetChild(0).GetComponent<Image>();
		}
	}

	static public void GoToScene(string sceneName, LoadSceneMode mode, float time)
	{
		Instance.StartCoroutine(goToScene(sceneName, mode, time));
	}

	static private IEnumerator goToScene(string sceneName, LoadSceneMode mode, float time)
	{
		BlackerImage.enabled = true;
		ready = false;
		while (BlackerImage.color.a < 1f)
		{
			Color col = BlackerImage.color;
			BlackerImage.color = new Color(col.r, col.g, col.b, col.a + (Time.deltaTime / time));
			yield return null;
		}

		yield return new WaitForSeconds(time);

		SceneManager.LoadScene(sceneName, mode);

		while (BlackerImage.color.a > 0f)
		{
			Color col = BlackerImage.color;
			BlackerImage.color = new Color(col.r, col.g, col.b, col.a - (Time.deltaTime / time));
			yield return null;
		}
		ready = true;
		BlackerImage.enabled = false;
	}

	static public void GoToScene(string sceneName, LoadSceneMode mode, float time, Action actionAfterArriving, Action actionAfterFinishing)
	{
		Instance.StartCoroutine(goToScene(sceneName, mode, time, actionAfterArriving, actionAfterFinishing));
	}

	static private IEnumerator goToScene(string sceneName, LoadSceneMode mode, float time, Action actionAfterArriving, Action actionAfterFinishing)
	{
		BlackerImage.enabled = true;
		ready = false;
		while (BlackerImage.color.a < 1f)
		{
			Color col = BlackerImage.color;
			BlackerImage.color = new Color(col.r, col.g, col.b, col.a + (Time.deltaTime / time));
			yield return null;
		}

		yield return new WaitForSeconds(time);

		SceneManager.LoadScene(sceneName, mode);

		if (actionAfterArriving != null)
			actionAfterArriving();

		while (BlackerImage.color.a > 0f)
		{
			Color col = BlackerImage.color;
			BlackerImage.color = new Color(col.r, col.g, col.b, col.a - (Time.deltaTime / time));
			yield return null;
		}

		if (actionAfterFinishing != null)
			actionAfterFinishing();
		ready = true;
		BlackerImage.enabled = false;
	}

}

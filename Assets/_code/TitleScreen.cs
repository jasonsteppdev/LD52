using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
	[SerializeField] Image fadeImage;

	float fadetime = 0;
	public float fadetimeSpeed = 1f;

	bool fade = false;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return))
			fade = true;

		if (fade)
			FadeIn();
	}

	void FadeIn()
	{
		if (fadetime < 1)
		{
			fadetime += Time.deltaTime * fadetimeSpeed;
			fadeImage.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), fadetime);
		}
		else
		{
			fadetime = 1;
			fadeImage.color = new Color(0, 0, 0, 1);
			SceneManager.LoadScene("Game");
		}
	}
}

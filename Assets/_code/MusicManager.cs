using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	[SerializeField] AudioSource audioSource;


	float fadetime = 0;
	public float fadetimeSpeed = 1f;
	public float volume = 1f;

	public static MusicManager Instance { get; private set; }

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{

	}

	void Update()
	{
		FadeIn();
	}

	void FadeIn()
	{
		if (fadetime < 1)
		{
			fadetime += Time.deltaTime * fadetimeSpeed;
			audioSource.volume = Mathf.Lerp(0, volume, fadetime);
		} else {
			fadetime = 1;
			audioSource.volume = volume;
		}
	}
}

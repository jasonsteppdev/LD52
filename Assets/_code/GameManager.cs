using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField] Image toxicityImage;

	[SerializeField] float toxicityDecayRate = 1f;
	[SerializeField] float toxicityDecayValue = 0.0001f;

	public ObjectPool bodyPool;
	public ObjectPool eyePool;
	public ObjectPool heartPool;
	public ObjectPool lungsPool;
	public ObjectPool kidneyPool;
	public ObjectPool iceboxPool;

	public float toxicity = 0f;
	public Gradient gradient;
	public bool toxic;

	float _toxicityDecayTimer;

	public static GameManager Instance { get; private set; }

	void Awake()
	{
		GetGameInstance();
	}

	void Start()
	{

	}

	void Update()
	{
		HandleToxicity();
		toxicityImage.color = ColorFromGradient(toxicity);
	}

	void HandleToxicity()
	{
		if (!toxic)
		{
			_toxicityDecayTimer += Time.deltaTime;
			if (_toxicityDecayTimer > toxicityDecayRate)
			{
				toxicity -= toxicityDecayValue;
				_toxicityDecayTimer = 0f;
			}
		}


		if (toxicity < 0)
			toxicity = 0;

		if (toxicity >= 1)
			toxicity = 1;

		toxicityImage.fillAmount = toxicity;

		if (toxicity >= 1)
			Debug.Log("Game Over");
	}

	Color ColorFromGradient(float value)  // float between 0-1
	{
		return gradient.Evaluate(value);
	}


	void GetGameInstance()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}
}

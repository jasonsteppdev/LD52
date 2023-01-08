using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
	[SerializeField] Image toxicityImage;

	[SerializeField] float toxicityDecayRate = 1f;
	[SerializeField] float toxicityDecayValue = 0.0001f;
	[SerializeField] TMP_Text creditText;

	[SerializeField] SpriteRenderer _sprite;

	[SerializeField] Sprite eyeSprite;
	[SerializeField] Sprite lungsSprite;
	[SerializeField] Sprite heartSprite;
	[SerializeField] Sprite kidneySprite;

	[SerializeField] Bubble bubble;


	public ObjectPool bodyPool;
	public ObjectPool eyePool;
	public ObjectPool heartPool;
	public ObjectPool lungsPool;
	public ObjectPool kidneyPool;
	public ObjectPool iceboxPool;

	public float toxicity = 0f;
	public Gradient gradient;
	public bool toxic;

	public OrganType organ = OrganType.Eyes;

	float _toxicityDecayTimer;
	bool _isAnimatingIn;
	bool _isAnimatingOut;

	int credits;

	public static GameManager Instance { get; private set; }

	void Awake()
	{
		GetGameInstance();
		credits = 0;
		SetCredits();
	}

	void Start()
	{
		RandomizeOrgan();
	}

	void Update()
	{
		HandleToxicity();
		toxicityImage.color = ColorFromGradient(toxicity);
	}

	public void Deposit(OrganType organ)
	{
		if (organ == this.organ)
			AddCredits(100);
		else
			RemoveCredits(100);

		bubble.isClosing = true;
	}

	public void SetCredits()
	{
		creditText.text = credits.ToString();
	}

	public void AddCredits(int credits)
	{
		this.credits += credits;
		SetCredits();
	}

	public void RemoveCredits(int credits)
	{
		this.credits -= credits;
		SetCredits();
	}

	public void RandomizeOrgan()
	{
		int random = Random.Range(0, 4);
		switch (random)
		{
			case 0:
				organ = OrganType.Eyes;
				_sprite.sprite = eyeSprite;
				break;
			case 1:
				organ = OrganType.Lungs;
				_sprite.sprite = lungsSprite;
				break;
			case 2:
				organ = OrganType.Heart;
				_sprite.sprite = heartSprite;
				break;
			case 3:
				organ = OrganType.Kidney;
				_sprite.sprite = kidneySprite;
				break;
		}
		bubble.isOpening = true;

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

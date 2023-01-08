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
	[SerializeField] TMP_Text timerText;
	[SerializeField] TMP_Text gameoverText;
	[SerializeField] Image fadeBlackImage;
	[SerializeField] GameObject endScreen;



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
	float _timer = 210f;
	//float _timer = 10f;
	bool _isAnimatingIn;
	bool _isAnimatingOut;

	float fadeTime = 0;
	float fadeTimeSpeed = 1f;

	float slideInTime = 0;
	float slideInSpeed = 1f;
	bool isSlideInText = false;

	int credits;
	int organsSold;

	public bool isGameOver;

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

		if (isGameOver)
			ShowGameOverScreen();

		if(!isGameOver)
			HandleToxicity();
			
		toxicityImage.color = ColorFromGradient(toxicity);


		if (_timer > 0)
		{
			_timer -= Time.deltaTime;
			if(!isGameOver)
				UpdateTimer(_timer);
		}
		else
		{
			_timer = 0;
			isGameOver = true;
			gameoverText.text = "TIMES UP!";
		}

	}


	void ShowGameOverScreen()
	{

		if (fadeTime < 1)
		{
			fadeTime += Time.deltaTime * fadeTimeSpeed;
			fadeBlackImage.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 0.5f), fadeTime);
		}
		else
		{
			fadeBlackImage.color = new Color(0, 0, 0, 0.5f);
			fadeTime = 1;
			isSlideInText = true;
		}

		if (isSlideInText)
		{
			if (slideInTime < 1)
			{
				slideInTime += Time.deltaTime * slideInSpeed;
				endScreen.transform.position = Vector3.Lerp(new Vector3(-20, 0, 0), new Vector3(0, 0, 0), slideInTime);
			}
			else
			{
				slideInTime = 1;
				endScreen.transform.position = new Vector3(0, 0, 0);
			}
		}


	}

	void UpdateTimer(float currentTime)
	{
		currentTime += 1;

		float minutes = Mathf.FloorToInt(currentTime / 60);
		float seconds = Mathf.FloorToInt(currentTime % 60);

		timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
	}

	public void Deposit(OrganType organ)
	{
		if (organ == this.organ)
		{
			AddCredits(1000);
			organsSold++;
		}
		else
			RemoveCredits(1000);

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
		{
			isGameOver = true;
			gameoverText.text = "GAME OVER";
		}
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

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

public class Manager : MonoBehaviour {
	public Sprite[] rootFrames;
	public TextMeshProUGUI daysLeftLabel;
	public GameObject textBoxBacking;
	public District[] districts;
	public GameObject[] initialRoots;
	public Popover popover;

	public GameObject timerNode;

	public TextAsset startingText;

	public TextAsset genericEndingText;
	public Sprite genericEndingSprite;

	public TextAsset badEventText;

	public TextAsset thanksForPlayingText;

	// the holders of the sprite, etc
	public TextMeshProUGUI vignetteText;
	public SpriteRenderer vignetteSprite;

	private int _weeksLeft, _endStage;
	private bool _showingPopover, _allowInteractivity;
	private Action _popoverContinuation;

	private List<Sprite> _endingSprites;
	private List<string> _endingStrings;
	private List<string> _endingSounds;

	// The roots should have all done Awake by this point
    void Start() {
		StartAmbience();
		_showingPopover = false;

		// nothing apart from the house to start
		_allowInteractivity = false;
		_weeksLeft = 40;

		_updateLabel();

		textBoxBacking.SetActive(false);

		foreach (GameObject root in initialRoots) {
			Root rt = root.GetComponent<Root>();
			rt.sourceRoot = true;
			rt.RealignStates();

			root.SetActive(false);
		}
    }

	void Update() {
		if (Input.GetKey("escape")) {
            Application.Quit();
        }
	}

	void _restartGame() {
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}

	void _updateLabel() {
		int totalWeekCount = 40;
		int weekCounter = 1 + totalWeekCount - _weeksLeft;
		if (weekCounter > totalWeekCount) {
			weekCounter = totalWeekCount;
		}

		daysLeftLabel.SetText($"Week {weekCounter} / {totalWeekCount}");
	}

	public int RootFrameCount() {
		return rootFrames.Length;
	}

	void _nextEndStage() {
		Action nextAction = _nextEndStage;
		if (_endStage == (_endingSprites.Count - 1)) {
			nextAction = _restartGame;
		}
		
		Play(_endingSounds[_endStage]);
		ShowTextAndSprite(_endingStrings[_endStage], _endingSprites[_endStage], nextAction);
		_endStage += 1;
	}

	void _startEndEvents() {
		PlayStorm();
			
		timerNode.SetActive(false);
		_allowInteractivity = false;

		_endStage = 0;
		_endingSprites = new List<Sprite>();
		_endingStrings = new List<string>();
		_endingSounds = new List<string>();

		_endingSprites.Add(null);
		_endingStrings.Add(badEventText.text);
		_endingSounds.Add("");

		bool needGeneric = true;

		foreach (District district in districts) {
			if (district.complete) {
				_endingSprites.Add(district.endingSprite);
				_endingStrings.Add(district.endingText.text);
				_endingSounds.Add(district.endingSound);
				needGeneric = false;
			}
		}

		if (needGeneric) {
			_endingSprites.Add(genericEndingSprite);
			_endingStrings.Add(genericEndingText.text);
			_endingSounds.Add("");
		}

		_endingSprites.Add(null);
		_endingStrings.Add(thanksForPlayingText.text);
		_endingSounds.Add("");

		_nextEndStage();
	}

	// Called when end events would not conflict with other activities
	public void CheckEndEvents() {
		if (_weeksLeft == 0) {
			_startEndEvents();
		}
	}

	public void _startGame() {
		_allowInteractivity = true;
		foreach (GameObject root in initialRoots) {
			root.SetActive(true);
		}
	}

	public void BackgroundClicked() {
		if (_showingPopover) {
			PlayClick();
			DismissPopover();
		}
	}

	public void DidClickHouse() {
		PlayClick();
		popover.PrepareToShow();
		ShowTextAndSprite(startingText.text, null, _startGame);
	}

	public void DismissPopover() {
		if (_popoverContinuation == null) {
			return;
		}
		_showingPopover = false;
		textBoxBacking.SetActive(false);
		_popoverContinuation();
	}

	// ie interactivity other than the main one
	public bool ShouldAllowInteractivity() {
		if (!_allowInteractivity) {
			return false;
		}

		return !_showingPopover;
	}

	// Push the day counter forward, NB don't check end events at this point
	public void DidAdvanceDay() {
		_weeksLeft -= 1;
		_updateLabel();
	}

	// If continuation is null, then the popover will not allow dismissal
	public void ShowTextAndSprite(string text, Sprite picture, Action continuation) {
		_popoverContinuation = continuation;
		_showingPopover = true;
		textBoxBacking.SetActive(true);
		vignetteText.SetText(text);
		vignetteSprite.sprite = picture;
	}

	public void Play(string key) {
		if (key == "") {
			return;
		}

        FMODUnity.RuntimeManager.PlayOneShot("event:" + key);
	}	

	public void PlayStorm() {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Storm");
	}	

	public void StartAmbience() {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ambience/Ambience");
	}

	public void PlayClick() {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Click");
	}
}

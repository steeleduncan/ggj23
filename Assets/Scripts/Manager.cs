using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Manager : MonoBehaviour {
	public Sprite[] rootFrames;
	public TextMeshProUGUI daysLeftLabel;
	public GameObject textBoxBacking;
	public District[] districts;
	public GameObject[] initialRoots;

	public GameObject timerNode;

	public TextAsset startingText;
	public Sprite startingSprite;

	public TextAsset genericEndingText;
	public Sprite genericEndingSprite;

	public TextAsset badEventText;
	public Sprite badEventSprite;

	public TextAsset thanksForPlayingText;

	// the holders of the sprite, etc
	public TextMeshProUGUI vignetteText;
	public SpriteRenderer vignetteSprite;

	private int _weeksLeft, _endStage;
	private bool _showingPopover, _allowInteractivity;
	private Action _popoverContinuation;

	private List<Sprite> _endingSprites;
	private List<string> _endingStrings;

	// The roots should have all done Awake by this point
    void Start() {
		_showingPopover = false;
		_allowInteractivity = true;
		_weeksLeft = 40;

		_updateLabel();

		textBoxBacking.SetActive(false);
    }

	void _updateLabel() {
		daysLeftLabel.SetText($"{_weeksLeft} Weeks Left");
	}

	public int RootFrameCount() {
		return rootFrames.Length;
	}

	void _nextEndStage() {
		Action nextAction = _nextEndStage;
		if (_endStage == (_endingSprites.Count - 1)) {
			nextAction = null;
		}
		
		ShowTextAndSprite(_endingStrings[_endStage], _endingSprites[_endStage], nextAction);
		_endStage += 1;
	}

	void _startEndEvents() {
		timerNode.SetActive(false);
		_allowInteractivity = false;

		_endStage = 0;
		_endingSprites = new List<Sprite>();
		_endingStrings = new List<string>();

		_endingSprites.Add(badEventSprite);
		_endingStrings.Add(badEventText.text);

		foreach (District district in districts) {
			if (district.complete) {
				_endingSprites.Add(district.endingSprite);
				_endingStrings.Add(district.endingText.text);
			}
		}

		_endingSprites.Add(null);
		_endingStrings.Add(thanksForPlayingText.text);

		_nextEndStage();
	}

	// Called when end events would not conflict with other activities
	public void CheckEndEvents() {
		if (_weeksLeft == 0) {
			_startEndEvents();
		}
	}

	public void _startGame() {
		foreach (GameObject root in initialRoots) {
			root.SetActive(true);
		}
	}

	public void DidClickHouse() {
		ShowTextAndSprite(startingText.text, startingSprite, _startGame);
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
}

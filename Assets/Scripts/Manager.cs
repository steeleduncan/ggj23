using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Manager : MonoBehaviour {
	public Sprite[] rootFrames;
	public TextMeshProUGUI daysLeftLabel;
	public GameObject textBoxBacking;

	public TextMeshProUGUI vignetteText;
	public SpriteRenderer vignetteSprite;

	private int _weeksLeft = 40;
	private bool _showingPopover = false;
	private Action _popoverContinuation;

    void Start() {
		_updateLabel();

		textBoxBacking.SetActive(false);
    }

	private List<Root> _roots;
	public void RegisterRoot(Root root) {
		if (_roots == null) {
			_roots = new List<Root>();
		}
		_roots.Add(root);
		root.manager = this;
	}

	void _updateLabel() {
		daysLeftLabel.SetText($"{_weeksLeft} Weeks Left");
	}

	public void DismissPopover() {
		_showingPopover = false;
		textBoxBacking.SetActive(false);
		if (_popoverContinuation != null) {
			_popoverContinuation();
		}
	}

	// ie interactivity other than the main one
	public bool ShouldAllowInteractivity() {
		return !_showingPopover;
	}

	public void DidAdvanceDay() {
		_weeksLeft -= 1;
		_updateLabel();
	}

	public void ShowTextAndSprite(string text, Sprite picture, Action continuation) {
		_popoverContinuation = continuation;
		_showingPopover = true;
		textBoxBacking.SetActive(true);
		vignetteText.SetText(text);
		vignetteSprite.sprite = picture;
	}

}

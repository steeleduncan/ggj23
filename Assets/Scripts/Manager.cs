using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour {
	public Sprite[] rootFrames;
	public TextMeshProUGUI daysLeftLabel;
	public GameObject textBoxBacking;

	public TextMeshProUGUI vignetteText;
	public SpriteRenderer vignetteSprite;

	int _daysLeft = 40;
	bool _showingPopover = false;

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
		daysLeftLabel.SetText($"{_daysLeft} Days Left");
	}

	public void DismissPopover() {
		_showingPopover = false;
		textBoxBacking.SetActive(false);
	}

	// ie interactivity other than the main one
	public bool ShouldAllowInteractivity() {
		return !_showingPopover;
	}

	public void DidAdvanceDay() {
		_daysLeft -= 1;
		_updateLabel();
	}

	public void ShowTextAndSprite(string text, Sprite picture) {
		_showingPopover = true;
		textBoxBacking.SetActive(true);
		vignetteText.SetText(text);
		vignetteSprite.sprite = picture;
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

enum Picture {
	One,
	Two,
	Three,
	Four,
	Five,
};

public class Manager : MonoBehaviour {
	public Sprite[] rootFrames;
	public Sprite[] endImages;
	public TextMeshProUGUI daysLeftLabel;
	public GameObject textBoxBacking;

	int _daysLeft = 40;

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

	public void DidAdvanceDay() {
		_daysLeft -= 1;
		_updateLabel();
	}

	/*
	public void ShowTextAndSprite(string text, Picture picture) {

	}
	*/

    void Start() {
		_updateLabel();

		textBoxBacking.SetActive(false);
    }
}

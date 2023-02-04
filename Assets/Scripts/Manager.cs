using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour {
	public Sprite[] rootFrames;
	public TextMeshProUGUI daysLeftLabel;

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

    void Start() {
		_updateLabel();
    }
}

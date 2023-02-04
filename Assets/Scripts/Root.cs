using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour {
	public Manager manager;
	public District district;
	private SpriteRenderer _spriteRenderer;
	public Sprite _vignetteSprite;
	public TextAsset _vignetteText;

	private Vector3 _originalScale;
	private int _stage = 0;
	private string _vignetteString;


	void Start() {
		_originalScale = transform.localScale;

		// Add box collider so the mouse enter/exit work
		gameObject.AddComponent<BoxCollider2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();

		_vignetteString = _vignetteText.text;

		_alignState();
	}

    void Update() {
        
    }

	void _alignState() {
		int cappedStage = _stage;
		if (cappedStage > 3) {
			cappedStage = 3;
		}
		_spriteRenderer.sprite = manager.rootFrames[cappedStage];
	}

	void _advanceDay() {
		_stage += 1;

		if (_stage == 4) {
			manager.ShowTextAndSprite(_vignetteString, _vignetteSprite);
			manager.DidAdvanceDay();
		} else if (_stage >= 4) {
			// do nothing, it should be inactive
		} else {
			manager.DidAdvanceDay();
		}

		_alignState();
	}

	void OnMouseDown() {
		_advanceDay();
	}

	void OnMouseEnter() {
		district.RegisterMouseIn();
	}

	void OnMouseExit() {
		district.RegisterMouseOut();
	}
}

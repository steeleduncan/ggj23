using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour {
	public Manager manager;
	public District district;
	private SpriteRenderer _spriteRenderer;
	public Sprite _vignetteSprite;
	public TextAsset _vignetteText;
	public Root nextRoot;

	private Vector3 _originalScale;
	private int _stage = 0;
	private string _vignetteString;
	private BoxCollider2D _collider;

	void Start() {
		_originalScale = transform.localScale;

		// Add box collider so the mouse enter/exit work
		_collider = gameObject.AddComponent<BoxCollider2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();

		_vignetteString = _vignetteText.text;

		_alignState();

		if (nextRoot != null) {
			nextRoot.SetRootActive(false);
		}
	}

    public void SetRootActive(bool active) {
		gameObject.SetActive(active);
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

			_collider.enabled = false;
			if (nextRoot != null) {
				nextRoot.SetRootActive(true);
			}
		} else if (_stage >= 4) {
			// do nothing, it should be inactive
		} else {
			manager.DidAdvanceDay();
		}

		_alignState();
	}

	void OnMouseDown() {
		if (!manager.ShouldAllowInteractivity()) {
			return;
		}
		_advanceDay();
	}

	void OnMouseEnter() {
		if (!manager.ShouldAllowInteractivity()) {
			return;
		}
		district.RegisterMouseIn();
	}

	void OnMouseExit() {
		if (!manager.ShouldAllowInteractivity()) {
			return;
		}
		district.RegisterMouseOut();
	}
}

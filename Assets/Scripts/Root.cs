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

	// NB we use awake for the roots as many get deactivated before they start, and this breaks functionality
	void Awake() {
		_originalScale = transform.localScale;

		// Add box collider so the mouse enter/exit work
		_collider = gameObject.AddComponent<BoxCollider2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();

		_vignetteString = _vignetteText.text;

		_alignState();

		if (nextRoot != null) {
			_setRootActive(false);
		}

		// disable me to start
		gameObject.SetActive(false);
	}

    void _setRootActive(bool active) {
		gameObject.SetActive(active);
	}

	/*
    public void DisableRootChain() {
		if (nextRoot != null) {
			nextRoot.DisableRootChain();
		}
	}
	*/

    void Update() {
        
    }

	void _alignState() {
		int cappedStage = _stage;
		if (cappedStage >= manager.RootFrameCount()) {
			cappedStage = manager.RootFrameCount() - 1;
		}
		_spriteRenderer.sprite = manager.rootFrames[cappedStage];
	}

	void _popoverDidReturn() {
		// called upon popover dismissal
		if (nextRoot != null) {
			nextRoot._setRootActive(true);
		}
		manager.CheckEndEvents();
	}

	void _advanceDay() {
		_stage += 1;

		if (_stage == manager.RootFrameCount()) {
			district.complete = true;
			manager.ShowTextAndSprite(_vignetteString, _vignetteSprite, _popoverDidReturn);
			manager.DidAdvanceDay();

			_collider.enabled = false;
			district.RegisterMouseOut();
		} else if (_stage >= manager.RootFrameCount()) {
			// do nothing, it should be inactive
		} else {
			manager.DidAdvanceDay();
			manager.CheckEndEvents();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour {
	public Manager manager;
	public District district;
	private SpriteRenderer _spriteRenderer;

	private Vector3 _originalScale;
	private int _stage = 2;

	void Start() {
		_originalScale = transform.localScale;

		// Add box collider so the mouse enter/exit work
		gameObject.AddComponent<BoxCollider2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();

		_alignState();
	}

    void Update() {
        
    }

	void _alignState() {
		_spriteRenderer.sprite = manager.rootFrames[_stage];
	}

	void OnMouseDown() {
		if (_stage >= 4) {
			return;
		}

		_stage += 1;
		_alignState();
	}

	void OnMouseEnter() {
		district.RegisterMouseIn();
	}

	void OnMouseExit() {
		district.RegisterMouseOut();
	}
}

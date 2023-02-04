using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour {
	public Manager manager;
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

	void OnPointerDown(PointerEventData eventData) {
		Debug.Log("pointer down");
	}
	
	void OnMouseDown() {
		Debug.Log("mouse down");

		if (_stage >= 4) {
			return;
		}

		_stage += 1;
		_alignState();
	}

	void OnMouseEnter() {
		float scale = 1.2f;
		transform.localScale = new Vector3(_originalScale.x * scale, _originalScale.y * scale, _originalScale.z);
	}

	void OnMouseExit() {
		transform.localScale = _originalScale;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {
	public Sprite deselectedSprite, selectedSprite;
	public Manager manager;

	private SpriteRenderer _spriteRenderer;
	private bool _mouseIn;
	private BoxCollider2D _collider;

    void Start() {
		_collider = gameObject.AddComponent<BoxCollider2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_mouseIn = false;
		_updateMouseState();
    }

    void StopInteracting() {
		_collider.enabled = false;
		_mouseIn = false;
		_updateMouseState();
	}

	void _updateMouseState() {
		if (_mouseIn) {
			_spriteRenderer.sprite = selectedSprite;
		} else {
			_spriteRenderer.sprite = deselectedSprite;
		}
	}

	void OnMouseDown() {
		StopInteracting();
		manager.DidClickHouse();
	}

	void OnMouseEnter() {
		_mouseIn = true;
		_updateMouseState();
	}

	void OnMouseExit() {
		_mouseIn = false;
		_updateMouseState();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class District : MonoBehaviour {
	public Sprite deselectedSprite, selectedSprite;
	private SpriteRenderer _spriteRenderer;

	int _refCount = 0;

    void Start() {
		//		gameObject.AddComponent<BoxCollider2D>();

		_spriteRenderer = GetComponent<SpriteRenderer>();
		_updateGlow();
    }

	void _updateGlow() {
		if (_refCount > 0) {
			_spriteRenderer.sprite = selectedSprite;
		} else {
			_spriteRenderer.sprite = deselectedSprite;
		}
	}

	public void RegisterMouseIn() {
		_refCount += 1;
		_updateGlow();
	}

	public void RegisterMouseOut() {
		_refCount -= 1;
		_updateGlow();
	}

	void OnMouseEnter() {
		RegisterMouseIn();
	}

	void OnMouseExit() {
		RegisterMouseOut();
	}
}
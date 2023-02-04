using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class District : MonoBehaviour {
	public Sprite deselectedSprite, selectedSprite;
	private SpriteRenderer _spriteRenderer;

    void Start() {
		gameObject.AddComponent<BoxCollider2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		setHovered(false);
    }

	void setHovered(bool hovering) {
		if (hovering) {
			_spriteRenderer.sprite = selectedSprite;
		} else {
			_spriteRenderer.sprite = deselectedSprite;
		}
	}

	void OnMouseEnter() {
		setHovered(true);
	}

	void OnMouseExit() {
		setHovered(false);
	}
}

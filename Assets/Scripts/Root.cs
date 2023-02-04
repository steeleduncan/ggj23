using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour {
	public Manager manager;
	private Vector3 _originalScale;

	void Start() {
		_originalScale = transform.localScale;
		manager.RegisterRoot(this);

		// Add box collider so the mouse enter/exit work
		gameObject.AddComponent<BoxCollider2D>();
	}

    void Update() {
        
    }

	void OnMouseDown() {
		Debug.Log("Did click root");
	}

	void OnMouseEnter() {
		float scale = 1.2f;
		transform.localScale = new Vector3(_originalScale.x * scale, _originalScale.y * scale, _originalScale.z);
	}

	void OnMouseExit() {
		transform.localScale = _originalScale;
	}
}

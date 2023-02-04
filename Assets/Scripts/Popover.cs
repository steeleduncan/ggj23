using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popover : MonoBehaviour {
	public Manager manager;

    void Start() {
		gameObject.AddComponent<BoxCollider2D>();
    }

	void OnMouseDown() {
		manager.DismissPopover();
	}
}

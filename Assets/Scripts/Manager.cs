using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
	public GameObject backgroundSprite;

	private List<Root> _roots;
	public void RegisterRoot(Root root) {
		if (_roots == null) {
			_roots = new List<Root>();
		}
		_roots.Add(root);
		root.manager = this;

		int count = _roots.Count;
		print($"Got {count} roots");
	}

    void Start() {
		print("Booting manager");
    }
}

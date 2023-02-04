using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
	public Sprite[] rootFrames;

	private List<Root> _roots;
	public void RegisterRoot(Root root) {
		if (_roots == null) {
			_roots = new List<Root>();
		}
		_roots.Add(root);
		root.manager = this;

		int count = _roots.Count;
		print($"Got {count} roots");

		int frameCount = rootFrames.Length;
		print($"Got {frameCount} roots");
	}

    void Start() {
		print("Booting manager");
    }
}

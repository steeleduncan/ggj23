using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public Sprite root1, root2, root3, root4;

	public UnityEngine.U2D.Animation.SpriteResolver spriteResolver;

	void Start() {
		print($"sr = {spriteResolver}");
		//		spriteResolver.
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureExample : MonoBehaviour {
	[SerializeField]
	private ComputeShader shader, shaderCopy;
	RenderTexture tex, texCopy;
	void Start () {
		tex = new RenderTexture(64, 64, 0);
		tex.enableRandomWrite = true;
		tex.Create();

		texCopy = new RenderTexture(64, 64, 0);
		texCopy.enableRandomWrite = true;
		texCopy.Create();

		shader.SetTexture(0, "tex", tex);
		shader.Dispatch(0, tex.width / 8, tex.height / 8, 1);

		shaderCopy.SetTexture(0, "tex", tex);
		shaderCopy.SetTexture(0, "texCopy", texCopy);
		shaderCopy.Dispatch(0, texCopy.width / 8, texCopy.height / 8, 1);
	}

	void OnGUI () {
		int w = Screen.width / 2;
		int h = Screen.height / 2;
		int s = 512;
		GUI.DrawTexture(new Rect(w - s / 2, h - s / 2, s, s), texCopy);
	}

	void OnDestroy () {
		tex.Release();
		texCopy.Release();
	}
}

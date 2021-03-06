﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyImage : MonoBehaviour {
	[SerializeField]
	private ComputeShader shader;
	[SerializeField]
	private GameObject image, imageCopy;

	RenderTexture textureCopy;

	void Start () {
		int row = 128;
		int imageSize = row * row;
		ComputeBuffer buffer = new ComputeBuffer(imageSize, sizeof(float));
		int kernel = shader.FindKernel("CSMain");
		shader.SetBuffer(kernel, "buffer", buffer);
		Texture texture = image.GetComponent<Renderer>().material.mainTexture;
		shader.SetTexture(kernel, "tex", texture);
		textureCopy = new RenderTexture(row, row, 0);
		textureCopy.enableRandomWrite = true;
		textureCopy.Create();
		shader.SetTexture(kernel, "texCopy", textureCopy);

		shader.Dispatch(kernel, row/8, row/8, 1);

		// Debug.Log(textureCopy);
		imageCopy.GetComponent<Renderer>().material.mainTexture = textureCopy;

		buffer.Release();
	}
	
	void Update () {
		
	}
}

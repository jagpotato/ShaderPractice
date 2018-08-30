using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample1 : MonoBehaviour {
	[SerializeField]
	private ComputeShader shader;
	[SerializeField]
	private Transform cubeTransform;

	private ComputeBuffer buffer;

	void Start () {
		buffer = new ComputeBuffer(1, sizeof(float));
		shader.SetBuffer(0, "Result", buffer);
	}
	
	void Update () {
		shader.SetFloat("positionX", cubeTransform.position.x);
		shader.Dispatch(0, 8, 8, 1);

		float[] data = new float[1];
		buffer.GetData(data);
		float positionX = data[0];
		var cubePosition = cubeTransform.position;
		cubePosition.x = positionX;
		cubeTransform.position = cubePosition;
	}

	void OnDestroy () {
		buffer.Release();
	}
}

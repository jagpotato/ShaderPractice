using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KernelExample : MonoBehaviour {
	[SerializeField]
	private ComputeShader shader;
	void Start () {
		//
		// CSMain1
		//
		// ComputeBuffer buffer = new ComputeBuffer(4 * 2, sizeof(int));
		// shader.SetBuffer(0, "buffer1", buffer);
		// shader.Dispatch(0, 2, 1, 1);

		// int[] data = new int[4 * 2];
		// buffer.GetData(data);
		// foreach(int e in data) {
		// 	Debug.Log(e);
		// }
		// buffer.Release();
		
		//
		// CSMain2
		//
		ComputeBuffer buffer = new ComputeBuffer(4 * 4 * 2 * 2, sizeof(int));
		int kernel = shader.FindKernel("CSMain2");
		shader.SetBuffer(kernel, "buffer2", buffer);
		shader.Dispatch(kernel, 2, 2, 1);

		int[] data = new int[4 * 4 * 2 * 2];
		buffer.GetData(data);
		for (int i = 0; i < 8; i++) {
			string line = "";
			for (int j = 0; j < 8; j++) {
				line += " " + data[j + i * 8];
			}
			Debug.Log(line);
		}
		buffer.Release();
	}
}

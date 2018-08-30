using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class SetCube : MonoBehaviour {
	[SerializeField]
	private int cubeNum;
	[SerializeField]
	private Mesh cube;
	[SerializeField]
	private Material cubeMaterial;
	[SerializeField]
	private ComputeShader setCubeShader;

  private ComputeBuffer setCubeBuffer;
	private ComputeBuffer argsBuffer;
	private int kernel;
	private uint[] args = new uint[] {0, 0, 0, 0, 0};

	struct PixelCube {
		Vector3 position;
	}

	void Start () {
		argsBuffer = new ComputeBuffer(1, args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
		setCubeBuffer = new ComputeBuffer(cubeNum, Marshal.SizeOf(typeof(PixelCube)));

    kernel = setCubeShader.FindKernel("CSMain");
		setCubeShader.SetBuffer(kernel, "pixelCubes", setCubeBuffer);
		setCubeShader.Dispatch(kernel, cubeNum, 1, 1);

		uint numIndices = (cube != null) ? (uint)cube.GetIndexCount(0) : 0;
		args[0] = numIndices;
		args[1] = (uint)cubeNum;
		argsBuffer.SetData(args);

		cubeMaterial.SetBuffer("pixelCubes", setCubeBuffer);
	}
	
	void Update () {
		Bounds bounds = new Bounds(Vector3.zero, new Vector3(10000f, 10000f, 10000f));
		Graphics.DrawMeshInstancedIndirect(cube, 0, cubeMaterial, bounds, argsBuffer);
	}

	void OnDisable () {
		// if (argsBuffer != null) argsBuffer.Release();
		// argsBuffer = null;
		// if (setCubeBuffer != null) setCubeBuffer.Release();
		// setCubeBuffer = null;
		argsBuffer.Release();
		setCubeBuffer.Release();
	}
}

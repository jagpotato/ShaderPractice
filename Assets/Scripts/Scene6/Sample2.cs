using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Sample2 : MonoBehaviour {
	[SerializeField]
	private int instanceCount;
	[SerializeField]
	private Mesh instanceMesh;
	[SerializeField]
	private Material instanceMaterial;
	[SerializeField]
	private ShadowCastingMode castShadows = ShadowCastingMode.Off;
	[SerializeField]
	private bool receiveShadows = false;
	[SerializeField]
	private ComputeShader positionComputeShader;

	private int positionComputeKernelId;
	private ComputeBuffer positionBuffer;
	private ComputeBuffer argsBuffer;
	private ComputeBuffer colorBuffer;
	private uint[] args = new uint[5] {0, 0, 0, 0, 0};
	void Start () {
		argsBuffer = new ComputeBuffer(5, sizeof(uint), ComputeBufferType.IndirectArguments);
		CreateBuffers();
	}
	
	void Update () {
		UpdateBuffers();
		Graphics.DrawMeshInstancedIndirect(instanceMesh, 0, instanceMaterial, instanceMesh.bounds, argsBuffer, 0, null, castShadows, receiveShadows);
	}

	private void CreateBuffers () {
		if (instanceCount < 1) instanceCount = 1;
		instanceCount = Mathf.ClosestPowerOfTwo(instanceCount);

		positionComputeKernelId = positionComputeShader.FindKernel("CSMain");
		instanceMesh.bounds = new Bounds(Vector3.zero, Vector3.one * 10000f);

		if (positionBuffer != null) positionBuffer.Release();
		if (colorBuffer != null) colorBuffer.Release();

		positionBuffer = new ComputeBuffer(instanceCount, 16);
		colorBuffer = new ComputeBuffer(instanceCount, 16);

		Vector4[] colors = new Vector4[instanceCount];
		for (int i = 0; i < instanceCount; i++)
		  colors[i] = Random.ColorHSV();
		colorBuffer.SetData(colors);

		instanceMaterial.SetBuffer("positionBuffer", positionBuffer);
		instanceMaterial.SetBuffer("colorBuffer", colorBuffer);

		uint numIndices = (instanceMesh != null) ? (uint)instanceMesh.GetIndexCount(0) : 0;
		args[0] = numIndices;
		args[1] = (uint)instanceCount;
		argsBuffer.SetData(args);

		positionComputeShader.SetBuffer(positionComputeKernelId, "positionBuffer", positionBuffer);
		positionComputeShader.SetFloat("_Dim", Mathf.Sqrt(instanceCount));
	}

	private void UpdateBuffers () {
		positionComputeShader.SetFloat("_Time", Time.time);

		int bs = instanceCount / 64;
		positionComputeShader.Dispatch(positionComputeKernelId, bs, 1, 1);
	}

	void OnDisable () {
		if (positionBuffer != null) positionBuffer.Release();
		positionBuffer = null;
		if (colorBuffer != null) colorBuffer.Release();
		colorBuffer = null;
		if (argsBuffer != null) argsBuffer.Release();
		argsBuffer = null;
	}

	void OnGUI () {
		GUI.Label(new Rect(265, 12, 200, 30), "Instance Count: " + instanceCount.ToString("N0"));
	}
}

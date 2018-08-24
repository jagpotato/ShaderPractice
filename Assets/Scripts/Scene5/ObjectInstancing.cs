using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class ObjectInstancing : MonoBehaviour {
	[SerializeField]
	private int instanceCount;
	[SerializeField]
	private Mesh instanceMesh;
	[SerializeField]
	private Material instanceMaterial;
	[SerializeField]
	private ComputeShader insObjShader;

	private Vector3 attractor = new Vector3(10, 23, 8 / 3);

	private int cachedInstanceCount = -1;
	private ComputeBuffer insObjBuffer;
	private ComputeBuffer argsBuffer;
	private uint[] args = new uint[] {0, 0, 0, 0, 0};

	private int initKernel;
	private int updateKernel;


  struct Object {
  	Vector3 pos;
  	Vector3 vel;
  	Vector3 acc;
  	Vector3 rot;
  	Vector3 angVel;
  	Vector3 scale;
  	Vector4 col;
  };

	void Start () {
		argsBuffer = new ComputeBuffer(1, args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
		InitInsObjBuffer();
		UpdateBuffers();
	}
	
	void Update () {
		// if (cachedInstanceCount != instanceCount) UpdateBuffers();
		// else UpdateInsObjBuffer();
		// if (Input.GetAxis("Horizontal") != 0.0f) instanceCount = (int)Mathf.Clamp(instanceCount + Input.GetAxis("Horizontal") * 40000, 1.0f, 5000000.0f);
		UpdateInsObjBuffer();
		Graphics.DrawMeshInstancedIndirect(instanceMesh, 0, instanceMaterial, new Bounds(Vector3.zero, new Vector3(100.0f, 100.0f, 100.0f)), argsBuffer);
	}

	// void OnGUI () {
	// 	GUI.Label(new Rect(265, 25, 200, 30), "Instance Count: " + instanceCount.ToString());
	// 	instanceCount = (int)GUI.HorizontalSlider(new Rect(25, 20, 200, 30), (float)instanceCount, 1.0f, 5000000.0f);
	// }

	void InitInsObjBuffer () {
		initKernel = insObjShader.FindKernel("Init");
		updateKernel = insObjShader.FindKernel("Update");

		if (insObjBuffer != null) insObjBuffer.Release();
		insObjBuffer = new ComputeBuffer(instanceCount, Marshal.SizeOf(typeof(Object)));

		insObjShader.SetBuffer(initKernel, "objects", insObjBuffer);

		// var posRange = new Vector3(1000.0f, 500.0f, 1000.0f);
		var posRange = new Vector3(50.0f, 50.0f, 50.0f);
		insObjShader.SetVector("posRange", posRange);
		insObjShader.Dispatch(initKernel, instanceCount, 1, 1);
	}

	void UpdateInsObjBuffer () {
		insObjShader.SetBuffer(updateKernel, "objects", insObjBuffer);
		insObjShader.SetFloat("deltaTime", Time.deltaTime);
		insObjShader.SetVector("attractor", attractor);
		insObjShader.Dispatch(updateKernel, instanceCount, 1, 1);
		instanceMaterial.SetBuffer("objects", insObjBuffer);
	}

	void UpdateBuffers () {
		InitInsObjBuffer();
		instanceMaterial.SetBuffer("objects", insObjBuffer);

		uint numIndices = (instanceMesh != null) ? (uint)instanceMesh.GetIndexCount(0) : 0;
		args[0] = numIndices;
		args[1] = (uint)instanceCount;
		argsBuffer.SetData(args);
		Debug.Log(instanceCount);

		cachedInstanceCount = instanceCount;
	}

	void OnDisable () {
		if (argsBuffer != null) argsBuffer.Release();
		argsBuffer = null;
		if (insObjBuffer != null) insObjBuffer.Release();
		insObjBuffer = null;
	}
}

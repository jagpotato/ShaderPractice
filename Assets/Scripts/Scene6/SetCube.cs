using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCube : MonoBehaviour {
	[SerializeField]
	private int cubeNum;
	[SerializeField]
	private Mesh cube;
	[SerializeField]
	private ComputeShader setCubeShader;

  private ComputeBuffer setCubeBuffer;
	private ComputeBuffer argsBuffer;
	private uint[] args = new uint[] {0, 0, 0, 0, 0};
	void Start () {
		argsBuffer = new ComputeBuffer(1, args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
	}
	
	void Update () {
		
	}
}

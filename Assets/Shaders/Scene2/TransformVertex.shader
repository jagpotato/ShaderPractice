Shader "Custom/TransformVertex" {
	Properties {
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		struct Input {
			float4 color : COLOR;
		};

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void vert (inout appdata_full v) {
			v.vertex.x += 0.2 * v.normal.x * sin(v.vertex.y * 3.14 * 16);
			v.vertex.z += 0.2 * v.normal.z * sin(v.vertex.y * 3.14 * 16);
		}
		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = half3(1, 0.5, 0.5);
		}
		ENDCG
	}
	FallBack "Diffuse"
}

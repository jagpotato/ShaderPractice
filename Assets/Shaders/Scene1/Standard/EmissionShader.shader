Shader "Custom/EmissionShader" {
	Properties {
		_Color ("Diffuse Color", Color) = (1,1,1,1)
		_MyEmissionColor("Emission Color", Color) = (0, 0, 0, 0)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert

		float4 _Color;
		float4 _MyEmissionColor;

		struct Input {
			float2 Dummy;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = _Color;
			o.Emission = _MyEmissionColor;
		}
		ENDCG
	}
	FallBack "Diffuse"
}

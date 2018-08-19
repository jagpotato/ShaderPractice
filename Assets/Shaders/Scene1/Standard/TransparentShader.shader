Shader "Custom/TransparentShader" {
	Properties {
		_MyTexture("Select Texture", 2D) = "white"{}
		_MyAlpha("Alpha", Range(0, 1)) = 1
	}
	SubShader {
		Tags {
			"Queue"="Transparent"
			"RenderType"="Opaque"
		}

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert alpha

		sampler2D _MyTexture;
		float _MyAlpha;

		struct Input {
			float2 uv_MyTexture;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D(_MyTexture, IN.uv_MyTexture).rgb * _MyAlpha;
		  o.Alpha = _MyAlpha;
		}
		ENDCG
	}
	FallBack "Diffuse"
}

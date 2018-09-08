Shader "Unlit/Eg2_6"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}

	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#define PI 3.14159265359

			#include "UnityCG.cginc"

			float4 frag (v2f_img i) : SV_Target
	    {
				float2 st = 0.5 - i.uv;
				float a = atan2(st.y, st.x);
				float d = min(abs(cos(a * 2.5)) + 0.4, abs(sin(a * 2.5)) + 1.1) * 0.32;
				float r = length(st);

				float4 color = lerp(0.8, float4(0, 0.4, 1, 1), i.uv.y);

				float petal = step(r, d);
				color = lerp(color, lerp(float4(1, 0.3, 1, 1), 1, r * 2.5), petal);

				float cap = step(distance(0, st), 0.07);
				color = lerp(color, float4(0.99, 0.78, 0, 1), cap);

				return color;
	    }
			ENDCG
		}
	}
}

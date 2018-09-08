Shader "Unlit/Eg2_5"
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

			#include "UnityCG.cginc"

			float heart (float2 st)
			{
				st = (st - float2(0.5, 0.38)) * float2(2.1, 2.8);

				return pow(st.x, 2) + pow(st.y - sqrt(abs(st.x)), 2);
			}

			float4 frag (v2f_img i) : SV_Target
	    {
				float d = heart(i.uv);
				return step(d, abs(sin(d * 8 - _Time.w * 2)));
	    }
			ENDCG
		}
	}
}

﻿Shader "Unlit/Eg2_1"
{
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
						
			float4 frag (v2f_img i) : COLOR
			{
				return float4(i.uv, 0.0, 1.0);
			}
			ENDCG
		}
	}
}

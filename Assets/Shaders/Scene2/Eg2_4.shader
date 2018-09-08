Shader "Unlit/Eg2_4"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}

	CGINCLUDE
	#include "UnityCG.cginc"
	float4 frag (v2f_img i) : SV_Target
	{
		float d = distance(float2(0.5, 0.5), i.uv);
		d *= 30;
		d = abs(sin(d));
		d = step(0.5, d);
		return d;
	}
	ENDCG

	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			ENDCG
		}
	}
}

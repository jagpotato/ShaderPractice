Shader "Unlit/PixelCube"
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
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			#pragma target 4.5
			
			#include "UnityCG.cginc"
			#include "PixelCube.cginc"

			#if SHADER_TARGET >= 45
			StructuredBuffer<PixelCube> pixelCubes;
			#endif

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v, uint instanceID : SV_InstanceID)
			{
				// PixelCube cube;
				// #if SHADER_TARGET >= 45
				// cube = pixelCubes[instanceID];
				// #endif
				
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				// float4 ver = v.vertex;
				// ver.x += 2;
				// o.vertex = UnityObjectToClipPos(ver);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// fixed4 col = fixed4(1, 0, 0, 1);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}

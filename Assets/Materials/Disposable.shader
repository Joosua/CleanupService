Shader "Custom/Disposable" {
// Unlit alpha-cutout shader.
// - no lighting
// - no lightmap support
// - no per-material color

Properties {
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_DisposeTex ("_DisposeTex (RGB) Trans (A)", 2D) = "white" {}
	_Cutoff ("Alpha cutoff", Range(0,2)) = 0.5
}
SubShader {
	Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
	LOD 100

	Lighting Off

	Pass {  
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
				half2 texcoord1 : TEXCOORD1;
			};

			sampler2D _MainTex;
			sampler2D _DisposeTex;
			float4 _MainTex_ST;
			fixed _Cutoff;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.texcoord1 = TRANSFORM_TEX(v.texcoord1, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.texcoord);
				fixed4 disposecol = tex2D(_DisposeTex, i.texcoord1);
				col = lerp(col, col * disposecol, _Cutoff);
				//clip(col.a - _Cutoff);
				clip(disposecol.r - max(0.0, _Cutoff-1.0));
				return col;
			}
		ENDCG
	}
}

}
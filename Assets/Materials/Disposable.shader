Shader "Custom/Disposable" {
// Unlit alpha-cutout shader.
// - no lighting
// - no lightmap support
// - no per-material color

Properties {
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_DisposeTex ("_DisposeTex (RGB) Trans (A)", 2D) = "white" {}
	_Cutoff ("Alpha cutoff", Range(0,2)) = 0.5
	_FleshColor ("_FleshColor Color", Color) = (1,1,1,1)
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
			fixed4 _FleshColor;
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
				col = lerp(col, col * disposecol*disposecol, min(1.0,_Cutoff*3.0));
				float mask = smoothstep(0.5, 0.6, disposecol); 
				//fixed4 burntCol = lerp( col*disposecol*disposecol,_FleshColor, mask);//col * disposecol*disposecol;
				fixed4 burnt = col*disposecol*disposecol;
				burnt = lerp(burnt, _FleshColor, mask );
				//col = lerp(col, burntCol, _Cutoff);
				col = lerp(col, burnt, min(1.0,_Cutoff));
				//col = burnt;//fixed4(mask,mask,mask,mask);
				//col = lerp(col, _FleshColor,  mask);
				//col = burntCol;
				//col = fixed4(mask,mask,mask,mask);
				//clip(col.a - _Cutoff);
				clip(disposecol.r - max(0.0, _Cutoff-1.5));
				return col;
			}
		ENDCG
	}
}

}
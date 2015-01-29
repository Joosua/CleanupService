Shader "Custom/DisposableDiffuse" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_Cutoff ("Alpha cutoff", Range(0,3)) = 0.5
	_DisposeTex ("_DisposeTex (RGB) Trans (A)", 2D) = "white" {}
	_BurnColor ("_BurnColor Color", Color) = (1,1,1,1)
	_BurnMask ("Alpha cutoff", Range(0,3)) = 0.5
}

SubShader {
	Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
	LOD 200
	
CGPROGRAM
#pragma surface surf Lambert alphatest:_Cutoff

sampler2D _MainTex;
sampler2D _DisposeTex;
fixed4 _Color;
fixed4 _BurnColor;
fixed _BurnMask;
struct Input {
	float2 uv_MainTex;
	float2 uv2_DisposeTex;
	//float2 uv2_MainTex;
};

void surf (Input IN, inout SurfaceOutput o) {
	//fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	//o.Albedo = c.rgb;
	//o.Alpha = c.a;
	//o.Alpha = tex2D(_DisposeTex, IN.uv2_DisposeTex);
	
	fixed4 col = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	fixed4 disposecol = tex2D(_DisposeTex, IN.uv2_DisposeTex);
	col = lerp(col, col * disposecol*disposecol, min(1.0,_BurnMask*3.0));
	float mask = smoothstep(0.5, 0.6, disposecol); 
	fixed4 burnt = col*disposecol*disposecol;
	burnt = lerp(burnt, _BurnColor, mask );
	col = lerp(col, burnt, min(1.0,_BurnMask));
	o.Albedo = col.rgb;
	o.Alpha = (disposecol.r - max(0.0, _BurnMask-1.5));
	//return col;
} 
ENDCG
}

Fallback "Transparent/Cutout/VertexLit"
}

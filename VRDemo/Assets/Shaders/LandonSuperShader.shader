
Shader "Custom/LandonSuperShader"{
	Properties{
		_Diffuse	("Diffuse Texture (RGB)", 2D)		= "white"{}
		_NormalMap	("Normal Map (RGB)", 2D)			= "bump"{}
		
		_TintMaskTex ("Tint Mask (RGBA)", 2D)			= "white" {}
		_Color1 ("Color1", Color)						= (1, 0, 0, 1)
		_Color2 ("Color2", Color)						= (1, 1, 0, 1)
		_Color3 ("Color3", Color)						= (0, 1, 0, 1)
		_Color4 ("Color4", Color)						= (0, 1, 1, 1)
		
		_Wrapping	("Light Wrapping", Range(0.0, 1.0))	= 0.5
		_FresColor		("Fresnel Color (RGBA)", Color)		= (1.0, 1.0, 1.0, 1.0)
		_Factor		("Fresnel Multiplier", float)			= 0.5
		_FPow		("Fresnel Tightness", float)			= 2.0
		_SpecMap	("Specular Mask (A)", 2D)			= "gray"{}
		_SpecK		("Glossiness", float)				= 46.0
		_Strength	("Specularity", float)		= 2.0
	}
	
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf SmoothFresnel fullforwardshadows approxview noambient exclude_path:prepass
		#pragma target 3.0
		#pragma exclude_renderers flash
		
		struct Input{
			half2 uv_Diffuse, uv_NormalMap, uv_SpecMap;
			half3 viewDir;
		};
		
		sampler2D	_Diffuse, _NormalMap, _SpecMap, _TintMaskTex;
		half4		_FresColor, _Color1, _Color2, _Color3, _Color4;
		half		_Factor, _FPow, _SpecK, _Strength;
		fixed		_Wrapping;
		
		half4 LightingSmoothFresnel(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten){
			half3 wrap = atten * 2.0 * dot(s.Normal, lightDir + (viewDir * _Wrapping));
			half diff = dot(s.Normal, lightDir) * 0.5 + 0.5;
			half spec = _Strength * s.Specular * pow(saturate(dot(normalize(lightDir + viewDir), s.Normal)), _SpecK);
			
			half4 c;
			c.rgb = wrap * ((s.Albedo * _LightColor0.rgb * diff) + (spec * lerp(s.Albedo, _LightColor0.rgb,0.25)));
			c.a = s.Alpha;
			return c;
		}
		
		void surf(Input IN, inout SurfaceOutput o){
			half4 m = tex2D (_TintMaskTex, IN.uv_Diffuse);
			half4 res = m;
			res = (_Color1.rgba * m.r) + (_Color2.rgba * m.g) + (_Color3.rgba * m.b) + (_Color4.rgba * m.a);
		
			o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
			half fresnel = _Factor * pow(1.0 - dot(normalize(IN.viewDir), o.Normal), _FPow);
			o.Specular = tex2D(_SpecMap, IN.uv_SpecMap).a;
			o.Albedo = tex2D(_Diffuse, IN.uv_Diffuse).rgb * res;
			o.Emission = lerp(o.Albedo, _FresColor.rgb, _FresColor.a) * fresnel;
		}
		
		ENDCG
	}
	FallBack "Diffuse"
}
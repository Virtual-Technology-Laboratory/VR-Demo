
Shader "Custom/LandonSuperShader2"{
	Properties{
		_Diffuse	("Diffuse Texture (RGB) Specular Mask (A)", 2D)	= "gray"{}
		_DifInfluence	("Diffuse Influence", Range(0.0, 1.0))		= 0.0
		_NormalMap	("Normal Map (RGB) Fresnel Mask (A)", 2D)		= "bump"{}
		_NormalMult	("Normal Multilpier", Range(0.0, 2.0))			= 1.0
		_Wrapping	("Light Wrapping", Range(0.0, 2.0))				= 1.0
		_TintMask ("Tint Mask (RGBA)", 2D)							= "white" {}
		_Color1 ("Color1", Color)							= (.5, .2, .1, 1)
		_Color2 ("Color2", Color)							= (.1, .5, .2, 1)
		_Color3 ("Color3", Color)							= (.2, .1, .5, 1)
		_Color4 ("Color4", Color)							= (.5, .1, .2, 1)
		_Gloss		("Glossiness", float)					= 24
		_SpecularColor	("Specular Color (RGBA)", Color)	= (.3, .18, .16, 1.0)
		_Factor		("Fresnel Multiplier", float)			= 0.38
		_FPow		("Fresnel Tightness", float)			= -0.32
		_FresColor		("Fresnel Color (RGBA)", Color)		= (.2, .32, .43, .9)
	}
	
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf SmoothFresnel fullforwardshadows approxview noambient exclude_path:prepass
		#pragma target 3.0
		#pragma exclude_renderers flash
		
		struct Input{
			half2 uv_Diffuse, uv_NormalMap;
			half3 viewDir;
		};
		
		sampler2D	_Diffuse, _NormalMap, _TintMask;
		half4		_FresColor, _Color1, _Color2, _Color3, _Color4, _SpecularColor;
		half		_Factor, _FPow, _Gloss;
		fixed		_Wrapping, _DifInfluence, _NormalMult;
		
		half4 LightingSmoothFresnel(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten){
			half3 wrap = atten * 2.0 * dot(s.Normal, lightDir + (viewDir * _Wrapping));
			half diff = dot(s.Normal, lightDir) * 0.5 + 0.5;
			half3 spec = _SpecularColor.rgba * 50 * s.Specular * pow(saturate(dot(normalize(lightDir + viewDir), s.Normal)), _Gloss);
			
			half4 c;
			c.rgb = wrap * ((s.Albedo * _LightColor0.rgb * diff) + (spec * lerp(s.Albedo, _LightColor0.rgb,0.25)));
			c.a = s.Alpha;
			return c;
		}
		
		void surf(Input IN, inout SurfaceOutput o){
			half4 m = tex2D (_TintMask, IN.uv_Diffuse);
			half4 res = m;
			res = (_Color1.rgba * m.r) + (_Color2.rgba * m.g) + (_Color3.rgba * m.b) + (_Color4.rgba * (1-m.a));
			float3 tintInfluence = 1-_DifInfluence;
		
			o.Normal = _NormalMult * UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
			half fresnel = _Factor * tex2D(_NormalMap, IN.uv_NormalMap).a * pow(1.0 - dot(normalize(IN.viewDir), o.Normal), _FPow);
			o.Specular = tex2D(_Diffuse, IN.uv_Diffuse).a;
			o.Albedo = tex2D(_Diffuse, IN.uv_Diffuse).rgb*_DifInfluence + res*tintInfluence;
			o.Emission = lerp(o.Albedo, _FresColor.rgb, _FresColor.a) * fresnel;
		}
		
		ENDCG
	}
	FallBack "Diffuse"
}
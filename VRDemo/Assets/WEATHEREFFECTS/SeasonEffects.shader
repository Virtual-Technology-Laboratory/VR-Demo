Shader "Custom/SeasonEffects" {
	Properties {
		_SnowDirection ("Snow direction", Vector) = (0, 1, 0)
		_SnowLevel ("Amount of Snow", Range(1, -1)) = 0
		_MainTex ("MainTexture", 2D) = "gray" {}
		_SnowTex ("MainTexture", 2D) = "white" {}
		_Bump ("MainBumpTex", 2D) = "bump" {}
		_BumpSnow ("SnowBumpTex", 2D) = "bump" {}
	}

	SubShader {
		Tags { "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert

		half4 _SnowColor;
		sampler2D _MainTex, _SnowTex, _Bump, _BumpSnow;
		half3 _SnowDirection;
		fixed _SnowLevel;

		struct Input {
			float2 uv_MainTex;
			float3 worldNormal;
			INTERNAL_DATA
		};

		void surf(Input IN, inout SurfaceOutput o)
		{
			half4 tex = tex2D(_MainTex, IN.uv_MainTex);
			half4 snowTex = tex2D(_SnowTex, IN.uv_MainTex);
			o.Normal = UnpackNormal (tex2D(_Bump, IN.uv_MainTex));
			o.Albedo = 1-dot(WorldNormalVector(IN, o.Normal), _SnowDirection)*tex.rgb + dot(WorldNormalVector(IN, o.Normal), _SnowDirection)*snowTex.rgb;






		//	if(dot(WorldNormalVector(IN, o.Normal), _SnowDirection) >= _SnowLevel)
		//	{
		//		o.Albedo = _SnowColor.rgb;
		//		o.Normal = UnpackNormal (tex2D(_BumpSnow, IN.uv_MainTex));
		//	}
		//	else
		//	{
		//		o.Albedo = tex.rgb;
		//	}





		}
		ENDCG
	}
}
Shader ".: nappycat :./Basic/SpecTexture"
{
	Properties
	{
		_MainTex ("Texture Map", 2D) = "white" {}
		_BumpMap ("Normal Map", 2D) = "bumpp" {}
		_SpecMap ("Specular Map", 2D) = "black" {}
		_SpeColor ("Specular Color", color) = (0.5, 0.5, 0.5, 1.0)
		_SpecPower ("Specular Power", Range (0, 1)) = 0.5
		_EmitMap	("Emissive", 2D) = "black" {}
		_EmitPower ("Emit Power" Range(0, 2) = 0.1
	}

	SubShader
	{
		Tags {"RenderType"= "Opaque"}

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf BlinnPhong

		sampler2D _MainTex;

		sampler2D _BumpMap;

		sampler2D _SpecMap;
		float4 _SpeColor;
		float _SpecPower;

		sampler2D _EmitMap;
		float _EmitPower;


		struct Input
		{
			float2 uv_MainTex; // (1.0, 1.0)  - [U, V]
			float2 uv_BumpMap;
			float2 uv_SpecMap;
			float2 uv_EmitMap;
		};

		void surf (Input IN, inout SurfaceOutput o)
		{
			float4 tex = tex2D(_MainTex, IN.uv_MainTex);
			float4 specTex = tex2D(_SpecMap, IN.uv_MainTex);
			float4 emitTex = tex2D(_EmitMap, IN.uv_EmitTex);

			o.Albedo = tex.rgb;
			o.Normal = UnpackNormal (tex2D(_BumpMap, IN.uv_BumpMap));
			o.Specular = _SpecPower * specTex;
			o.Gloss = specTex.rgb;
			o.Emission = emitTex.rgb * _EmitPower;
		}

		ENDCG
	}
	FallBack "Diffuse"
}



// Shader "Custom/SimpleShader" {
//	Properties {
//		_Color ("Color", Color) = (1,1,1,1)
//		_MainTex ("Albedo (RGB)", 2D) = "white" {}
//		_Glossiness ("Smoothness", Range(0,1)) = 0.5
//		_Metallic ("Metallic", Range(0,1)) = 0.0
//	}
//	SubShader {
//		Tags { "RenderType"="Opaque" }
//		LOD 200
		
//		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
//		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
//		#pragma target 3.0

//		sampler2D _MainTex;

//		struct Input {
//			float2 uv_MainTex;
//		};

//		half _Glossiness;
//		half _Metallic;
//		fixed4 _Color;

//		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
//			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
//			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
//			o.Metallic = _Metallic;
//			o.Smoothness = _Glossiness;
//			o.Alpha = c.a;
//		}
//		ENDCG
//	}
//	FallBack "Diffuse"
//}

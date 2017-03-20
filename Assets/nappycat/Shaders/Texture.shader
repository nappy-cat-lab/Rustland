Shader ".: nappycat :./Basic/SpecTexture"
{
	Properties
	{
		_MainTex ("Texture Map", 2D) = "white" {}
		_BumpMap ("Normal Map", 2D) = "bumpp" {}
	}

	SubShader
	{
		Tags {"RenderType"= "Opaque"}

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert

		struct Input
		{
			float2 uv_MainTex; // (1.0, 1.0)  - [U, V]
			float2 uv_BumpMap;
		};

		sampler2D _MainTex;
		sampler2D _BumpMap;

		void surf (Input IN, inout SurfaceOutput o)
		{
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
			o.Normal = UnpackNormal (tex2D(_BumpMap, IN.uv_BumpMap));
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

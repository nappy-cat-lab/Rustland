Shader ".: nappycat :./Basic/Simple"
{
	SubShader
	{
		Tags {"RenderType"= "Opaque"}

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert

		struct Input
		{
			float4 color : COLOR; //(1.0, 1.0, 1.0, 1.0) R,G,B,A
		};

		void surf (Input IN, inout SurfaceOutput o)
		{
			o.Albedo = 1;
		}

		ENDCG
	}
	FallBack "Diffuse"
}
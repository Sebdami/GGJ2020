// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Lanterne"
{
	Properties
	{
		_Env_Props("Env_Props", 2D) = "white" {}
		_Env_Props_mask("Env_Props_mask", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Env_Props;
		uniform float4 _Env_Props_ST;
		uniform sampler2D _Env_Props_mask;
		uniform float4 _Env_Props_mask_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Env_Props = i.uv_texcoord * _Env_Props_ST.xy + _Env_Props_ST.zw;
			float4 tex2DNode1 = tex2D( _Env_Props, uv_Env_Props );
			o.Albedo = tex2DNode1.rgb;
			float2 uv_Env_Props_mask = i.uv_texcoord * _Env_Props_mask_ST.xy + _Env_Props_mask_ST.zw;
			float lerpResult4 = lerp( 0.0 , tex2DNode1.b , tex2D( _Env_Props_mask, uv_Env_Props_mask ).r);
			float4 color6 = IsGammaSpace() ? float4(7.245283,2.903517,0.03417594,0) : float4(78.00489,10.43359,0.002645197,0);
			float mulTime17 = _Time.y * 3.0;
			float ifLocalVar18 = 0;
			if( ( _SinTime.w * 2.0 ) <= 0.5 )
				ifLocalVar18 = 1.0;
			else
				ifLocalVar18 = frac( mulTime17 );
			float4 lerpResult12 = lerp( float4( 0,0,0,0 ) , ( lerpResult4 * color6 ) , ifLocalVar18);
			o.Emission = lerpResult12.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17700
193;595;1920;863;1339.401;-36.36096;1;True;False
Node;AmplifyShaderEditor.SamplerNode;1;-899,-203.5;Inherit;True;Property;_Env_Props;Env_Props;0;0;Create;True;0;0;False;0;-1;a185eade4909af5428e9ae2f15ed2734;a185eade4909af5428e9ae2f15ed2734;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-1057,63.5;Inherit;True;Property;_Env_Props_mask;Env_Props_mask;1;0;Create;True;0;0;False;0;-1;139799f37f7a3e945a1a899a3880f8a5;139799f37f7a3e945a1a899a3880f8a5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;17;-1179.974,609.2292;Inherit;False;1;0;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinTimeNode;10;-1074.675,363.4291;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;4;-614,4.5;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-882.4011,840.361;Inherit;False;Constant;_Float1;Float 1;2;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;11;-900.6746,576.4291;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-712.4011,501.361;Inherit;False;Constant;_Float0;Float 0;2;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-881.4011,439.361;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;6;-684.2888,264.9002;Inherit;False;Constant;_Color0;Color 0;2;1;[HDR];Create;True;0;0;False;0;7.245283,2.903517,0.03417594,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-337,113.5;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ConditionalIfNode;18;-493.4011,509.361;Inherit;True;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;12;-73.67456,269.4291;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;86,-150;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Lanterne;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;1;1;3
WireConnection;4;2;2;0
WireConnection;11;0;17;0
WireConnection;21;0;10;4
WireConnection;5;0;4;0
WireConnection;5;1;6;0
WireConnection;18;0;21;0
WireConnection;18;1;19;0
WireConnection;18;2;11;0
WireConnection;18;3;20;0
WireConnection;18;4;20;0
WireConnection;12;1;5;0
WireConnection;12;2;18;0
WireConnection;0;0;1;0
WireConnection;0;2;12;0
ASEEND*/
//CHKSM=8C175D99BFC9CB22B4763DF0F7F2CE808F97C170
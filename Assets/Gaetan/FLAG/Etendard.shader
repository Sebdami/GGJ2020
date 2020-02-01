// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Etendard"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Flag_Albedo("Flag_Albedo", 2D) = "white" {}
		_Env_Flag("Env_Flag", 2D) = "white" {}
		_Intensity("Intensity", Float) = 0.15
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform float _Intensity;
		uniform sampler2D _Flag_Albedo;
		uniform float4 _Flag_Albedo_ST;
		uniform sampler2D _Env_Flag;
		uniform float4 _Env_Flag_ST;
		uniform float _Cutoff = 0.5;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float2 panner5 = ( 1.0 * _Time.y * float2( 1,0 ) + ( ase_worldPos * 1 ).xy);
			float simplePerlin2D2 = snoise( panner5 );
			simplePerlin2D2 = simplePerlin2D2*0.5 + 0.5;
			float3 ase_vertex3Pos = v.vertex.xyz;
			float lerpResult9 = lerp( ( simplePerlin2D2 * _Intensity ) , 0.0 , step( 0.0 , ( ase_vertex3Pos.y - 1.0 ) ));
			float3 temp_cast_1 = (lerpResult9).xxx;
			v.vertex.xyz += temp_cast_1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Flag_Albedo = i.uv_texcoord * _Flag_Albedo_ST.xy + _Flag_Albedo_ST.zw;
			o.Albedo = tex2D( _Flag_Albedo, uv_Flag_Albedo ).rgb;
			o.Alpha = 1;
			float2 uv_Env_Flag = i.uv_texcoord * _Env_Flag_ST.xy + _Env_Flag_ST.zw;
			float simplePerlin2D22 = snoise( i.uv_texcoord*5.0 );
			simplePerlin2D22 = simplePerlin2D22*0.5 + 0.5;
			float4 temp_cast_1 = (step( 0.25 , simplePerlin2D22 )).xxxx;
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float4 lerpResult29 = lerp( tex2D( _Env_Flag, uv_Env_Flag ) , temp_cast_1 , ( ( 1.0 - ase_vertex3Pos.y ) - 1.5 ));
			clip( lerpResult29.r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17700
510;587;1920;869;2425.865;622.0878;1.46571;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;3;-1429.61,-94.5044;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ScaleNode;4;-1163.11,-51.60431;Inherit;False;1;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.PannerNode;5;-949.21,-52.80431;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;31;-1950.22,-493.3824;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PosVertexDataNode;7;-921.3103,299.3953;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;27;-653.093,487.6001;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-502.14,204.4898;Inherit;False;Property;_Intensity;Intensity;3;0;Create;True;0;0;False;0;0.15;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;20;-659.8943,322.1746;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;2;-683.4102,-32.10422;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;22;-1646.871,-491.1231;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;24;-1294.72,-446.5998;Inherit;True;2;0;FLOAT;0.25;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-348.5103,94.29535;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;13;-563.8507,-405.4498;Inherit;True;Property;_Env_Flag;Env_Flag;2;0;Create;True;0;0;False;0;-1;92f38222612fa49499ce1034a3ad8552;92f38222612fa49499ce1034a3ad8552;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;19;-445.6671,317.3838;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;28;-433.3282,492.4642;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;1.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;12;-539.2811,-596.1437;Inherit;True;Property;_Flag_Albedo;Flag_Albedo;1;0;Create;True;0;0;False;0;-1;bdff9396ddef5cb4e8feda50d6137a01;bdff9396ddef5cb4e8feda50d6137a01;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;9;-136.7103,111.5953;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;29;-242.1007,-246.5548;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;206,-137;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Etendard;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;False;TransparentCutout;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;0;3;0
WireConnection;5;0;4;0
WireConnection;27;0;7;2
WireConnection;20;0;7;2
WireConnection;2;0;5;0
WireConnection;22;0;31;0
WireConnection;24;1;22;0
WireConnection;6;0;2;0
WireConnection;6;1;21;0
WireConnection;19;1;20;0
WireConnection;28;0;27;0
WireConnection;9;0;6;0
WireConnection;9;2;19;0
WireConnection;29;0;13;0
WireConnection;29;1;24;0
WireConnection;29;2;28;0
WireConnection;0;0;12;0
WireConnection;0;10;29;0
WireConnection;0;11;9;0
ASEEND*/
//CHKSM=0CDA678ECAE6CE12415699E1395EB4D8ED98AA86
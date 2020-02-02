// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Sample Rotate"
{
	Properties
	{
		_Rotation("Rotation", Float) = 0
		[Toggle]_AnimatedRotation("Animated Rotation", Float) = 1
		_Env_WindMill("Env_WindMill", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
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

		uniform float _AnimatedRotation;
		uniform float _Rotation;
		uniform sampler2D _Env_WindMill;
		uniform float4 _Env_WindMill_ST;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float temp_output_5_0 = ( 0.0 + (( _AnimatedRotation )?( _Time.y ):( _Rotation )) );
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float4 transform2 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float4 temp_output_6_0 = ( ( float4( ase_worldPos , 0.0 ) - transform2 ) + float4( 0,0,0,0 ) );
			float4 appendResult22 = (float4(( ( sin( temp_output_5_0 ) * (temp_output_6_0).y ) + ( (temp_output_6_0).x * cos( temp_output_5_0 ) ) ) , ( ( cos( temp_output_5_0 ) * (temp_output_6_0).y ) - ( (temp_output_6_0).x * sin( temp_output_5_0 ) ) ) , (temp_output_6_0).z , 0.0));
			v.vertex.xyz += ( appendResult22 - temp_output_6_0 ).xyz;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Env_WindMill = i.uv_texcoord * _Env_WindMill_ST.xy + _Env_WindMill_ST.zw;
			o.Albedo = tex2D( _Env_WindMill, uv_Env_WindMill ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17700
344;652;1920;827;2686.386;568.9758;1.6;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;1;-3059.015,-676.1965;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ObjectToWorldTransfNode;2;-3064.015,-483.1965;Inherit;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;26;-3026.864,-797.2829;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-3016.865,-899.2822;Float;False;Property;_Rotation;Rotation;0;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;28;-2731.969,-882.943;Float;False;Property;_AnimatedRotation;Animated Rotation;1;0;Create;True;0;0;False;0;1;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;3;-2710.015,-503.1965;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleAddOpNode;5;-2406.115,-605.4965;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;6;-2382.415,-83.59702;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ComponentMaskNode;14;-1969.082,-204.1679;Inherit;False;True;False;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;13;-1964.948,263.3652;Inherit;False;False;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;8;-1935.282,128.6322;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;9;-1976.241,399.1161;Inherit;False;True;False;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CosOpNode;10;-1956.648,547.3792;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;11;-1967.782,-378.3678;Inherit;False;False;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CosOpNode;12;-1923.582,-546.0676;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;7;-1920.982,-71.56822;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-1654.482,-145.668;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-1689.244,483.2451;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-1692.582,179.3323;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-1705.182,-469.3678;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;20;-1425.576,283.2123;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;19;-1434.782,-302.9679;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;21;-1967.838,672.1143;Inherit;False;False;False;True;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;22;-1113.703,-61.53099;Inherit;True;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;23;-668.249,233.7083;Inherit;True;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SamplerNode;29;-403.0217,-223.0964;Inherit;True;Property;_Env_WindMill;Env_WindMill;2;0;Create;True;0;0;False;0;-1;c49ae99c79fb0314aa42bccfc6dfc81d;c49ae99c79fb0314aa42bccfc6dfc81d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Sample Rotate;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;28;0;4;0
WireConnection;28;1;26;0
WireConnection;3;0;1;0
WireConnection;3;1;2;0
WireConnection;5;1;28;0
WireConnection;6;0;3;0
WireConnection;14;0;6;0
WireConnection;13;0;6;0
WireConnection;8;0;5;0
WireConnection;9;0;6;0
WireConnection;10;0;5;0
WireConnection;11;0;6;0
WireConnection;12;0;5;0
WireConnection;7;0;5;0
WireConnection;15;0;14;0
WireConnection;15;1;7;0
WireConnection;18;0;9;0
WireConnection;18;1;10;0
WireConnection;16;0;8;0
WireConnection;16;1;13;0
WireConnection;17;0;12;0
WireConnection;17;1;11;0
WireConnection;20;0;16;0
WireConnection;20;1;18;0
WireConnection;19;0;17;0
WireConnection;19;1;15;0
WireConnection;21;0;6;0
WireConnection;22;0;20;0
WireConnection;22;1;19;0
WireConnection;22;2;21;0
WireConnection;23;0;22;0
WireConnection;23;1;6;0
WireConnection;0;0;29;0
WireConnection;0;11;23;0
ASEEND*/
//CHKSM=94BE98772F28460C84492F095387F08EA61F8A85
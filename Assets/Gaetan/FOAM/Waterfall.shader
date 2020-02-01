// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Waterfall"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_alpha_ok("alpha_ok", 2D) = "white" {}
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Fade_G("Fade_G", Float) = 1.5
		_Fade_R("Fade_R", Float) = 0.3
		_Mask_size("Mask_size", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform sampler2D _alpha_ok;
		uniform float _Fade_R;
		uniform sampler2D _TextureSample0;
		uniform float _Fade_G;
		uniform float _Mask_size;
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


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 color74 = IsGammaSpace() ? float4(1,1,1,0) : float4(1,1,1,0);
			o.Albedo = color74.rgb;
			o.Alpha = 1;
			float mulTime38 = _Time.y * 0.1;
			float mulTime12 = _Time.y * 0.5;
			float2 appendResult13 = (float2(mulTime38 , mulTime12));
			float2 uv_TexCoord11 = i.uv_texcoord * float2( 3,2 ) + appendResult13;
			float smoothstepResult76 = smoothstep( 0.2 , 0.75 , tex2D( _alpha_ok, uv_TexCoord11 ).r);
			float lerpResult63 = lerp( 0.0 , smoothstepResult76 , pow( i.uv_texcoord.y , _Fade_R ));
			float smoothstepResult55 = smoothstep( 0.55 , 0.83 , lerpResult63);
			float mulTime41 = _Time.y * -0.05;
			float2 appendResult40 = (float2(mulTime41 , mulTime12));
			float2 uv_TexCoord34 = i.uv_texcoord * float2( 1,2 ) + appendResult40;
			float smoothstepResult75 = smoothstep( 0.2 , 0.75 , tex2D( _TextureSample0, uv_TexCoord34 ).g);
			float lerpResult56 = lerp( 0.0 , smoothstepResult75 , pow( i.uv_texcoord.y , _Fade_G ));
			float smoothstepResult36 = smoothstep( 0.2 , 0.8 , lerpResult56);
			float3 ase_worldPos = i.worldPos;
			float2 appendResult94 = (float2(( ase_worldPos.x * 0.2 ) , ( ase_worldPos.z * 0.2 )));
			float2 panner131 = ( _Time.y * float2( 1.5,0 ) + appendResult94);
			float simplePerlin2D80 = snoise( panner131 );
			simplePerlin2D80 = simplePerlin2D80*0.5 + 0.5;
			float smoothstepResult100 = smoothstep( 0.3 , 0.5 , pow( i.uv_texcoord.y , ( _Mask_size / 2.0 ) ));
			float smoothstepResult112 = smoothstep( 0.3 , 0.5 , pow( i.uv_texcoord.y , _Mask_size ));
			float Mask_du_boss125 = step( ( ( simplePerlin2D80 * smoothstepResult100 ) + ( 1.0 - smoothstepResult112 ) ) , 0.99 );
			float lerpResult127 = lerp( 0.0 , ( smoothstepResult55 + smoothstepResult36 ) , Mask_du_boss125);
			clip( lerpResult127 - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17101
988;350;1520;991;2827.683;306.8174;1.306017;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;88;-2354.479,4046.587;Inherit;True;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;92;-2011.312,4047.399;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;93;-2016.419,4168.347;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;108;-2031.313,2458.923;Inherit;False;Property;_Mask_size;Mask_size;5;0;Create;True;0;0;False;0;1;0.75;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;94;-1850.336,4113.804;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleTimeNode;38;-2341,64;Inherit;False;1;0;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;12;-2335.853,194.2741;Inherit;False;1;0;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;77;-2312.536,2778.145;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;124;-1948.314,3295.856;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;128;-2061.898,4316.234;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;41;-2332.653,334.2565;Inherit;False;1;0;FLOAT;-0.05;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;40;-2072.653,345.2565;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;131;-1663.899,4142.234;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1.5,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;110;-1681.876,2885.75;Inherit;False;Constant;_Float7;Float 7;10;0;Create;True;0;0;False;0;0.5;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;102;-1676.8,3663.256;Inherit;False;Constant;_Float5;Float 5;9;0;Create;True;0;0;False;0;0.5;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;13;-2110.086,97.87132;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PowerNode;109;-1742.448,2342.497;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;79;-1728.671,3126.804;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;111;-1693.995,2784.676;Inherit;False;Constant;_Float8;Float 8;8;0;Create;True;0;0;False;0;0.3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;101;-1680.218,3568.981;Inherit;False;Constant;_Float4;Float 4;6;0;Create;True;0;0;False;0;0.3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;80;-1424.867,4156.895;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;34;-1828.558,222.4743;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,2;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;11;-1820.188,-105.7291;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;3,2;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;100;-1394.017,3480.584;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;112;-1407.793,2696.278;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;69;-1023.501,-441.0367;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;120;-1091.945,2764.554;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;49;-1001.159,563.2832;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;52;-1469.62,226.1232;Inherit;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;a3b899217d109dd4d8741fa6d48f58f3;a3b899217d109dd4d8741fa6d48f58f3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;119;-983.8405,4167.18;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;60;-940.9312,736.597;Inherit;False;Property;_Fade_G;Fade_G;3;0;Create;True;0;0;False;0;1.5;1.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;51;-1492.609,-114.8058;Inherit;True;Property;_alpha_ok;alpha_ok;1;0;Create;True;0;0;False;0;a3b899217d109dd4d8741fa6d48f58f3;a3b899217d109dd4d8741fa6d48f58f3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;70;-1007.546,-303.8201;Inherit;False;Property;_Fade_R;Fade_R;4;0;Create;True;0;0;False;0;0.3;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;68;-744.7179,-413.0871;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;121;-730.6901,4191.45;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;75;-768.0106,248.9472;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0.2;False;2;FLOAT;0.75;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;76;-765.0586,-54.52744;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0.2;False;2;FLOAT;0.75;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;59;-712.6006,609.5099;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;123;-489.2135,4152.102;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0.99;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;63;-427.3326,-69.27237;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;56;-428.541,270.7174;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;36;-76.25938,268.5179;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0.2;False;2;FLOAT;0.8;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;125;-179.1527,4145.353;Inherit;False;Mask_du_boss;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;55;-54.15734,-74.25815;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0.55;False;2;FLOAT;0.83;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;71;295.6285,180.5492;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;126;220.2167,600.6395;Inherit;False;125;Mask_du_boss;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;127;529.984,489.5653;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;74;643.3449,129.3268;Inherit;False;Constant;_Color0;Color 0;5;0;Create;True;0;0;False;0;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;964.2563,236.1025;Float;False;True;2;ASEMaterialInspector;0;0;Standard;Waterfall;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;TransparentCutout;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;92;0;88;1
WireConnection;93;0;88;3
WireConnection;94;0;92;0
WireConnection;94;1;93;0
WireConnection;124;0;108;0
WireConnection;40;0;41;0
WireConnection;40;1;12;0
WireConnection;131;0;94;0
WireConnection;131;1;128;0
WireConnection;13;0;38;0
WireConnection;13;1;12;0
WireConnection;109;0;77;2
WireConnection;109;1;108;0
WireConnection;79;0;77;2
WireConnection;79;1;124;0
WireConnection;80;0;131;0
WireConnection;34;1;40;0
WireConnection;11;1;13;0
WireConnection;100;0;79;0
WireConnection;100;1;101;0
WireConnection;100;2;102;0
WireConnection;112;0;109;0
WireConnection;112;1;111;0
WireConnection;112;2;110;0
WireConnection;120;0;112;0
WireConnection;52;1;34;0
WireConnection;119;0;80;0
WireConnection;119;1;100;0
WireConnection;51;1;11;0
WireConnection;68;0;69;2
WireConnection;68;1;70;0
WireConnection;121;0;119;0
WireConnection;121;1;120;0
WireConnection;75;0;52;2
WireConnection;76;0;51;1
WireConnection;59;0;49;2
WireConnection;59;1;60;0
WireConnection;123;0;121;0
WireConnection;63;1;76;0
WireConnection;63;2;68;0
WireConnection;56;1;75;0
WireConnection;56;2;59;0
WireConnection;36;0;56;0
WireConnection;125;0;123;0
WireConnection;55;0;63;0
WireConnection;71;0;55;0
WireConnection;71;1;36;0
WireConnection;127;1;71;0
WireConnection;127;2;126;0
WireConnection;0;0;74;0
WireConnection;0;10;127;0
ASEEND*/
//CHKSM=E7D8E7A041BC119F62572C7C4F899F8CB890368A
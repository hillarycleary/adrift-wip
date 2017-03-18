// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "The Moose Factory/ Low Poly Water"
{
	Properties
	{
		_BaseColour("Water Colour", COLOR) = (1, 1, 1, 1)
		_ShoreDepth("ShoreDepth", Float) = 10
		_Edge("Edge", Float) = 10


	}

		SubShader
	{
		Tags{"RenderType" = "Opaque"}
		Blend SrcAlpha OneMinusSrcAlpha
		ZTest LEqual
		Pass
		{
			CGPROGRAM
			#pragma target 4.0
			#pragma vertex vert
			#pragma fragment frag
			#pragma geometry geom
			#include "UnityCG.cginc" 
			#include "UnityLightingCommon.cginc"

			fixed4 _BaseColour;
			uniform float _ShoreDepth;
			uniform float _Edge;
			sampler2D_float _CameraDepthTexture;
			uniform float4 _InvFadeParameter;

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct V2F
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float3 worldPosition : TEXCOORD0;
				float4 posWorld : TEXCOORD1;
				float4 screenPos : TEXCOORD2;
				float4 viewInt : TEXCOORD3;
			};
			

			V2F vert(appdata v)
			{
				V2F o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.normal = v.normal;
				o.worldPosition = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.screenPos = ComputeScreenPos(v.vertex);
				o.viewInt.xyz = (mul(unity_ObjectToWorld, (v.vertex)).xyz) - _WorldSpaceCameraPos;
				o.posWorld = mul(unity_ObjectToWorld, v.vertex);
				return o;
			}

			[maxvertexcount(3)]
			void geom(triangle V2F input[3], inout TriangleStream<V2F> OutputStream)
			{
				V2F test = (V2F)0;
				float3 normal = normalize(cross(input[1].worldPosition.xyz - input[0].worldPosition.xyz, input[2].worldPosition.xyz - input[0].worldPosition.xyz));
				for (int i = 0; i < 3; i++)
				{
					test.normal = normal;
					test.vertex = input[i].vertex;
					//test.posWorld = input[i].posWorld;
					test.viewInt.xyz = (mul(unity_ObjectToWorld, (input[i].vertex)).xyz) - _WorldSpaceCameraPos;
					test.screenPos = ComputeScreenPos(input[i].vertex);
					OutputStream.Append(test);
				}
			}

			fixed4 frag(V2F i): SV_TARGET
			{
				fixed4 col = _BaseColour;
				float3 viewDirection = normalize(_WorldSpaceCameraPos - i.posWorld.xyz);
				float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
				float3 ambientLight = UNITY_LIGHTMODEL_AMBIENT.rgb * _BaseColour.rgb;
				float ndotl = dot(i.normal, lightDir);
				float3 dRef = _LightColor0.rgb * _BaseColour.rgb * max(0.0, dot(ndotl, lightDir));
				float3 specRef = float3(0.0,0.0,0.0);

				if (dot(ndotl, lightDir) > 0.0)
				{
					specRef = _LightColor0.rgb * _SpecColor.rgb * pow(max(0.0, dot(reflect(-lightDir, ndotl), viewDirection)), 1.0);
				}
				else {
					specRef = float3(0, 0, 0);
				}

				float2 edges = float2(1.0, 0.0);
				half depth = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos));
				depth = LinearEyeDepth(depth);
				edges = saturate(float2(_Edge,_ShoreDepth) * (depth - i.screenPos.w));
				edges.y = 1.0 - edges.y;

				col.rgb += edges.y + saturate(i.viewInt.w);
				col.rgb += 1.0 - edges.x;
				return fixed4(col.rgb+ambientLight + dRef + specRef, col.a);
			}
			
			ENDCG
		}
	}

	FallBack Off
}
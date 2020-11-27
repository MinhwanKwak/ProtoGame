// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_dash"
{
    Properties
    {
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_T_shockwave_01("T_shockwave_01", 2D) = "white" {}
		__gra_011("__gra_01 1", 2D) = "white" {}
		_Uoffset("Uoffset", Float) = 0
		_Voffset("Voffset", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

    }

    SubShader
    {
		LOD 0

		

        Tags { "RenderPipeline"="LightweightPipeline" "RenderType"="Transparent" "Queue"="Transparent" }
        Cull Off
		HLSLINCLUDE
		#pragma target 3.0
		ENDHLSL

		
        Pass
        {
			
            Tags { "LightMode"="LightweightForward" }
            Name "Base"

            Blend SrcAlpha OneMinusSrcAlpha , One OneMinusSrcAlpha
			ZWrite Off
			ZTest LEqual
			Offset 0 , 0
			ColorMask RGBA
			

            HLSLPROGRAM
            #define ASE_SRP_VERSION 50702

            // Required to compile gles 2.0 with standard srp library
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x

            // -------------------------------------
            // Lightweight Pipeline keywords
            #pragma shader_feature _SAMPLE_GI

            // -------------------------------------
            // Unity defined keywords
			#ifdef ASE_FOG
            #pragma multi_compile_fog
			#endif
            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            
            #pragma vertex vert
            #pragma fragment frag


            // Lighting include is needed because of GI
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.render-pipelines.lightweight/Shaders/UnlitInput.hlsl"

            #define ASE_NEEDS_FRAG_COLOR


			sampler2D _T_shockwave_01;
			sampler2D _TextureSample0;
			sampler2D __gra_011;
			CBUFFER_START( UnityPerMaterial )
			float4 _TextureSample0_ST;
			float4 __gra_011_ST;
			float _Uoffset;
			float _Voffset;
			CBUFFER_END


            struct GraphVertexInput
            {
                float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct GraphVertexOutput
            {
                float4 position : POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 worldPos : TEXCOORD0;
				#endif
				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 shadowCoord : TEXCOORD1;
				#endif
				#ifdef ASE_FOG
				float fogCoord : TEXCOORD2;
				#endif
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_texcoord4 : TEXCOORD4;
				float4 ase_color : COLOR;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };

			
            GraphVertexOutput vert (GraphVertexInput v)
            {
                GraphVertexOutput o = (GraphVertexOutput)0;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.ase_texcoord3.xy = v.ase_texcoord.xy;
				o.ase_texcoord4 = v.ase_texcoord1;
				o.ase_color = v.ase_color;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord3.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				float3 defaultVertexValue = v.vertex.xyz;
				#else
				float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue =  defaultVertexValue ;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue; 
				#else
				v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal =  v.ase_normal ;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				float4 positionCS = TransformWorldToHClip( positionWS );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				o.worldPos = positionWS;
				#endif

                o.position = positionCS;
				#if defined(_MAIN_LIGHT_SHADOWS) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = positionCS;
        			o.shadowCoord = GetShadowCoord(vertexInput);
        		#endif
				#ifdef ASE_FOG
				o.fogCoord = ComputeFogFactor( positionCS.z );
				#endif
                return o;
            }

            half4 frag (GraphVertexOutput IN ) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 WorldPosition = IN.worldPos;
				#endif
				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 ShadowCoords = IN.shadowCoord;
				#endif

				float4 color52 = IsGammaSpace() ? float4(0.2773585,1,1,0) : float4(0.06252452,1,1,0);
				float2 appendResult83 = (float2(( IN.ase_texcoord4.x + _Uoffset ) , ( IN.ase_texcoord4.y + _Voffset )));
				float2 uv084 = IN.ase_texcoord3.xy * float2( 1.2,1 ) + appendResult83;
				float4 temp_cast_0 = (IN.ase_texcoord4.w).xxxx;
				float4 temp_output_54_0 = ( color52 * ( saturate( ( tex2D( _T_shockwave_01, uv084 ) - temp_cast_0 ) ) * 4.0 ) );
				float4 color49 = IsGammaSpace() ? float4(0.1790317,0.7594489,0.8867924,0) : float4(0.02694346,0.5373625,0.7615293,0);
				float3 appendResult39 = (float3(IN.ase_color.r , IN.ase_color.g , IN.ase_color.b));
				float2 uv_TextureSample0 = IN.ase_texcoord3.xy * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
				float4 tex2DNode10 = tex2D( _TextureSample0, uv_TextureSample0 );
				
				float2 uv__gra_011 = IN.ase_texcoord3.xy * __gra_011_ST.xy + __gra_011_ST.zw;
				
				float3 Color = ( temp_output_54_0 + ( color49 * float4( ( appendResult39 * tex2DNode10.g ) , 0.0 ) ) ).rgb;
				float Alpha = ( ( temp_output_54_0 + ( tex2DNode10.a * IN.ase_color.a ) ) * tex2D( __gra_011, uv__gra_011 ).a ).r;
				float AlphaClipThreshold = 0.5;
			
				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				#ifdef ASE_FOG
					Color = MixFog( Color, IN.fogCoord );
				#endif

				#ifdef LOD_FADE_CROSSFADE
					LODDitheringTransition (IN.clipPos.xyz, unity_LODFade.x);
				#endif

                return half4(Color, Alpha);
            }
            ENDHLSL
        }

		
        Pass
        {
			
            Name "ShadowCaster"
            Tags { "LightMode"="ShadowCaster" }
			ZWrite On
			ColorMask 0

            HLSLPROGRAM
            #define ASE_SRP_VERSION 50702

            // Required to compile gles 2.0 with standard srp library
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            
            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing

            #pragma vertex ShadowPassVertex
            #pragma fragment ShadowPassFragment


            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"

            

			sampler2D _T_shockwave_01;
			sampler2D _TextureSample0;
			sampler2D __gra_011;
			CBUFFER_START( UnityPerMaterial )
			float4 _TextureSample0_ST;
			float4 __gra_011_ST;
			float _Uoffset;
			float _Voffset;
			CBUFFER_END


            struct GraphVertexInput
            {
                float4 vertex : POSITION;
                float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct VertexOutput
            {
                float4 clipPos : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 worldPos : TEXCOORD0;
				#endif
				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 shadowCoord : TEXCOORD1;
				#endif
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_color : COLOR;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };

            float3 _LightDirection;

			
            VertexOutput ShadowPassVertex(GraphVertexInput v )
            {
                VertexOutput o = ( VertexOutput )0;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                
				o.ase_texcoord2.xy = v.ase_texcoord.xy;
				o.ase_texcoord3 = v.ase_texcoord1;
				o.ase_color = v.ase_color;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord2.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				float3 defaultVertexValue = v.vertex.xyz;
				#else
				float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue =  defaultVertexValue ;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal =  v.ase_normal ;

                float3 positionWS = TransformObjectToWorld(v.vertex.xyz);
                float3 normalWS = TransformObjectToWorldDir(v.ase_normal.xyz);

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				o.worldPos = positionWS;
				#endif

                float invNdotL = 1.0 - saturate(dot(_LightDirection, normalWS));
                float scale = invNdotL * _ShadowBias.y;

                positionWS = _LightDirection * _ShadowBias.xxx + positionWS;
				positionWS = normalWS * scale.xxx + positionWS;
                float4 clipPos = TransformWorldToHClip(positionWS);

				#if UNITY_REVERSED_Z
					clipPos.z = min(clipPos.z, clipPos.w * UNITY_NEAR_CLIP_VALUE);
				#else
					clipPos.z = max(clipPos.z, clipPos.w * UNITY_NEAR_CLIP_VALUE);
				#endif
				
				#if defined(_MAIN_LIGHT_SHADOWS) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = clipPos;
        			o.shadowCoord = GetShadowCoord(vertexInput);
        		#endif
                o.clipPos = clipPos;

                return o;
            }

            half4 ShadowPassFragment(VertexOutput IN  ) : SV_TARGET
            {
                UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);
				
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 WorldPosition = IN.worldPos;
				#endif
				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 ShadowCoords = IN.shadowCoord;
				#endif

        		float4 color52 = IsGammaSpace() ? float4(0.2773585,1,1,0) : float4(0.06252452,1,1,0);
        		float2 appendResult83 = (float2(( IN.ase_texcoord3.x + _Uoffset ) , ( IN.ase_texcoord3.y + _Voffset )));
        		float2 uv084 = IN.ase_texcoord2.xy * float2( 1.2,1 ) + appendResult83;
        		float4 temp_cast_0 = (IN.ase_texcoord3.w).xxxx;
        		float4 temp_output_54_0 = ( color52 * ( saturate( ( tex2D( _T_shockwave_01, uv084 ) - temp_cast_0 ) ) * 4.0 ) );
        		float2 uv_TextureSample0 = IN.ase_texcoord2.xy * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
        		float4 tex2DNode10 = tex2D( _TextureSample0, uv_TextureSample0 );
        		float2 uv__gra_011 = IN.ase_texcoord2.xy * __gra_011_ST.xy + __gra_011_ST.zw;
        		

				float Alpha = ( ( temp_output_54_0 + ( tex2DNode10.a * IN.ase_color.a ) ) * tex2D( __gra_011, uv__gra_011 ).a ).r;
				float AlphaClipThreshold = 0.5;
				#ifdef _ALPHATEST_ON
        			clip(Alpha - AlphaClipThreshold);
				#endif

				#ifdef LOD_FADE_CROSSFADE
					LODDitheringTransition (IN.clipPos.xyz, unity_LODFade.x);
				#endif
                return 0;
            }

            ENDHLSL
        }

		
        Pass
        {
			
            Name "DepthOnly"
            Tags { "LightMode"="DepthOnly" }

            ZWrite On
			ZTest LEqual
			ColorMask 0

            HLSLPROGRAM
            #define ASE_SRP_VERSION 50702

            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x

            #pragma multi_compile_instancing

            #pragma vertex vert
            #pragma fragment frag


            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"

            

			sampler2D _T_shockwave_01;
			sampler2D _TextureSample0;
			sampler2D __gra_011;
			CBUFFER_START( UnityPerMaterial )
			float4 _TextureSample0_ST;
			float4 __gra_011_ST;
			float _Uoffset;
			float _Voffset;
			CBUFFER_END


			struct GraphVertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

            struct VertexOutput
            {
                float4 clipPos : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 worldPos : TEXCOORD0;
				#endif
				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 shadowCoord : TEXCOORD1;
				#endif
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_color : COLOR;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };

			
			VertexOutput vert( GraphVertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.ase_texcoord2.xy = v.ase_texcoord.xy;
				o.ase_texcoord3 = v.ase_texcoord1;
				o.ase_color = v.ase_color;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord2.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				float3 defaultVertexValue = v.vertex.xyz;
				#else
				float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue =  defaultVertexValue ;	
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				v.ase_normal =  v.ase_normal ;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				float4 positionCS = TransformWorldToHClip( positionWS );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				o.worldPos = positionWS;
				#endif

				#if defined(_MAIN_LIGHT_SHADOWS) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = positionCS;
        			o.shadowCoord = GetShadowCoord(vertexInput);
        		#endif

				o.clipPos = positionCS;
				return o;
			}

            half4 frag( VertexOutput IN  ) : SV_TARGET
            {
                UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 WorldPosition = IN.worldPos;
				#endif
				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 ShadowCoords = IN.shadowCoord;
				#endif

				float4 color52 = IsGammaSpace() ? float4(0.2773585,1,1,0) : float4(0.06252452,1,1,0);
				float2 appendResult83 = (float2(( IN.ase_texcoord3.x + _Uoffset ) , ( IN.ase_texcoord3.y + _Voffset )));
				float2 uv084 = IN.ase_texcoord2.xy * float2( 1.2,1 ) + appendResult83;
				float4 temp_cast_0 = (IN.ase_texcoord3.w).xxxx;
				float4 temp_output_54_0 = ( color52 * ( saturate( ( tex2D( _T_shockwave_01, uv084 ) - temp_cast_0 ) ) * 4.0 ) );
				float2 uv_TextureSample0 = IN.ase_texcoord2.xy * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
				float4 tex2DNode10 = tex2D( _TextureSample0, uv_TextureSample0 );
				float2 uv__gra_011 = IN.ase_texcoord2.xy * __gra_011_ST.xy + __gra_011_ST.zw;
				

				float Alpha = ( ( temp_output_54_0 + ( tex2DNode10.a * IN.ase_color.a ) ) * tex2D( __gra_011, uv__gra_011 ).a ).r;
				float AlphaClipThreshold = 0.5;

				#ifdef _ALPHATEST_ON
        			clip(Alpha - AlphaClipThreshold);
				#endif
					return 0;
				#ifdef LOD_FADE_CROSSFADE
					LODDitheringTransition (IN.clipPos.xyz, unity_LODFade.x);
				#endif
            }
            ENDHLSL
        }
		
    }
    Fallback "Hidden/InternalErrorShader"
	CustomEditor "ASEMaterialInspector"
	
}
/*ASEBEGIN
Version=18100
46.4;448;1524;785;595.3714;946.1725;1.371712;True;False
Node;AmplifyShaderEditor.TexCoordVertexDataNode;75;-1114.91,-744.5064;Inherit;True;1;4;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;81;-1055.8,-488.1615;Inherit;False;Property;_Voffset;Voffset;5;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;80;-1077.547,-614.6259;Inherit;False;Property;_Uoffset;Uoffset;4;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;78;-868.5467,-721.6259;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;82;-866.5467,-612.6259;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;83;-694.5467,-692.6259;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;84;-509.5467,-698.6259;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1.2,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;42;-294.6536,-709.7639;Inherit;True;Property;_T_shockwave_01;T_shockwave_01;1;0;Create;True;0;0;False;0;False;-1;ac9b2fb8687245246b64602ef80dc1f1;ac9b2fb8687245246b64602ef80dc1f1;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;90;13.98186,-651.8622;Inherit;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;91;86.88726,-712.1815;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;56;243.7681,-539.501;Inherit;False;Constant;_Float0;Float 0;4;0;Create;True;0;0;False;0;False;4;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;52;92.11844,-886.9622;Inherit;False;Constant;_Color1;Color 1;4;0;Create;True;0;0;False;0;False;0.2773585,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;12;-795.5565,-406.6764;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;10;-539.8779,-140.6095;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;False;-1;b231480efff65854aa8029738a73883d;b231480efff65854aa8029738a73883d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;55;273.6892,-650.5897;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;328.1968,-752.7222;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;53.09775,-120.9315;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;71;556.3341,-371.6971;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;74;394.0442,-57.72909;Inherit;True;Property;__gra_011;__gra_01 1;3;0;Create;True;0;0;False;0;False;-1;4b89f6fad230fca4681742843f8e483c;4b89f6fad230fca4681742843f8e483c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;39;-368.442,-421.9604;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;73;761.9713,-354.4573;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;50;608.1586,-655.2523;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;51;238.6693,-390.6349;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-24.40295,-334.9066;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;49;7.005426,-505.2922;Inherit;False;Constant;_Color0;Color 0;4;0;Create;True;0;0;False;0;False;0.1790317,0.7594489,0.8867924,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;72;798.3435,47.57052;Inherit;True;Property;__gra_01;__gra_01;2;0;Create;True;0;0;False;0;False;-1;42ca65984c67dfb4b861457d8a56b021;42ca65984c67dfb4b861457d8a56b021;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;102;1107.385,-613.4215;Float;False;True;-1;2;ASEMaterialInspector;0;3;S_dash;e2514bdcf5e5399499a9eb24d175b9db;True;Base;0;1;Base;5;False;False;False;True;2;False;-1;False;False;False;False;False;True;3;RenderPipeline=LightweightPipeline;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;2;0;True;1;5;False;-1;10;False;-1;1;1;False;-1;10;False;-1;False;False;False;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;LightMode=LightweightForward;False;0;Hidden/InternalErrorShader;0;0;Standard;9;Surface;1;  Blend;0;Two Sided;0;Cast Shadows;1;Receive Shadows;1;Built-in Fog;0;LOD CrossFade;0;Extra Pre Pass;0;Vertex Position,InvertActionOnDeselection;1;0;4;False;True;True;True;False;;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;103;1107.385,-613.4215;Float;False;False;-1;2;ASEMaterialInspector;0;1;New Amplify Shader;e2514bdcf5e5399499a9eb24d175b9db;True;ShadowCaster;0;2;ShadowCaster;0;False;False;False;True;0;False;-1;False;False;False;False;False;True;3;RenderPipeline=LightweightPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;2;0;False;False;False;False;True;False;False;False;False;0;False;-1;False;True;1;False;-1;False;False;True;1;LightMode=ShadowCaster;False;0;Hidden/InternalErrorShader;0;0;Standard;0;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;101;1107.385,-613.4215;Float;False;False;-1;2;ASEMaterialInspector;0;1;New Amplify Shader;e2514bdcf5e5399499a9eb24d175b9db;True;ExtraPrePass;0;0;ExtraPrePass;5;False;False;False;True;0;False;-1;False;False;False;False;False;True;3;RenderPipeline=LightweightPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;2;0;True;1;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;False;False;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;0;False;0;Hidden/InternalErrorShader;0;0;Standard;0;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;104;1107.385,-613.4215;Float;False;False;-1;2;ASEMaterialInspector;0;1;New Amplify Shader;e2514bdcf5e5399499a9eb24d175b9db;True;DepthOnly;0;3;DepthOnly;0;False;False;False;True;0;False;-1;False;False;False;False;False;True;3;RenderPipeline=LightweightPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;2;0;False;False;False;False;True;False;False;False;False;0;False;-1;False;True;1;False;-1;True;3;False;-1;False;True;1;LightMode=DepthOnly;False;0;Hidden/InternalErrorShader;0;0;Standard;0;0
WireConnection;78;0;75;1
WireConnection;78;1;80;0
WireConnection;82;0;75;2
WireConnection;82;1;81;0
WireConnection;83;0;78;0
WireConnection;83;1;82;0
WireConnection;84;1;83;0
WireConnection;42;1;84;0
WireConnection;90;0;42;0
WireConnection;90;1;75;4
WireConnection;91;0;90;0
WireConnection;55;0;91;0
WireConnection;55;1;56;0
WireConnection;54;0;52;0
WireConnection;54;1;55;0
WireConnection;14;0;10;4
WireConnection;14;1;12;4
WireConnection;71;0;54;0
WireConnection;71;1;14;0
WireConnection;39;0;12;1
WireConnection;39;1;12;2
WireConnection;39;2;12;3
WireConnection;73;0;71;0
WireConnection;73;1;74;4
WireConnection;50;0;54;0
WireConnection;50;1;51;0
WireConnection;51;0;49;0
WireConnection;51;1;13;0
WireConnection;13;0;39;0
WireConnection;13;1;10;2
WireConnection;102;0;50;0
WireConnection;102;1;73;0
ASEEND*/
//CHKSM=385392CDBC7BE25B0957233EE142DD71792ADD21
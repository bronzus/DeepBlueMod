Shader "KZ/ToonFish"
{
    Properties
    {
        [MainTexture] _BaseMap            ("Texture", 2D)                       = "white" {}
        [MainColor] _BaseColor          ("Color", Color)                      = (0.5,0.5,0.5,1)
        
        [Space]
        [Normal]_NormalTex ("_NormalTex", 2D) = "bump"{}
        _NormalScale("_NormalScale",Range(0,1)) = 1
        
        [Space]
        _TattooTex ("Tattoo Texture", 2D) = "white" {}
        _TattooOpacity ("Tattoo Opacity", Range(0,1)) = 0.0
        
        [Space]
        _ShadowStep         ("ShadowStep", Range(0, 1))           = 0.5
        _ShadowStepSmooth   ("ShadowStepSmooth", Range(0, 1))     = 0.2
        
        [Space] 
        _SpecularStep       ("SpecularStep", Range(0, 1))         = 0
        _SpecularStepSmooth ("SpecularStepSmooth", Range(0, 1))   = 0.035
        [HDR]_SpecularColor ("SpecularColor", Color)              = (1,1,1,1)
        
        [Space]
        _RimStep            ("RimStep", Range(0, 1))              = 0
        _RimStepSmooth      ("RimStepSmooth",Range(0,1))          = 0
        _RimColor           ("RimColor", Color)                   = (1,1,1,1)
        
        [Space]   
        _OutlineWidth      ("OutlineWidth", Range(0.0, 1.0))      = 0.15
        _OutlineColor      ("OutlineColor", Color)                = (0.0, 0.0, 0.0, 1)
        
        [Space(12)]
        [HideInInspector]_StopTime("Stop Time", Float) = -1 // 如果这个大于等于 0 则表示该鱼类停止运动，并表示停止的时间
        [HideInInspector]_TimeOffset("time offset", Float) = 0
        
        [Space(5)]
        [Header(Side To Side Movement)]
        [Space(5)]
        [Toggle(APPLY_SIDE_MOVE)]
        _ApplySideMove ("APPLY SIDE MOVE", Float) = 0
        _SideDisplaceFreq("SideDisplace Freq", Range(0,10)) = 5.5
        _SideDisplaceLength("SideDisplace Length", Range(0,300)) = 0
        _SideDisplaceCorrectFreq("SideDisplace Correct Freq", Float) = 0
        [Space(12)]
        [Header(Roll Along Spine)]
        [Space(5)]
        [Toggle(APPLY_SPINE_ROLL)]
        _ApplySpineRoll ("APPLY SPINE ROLL", Float) = 0
        _RollLength("Roll Length", Range(0,60)) = 30
        _RollFreq("Roll Freq", Range(0,20)) = 5.5
        [Space(12)]
        [Header(Yaw Along Spine)]
        [Space(5)]
        [Toggle(APPLY_SPINE_YAW)]
        _ApplySpineYaw ("APPLY SPINE YAW", Float) = 0
        [Toggle(APPLY_SPINE_PITCH)]
        _ApplySpinePitch ("Switch Yaw to Pitch", Float) = 0
        _SpineYawSize("Spine Yaw Size", Range(0, 100)) = 1
        _SpineYawLength("Spine Yaw Length", Range(0, 100)) = 30 // 正弦函数的周期
        _SpineYawFreq("Spine Yaw Freq", Range(0, 20)) = 5.5 // 频率
        [HideInInspector]_SpineYawCorrectFreq("SpineYawCorrectFreq", Float) = 0 // 修正频率，代码中使用，无须修改
        
        [Space(12)]
        [Header(Apple Spine Turn)]
        [Space(5)]
        [Toggle(APPLY_SPINE_TURN)]
        _ApplySpineTurn ("Apple Spine Turn", Float) = 0
        _TurnFactorWhenZIsPositive ("TurnFactorWhenZIsPositive", Range(-10,10)) = 0
        _TurnFactorWhenZIsNegative ("TurnFactorWhenZIsNegative", Range(-10,10)) = 0
        _CutOffValueWhenZIsPositive ("CutOffValueWhenZIsPositive", Range(0,100)) = 0
        _CutOffValueWhenZIsNegative ("CutOffValueWhenZIsNegative", Range(0,100)) = 0
        
        [Space(12)]
        [Header(Animation Masking)]
        [Space(5)]
        _MaskZ("MaskZ", Range(-1, 1)) = 0
        _InvertScale("_InvertScale", float) = 0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }
        
        Pass
        {
            Name "UniversalForward"
            Tags
            {
                "LightMode" = "UniversalForward"
            }
            HLSLPROGRAM
            // Required to compile gles 2.0 with standard srp library
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x

            #pragma vertex vert
            #pragma fragment frag
            // #pragma shader_feature _ALPHATEST_ON
            // #pragma shader_feature _ALPHAPREMULTIPLY_ON
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            // -------------------------------------
            // Unity defined keywords
            #pragma multi_compile_fog
            #pragma multi_compile_instancing
             
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"

            #include "ToonFishAnimation.cginc"
            #pragma shader_feature APPLY_SIDE_MOVE
            #pragma shader_feature APPLY_SPINE_ROLL
            #pragma shader_feature APPLY_SPINE_YAW
            #pragma shader_feature APPLY_SPINE_PITCH
            #pragma shader_feature APPLY_SPINE_TURN

            float _NormalScale;
            float _TattooOpacity;
            float _StopTime;
            float _TimeOffset;
            float _SideDisplaceFreq;
            float _SideDisplaceCorrectFreq;
            float _SideDisplaceLength;
            float _MaskZ;
            float _RollLength;
            float _RollFreq;
            float _SpineYawSize;
            float _SpineYawLength;
            float _SpineYawFreq;
            float _SpineYawCorrectFreq;
            float _InvertScale;
            float _TurnFactorWhenZIsPositive;
            float _TurnFactorWhenZIsNegative;
            float _CutOffValueWhenZIsPositive;
            float _CutOffValueWhenZIsNegative;

            TEXTURE2D(_BaseMap); SAMPLER(sampler_BaseMap);
            TEXTURE2D(_TattooTex); SAMPLER(sampler_TattooTex);
            TEXTURE2D (_NormalTex);SAMPLER(sampler_NormalTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _BaseMap_ST,_NormalTex_ST;
                float4 _BaseColor;
                float _ShadowStep;
                float _ShadowStepSmooth;
                float _SpecularStep;
                float _SpecularStepSmooth;
                float4 _SpecularColor;
                float _RimStepSmooth;
                float _RimStep;
                float4 _RimColor;
            CBUFFER_END

            struct Attributes
            {     
                float4 positionOS   : POSITION;
                float3 normalOS     : NORMAL;
                float4 tangentOS    : TANGENT;
                float2 uv           : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            }; 

            struct Varyings
            {
                float4 uv            : TEXCOORD0;
                float4 normalWS      : TEXCOORD1;    // xyz: normal, w: viewDir.x
                float4 tangentWS     : TEXCOORD2;    // xyz: tangent, w: viewDir.y
                float4 bitangentWS   : TEXCOORD3;    // xyz: bitangent, w: viewDir.z
                float3 viewDirWS     : TEXCOORD4;
				float4 shadowCoord	 : TEXCOORD5;	// shadow receive 
				float4 fogCoord	     : TEXCOORD6;	
				float3 positionWS	 : TEXCOORD7;	
                float4 positionCS    : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            Varyings vert(Attributes input)
            {
                Varyings output = (Varyings)0;
                    
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_TRANSFER_INSTANCE_ID(input, output);

                // ------------------------
                half fac = smoothstep(-1/_InvertScale + _MaskZ/_InvertScale, 1/_InvertScale, input.positionOS.z);
                #ifdef APPLY_SIDE_MOVE
                input.positionOS = SideMove(input.positionOS,_SideDisplaceFreq,_SideDisplaceCorrectFreq,_SideDisplaceLength,fac,_StopTime,_TimeOffset);
                #endif
                #ifdef APPLY_SPINE_ROLL
                input.positionOS = SpineRoll(input.positionOS,_RollLength,_RollFreq,fac,_StopTime,_TimeOffset);
                #endif
                #ifdef APPLY_SPINE_YAW
                #ifdef APPLY_SPINE_PITCH
                input.positionOS = SpinePitch(input.positionOS,_SpineYawSize,_SpineYawLength,_SpineYawFreq,_SpineYawCorrectFreq,fac,_StopTime,_TimeOffset);
                #else
                input.positionOS = SpineYaw(input.positionOS,_SpineYawSize,_SpineYawLength,_SpineYawFreq, _SpineYawCorrectFreq,fac,_StopTime,_TimeOffset);
                #endif
                #endif
                #ifdef APPLY_SPINE_TURN
                input.positionOS = SpineTurn(input.positionOS,_TurnFactorWhenZIsPositive, _TurnFactorWhenZIsNegative, _CutOffValueWhenZIsPositive,_CutOffValueWhenZIsNegative);
                #endif
                // ---------------
                
                VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
                VertexNormalInputs normalInput = GetVertexNormalInputs(input.normalOS, input.tangentOS);
                float3 viewDirWS = GetCameraPositionWS() - vertexInput.positionWS;
                // float3 vertexLight = VertexLighting(vertexInput.positionWS, normalInput.normalWS);

                output.positionCS = vertexInput.positionCS;
                output.positionWS = vertexInput.positionWS;
                // output.uv = input.uv;
                output.uv.xy = TRANSFORM_TEX(input.uv, _BaseMap);                                    //xy分量，储存颜色贴图uv
                output.uv.zw = TRANSFORM_TEX(input.uv, _NormalTex);
                output.normalWS = float4(normalInput.normalWS, viewDirWS.x);
                output.tangentWS = float4(normalInput.tangentWS, viewDirWS.y);
                output.bitangentWS = float4(normalInput.bitangentWS, viewDirWS.z);
                output.viewDirWS = viewDirWS;
                output.fogCoord = ComputeFogFactor(output.positionCS.z);
                return output;
            }
            
            half remap(half x, half t1, half t2, half s1, half s2)
            {
                return (x - t1) / (t2 - t1) * (s2 - s1) + s1;
            }
            
            float4 frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(input);

                float2 uv = input.uv;
                float3 N = normalize(input.normalWS.xyz);
                float3 T = normalize(input.tangentWS.xyz);
                float3 B = normalize(input.bitangentWS.xyz);
                float3 V = normalize(input.viewDirWS.xyz);
                float3 L = normalize(_MainLightPosition.xyz);
                float3 H = normalize(V+L);
                
                

                float4 baseMap = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, uv);
                
                // -----
                half4 NormalTex = SAMPLE_TEXTURE2D(_NormalTex,sampler_NormalTex,input.uv.zw);   //法线贴图
                float3x3 TBN = {input.tangentWS.xyz,input.bitangentWS.xyz,input.normalWS.xyz};          //世界空间法线方向
                float3 normalTS = UnpackNormalScale(NormalTex,_NormalScale);               //控制法线强度
                normalTS.z = pow((1 - pow(normalTS.x,2) - pow(normalTS.y,2)),0.5);         //规范化法线

                float3 norWS = mul(normalTS,TBN);                                          //顶点法线，和法线贴图融合 == 世界空间的法线信息
                // ----
                // norWS = normalize(input.normalWS.xyz);
                float NV = dot(norWS,V);
                float NH = dot(norWS,H);
                float NL = dot(norWS,L);
                
                NL = NL * 0.5 + 0.5;
                
                float4 tattooMap = SAMPLE_TEXTURE2D(_TattooTex, sampler_TattooTex, uv);
                tattooMap.a *= _TattooOpacity; 
                baseMap = lerp(baseMap, tattooMap, tattooMap.a);

                // return NH;
                float specularNH = smoothstep((1-_SpecularStep * 0.05)  - _SpecularStepSmooth * 0.05, (1-_SpecularStep* 0.05)  + _SpecularStepSmooth * 0.05, NH) ;
                float shadowNL = smoothstep(_ShadowStep - _ShadowStepSmooth, _ShadowStep + _ShadowStepSmooth, NL);

				input.shadowCoord = TransformWorldToShadowCoord(input.positionWS);
                
                //shadow
                float shadow = MainLightRealtimeShadow(input.shadowCoord);
                
                //rim
                float rim = smoothstep((1-_RimStep) - _RimStepSmooth * 0.5, (1-_RimStep) + _RimStepSmooth * 0.5, 0.5 - NV);
                
                //diffuse
                float3 diffuse = _MainLightColor.rgb  * baseMap * _BaseColor * shadowNL * shadow;
                
                //specular
                float3 specular = _SpecularColor * shadow * shadowNL * specularNH;
                
                //ambient
                float3 ambient =  rim * _RimColor + SampleSH(N) * _BaseColor * baseMap;
            
                float3 finalColor = diffuse + ambient + specular;
                finalColor = MixFog(finalColor, input.fogCoord);
                return float4(finalColor , 1.0);
            }
            ENDHLSL
        }
        
        //Outline
        Pass
        {
            Name "Outline"
            Cull Front
            Tags
            {
                "LightMode" = "SRPDefaultUnlit"
            }
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            #include "ToonFishAnimation.cginc"
            #pragma shader_feature APPLY_SIDE_MOVE
            #pragma shader_feature APPLY_SPINE_ROLL
            #pragma shader_feature APPLY_SPINE_YAW
            #pragma shader_feature APPLY_SPINE_PITCH
            #pragma shader_feature APPLY_SPINE_TURN
            
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };

            struct v2f
            {
                float4 pos      : SV_POSITION;
                float4 fogCoord	: TEXCOORD0;
            };
                
            float _OutlineWidth;
            float4 _OutlineColor;

            float _StopTime;
            float _TimeOffset;
            float _SideDisplaceFreq;
            float _SideDisplaceCorrectFreq;
            float _SideDisplaceLength;
            float _MaskZ;
            float _RollLength;
            float _RollFreq;
            float _SpineYawSize;
            float _SpineYawLength;
            float _SpineYawFreq;
            float _SpineYawCorrectFreq;
            float _InvertScale;
            float _TurnFactorWhenZIsPositive;
            float _TurnFactorWhenZIsNegative;
            float _CutOffValueWhenZIsPositive;
            float _CutOffValueWhenZIsNegative;
            
            v2f vert(appdata v)
            {
                v2f o;

                half fac = smoothstep(-1/_InvertScale + _MaskZ/_InvertScale, 1/_InvertScale, v.vertex.z);
                #ifdef APPLY_SIDE_MOVE
                v.vertex = SideMove(v.vertex,_SideDisplaceFreq,_SideDisplaceCorrectFreq,_SideDisplaceLength,fac,_StopTime,_TimeOffset);
                #endif
                #ifdef APPLY_SPINE_ROLL
                v.vertex = SpineRoll(v.vertex,_RollLength,_RollFreq,fac,_StopTime,_TimeOffset);
                #endif
                #ifdef APPLY_SPINE_YAW
                #ifdef APPLY_SPINE_PITCH
                v.vertex = SpinePitch(v.vertex,_SpineYawSize,_SpineYawLength,_SpineYawFreq,_SpineYawCorrectFreq,fac,_StopTime,_TimeOffset);
                #else
                v.vertex = SpineYaw(v.vertex,_SpineYawSize,_SpineYawLength,_SpineYawFreq,_SpineYawCorrectFreq,fac,_StopTime,_TimeOffset);
                #endif
                #endif
                #ifdef APPLY_SPINE_TURN
                v.vertex = SpineTurn(v.vertex, _TurnFactorWhenZIsPositive, _TurnFactorWhenZIsNegative,_CutOffValueWhenZIsPositive,_CutOffValueWhenZIsNegative);
                #endif
                
                VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
                o.pos = TransformObjectToHClip(float4(v.vertex.xyz + v.normal * _OutlineWidth * 0.1 ,1));
                o.fogCoord = ComputeFogFactor(vertexInput.positionCS.z);

                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float3 finalColor = MixFog(_OutlineColor, i.fogCoord);
                return float4(finalColor,1.0);
            }
            
            ENDHLSL
        }
        UsePass "Universal Render Pipeline/Lit/ShadowCaster"
    }
}

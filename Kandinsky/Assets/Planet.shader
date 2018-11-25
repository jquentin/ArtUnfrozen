// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:1,cusa:True,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:True,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:True,atwp:True,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:1873,x:34761,y:32728,varname:node_1873,prsc:2|emission-4971-OUT,alpha-4805-A;n:type:ShaderForge.SFN_Tex2d,id:4805,x:32543,y:32690,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:True,tagnsco:False,tagnrm:False,ntxv:1,isnm:False;n:type:ShaderForge.SFN_Blend,id:3963,x:33000,y:32772,varname:node_3963,prsc:2,blmd:17,clmp:True|SRC-4805-RGB,DST-1972-RGB;n:type:ShaderForge.SFN_SceneColor,id:1972,x:32518,y:33051,varname:node_1972,prsc:2;n:type:ShaderForge.SFN_Slider,id:8872,x:32883,y:32975,ptovrint:False,ptlb:DifferenceBlendRatio,ptin:_DifferenceBlendRatio,varname:node_8872,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Add,id:3758,x:33000,y:33133,varname:node_3758,prsc:2|A-4805-RGB,B-1972-RGB;n:type:ShaderForge.SFN_Slider,id:5951,x:32882,y:32694,ptovrint:False,ptlb:BaseRatio,ptin:_BaseRatio,cmnt:The ratio of the base texture that would be shown with a Default Sprite Shader,varname:node_5951,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Add,id:6405,x:33472,y:32661,varname:node_6405,prsc:2|A-2160-OUT,B-6877-OUT,C-9042-OUT;n:type:ShaderForge.SFN_Divide,id:5695,x:33766,y:32776,varname:node_5695,prsc:2|A-6405-OUT,B-465-OUT;n:type:ShaderForge.SFN_Slider,id:2174,x:32843,y:33278,ptovrint:False,ptlb:AdditiveRatio,ptin:_AdditiveRatio,varname:node_2174,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:2160,x:33265,y:32533,varname:node_2160,prsc:2|A-4805-RGB,B-5951-OUT;n:type:ShaderForge.SFN_Multiply,id:6877,x:33262,y:32816,varname:node_6877,prsc:2|A-3963-OUT,B-8872-OUT;n:type:ShaderForge.SFN_Multiply,id:9042,x:33259,y:33116,varname:node_9042,prsc:2|A-3758-OUT,B-2174-OUT;n:type:ShaderForge.SFN_Get,id:3299,x:32948,y:33473,varname:node_3299,prsc:2|IN-644-OUT;n:type:ShaderForge.SFN_Set,id:644,x:33262,y:32982,varname:DifferenceBlendRatio,prsc:2|IN-8872-OUT;n:type:ShaderForge.SFN_Add,id:4705,x:33191,y:33473,varname:node_4705,prsc:2|A-3299-OUT,B-7294-OUT,C-1537-OUT;n:type:ShaderForge.SFN_Set,id:8781,x:33259,y:33290,varname:AdditiveRatio,prsc:2|IN-2174-OUT;n:type:ShaderForge.SFN_Set,id:5383,x:33241,y:32697,varname:BaseRatio,prsc:2|IN-5951-OUT;n:type:ShaderForge.SFN_Get,id:7294,x:32948,y:33519,varname:node_7294,prsc:2|IN-5383-OUT;n:type:ShaderForge.SFN_Get,id:1537,x:32948,y:33565,varname:node_1537,prsc:2|IN-8781-OUT;n:type:ShaderForge.SFN_Set,id:3243,x:33379,y:33473,varname:TotalRatios,prsc:2|IN-4705-OUT;n:type:ShaderForge.SFN_Get,id:465,x:33542,y:32812,varname:node_465,prsc:2|IN-3243-OUT;n:type:ShaderForge.SFN_HsvToRgb,id:4971,x:34533,y:32797,varname:node_4971,prsc:2|H-2409-OUT,S-6774-OUT,V-3961-VOUT;n:type:ShaderForge.SFN_RgbToHsv,id:3961,x:33934,y:32776,varname:node_3961,prsc:2|IN-5695-OUT;n:type:ShaderForge.SFN_Add,id:2308,x:34142,y:32552,varname:node_2308,prsc:2|A-1952-OUT,B-3961-HOUT;n:type:ShaderForge.SFN_RgbToHsv,id:878,x:32455,y:33270,varname:node_878,prsc:2|IN-1972-RGB;n:type:ShaderForge.SFN_Set,id:7537,x:32653,y:33296,varname:BackgroundValue,prsc:2|IN-878-VOUT;n:type:ShaderForge.SFN_Get,id:3226,x:33692,y:32297,varname:node_3226,prsc:2|IN-7537-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:2409,x:34332,y:32653,ptovrint:False,ptlb:EnableHueShift,ptin:_EnableHueShift,varname:node_2409,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-3961-HOUT,B-2308-OUT;n:type:ShaderForge.SFN_If,id:1952,x:33911,y:32382,cmnt:If the pixel is not over black BG add given hue shift,varname:node_1952,prsc:2|A-3226-OUT,B-1504-OUT,GT-2484-OUT,EQ-6734-OUT,LT-6734-OUT;n:type:ShaderForge.SFN_Vector1,id:8166,x:33265,y:32316,varname:node_8166,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Vector1,id:6734,x:33692,y:32517,varname:node_6734,prsc:2,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:2484,x:33692,y:32448,ptovrint:False,ptlb:HueShift,ptin:_HueShift,varname:node_2484,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Add,id:6774,x:34252,y:32873,varname:node_6774,prsc:2|A-3961-SOUT,B-280-OUT;n:type:ShaderForge.SFN_Vector1,id:4675,x:33600,y:33062,varname:node_4675,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:4228,x:33600,y:33134,varname:node_4228,prsc:2,v1:0;n:type:ShaderForge.SFN_If,id:280,x:33819,y:32994,varname:node_280,prsc:2|A-3713-OUT,B-9551-OUT,GT-4675-OUT,EQ-4228-OUT,LT-4228-OUT;n:type:ShaderForge.SFN_Get,id:3713,x:33593,y:32934,varname:node_3713,prsc:2|IN-7537-OUT;n:type:ShaderForge.SFN_Set,id:841,x:33456,y:32329,varname:BGValThershold,prsc:2|IN-8166-OUT;n:type:ShaderForge.SFN_Get,id:1504,x:33692,y:32367,varname:node_1504,prsc:2|IN-841-OUT;n:type:ShaderForge.SFN_Get,id:9551,x:33579,y:32994,varname:node_9551,prsc:2|IN-841-OUT;proporder:4805-8872-5951-2174-2409-2484;pass:END;sub:END;*/

Shader "Shader Forge/Planet" {
    Properties {
        [PerRendererData]_MainTex ("MainTex", 2D) = "gray" {}
        _DifferenceBlendRatio ("DifferenceBlendRatio", Range(0, 1)) = 0
        _BaseRatio ("BaseRatio", Range(0, 1)) = 0
        _AdditiveRatio ("AdditiveRatio", Range(0, 1)) = 0
        [MaterialToggle] _EnableHueShift ("EnableHueShift", Float ) = 0
        _HueShift ("HueShift", Float ) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        _Stencil ("Stencil ID", Float) = 0
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilComp ("Stencil Comparison", Float) = 8
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilOpFail ("Stencil Fail Operation", Float) = 0
        _StencilOpZFail ("Stencil Z-Fail Operation", Float) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True"
            "PreviewType"="Plane"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            Stencil {
                Ref [_Stencil]
                ReadMask [_StencilReadMask]
                WriteMask [_StencilWriteMask]
                Comp [_StencilComp]
                Pass [_StencilOp]
                Fail [_StencilOpFail]
                ZFail [_StencilOpZFail]
            }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
//            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _DifferenceBlendRatio;
            uniform float _BaseRatio;
            uniform float _AdditiveRatio;
            uniform fixed _EnableHueShift;
            uniform float _HueShift;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 projPos : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                #ifdef PIXELSNAP_ON
                    o.pos = UnityPixelSnap(o.pos);
                #endif
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 node_1972 = sceneColor;
                float DifferenceBlendRatio = _DifferenceBlendRatio;
                float BaseRatio = _BaseRatio;
                float AdditiveRatio = _AdditiveRatio;
                float TotalRatios = (DifferenceBlendRatio+BaseRatio+AdditiveRatio);
                float3 node_5695 = (((_MainTex_var.rgb*_BaseRatio)+(saturate(abs(_MainTex_var.rgb-node_1972.rgb))*_DifferenceBlendRatio)+((_MainTex_var.rgb+node_1972.rgb)*_AdditiveRatio))/TotalRatios);
                float4 node_3961_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_3961_p = lerp(float4(float4(node_5695,0.0).zy, node_3961_k.wz), float4(float4(node_5695,0.0).yz, node_3961_k.xy), step(float4(node_5695,0.0).z, float4(node_5695,0.0).y));
                float4 node_3961_q = lerp(float4(node_3961_p.xyw, float4(node_5695,0.0).x), float4(float4(node_5695,0.0).x, node_3961_p.yzx), step(node_3961_p.x, float4(node_5695,0.0).x));
                float node_3961_d = node_3961_q.x - min(node_3961_q.w, node_3961_q.y);
                float node_3961_e = 1.0e-10;
                float3 node_3961 = float3(abs(node_3961_q.z + (node_3961_q.w - node_3961_q.y) / (6.0 * node_3961_d + node_3961_e)), node_3961_d / (node_3961_q.x + node_3961_e), node_3961_q.x);;
                float4 node_878_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_878_p = lerp(float4(float4(node_1972.rgb,0.0).zy, node_878_k.wz), float4(float4(node_1972.rgb,0.0).yz, node_878_k.xy), step(float4(node_1972.rgb,0.0).z, float4(node_1972.rgb,0.0).y));
                float4 node_878_q = lerp(float4(node_878_p.xyw, float4(node_1972.rgb,0.0).x), float4(float4(node_1972.rgb,0.0).x, node_878_p.yzx), step(node_878_p.x, float4(node_1972.rgb,0.0).x));
                float node_878_d = node_878_q.x - min(node_878_q.w, node_878_q.y);
                float node_878_e = 1.0e-10;
                float3 node_878 = float3(abs(node_878_q.z + (node_878_q.w - node_878_q.y) / (6.0 * node_878_d + node_878_e)), node_878_d / (node_878_q.x + node_878_e), node_878_q.x);;
                float BackgroundValue = node_878.b;
                float BGValThershold = 0.2;
                float node_1952_if_leA = step(BackgroundValue,BGValThershold);
                float node_1952_if_leB = step(BGValThershold,BackgroundValue);
                float node_6734 = 0.0;
                float node_280_if_leA = step(BackgroundValue,BGValThershold);
                float node_280_if_leB = step(BGValThershold,BackgroundValue);
                float node_4228 = 0.0;
                float3 emissive = (lerp(float3(1,1,1),saturate(3.0*abs(1.0-2.0*frac(lerp( node_3961.r, (lerp((node_1952_if_leA*node_6734)+(node_1952_if_leB*_HueShift),node_6734,node_1952_if_leA*node_1952_if_leB)+node_3961.r), _EnableHueShift )+float3(0.0,-1.0/3.0,1.0/3.0)))-1),(node_3961.g+lerp((node_280_if_leA*node_4228)+(node_280_if_leB*0.0),node_4228,node_280_if_leA*node_280_if_leB)))*node_3961.b);
                float3 finalColor = emissive;
                return fixed4(finalColor,_MainTex_var.a);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                #ifdef PIXELSNAP_ON
                    o.pos = UnityPixelSnap(o.pos);
                #endif
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}

Shader "Chrisxdef/TriColorCel"
{
    Properties
    {
        [Header(Grass Properties)]
        _Base("Base Color", Color) = (1, 1, 1, 1)
        _Light("Light Color", Color) = (1, 1, 1, 1)
        _Dark("Dark Color", Color) = (1, 1, 1, 1)
        _lThreshold("Light Threshold", Range(0.0, 1.0)) = 0.25
        _dThreshold("Dark Threshold", Range(0.0, 1.0)) = 0.75
        _Blending("Blending", Range(0.0, 1.0)) = 0.0
        [Header(Highlight)]
        _Highlight("Highlight Color", Color) = (1, 1, 1, 1)
        _hBlend("Highlight Blend", Range(0.0, 1.0)) = 0.1
        _hThreshold("Highlight Threshold", Range(0.0, 1.0)) = 0.9

        [Header(Shadow)]
        _Shadow("Shadow Color", Color) = (1, 1, 1, 1)
        _sBlend("Shadow Blend", Range(0.0, 1.0)) = 0.1

    }
    SubShader
    {
        Tags { 
            "RenderType" = "Opaque"
			"LightMode" = "ForwardBase"
			"PassFlags" = "OnlyDirectional"
            "RenderType"="GrassBillboard" 
        }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			#pragma multi_compile_fwdbase

            	#include "UnityCG.cginc"
			// Files below include macros and functions to assist
			// with lighting and shadows.
			#include "Lighting.cginc"
			#include "AutoLight.cginc"

			struct appdata
			{
				float4 vertex : POSITION;				
				float4 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
				float3 viewDir : TEXCOORD1;	
				// Macro found in Autolight.cginc. Declares a vector4
				// into the TEXCOORD2 semantic with varying precision 
				// depending on platform target.
				SHADOW_COORDS(2)
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.normal = v.normal;		
				o.viewDir = WorldSpaceViewDir(v.vertex);
				o.uv = v.uv;
				// Defined in Autolight.cginc. Assigns the above shadow coordinate
				// by transforming the vertex from world space to shadow-map space.
				TRANSFER_SHADOW(o)
				return o;
			}

            //Colors
            float4 _Base;
            float4 _Light;
            float4 _Dark;
            float4 _Shadow;
            float4 _Highlight;

            //Settings
            float _lThreshold;
            float _dThreshold;
            float _Blending;
            float _sBlend;
            float _hThreshold;
            float _hBlend;

            fixed4 frag (v2f i) : SV_Target
            {
                float3 normal = normalize(i.normal);
				float3 viewDir = normalize(i.viewDir);

				// Lighting below is calculated using Blinn-Phong,
				// with values thresholded to creat the "toon" look.
				// https://en.wikipedia.org/wiki/Blinn-Phong_shading_model

				// Calculate illumination from directional light.
				// _WorldSpaceLightPos0 is a vector pointing the OPPOSITE
				// direction of the main directional light.
				float NdotL = dot(_WorldSpaceLightPos0, normal);
                float intensity = saturate(NdotL);
                // i - t = r
                // 0 <= r <= 1
                // ceil(r)
                // lightAtten becomes 0 or 1
                float lightAtten = ceil(max(0, intensity-_lThreshold));
                // calc darkAtten, is lightAtten is 1 then disable darkAtten
                float darkAtten = max(0, ceil(max(0, intensity-_dThreshold)) - lightAtten);
                //start blending colors to achieve tri levels
                float4 darkShade = lerp(_Dark, _Dark*(1.0-intensity), _Blending);
                float4 lightShade = lerp(_Light, _Light*(intensity), _Blending);
                float4 lightAndDark = lerp(darkShade, lightShade, lightAtten );
                float4 newShade = lerp(lightAndDark, _Base, darkAtten);
                
                // 0 in shadow, 1 not in shadow
                float shadowAtten = SHADOW_ATTENUATION(i);

                float highlightAtten = ceil(max(0, intensity-_hThreshold)) * shadowAtten;
                float4 highlightBase = lerp(newShade, _Highlight, highlightAtten*intensity*(1.0 -_hBlend));

                float4 shadowShade = lerp(_Shadow, highlightBase, shadowAtten);
                float4 shadowBase = lerp(shadowShade, highlightBase, 1.0 - _sBlend);

                return shadowBase;
            }
            ENDCG
        }
		// Shadow casting support.
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}

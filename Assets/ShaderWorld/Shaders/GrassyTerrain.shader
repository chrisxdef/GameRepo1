Shader "Chrisxdef/GrassyTerrain"
{
    Properties
    {
        [Header(Grass Properties)]
        _GrassBaseColor("Grass Base Color", Color) = (0.4, 0.2, 0.8, 1.0)
        _GrassLightColor("Grass Light Color", Color) = (0.7, 0.4, 0.9, 1.0)
        _GrassDarkColor("Grass Dark Color", Color) = (0.3, 0.1, 0.6, 1.0)
        _GrassHighlight("Grass Highlight Color", Color) = (0.9, 0.8, 1.0, 1.0)
        _MaximumGrassAngle("Maximum Grass Angle", Range(0.0, 90.0)) = 90.0
        [Header(Dirt Properties)]
        _DirtBaseColor("Dirt Base Color", Color) = (0.8, 0.5, 0.2, 1.0)
        _DirtLightColor("Dirt Light Color", Color) = (1.0, 0.9, 0.8, 1.0)
        _DirtDarkColor("Dirt Dark Color", Color) = (0.6, 0.3, 0.1, 1.0)

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {

            };

            v2f vert (appdata v)
            {
                v2f o;
    
                return o;
            }

            float4 _GrassBaseColor;

            fixed4 frag (v2f i) : SV_Target
            {
                return _GrassBaseColor;
            }
            ENDCG
        }
    }
}

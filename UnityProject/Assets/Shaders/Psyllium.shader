﻿// 参考文献
// https://qiita.com/kaneta1992/items/af7793e5450b891c2e27

Shader "MTE/Psyllium"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color1a ("Color1a", Color) = (1, 1, 1, 1)
        _Color1b ("Color1b", Color) = (1, 1, 1, 1)
        _Color1c ("Color1c", Color) = (1, 1, 1, 1)
        _Color2a ("Color2a", Color) = (1, 1, 1, 1)
        _Color2b ("Color2b", Color) = (1, 1, 1, 1)
        _Color2c ("Color2c", Color) = (1, 1, 1, 1)
        _CutoffAlpha ("Cutoff Alpha", Float) = 0.5
    }
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
        }

        Pass
        {
            Cull Off
            ZWrite Off
            ZTest LEqual
            Blend One One
            Lighting Off
            Fog { Mode Off }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 4.5

            #include "PsylliumVert.cginc"

            float4 _Color1a;
            float4 _Color1b;
            float4 _Color1c;
            float4 _Color2a;
            float4 _Color2b;
            float4 _Color2c;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 colorC = lerp(_Color1c, _Color2c, i.uv2.y);
                col.rgb = colorC.rgb * colorC.a * col.a;
                col.a = 1;

                return col;
            }
            ENDCG
        }

        Pass
        {
            Cull Off
            ZWrite On
            ZTest LEqual
            Blend SrcAlpha OneMinusSrcAlpha
            Lighting Off
            Fog { Mode Off }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 4.5

            #include "PsylliumVert.cginc"

            float4 _Color1a;
            float4 _Color1b;
            float4 _Color1c;
            float4 _Color2a;
            float4 _Color2b;
            float4 _Color2c;
            float _CutoffAlpha;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                clip(col.a - _CutoffAlpha);

                fixed4 colorA = lerp(_Color1a, _Color2a, i.uv2.y);
                fixed4 colorB = lerp(_Color1b, _Color2b, i.uv2.y);
                col.rgb = lerp(colorB, colorA, col.r);

                return col;
            }
            ENDCG
        }
    }
}

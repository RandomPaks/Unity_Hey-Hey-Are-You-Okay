Shader "Custom/Blur"
{
    Properties
    {
        _Size("Blur", Range(0, 4)) = 3
        [HideInInspector] _MainTex("Masking Texture", 2D) = "white" {}
    }

    Category
    {
        Tags 
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Opaque" 
        }

        SubShader
        {
            GrabPass
            {
                "_HorizontalBlur"
            }

            Cull Off
            Lighting Off
            ZWrite Off
            ZTest Always
            Blend SrcAlpha OneMinusSrcAlpha

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #include "UnityCG.cginc"

                struct appdata_t 
                {
                    float4 vertex : POSITION;
                    float2 texcoord : TEXCOORD0;
                };

                struct v2f 
                {
                    float4 vertex : POSITION;
                    float4 uvgrab : TEXCOORD0;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;

                v2f vert(appdata_t v)
                {
                    v2f OUT;
                    OUT.vertex = UnityObjectToClipPos(v.vertex);

                    #if UNITY_UV_STARTS_AT_TOP
                    float scale = -1.0;
                    #else
                    float scale = 1.0;
                    #endif

                    OUT.uvgrab.xy = (float2(OUT.vertex.x, OUT.vertex.y * scale) + OUT.vertex.w) * 0.5;
                    OUT.uvgrab.zw = OUT.vertex.zw;

                    return OUT;
                }

                sampler2D _HorizontalBlur;
                float4 _HorizontalBlur_TexelSize;
                float _Size;

                half4 frag(v2f i) : COLOR
                {
                    half4 sum = half4(0,0,0,0);

                    #define BLURPIXEL(weight,kernelx) tex2Dproj( _HorizontalBlur, UNITY_PROJ_COORD(float4(i.uvgrab.x + _HorizontalBlur_TexelSize.x * kernelx * _Size, i.uvgrab.y, i.uvgrab.z, i.uvgrab.w))) * weight

                    sum += BLURPIXEL(0.05, -4.0);
                    sum += BLURPIXEL(0.09, -3.0);
                    sum += BLURPIXEL(0.12, -2.0);
                    sum += BLURPIXEL(0.15, -1.0);
                    sum += BLURPIXEL(0.18,  0.0);
                    sum += BLURPIXEL(0.15, +1.0);
                    sum += BLURPIXEL(0.12, +2.0);
                    sum += BLURPIXEL(0.09, +3.0);
                    sum += BLURPIXEL(0.05, +4.0);

                    return sum;
                }
                ENDCG
            }

            // Vertical blur
            GrabPass
            {
                "_VerticalBlur"
            }

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #include "UnityCG.cginc"

                struct appdata_t 
                {
                    float4 vertex : POSITION;
                    float2 texcoord: TEXCOORD0;
                };

                struct v2f 
                {
                    float4 vertex : POSITION;
                    float4 uvgrab : TEXCOORD0;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;

                v2f vert(appdata_t v) 
                {
                    v2f OUT;
                    OUT.vertex = UnityObjectToClipPos(v.vertex);

                    #if UNITY_UV_STARTS_AT_TOP
                    float scale = -1.0;
                    #else
                    float scale = 1.0;
                    #endif

                    OUT.uvgrab.xy = (float2(OUT.vertex.x, OUT.vertex.y * scale) + OUT.vertex.w) * 0.5;
                    OUT.uvgrab.zw = OUT.vertex.zw;

                    return OUT;
                }

                sampler2D _VerticalBlur;
                float4 _VerticalBlur_TexelSize;
                float _Size;

                half4 frag(v2f i) : COLOR
                {
                    half4 sum = half4(0,0,0,0);

                    #define BLURPIXEL(weight,kernely) tex2Dproj( _VerticalBlur, UNITY_PROJ_COORD(float4(i.uvgrab.x, i.uvgrab.y + _VerticalBlur_TexelSize.y * kernely * _Size, i.uvgrab.z, i.uvgrab.w))) * weight

                    sum += BLURPIXEL(0.05, -4.0);
                    sum += BLURPIXEL(0.09, -3.0);
                    sum += BLURPIXEL(0.12, -2.0);
                    sum += BLURPIXEL(0.15, -1.0);
                    sum += BLURPIXEL(0.18,  0.0);
                    sum += BLURPIXEL(0.15, +1.0);
                    sum += BLURPIXEL(0.12, +2.0);
                    sum += BLURPIXEL(0.09, +3.0);
                    sum += BLURPIXEL(0.05, +4.0);

                    return sum;
                }
                ENDCG
            }
        }
    }
}
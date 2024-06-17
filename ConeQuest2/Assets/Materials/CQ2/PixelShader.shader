Shader "Hidden/PixelShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PixelRange("PixelSize", float) = 64
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float _PixelRange;

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                uv *= _PixelRange;
                uv.xy = floor(uv.xy);
                uv.xy /= _PixelRange;

                fixed4 col = tex2D(_MainTex, uv);
                // just invert the colors
                return col;
            }
            ENDCG
        }
    }
}

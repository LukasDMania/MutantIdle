Shader "Custom/TextMeshProPerspective"
{
    Properties
    {
        _MainTex ("Font Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float perspectiveAmount = 0.5; // Adjust this to control how extreme the perspective is

            v2f vert(appdata_t v)
            {
                v2f o;
                float3 pos = v.vertex.xyz;

                // Scale the vertices based on their Y-position
                float scale = 1.0 - (pos.y * perspectiveAmount);
                pos.x *= scale;

                o.vertex = UnityObjectToClipPos(float4(pos, 1.0));
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}

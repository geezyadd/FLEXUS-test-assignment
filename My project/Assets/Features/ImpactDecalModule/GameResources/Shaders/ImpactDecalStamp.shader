Shader "Hidden/ImpactDecalStamp"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DecalTex ("Decal Texture", 2D) = "white" {}
        _DecalColor ("Decal Color", Color) = (0, 0, 0, 1)
        _DecalUV ("Decal UV", Vector) = (0.5, 0.5, 0, 0)
        _DecalSize ("Decal Size", Float) = 0.5
        _DecalRotation ("Decal Rotation", Float) = 0
        _SurfaceSize ("Surface Size", Vector) = (10, 10, 0, 0)
    }

    SubShader
    {
        Cull Off
        ZWrite Off
        ZTest Always
        Blend One Zero, One OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _DecalTex;
            float4 _DecalColor;
            float4 _DecalUV;
            float _DecalSize;
            float _DecalRotation;
            float4 _SurfaceSize;

            float2 GetDecalUV(float2 uv)
            {
                float2 offset = (uv - _DecalUV.xy) * _SurfaceSize.xy;
                float sinRotation;
                float cosRotation;
                sincos(_DecalRotation, sinRotation, cosRotation);

                float2 rotated = float2(
                    cosRotation * offset.x - sinRotation * offset.y,
                    sinRotation * offset.x + cosRotation * offset.y);

                return rotated / _DecalSize * 0.5 + 0.5;
            }

            fixed4 frag(v2f_img input) : SV_Target
            {
                float2 decalUV = GetDecalUV(input.uv);

                clip(min(min(decalUV.x, decalUV.y), 1.0 - max(decalUV.x, decalUV.y)));

                fixed4 decal = tex2D(_DecalTex, decalUV) * _DecalColor;
                clip(decal.a - 0.001);
                return decal;
            }
            ENDCG
        }
    }
}

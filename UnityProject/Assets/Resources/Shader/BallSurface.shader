Shader "Custom/ConeSag"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Direction("Direction", vector) = (0,0,0,0)
        _SagRange("SagRange", Range(0, 90)) = 0
        _SagDistance ("SagDistance", Range(0, 1)) = 0.0
        _Tess ("Tessellation", Range(1,32)) = 4
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard vertex:vert tessellate:tess  fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0
        #include "Tessellation.cginc"

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        float _Tess;


        half _Glossiness;
        half _Metallic;
        half _SagDistance;
        half _SagRange;
        float4 _Direction;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        float4 tess () 
        {
            return _Tess;
        }


        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }

        void vert (inout appdata_full v)
        {
            float sagRange = radians(_SagRange);
            if(sagRange > 0)
            {
                float3 dir = normalize(_Direction.xyz);
                float3 normal = normalize(v.vertex.xyz);
                float angle = acos(dot(dir, normal) / (length(dir) * length(normal)));
                if(angle <= sagRange)
                {
                    v.vertex = float4(v.vertex.xyz - v.normal * _SagDistance, v.vertex.z);
                }
            }
        }

        ENDCG
    }
    FallBack "Diffuse"
}

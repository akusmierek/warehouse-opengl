Shader "Custom/ObjectRenderTest"
{
    Properties
    {
        _Tint("Tint", Color) = (1, 1, 1, 1)
        _MainTex ("Texture", 2D) = "white" {}
        [Gamma] _Metallic ("Metallic", Range(0,1)) = 0
        _Smoothness ("Smoothness", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma target 3.0

            #pragma vertex VertexProgram
            #pragma fragment FragmentProgram

            // make fog work
            #pragma multi_compile_fog

            #include "ObjectRenderTest.cginc"
            
            ENDCG
        }

        Pass
        {
            Blend One One
            ZWrite Off

            CGPROGRAM
            #pragma target 3.0

            #pragma vertex VertexProgram
            #pragma fragment FragmentProgram

            // make fog work
            #pragma multi_compile_fog

            #include "ObjectRenderTest.cginc"

            ENDCG
        }
    }
}

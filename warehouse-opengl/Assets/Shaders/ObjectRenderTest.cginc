#ifndef OBJECT_RENDER_TEST_INCLUDED
#define OBJECT_RENDER_TEST_INCLUDED

//#include "UnityCG.cginc"
//#include "UnityStandardBRDF.cginc"
//#include "UnityStandardUtils.cginc"
//#include "AutoLight.cginc"
#include "UnityPBSLighting.cginc"

struct appdata
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
    float3 normal : TEXCOORD1;
};

struct v2f
{
    float4 vertex : SV_POSITION;
    float2 uv : TEXCOORD0;
    float3 normal : TEXCOORD1;
    float3 worldPos : TEXCOORD2;
    //UNITY_FOG_COORDS(1)
};

sampler2D _MainTex;
float4 _MainTex_ST;
float4 _Tint;
float _Metallic;
float4 _Light0;
float4 _Light0Color;
float _Smoothness;

v2f VertexProgram(appdata v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.worldPos = mul(unity_ObjectToWorld, v.vertex);
    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
    o.normal = v.normal;
    //UNITY_TRANSFER_FOG(o,o.vertex);
    return o;
}

UnityLight CreateLight(v2f i)
{
    UnityLight light;
    
    if (_Light0.w == 0)
    {
        light.dir = _Light0.xyz;
        light.color = _Light0Color.rgb;
    }
    else
    {
        float3 lightVector = _Light0.xyz - i.worldPos;
        light.dir = normalize(lightVector);
        float attenuation = _Light0.w / (1 + dot(lightVector, lightVector));
        light.color = _Light0Color.rgb * attenuation * _Light0Color.w;
    }

    //UNITY_LIGHT_ATTENUATION(attenuation, 0, i.worldPos);
    light.ndotl = DotClamped(i.normal, light.dir);
    return light;
}

fixed4 FragmentProgram(v2f i) : SV_Target
{
    // apply fog
    //UNITY_APPLY_FOG(i.fogCoord, col);
    i.normal = normalize(i.normal);
    float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);

    float3 albedo = tex2D(_MainTex, i.uv).rgb * _Tint.rgb;
    float3 specularTint;
    float oneMinusReflectivity;
    albedo = DiffuseAndSpecularFromMetallic(albedo, _Metallic, specularTint, oneMinusReflectivity);

    UnityIndirect indirectLight;
    indirectLight.diffuse = 0;
    indirectLight.specular = 0;

    return UNITY_BRDF_PBS(
        albedo, specularTint,
        oneMinusReflectivity, _Smoothness,
        i.normal, viewDir,
        CreateLight(i), indirectLight);
}

#endif
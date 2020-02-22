Shader "TNTLad/Vertex Color"
{
  Properties
  {
      _Color("Main Color", Color) = (1,1,1,1)
      _SpecColor("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
      _Shininess("Shininess", Range(0.01, 1)) = 0.078125
    //_MainTex ("Base (RGB)", 2D) = "white" {}
    _Detail("Detail (RGB) Gloss (A)", 2D) = "gray" {}
  }

    SubShader
  {
      Tags { "RenderType" = "Opaque" }
      LOD 300

    CGPROGRAM
    #pragma surface surf BlinnPhong vertex:vert
    //#pragma surface surf BlinnPhong
    //#pragma surface surf Lambert

    //sampler2D _MainTex;
    sampler2D _Detail;
    float4 _Color;
    float _Shininess;

    struct Input
    {
      //float2 uv2_MainTex;
      float2 uv_Detail;
      float3 vertColors;
  };

  void vert(inout appdata_full v, out Input o)
  {
    o.vertColors = v.color.rgb;    // grab vertex colors from appdata
    o.uv_Detail = v.texcoord;   // maybe need this

  }


  void surf(Input IN, inout SurfaceOutput o)
  {
    //half4 c = tex2D(_MainTex, IN.uv2_MainTex) * _Color;
    half3 c = IN.vertColors.rgb * _Color.rgb;               //half3 check to see that alpha works
    c.rgb *= tex2D(_Detail,IN.uv_Detail).rgb * 2;
    o.Albedo = c.rgb;
    o.Gloss = tex2D(_Detail,IN.uv_Detail).a;
    //o.Alpha = c.a * _Color.a;
    o.Specular = _Shininess;
    //o.Normal = UnpackNormal(tex2D(_Detail, IN.uv_Detail));
}
ENDCG
  }

    Fallback "Specular"
}
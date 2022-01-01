Shader "Planet/Test/Circle"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
	    _Color("Color", Color) = (255, 0, 0, 1)
		_Radius("Radius", range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline" "IgnoreProjector" = "True" "Queue"="Transparent"}
        LOD 100

        Pass
        {
			CULL Off
			ZWrite Off
         	Blend SrcAlpha OneMinusSrcAlpha
        	
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			fixed4 _Color;
			float _Radius;
            
			struct Input
	        {
	            float2 uv_MainTex;
	        };

            v2f vert (appdata v){
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

			fixed4 frag(v2f i) : COLOR {
				float centerDistance = distance(i.uv, float2(0.5,0.5)); // Cut out a circle
				float3 color = _Color.rgb; // Apply colors

            	return fixed4(color, step(centerDistance, _Radius));
			}
            
            ENDCG
        }
    }
}

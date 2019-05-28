// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'



CGPROGRAM
#pragma vertex VertexProgram
#pragma fragment FragmentProgram
#include "CardboardDistortion.cginc"

struct VertexInput {
    float4 position : POSITION;
};

struct VertexToFragment {
    half4 position : SV_POSITION;
};    

VertexToFragment VertexProgram (VertexInput vertex){
    VertexToFragment output;

    #if SHADER_API_MOBILE

        ///easy as that.
        output.position = undistortVertex(vertex.position);
    #else

        ///this is how a standard shader works
        output.position = UnityObjectToClipPos(vertex.position);
    #endif


    return output;         
};


fixed4 _Color;
fixed4 FragmentProgram (VertexToFragment fragment) : COLOR{  
    return _Color;
}
ENDCG

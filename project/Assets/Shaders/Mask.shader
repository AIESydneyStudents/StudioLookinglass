Shader "Custom/Mask"
{
	Properties{
        _Color ("Main Color", Color) = (1,0.5,0.5,1)
        _ShadowTex ("Cookie", 2D) = "" { TexGen ObjectLinear }
        _FalloffTex ("FallOff", 2D) = "" { TexGen ObjectLinear }
    }
	SubShader{

		Tags {
			"RenderType" = "Opaque"
		}

		Pass {
			ZWrite Off
			Fog { Color (1, 1, 1) }
             ColorMask RGB
             Blend One One
             SetTexture [_ShadowTex] {
                combine texture, ONE - texture
                Matrix [_Projector]
             }
             SetTexture [_FalloffTex] {
                constantColor (0,0,0,0)
                combine previous lerp (texture) constant
                Matrix [_ProjectorClip]
             }
		}
	}
}
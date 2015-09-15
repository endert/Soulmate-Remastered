uniform sampler2D texture;

void main()
{
	vec2 texCoord = gl_TexCoord[0].xy;

	vec4 color = texture2D(texture, texCoord).rgba;

	//black
	vec4 black = vec4(0,0,0,1);

	gl_FragColor = vec4(black.rgb, color.a);
}
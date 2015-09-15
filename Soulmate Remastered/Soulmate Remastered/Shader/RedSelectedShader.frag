uniform sampler2D texture;

void main()
{
	vec2 texCoord = gl_TexCoord[0].xy;

	vec4 color = texture2D(texture, texCoord).rgba;

	gl_FragColor = color + vec4(1, 0, 0, 0);
}
uniform sampler2D texture;
uniform float time;

void main()
{
	vec2 texCoord = gl_TexCoord[0].xy;

	vec4 color = texture2D(texture, texCoord).rgba;

	float help = cos(time) * 0.5 + 0.5;

	gl_FragColor = color - vec4(0.5, 0.5, 0.5, 0) * help;
}
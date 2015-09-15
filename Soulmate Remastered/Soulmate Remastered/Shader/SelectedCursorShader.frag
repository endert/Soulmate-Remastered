uniform sampler2D texture;

//just making it half transparent and green, everything of this texture
void main()
{
	gl_FragColor = vec4(vec3(0, 1, 0), 0.5f);
}
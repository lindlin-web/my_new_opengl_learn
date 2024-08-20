#version 330 core
out vec4 FragColor;
uniform samplerCube cubeMap;
in vec3 TexCoords;
void main() {
	FragColor = texture(cubeMap, TexCoords);
}
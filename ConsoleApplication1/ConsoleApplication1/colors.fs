#version 330 core
struct Material {
	sampler2D diffuse;
	sampler2D specular;
	float shininess;
};

struct Light {
	// vec3 position; // no longer necessary when using directional lights.
	vec3 direction;
	vec3 ambient; 
	vec3 diffuse;
	vec3 specular;
};

uniform Material material;
uniform Light light;



out vec4 FragColor;
uniform vec3 lightColor;


in vec3 Normal;
in vec3 FragPos;
uniform vec3 lightPos;

uniform vec3 viewPos;

float specularStrength = 0.5;



in vec2 TexCoords;

void main() {
	// ambient 
	vec3 ambient = light.ambient * vec3(texture(material.diffuse, TexCoords));

	// diffuse 
	vec3 norm = normalize(Normal);
	vec3 lightDir = normalize(-light.direction);
	float diff = max(dot(norm, lightDir), 0.0);
	vec3 diffuse = light.diffuse * diff * vec3(texture(material.diffuse, TexCoords));

	// specular 
	vec3 viewDir = normalize(viewPos - FragPos);
	vec3 reflectDir = reflect(-lightDir, norm);
	float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
	vec3 specular = light.specular * spec * vec3(texture(material.specular, TexCoords));

	vec3 result = ambient + diffuse + specular;
	FragColor = vec4(result, 1.0);
}
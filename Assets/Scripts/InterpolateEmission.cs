using UnityEngine;

public class InterpolateEmission : MonoBehaviour
{
    [SerializeField] Material targetMaterial;
    public float intensity;

    [SerializeField] Color emissionColor;
    [SerializeField] float minIntensity;
    [SerializeField] float maxIntensity;
    [SerializeField] float currentIntensity;
    [SerializeField] float speed;
    [SerializeField] bool up = true;

    private void Start()
    {
        currentIntensity = (maxIntensity + minIntensity) * 2;
        targetMaterial.SetVector("_EmissionColor", emissionColor * currentIntensity);
    }

    private void Update()
    {
        targetMaterial.SetVector("_EmissionColor", Color.red * intensity);

        if (up == true)
        {
            if (currentIntensity < maxIntensity)
            {
                currentIntensity += Time.deltaTime * speed;
                targetMaterial.SetVector("_EmissionColor", emissionColor * currentIntensity);
            }
            else
            {
                up = false;
            }
        }
        else
        {
            if (currentIntensity > minIntensity)
            {
                currentIntensity -= Time.deltaTime * speed;
                targetMaterial.SetVector("_EmissionColor", emissionColor * currentIntensity);
            }
            else
            {
                up = true;
            }
        }
    }
}

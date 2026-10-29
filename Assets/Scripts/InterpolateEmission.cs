using UnityEngine;

public class InterpolateEmission : MonoBehaviour
{
    [SerializeField] Material targetMaterial;
    public float intensity;

    private void Start()
    {
    }

    private void Update()
    {
        targetMaterial.SetVector("_EmissionColor", Color.red * intensity );
        
    }
}

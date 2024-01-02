using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class LaunchArcMesh : MonoBehaviour
{
    Mesh mesh;
    public float meshWidth;

    public float velocity;
    public float angle;
    public int resolution;

    float g; //force of gravity on the y axis
    float radianAngle;

    public LayerMask targetLayer; // La capa del objeto que quieres interceptar

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        g = Mathf.Abs(Physics2D.gravity.y);
    }

    private void OnValidate()
    {
        if (mesh != null && Application.isPlaying)
        {
            MakeArcMesh(CalculateArcArray());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeArcMesh(CalculateArcArray());
    }

    void MakeArcMesh(Vector3[] arcVerts)
    {
        mesh.Clear();
        Vector3[] vertices = new Vector3[(resolution + 1) * 2];
        int[] triangles = new int[resolution * 6 * 2]; 

        for (int i = 0; i <= resolution; i++)
        {
            vertices[i * 2] = new Vector3(meshWidth * 0.5f, arcVerts[i].y, arcVerts[i].x);
            vertices[i * 2 + 1] = new Vector3(meshWidth * -0.5f, arcVerts[i].y, arcVerts[i].x);

            if (i != resolution)
            {
                triangles[i * 12] = i * 2;
                triangles[i * 12 + 1] = triangles[i * 12 + 4] = i * 2 + 1;
                triangles[i * 12 + 2] = triangles[i * 12 + 3] = (i + 1) * 2;
                triangles[i * 12 + 5] = (i + 1) * 2 + 1;

                triangles[i * 12 + 6] = i * 2;
                triangles[i * 12 + 7] = triangles[i * 12 + 10] = (i + 1) * 2;
                triangles[i * 12 + 8] = triangles[i * 12 + 9] = i * 2 + 1;
                triangles[i * 12 + 11] = (i + 1) * 2 + 1;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];

        radianAngle = Mathf.Deg2Rad * angle;
        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;

        RaycastHit hit;
        bool hasHit = false;

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);

            // Realiza un Raycast para verificar la intersección con la capa deseada
            if (Physics.Raycast(transform.position, arcArray[i] - transform.position, out hit, maxDistance, targetLayer))
            {
                arcArray[i] = hit.point; // Actualiza el punto de intersección
                hasHit = true;
                break; // Detén el cálculo de la trayectoria
            }
        }

        if (!hasHit)
        {
            // Si no se ha encontrado ninguna intersección, muestra la trayectoria completa
            Debug.Log("No se encontró intersección con el objeto de la capa deseada.");
        }

        return arcArray;
    }

    Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(radianAngle) -
                  ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));
        return new Vector3(x, y);
    }
}

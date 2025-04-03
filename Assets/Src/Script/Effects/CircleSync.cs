using UnityEngine;
using System.Collections.Generic;

public class CircleSync : MonoBehaviour
{
    public static int PosID = Shader.PropertyToID("_Position");
    public static int SizeID = Shader.PropertyToID("_Size");
    public static int tintID = Shader.PropertyToID("_tint");

    [SerializeField] Camera cam;

    [SerializeField] float lerpSpeed = 2f;

    [SerializeField] Transform[] seeThroughtObjects;

    float duration = 0f;
    HashSet<Material> matSet = new HashSet<Material>();

    // Update is called once per frame
    void Update()
    {
        
        Vector3 dir = cam.transform.position - transform.position;
        Ray ray = new Ray(transform.position, dir.normalized);

        RaycastHit info;
        bool hit = Physics.Raycast(ray, out info, dir.magnitude);
        Material mat = null;
        

        if (hit)
        {
            Transform hitTr= info.collider.transform;
            foreach (Transform tr in seeThroughtObjects)
            {
                if (hitTr.position.Equals(tr.position) && hitTr.rotation.Equals(tr.rotation))
                {
                    float   size = Mathf.Lerp(0f, 0.5f, duration);
                    Vector3 view = cam.WorldToViewportPoint(transform.position);

                    duration += Time.deltaTime * lerpSpeed;

                    mat = tr.GetComponent<Renderer>().material;
                    mat.SetFloat(SizeID, size);
                    mat.SetVector(PosID, view);
                    if (!matSet.Contains(mat)) matSet.Add(mat);
                }
            }
        }
        else
        {
            foreach (Material m in matSet)
            {
                float size = Mathf.Lerp(0f, 0.5f, duration);
                m.SetFloat(SizeID, size);
                duration -= Time.deltaTime * lerpSpeed;
            }

        }

        duration = Mathf.Clamp(duration, 0f, 1f);

    }

}

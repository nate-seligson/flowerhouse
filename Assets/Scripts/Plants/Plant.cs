using UnityEngine;

public class Plant : MonoBehaviour
{
    public float lifetime = 5f;
    private float timer = 0f;

    private bool dead = false;
    private Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mat.color = Color.white;
    }

    void Update()
    {
        if (timer < lifetime)
        {
            timer += Time.deltaTime;
            float t = timer / lifetime;
            mat.color = Color.Lerp(Color.white, Color.black, t);
        }
        else
        {
            dead = true;
        }
    }
    public void Water()
    {
        if (dead) return;
        timer = 0;
        mat.color = Color.white;
    }
}

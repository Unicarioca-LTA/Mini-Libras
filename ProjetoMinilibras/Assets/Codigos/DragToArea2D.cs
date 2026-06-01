using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class DragToArea2D : MonoBehaviour
{
    [Header("Seleção / Arraste")]
    public bool selected = false;                 // Você controla isso na UI/Eventos
    [Tooltip("Se verdadeiro, mantém o offset do ponto clicado (pega onde o mouse tocou). Se falso, cola o centro do objeto no cursor.")]
    public bool usePickupOffset = true;

    [Header("Área de Encaixe (auto)")]
    public Transform areaTransform;               // Centro da área. Tamanho detectado automaticamente.

    [Header("Comportamento ao soltar")]
    public bool snapInside = true;                // Encaixa no centro da área se estiver dentro
    public float snapDuration = 0.15f;            // Tempo do tween de encaixe
    public float returnDuration = 0.20f;          // Tempo do tween para voltar à origem
    public AnimationCurve ease = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Eventos")]
    public UnityEvent onInsideArea;               // Dispara quando SOLTO e está dentro
    public UnityEvent onOutsideArea;              // Dispara quando SOLTO e está fora

    // Internos
    Camera cam;
    Vector3 originPos;
    float fixedZ;
    Vector3 dragOffset;                           // Offset capturado no momento do "Select"
    Coroutine tweenCo;

    void Awake()
    {
        cam = Camera.main;
        originPos = transform.position;
        fixedZ = transform.position.z;
    }

    void Update()
    {
        if (!selected || cam == null) return;

        // Posição do mouse em mundo
        var mp = Input.mousePosition;
        var wp = cam.ScreenToWorldPoint(new Vector3(mp.x, mp.y, GetCameraToObjectZ()));

        // Mantém Z original
        Vector3 target = new Vector3(wp.x, wp.y, fixedZ);
        if (usePickupOffset) target += dragOffset;

        transform.position = target;
    }

    // === Transfere Transform Values ===
    public void PassTransform(Transform passTransform)
    {
        transform.position = passTransform.position;
        transform.rotation = passTransform.rotation;
        transform.localScale = passTransform.localScale;
    }

    // === Métodos para ligar nos seus eventos (PointerDown/Up, etc.) ===
    public void Select()
    {
        selected = true;
        CaptureDragOffset();
        StopTween();
    }

    public void DeselectAndDrop()
    {
        selected = false;
        ResolveDrop();
    }

    public void SetSelected(bool value)
    {
        if (value && !selected) Select();
        else if (!value && selected) DeselectAndDrop();
        else
        {
            selected = value;
            if (!selected) ResolveDrop();
        }
    }

    // === Lógica principal ===
    void ResolveDrop()
    {
        bool inside = IsInsideArea(transform.position);

        if (inside)
        {
            onInsideArea?.Invoke();

            if (snapInside && areaTransform != null)
            {
                Vector3 snapTarget = new Vector3(areaTransform.position.x, areaTransform.position.y, fixedZ);
                StartTween(transform.position, snapTarget, snapDuration);
            }
            // Se não quiser snap, apenas não mova.
        }
        else
        {
            onOutsideArea?.Invoke();
            StartTween(transform.position, new Vector3(originPos.x, originPos.y, fixedZ), returnDuration);
        }
    }

    bool IsInsideArea(Vector3 worldPos)
    {
        if (areaTransform == null) return false;

        Vector2 size = GetAreaWorldSize(areaTransform, out Vector2 center2D);
        float w = Mathf.Abs(size.x);
        float h = Mathf.Abs(size.y);

        float minX = center2D.x - w * 0.5f;
        float maxX = center2D.x + w * 0.5f;
        float minY = center2D.y - h * 0.5f;
        float maxY = center2D.y + h * 0.5f;

        return (worldPos.x >= minX && worldPos.x <= maxX &&
                worldPos.y >= minY && worldPos.y <= maxY);
    }

    // Detecta automaticamente o tamanho da área a partir de componentes comuns.
    Vector2 GetAreaWorldSize(Transform t, out Vector2 center)
    {
        center = t.position;

        // 1) BoxCollider2D (mais preciso para áreas de jogo 2D)
        var col2D = t.GetComponent<BoxCollider2D>();
        if (col2D)
        {
            // size está em unidades do objeto; convertemos para mundo via scale
            Vector2 scaled = Vector2.Scale(col2D.size, new Vector2(Mathf.Abs(t.lossyScale.x), Mathf.Abs(t.lossyScale.y)));
            center = (Vector2)col2D.bounds.center; // center real do collider
            return scaled;
        }

        // 2) SpriteRenderer (usa bounds em mundo)
        var sr = t.GetComponent<SpriteRenderer>();
        if (sr)
        {
            var b = sr.bounds;
            center = (Vector2)b.center;
            return new Vector2(b.size.x, b.size.y);
        }

        // 3) RectTransform (UI em World Space)
        var rt = t.GetComponent<RectTransform>();
        if (rt)
        {
            // rect.size em local; multiplicar por lossyScale para mundo
            Vector2 s = rt.rect.size;
            Vector2 scaled = Vector2.Scale(s, new Vector2(Mathf.Abs(t.lossyScale.x), Mathf.Abs(t.lossyScale.y)));
            center = (Vector2)rt.TransformPoint(rt.rect.center);
            return scaled;
        }

        // 4) Fallback: usa lossyScale como tamanho (convenciona-se 1 unidade = 1 metro)
        return new Vector2(Mathf.Abs(t.lossyScale.x), Mathf.Abs(t.lossyScale.y));
    }

    void StartTween(Vector3 from, Vector3 to, float duration)
    {
        StopTween();
        tweenCo = StartCoroutine(TweenPosition(from, to, duration));
    }

    void StopTween()
    {
        if (tweenCo != null)
        {
            StopCoroutine(tweenCo);
            tweenCo = null;
        }
    }

    System.Collections.IEnumerator TweenPosition(Vector3 from, Vector3 to, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float k = duration > 0f ? Mathf.Clamp01(t / duration) : 1f;
            float e = ease != null ? ease.Evaluate(k) : k;
            transform.position = Vector3.LerpUnclamped(from, to, e);
            yield return null;
        }
        transform.position = to;
        tweenCo = null;
    }

    void CaptureDragOffset()
    {
        if (!usePickupOffset || cam == null) { dragOffset = Vector3.zero; return; }

        var mp = Input.mousePosition;
        var wp = cam.ScreenToWorldPoint(new Vector3(mp.x, mp.y, GetCameraToObjectZ()));
        Vector3 pickWorld = new Vector3(wp.x, wp.y, fixedZ);
        dragOffset = transform.position - pickWorld;
    }

    float GetCameraToObjectZ()
    {
        if (cam.orthographic)
            return Mathf.Abs(cam.transform.position.z - fixedZ);
        else
            return Mathf.Abs(fixedZ - cam.transform.position.z);
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        if (areaTransform == null) return;

        Vector2 center2D;
        Vector2 size = GetAreaWorldSize(areaTransform, out center2D);

        Vector3 c = new Vector3(center2D.x, center2D.y, 0f);
        Vector3 gizmoSize = new Vector3(size.x, size.y, 0.01f);

        Gizmos.color = new Color(1f, 0.65f, 0f, 0.25f);
        Gizmos.DrawCube(c, gizmoSize);
        Gizmos.color = new Color(1f, 0.65f, 0f, 1f);
        Gizmos.DrawWireCube(c, gizmoSize);
    }
#endif
}

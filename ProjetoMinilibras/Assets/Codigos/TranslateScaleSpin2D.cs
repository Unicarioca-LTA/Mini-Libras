using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using TMPro;

public class TranslateScaleSpin2D : MonoBehaviour
{
    [Header("Opções de Posição")]
    public bool ceu = false;
    public bool setIdaEVolta = false;

    [Header("Refs")]
    public Transform origin;
    public Transform destination;
    public Transform targetToMove;

    [Header("Motion")]
    [Min(0.01f)] public float duration = 1.5f;
    [Tooltip("Voltas inteiras durante o trajeto. Use negativo para sentido oposto.")]
    public int spinCount = 2;
    public AnimationCurve easing = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Space & Start")]
    public bool useLocalSpace = true;
    [Tooltip("Se true, inicia do estado atual do alvo (posição/rotação/size).")]
    public bool startFromCurrent = false;

    [Header("Resize mode (mantém scale=1)")]
    [Tooltip("Se marcado e o alvo for UI, anima RectTransform.sizeDelta (W/H).")]
    public bool useRectTransformSize = true;
    [Tooltip("Se marcado e o alvo tiver SpriteRenderer em Sliced/Tiled, anima SpriteRenderer.size (W/H).")]
    public bool useSpriteRendererSize = false;

    [Header("Events")]
    public UnityEvent onMoveStart;
    public UnityEvent onMoveComplete;
    public event System.Action MoveStarted;
    public event System.Action MoveCompleted;

    public bool IsMoving { get; private set; }
    Coroutine moveRoutine;

    void Reset() => targetToMove = transform;

    public void StartMove()
    {
        if (!ValidateRefs()) return;
        if (moveRoutine != null) StopCoroutine(moveRoutine);
        moveRoutine = StartCoroutine(MoveCo());
    }

    IEnumerator MoveCo()
    {
        var tMove = GetTarget();
        var rt = tMove as RectTransform; // null se não for UI
        var sr = tMove.GetComponent<SpriteRenderer>();

        // ---- Estados iniciais/finais (posição) ----
        Vector3 startPos, endPos;
        if (startFromCurrent)
        {
            startPos = useLocalSpace ? tMove.localPosition : tMove.position;
            endPos = useLocalSpace ? destination.localPosition : destination.position;
        }
        else
        {
            startPos = useLocalSpace ? origin.localPosition : origin.position;
            endPos = useLocalSpace ? destination.localPosition : destination.position;
        }

        // ---- Estados iniciais/finais (rotação contínua) ----
        float startZ = GetZ(startFromCurrent ? tMove : origin);
        float endZ = GetZ(destination);
        float endZContinuous = ComputeContinuousEndAngle(startZ, endZ, spinCount);

        // ---- Estados iniciais/finais (tamanho W/H mantendo scale=1) ----
        // Forçamos scale=1 o tempo todo
        tMove.localScale = Vector3.one;

        Vector2 startSize = Vector2.zero, endSize = Vector2.zero;
        bool animateRectSize = useRectTransformSize && rt != null;
        bool animateSpriteSize = useSpriteRendererSize && sr != null;

        if (animateRectSize)
        {
            if (startFromCurrent) startSize = rt.sizeDelta;
            else startSize = GetRectLikeSize(origin as RectTransform, tMove, fallbackFromTransform: true);

            endSize = GetRectLikeSize(destination as RectTransform, tMove, fallbackFromTransform: true);
        }
        else if (animateSpriteSize && sr.drawMode != SpriteDrawMode.Simple)
        {
            if (startFromCurrent) startSize = sr.size;
            else startSize = GetSpriteLikeSize(sr, origin, tMove);

            endSize = GetSpriteLikeSize(sr, destination, tMove);
        }
        // Caso nenhum modo seja usado, não mexemos em tamanho (scale fica 1).

        // ---- Eventos de início ----
        IsMoving = true;
        onMoveStart?.Invoke();
        MoveStarted?.Invoke();

        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            float e = easing != null ? easing.Evaluate(t) : t;

            // Posição
            Vector3 pos = Vector3.Lerp(startPos, endPos, e);
            if (useLocalSpace) tMove.localPosition = pos; else tMove.position = pos;

            // Tamanho (mantendo scale=1)
            if (animateRectSize)
            {
                Vector2 sz = Vector2.Lerp(startSize, endSize, e);
                rt.sizeDelta = sz;
            }
            else if (animateSpriteSize && sr.drawMode != SpriteDrawMode.Simple)
            {
                Vector2 sz = Vector2.Lerp(startSize, endSize, e);
                sr.size = sz;
            }

            // Rotação contínua com voltas (sem shortest path)
            float z = Mathf.Lerp(startZ, endZContinuous, e);
            SetZRotation(tMove, z);

            yield return null;
        }

        // Snap final
        if (useLocalSpace) tMove.localPosition = endPos; else tMove.position = endPos;

        if (animateRectSize) (tMove as RectTransform).sizeDelta = endSize;
        else if (animateSpriteSize && sr.drawMode != SpriteDrawMode.Simple) sr.size = endSize;

        // Garante término exatamente no Z do destino
        SetZRotation(tMove, endZ);

        IsMoving = false;
        moveRoutine = null;

        onMoveComplete?.Invoke();
        MoveCompleted?.Invoke();

        //Matém a marcação correta da estrela
        if (ceu)
            ceu = false;
        else
            ceu = true;

        //Marca o posicionamento e Faz a Troca de Destino e Origin Caso Selecionado
        if (setIdaEVolta)
        {
            Transform parse = origin;
            origin = destination;
            destination = parse;
        }
    }

    // --- Helpers ---
    Transform GetTarget() => targetToMove != null ? targetToMove : transform;

    bool ValidateRefs()
    {
        if (origin == null || destination == null)
        {
            Debug.LogWarning("[TranslateScaleSpin2D] Defina 'origin' e 'destination'.");
            return false;
        }
        if (targetToMove == null) targetToMove = transform;
        return true;
    }

    float GetZ(Transform t) => useLocalSpace ? t.localEulerAngles.z : t.eulerAngles.z;

    void SetZRotation(Transform t, float zDegrees)
    {
        // rotação contínua: aplicamos o valor e deixamos o Unity normalizar internamente
        if (useLocalSpace) t.localRotation = Quaternion.Euler(0f, 0f, zDegrees);
        else t.rotation = Quaternion.Euler(0f, 0f, zDegrees);
    }

    /// <summary>
    /// Gera um ângulo final contínuo que garante 'spinCount' voltas completas
    /// e termina no mesmo valor visual do endZ. Evita o 'shortest path' do LerpAngle.
    /// </summary>
    float ComputeContinuousEndAngle(float startZ, float endZ, int spins)
    {
        // Normaliza para [0,360)
        float s = Mathf.Repeat(startZ, 360f);
        float e = Mathf.Repeat(endZ, 360f);

        if (spins >= 0)
        {
            // garante que o destino contínuo esteja à frente do start
            while (e < s) e += 360f;
            e += 360f * spins;
        }
        else
        {
            // garante que o destino contínuo esteja "atrás" do start
            while (e > s) e -= 360f;
            e += 360f * spins; // spins negativo soma voltas para trás
        }
        // Retorna no mesmo “domínio” contínuo do start
        float continuousStart = startZ; // já pode vir fora de [0,360)
        float baseDelta = e - s;
        return continuousStart + baseDelta;
    }

    // --- Utilitários de tamanho (W/H) ---
    Vector2 GetRectLikeSize(RectTransform rt, Transform fallback, bool fallbackFromTransform)
    {
        if (rt != null) return rt.sizeDelta;
        if (!fallbackFromTransform) return Vector2.zero;

        // Fallback simples: usa escala * sprite bounds se existir
        var sr = GetTarget().GetComponent<SpriteRenderer>();
        if (sr != null && sr.sprite != null)
        {
            var bounds = sr.sprite.bounds.size;
            // pixels para unidades já está embutido em 'bounds'; tamanho final = bounds * scale (mas scale=1)
            return new Vector2(bounds.x, bounds.y);
        }
        // Último recurso: usa 1x1
        return Vector2.one;
    }

    Vector2 GetSpriteLikeSize(SpriteRenderer sr, Transform refT, Transform holder)
    {
        // Se drawMode for Simple, size não é aplicável – retornamos o tamanho atual do sprite.
        if (sr.drawMode == SpriteDrawMode.Simple)
        {
            if (sr.sprite != null) return sr.sprite.bounds.size;
            return Vector2.one;
        }

        // Se existir um SpriteRenderer nos refs, podemos derivar pelo scale (que é 1) e size atual
        if (refT != null)
        {
            var refSR = refT.GetComponent<SpriteRenderer>();
            if (refSR != null && refSR.drawMode != SpriteDrawMode.Simple) return refSR.size;
        }

        // Fallback: usa size atual
        return sr.size;
    }
}

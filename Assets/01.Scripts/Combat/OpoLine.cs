using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpoLine : PoolableMono
{
    [SerializeField] private Material _lineMat;
    private LineRenderer _opoLineRenderer;

    public void StartOpoLine(Vector3 startPos, Vector3 point)
    {
        Vector3 ranndPoint = point + Random.insideUnitSphere * 0.2f;

        Material mat = new Material(_lineMat);
        _opoLineRenderer.material = mat;

        _opoLineRenderer.SetPosition(0, startPos);
        _opoLineRenderer.SetPosition(1, ranndPoint);

        mat.SetFloat("_Alpha", 0.3f);
        mat.DOFloat(0f, "_Alpha", 0.2f).OnComplete(() => PoolManager.Instance.Push(this));
    }

    public override void OnPop()
    {
        if(_opoLineRenderer == null)
            _opoLineRenderer = GetComponent<LineRenderer>();
    }

    public override void OnPush()
    {
    }
}

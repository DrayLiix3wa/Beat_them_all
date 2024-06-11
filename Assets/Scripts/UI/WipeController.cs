using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WipeController : MonoBehaviour
{
    private Animator _animator;
    private Image _image;
    private readonly int _CircleSizeId = Shader.PropertyToID("_CircleSize");

    public float circleSize = 0;

    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _image = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        _image.materialForRendering.SetFloat(_CircleSizeId, circleSize);
    }

    public void WipeIn()
    {
        _animator.SetTrigger("In");
    }

    public void WipeOut()
    {
        _animator.SetTrigger("Out");
    }
}

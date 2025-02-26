using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class FireLightController : MonoBehaviour
{
    private Light _light;
    [SerializeField, Range(0, 10)] private int _speed;
    [SerializeField, TwoWayRange(0, 3)] private Vector2 _intensity;

    private int _timer;
    
    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _timer++;
        
        if(_timer >= 10 - _speed){
            _light.intensity = Random.Range(_intensity.x, _intensity.y);
            _timer = 0;
        }
    }
}

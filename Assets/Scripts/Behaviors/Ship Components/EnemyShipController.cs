using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component defines the behavior and actions of a non-player-controlled ship.
/// </summary>
[RequireComponent(typeof(ShipBHV))]
public class EnemyShipController : MonoBehaviour
{
    [System.Serializable]
    public struct DirectionalTrajectory
    {
        public float amplitude;
        public float length;
        public AnimationCurve curve;
        public bool loop;
    }

    public Vector2 reference = -Vector2.up;
    public DirectionalTrajectory verticalTrajectory;
    public DirectionalTrajectory horizontalTrajectory;
    public float pace = 0.01f;
    public float hFrame = 0;
    public float vFrame = 0;

    private ShipBHV ship;
    [SerializeField]
    private float frame = 0;

    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<ShipBHV>();
    }

    // Update is called once per frame
    void Update()
    {
        //Shooting
        ship.Fire1(); // tries to shoot

        //Movement
        float distance = 0;
        float nextFrame = frame;
        hFrame = frame / horizontalTrajectory.length;
        vFrame = frame / verticalTrajectory.length;
        //Loop if true
        if (horizontalTrajectory.loop)
        {
            hFrame %= 1;
        }
        if (verticalTrajectory.loop)
        {
            vFrame %= 1;
        }
        Vector3 target = Vector3.zero;
        while (distance < ship.speed && nextFrame - frame <= 1)
        {
            nextFrame += pace;
            float nextHFrame = nextFrame / horizontalTrajectory.length;
            float nextVFrame = nextFrame / verticalTrajectory.length;
            //Loop if true
            if (horizontalTrajectory.loop)
            {
                nextHFrame %= 1;
            }
            if (verticalTrajectory.loop)
            {
                nextVFrame %= 1;
            }
            //Calculates offset
            target = new Vector2(horizontalTrajectory.curve.Evaluate(nextHFrame), verticalTrajectory.curve.Evaluate(nextVFrame));
            target -= new Vector3(horizontalTrajectory.curve.Evaluate(hFrame), verticalTrajectory.curve.Evaluate(vFrame));
            target = new Vector2(target.x * horizontalTrajectory.amplitude, target.y * verticalTrajectory.amplitude);
            //target = new Vector2( (horizontalTrajectory.curve.Evaluate(nextFrame / horizontalTrajectory.length) - horizontalTrajectory.curve.Evaluate(frame / horizontalTrajectory.length)) * horizontalTrajectory.amplitude,
            //    verticalTrajectory.curve.Evaluate(nextFrame / verticalTrajectory.length) - verticalTrajectory.curve.Evaluate(frame / verticalTrajectory.length) * verticalTrajectory.amplitude);
            distance = target.magnitude;
        }
        transform.position += target.normalized * ship.speed;
        frame = nextFrame;
    }
}

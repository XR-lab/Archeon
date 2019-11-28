using UnityEngine;

namespace UnityMovementAI
{
    public class ColAvoidUnit : MonoBehaviour
    {

        SteeringBasics steeringBasics;
        CollisionAvoidance colAvoid;

        NearSensor colAvoidSensor;

        void Start()
        {
            steeringBasics = GetComponent<SteeringBasics>();
            colAvoid = GetComponent<CollisionAvoidance>();

            colAvoidSensor = transform.Find("ColAvoidSensor").GetComponent<NearSensor>();
        }

        void FixedUpdate()
        {
            Vector3 accel = colAvoid.GetSteering(colAvoidSensor.targets);

            if (accel.magnitude < 0.005f)
            {
                //accel = followPath.GetSteering(path);
            }

            steeringBasics.Steer(accel);
            steeringBasics.LookWhereYoureGoing();
        }
    }
}
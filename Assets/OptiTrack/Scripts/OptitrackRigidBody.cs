//======================================================================================================
// Copyright 2016, NaturalPoint Inc.
//======================================================================================================

using System;
using UnityEngine;


public class OptitrackRigidBody : MonoBehaviour
{
    public OptitrackStreamingClient StreamingClient;
    public Int32 RigidBodyId;


    void Start()
    {
        // If the user didn't explicitly associate a client, find a suitable default.
        if ( this.StreamingClient == null )
        {
            this.StreamingClient = OptitrackStreamingClient.FindDefaultClient();

            // If we still couldn't find one, disable this component.
            if ( this.StreamingClient == null )
            {
                Debug.LogError( GetType().FullName + ": Streaming client not set, and no " + typeof( OptitrackStreamingClient ).FullName + " components found in scene; disabling this component.", this );
                this.enabled = false;
                return;
            }
        }
    }


#if UNITY_2017_1_OR_NEWER
    void OnEnable()
    {
        Application.onBeforeRender += OnBeforeRender;
    }


    void OnDisable()
    {
        Application.onBeforeRender -= OnBeforeRender;
    }


    void OnBeforeRender()
    {
        UpdatePose();
    }
#endif


    void Update()
    {
        UpdatePose();
    }


    void UpdatePose()
    {
        OptitrackRigidBodyState rbState = StreamingClient.GetLatestRigidBodyState( RigidBodyId );
        if ( rbState != null )
        {
            Quaternion newRotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
            Vector3 newRot = rbState.Pose.Orientation.eulerAngles;
            newRot.y *= -1;
            newRot.z *= -1;
            this.transform.localPosition = rbState.Pose.Position;
            this.transform.localRotation = rbState.Pose.Orientation;
            //transform.localRotation *= newRotation;
        }
    }

    public Quaternion OptiRotation()
    {
        if (StreamingClient != null)
        {
            OptitrackRigidBodyState rbState = StreamingClient.GetLatestRigidBodyState(RigidBodyId);
            return rbState.Pose.Orientation;
        }
        else
        {
            Debug.Log("no se encontro el streaming client de optitrack");
            return Quaternion.identity;
        }
    }

    public Vector3 OptiPos()
    {
        if(StreamingClient != null)
        {
            OptitrackRigidBodyState rbState = StreamingClient.GetLatestRigidBodyState(RigidBodyId);
            Vector3 newOptiPos = rbState.Pose.Position;
            //newOptiPos.z = newOptiPos.z * -1;
            newOptiPos.x = newOptiPos.x * -1;
            return newOptiPos;
        }
        else
        {
            Debug.Log("no se encontro el streaming client de optitrack");
            return Vector3.zero;
        }
    }
}

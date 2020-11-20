using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativePos : MonoBehaviour {

    [SerializeField]
    GameObject _realGlasses;

    [SerializeField]
    OptitrackRigidBody _optiGlasses;

    [SerializeField]
    Vector3 _offSet;

    [SerializeField]
    Vector3 _rotationOffSet;

    OptitrackRigidBody _optiPos;

	void Start () {
        _optiPos = GetComponent<OptitrackRigidBody>();
        transform.localPosition = _realGlasses.transform.localPosition;
    }
	void Update () {
        Vector3 newPos = _optiGlasses.gameObject.transform.InverseTransformVector(_optiPos.OptiPos());
        newPos.z = -newPos.z;
        newPos = newPos + _offSet;
        transform.localPosition = newPos;
        Quaternion newRotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
        Vector3 correctOptiRotation = _optiPos.OptiRotation().eulerAngles;
        //correctOptiRotation.y *= -1;
        correctOptiRotation.z *= -1;
        correctOptiRotation.x *= -1;
        //_optiPos.OptiRotation().eulerAngles = Quaternion.Euler(newRotation);
        transform.localRotation = Quaternion.Euler(correctOptiRotation);
        transform.localRotation *= newRotation;
    }
}
using UnityEngine;
using Cinemachine;

[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")] // Hide in menu
public class CMLockCamera : CinemachineExtension
{
    public enum Axis
    {
        NoLock,
        XAxis,
        YAxis,
        ZAxis
    }

    public Axis lockOnAxis;
    public float XPosition;
    public float YPosition;
    public float ZPosition;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (lockOnAxis == Axis.NoLock) return;

        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            switch (lockOnAxis)
            {
                case Axis.XAxis:      
                    pos.x = XPosition;
                    state.RawPosition = pos;
                    break;
                case Axis.YAxis:
                    pos.y = YPosition;
                    state.RawPosition = pos;
                    break;
                case Axis.ZAxis:
                    pos.z = ZPosition;
                    state.RawPosition = pos;
                    break;
            }
        }
    }
}

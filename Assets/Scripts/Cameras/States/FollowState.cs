using System;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Orpul1.Camera
{
    public class FollowState : CameraState
    {
        public FollowState(CameraContext data, Action<int> transitionTo) : base(data, transitionTo) {}
        
        public override void OnStart()
        {

        }

        public override void OnUpdate()
        {
            if (IsOverBoundary()) return;
            Vector2 dir = ((Vector2)data.target.position - data.rigid.position);
            data.rigid.MovePosition(data.rigid.position + dir * data.moveSpeed*Time.deltaTime);
            
        }

        public override void OnExit()
        {
        }

        private bool IsOverBoundary()
        {
            if (IsOverLeftSide()) return true;
            if (IsOverRightSide()) return true;
            if (IsOverTopSide()) return true;
            if (IsOverBottomSide()) return true;
            return false;
        }
        private bool IsOverLeftSide()
        {
            if (data.viewBoundary[(int)EViewPosi.StartPosi].position.x <= data.transform.position.x - data.cameraView.x) return false;
            float moveX = data.viewBoundary[(int)EViewPosi.StartPosi].position.x + data.cameraView.x + 0.01f;
            data.transform.position = new Vector3(moveX,data.transform.position.y, data.transform.position.z);
            return true;
        }
        private bool IsOverRightSide()
        {
            if (data.viewBoundary[(int)EViewPosi.EndPosi].position.x >= data.transform.position.x + data.cameraView.x) return false;
            float moveX = data.viewBoundary[(int)EViewPosi.EndPosi].position.x - data.cameraView.x - 0.01f;
            data.transform.position = new Vector3(moveX, data.transform.position.y, data.transform.position.z);
            return true;
        }
        private bool IsOverTopSide()
        {
            if (data.viewBoundary[(int)EViewPosi.EndPosi].position.y >= data.transform.position.y + data.cameraView.y) return false;
            float moveY = data.viewBoundary[(int)EViewPosi.EndPosi].position.y - data.cameraView.y - 0.01f;
            data.transform.position = new Vector3(data.transform.position.x, moveY, data.transform.position.z);
            return true;
        }
        private bool IsOverBottomSide()
        {
            if (data.viewBoundary[(int)EViewPosi.StartPosi].position.y <= data.transform.position.y - data.cameraView.y) return false;
            float moveY = data.viewBoundary[(int)EViewPosi.StartPosi].position.y + data.cameraView.y + 0.01f;
            data.transform.position = new Vector3(data.transform.position.x, moveY, data.transform.position.z);
            return true;
        }
    }
}
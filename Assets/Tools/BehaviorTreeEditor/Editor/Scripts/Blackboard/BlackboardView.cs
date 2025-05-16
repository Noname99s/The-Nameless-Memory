
using SOBehaviorTree;
using TreeEditor;
using UnityEditor;
using UnityEngine;
namespace SOBehaviorTree
{
    [InitializeOnLoad]
    public class BlackboardView
    {
        ScriptableObject blackboard;
        BlackboardCreator blackboardCreator = new BlackboardCreator();

        public BlackboardView()
        {
            DispalyBlackboardCreator();
        }
        public void SetBlackboard(string treePath)
        {
            blackboardCreator.SetTreePath(treePath);
            DispalyBlackboardCreator();
        }
        public ScriptableObject GetBlackboard()
        {
            return blackboard;
        }
        
        //TODO : 블랙보드 윈도우를 통해 블랙보드 이름 정하기
        public void CreateBlackboard()
        {
            if (!blackboard)
                new BlackBoardWindow(blackboardCreator.CreateBlackboard);
            else
                Debug.Log("not Null Blackboard");
        }
        private void DispalyBlackboardCreator()
        {
            blackboard = blackboardCreator.GetBlackboard();
            if (blackboard != null)
                new BlackBoardWindow(blackboardCreator.CreateBlackboard);
        }

    }
}

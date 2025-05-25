
using SOBehaviorTree;
using System;
using System.IO;
using System.Reflection;
using TreeEditor;
using UnityEditor;
using UnityEngine;
using static BlackboardCreator;
namespace SOBehaviorTree
{
    [InitializeOnLoad]
    public class BlackboardView
    {
        ScriptableObject blackboard = new ScriptableObject();
        BlackboardCreator blackboardCreator = new BlackboardCreator();

        BehaviorTree tree;
        public BlackboardView(BehaviorTree tree)
        {
            this.tree = tree;
            blackboardCreator.SetTreePath(tree.TreePath);
        }
        
        public void SetBlackboard()
        {
            //1. 블랙보드 있는지 확인
            // 있으면 리턴
            //2. 블랙보드 경로가 있는지 확인
            // 있으면 에셋 가져오고 리턴
            //3. 블랙보드 생성
            if (tree.blackboard)
            {
                tree.blackboardName = tree.blackboard.name;
                return;
            }
            SetBlackboardFromPath(tree);
            CreateBlackboard();
        }
        private void SetBlackboardFromPath(BehaviorTree tree)
        {
            if (tree.BlackboardPath == "" || string.IsNullOrEmpty(tree.BlackboardPath)) return;
            tree.blackboard = AssetDatabase.LoadAssetAtPath<ScriptableObject>(tree.BlackboardPath);
        }
        public ScriptableObject GetBlackboard()
        {
            return blackboard;
        }
        public ScriptableObject GetBlackboard(string path)
        {
            blackboard = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
            return blackboard;
        }
        //TODO : 블랙보드 윈도우를 통해 블랙보드 이름 정하기
        public void CreateBlackboard()
        {
            if (tree.blackboard) return;
            

            blackboard = blackboardCreator.CreateBlackboard(tree);

            if (!blackboard)
            {
                new BlackBoardWindow(blackboardCreator.CreateBlackboard);
            }
            else
            {
                tree.blackboard = blackboard;
                tree.blackboardName = blackboard.name;
                Debug.Log("not Null Blackboard");
            }
        }

    }
}

//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;
//using UnityEngine.UI;

//public class TicTacToe : MonoBehaviour
//{
//    Node root;
//    int countLeaf = 0;

//    [SerializeField] GameObject[] visualBoard;

//    void Start()
//    {
//        root = new Node();

//        PlayAI();
//    }

//    void PlayAI()
//    {
//        root.children.Clear();
//        countLeaf = 0;
//        GenerateTree(root);

//        if(countLeaf == 1)
//        {
//            Debug.Log("NUL");
//        }

//        int minMax = MinMax(root);
//        root = root.children.First(x => x.value == minMax);

//        UpdateBoard();

//        if (root.IsAligned() == Node.State.Yellow_Token)
//        {
//            Debug.Log("VICTOIRE AI");
//        }
//    }

//    void UpdateBoard()
//    {
//        for (int i = 0; i < 3; i++)
//        {
//            for (int j = 0; j < 3; j++)
//            {
//                Text text = visualBoard[i * 3 + j].GetComponentInChildren<Text>();
//                switch (root.board[i,j])
//                {
//                    case Node.State.Empty:
//                        text.text = "";
//                        break;
//                    case Node.State.Red_Token:
//                        text.text = "X";
//                        break;
//                    case Node.State.Yellow_Token:
//                        text.text = "O";
//                        break;
//                    default:
//                        break;
//                }
//            }
//        }
//    }


//    void GenerateTree(Node _node)
//    {
//        if(_node.IsAligned() != Node.State.Empty)
//        {
//            countLeaf++;
//            //if (countLeaf % 10000 == 0)
//            //{
//            //    Debug.Log(_node);
//            //}
//            return;
//        }

//        bool hasChild = false;
//        for (int i = 0; i < 3; i++)
//        {
//            for (int j = 0; j < 3; j++)
//            {
//                if(_node.board[i, j ] == Node.State.Empty)
//                {
//                    Node newNode = new Node();
//                    Array.Copy(_node.board, newNode.board, 9);
//                    newNode.isOpponent = !_node.isOpponent;
//                    newNode.board[i, j] = newNode.isOpponent ? Node.State.Yellow_Token : Node.State.Red_Token;
//                    _node.children.Add(newNode);

//                    hasChild = true;
//                    GenerateTree(newNode);
//                }
//            }
//        }

//        if(!hasChild)
//        {
//            countLeaf++;

//            //if(countLeaf % 10000 == 0)
//            //{
//            //    Debug.Log(_node);
//            //}
//        }
//    }

//    //int MinMax(Node _node)
//    //{
//    //    if(_node.children.Count == 0)
//    //    {
//    //        _node.value = _node.Evaluate();
//    //        return _node.value;
//    //    }

//    //    if(_node.isOpponent)
//    //    {
//    //        int min = int.MaxValue;
//    //        for (int i = 0; i < _node.children.Count; i++)
//    //        {
//    //            int tmp = MinMax(_node.children[i]);
//    //            min = Math.Min(min, tmp);
//    //        }
//    //        _node.value = min;
//    //        return min;
//    //    }
//    //    else
//    //    {
//    //        int max = int.MinValue;
//    //        for (int i = 0; i < _node.children.Count; i++)
//    //        {
//    //            int tmp = MinMax(_node.children[i]);
//    //            max = Math.Max(max, tmp);
//    //        }
//    //        _node.value = max;
//    //        return max;
//    //    }
//    //}

//    public void Play(int _id)
//    {
//        if(root.board[_id / 3, _id % 3] != Node.State.Empty)
//        {
//            return;
//        }

//        root.board[_id / 3, _id % 3] = Node.State.Red_Token;
//        root.isOpponent = false;

//        UpdateBoard();

//        if (root.IsAligned() == Node.State.Red_Token)
//        {
//            Debug.Log("VICTOIRE JOUEUR");
//        }
//        else
//        {
//            PlayAI();
//        }
//    }
//}

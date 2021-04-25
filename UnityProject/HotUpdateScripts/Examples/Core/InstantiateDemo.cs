﻿//
// InstantiateDemo.cs
//
// Author:
//       JasonXuDeveloper（傑） <jasonxudeveloper@gmail.com>
//
// Copyright (c) 2021 JEngine
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using UnityEngine;
using JEngine.Core;

namespace JEngine.Examples
{
    public class InstantiateDemo:MonoBehaviour
    {
        private static bool IsFirst = true;

        public void Awake()
        {
            if (IsFirst)
            {
                IsFirst = false;
                var prefab = new JPrefab("InstantiateDemo",(result,prefab)=>
                {
                    Log.Print("开始Instantiate");

                    Log.Print("先测试Instantiate (Object original)，只有一个参数，为gameObject");
                    GameObject g1 = Object.Instantiate(prefab.Instance);
                    g1.name = "g1";

                    Log.Print("开始测 Instantiate (Object original, Transform parent)，parent是g1");
                    GameObject g2 = Object.Instantiate(prefab.Instance, parent: g1.transform);
                    g2.name = "g2";

                    Log.Print("开始测有Instantiate (Object original, Transform parent, bool instantiateInWorldSpace)，parent是g2,worldPositionStays=true");
                    GameObject g3 = Object.Instantiate(prefab.Instance, parent: g2.transform, true);
                    g3.name = "g3";

                    Log.Print("开始测有Instantiate (Object original, Vector3 position, Quaternion rotation)，position是g3.transform.position,rotation是new Quaternion(0,0,0,0)");
                    GameObject g4 = Object.Instantiate(prefab.Instance, position: g3.transform.position, new Quaternion(0, 0, 0, 0));
                    g4.name = "g4";

                    Log.Print("开始测有Instantiate (Object original, Vector3 position, Quaternion rotation, Transform parent)，position是g4.transform.position,rotation是new Quaternion(0,0,0,0), parent是g2");
                    GameObject g5 = Object.Instantiate(prefab.Instance, position: g4.transform.position, rotation: new Quaternion(0, 0, 0, 0), parent: g2.transform);
                    g5.name = "g5";

                    Log.Print("开始测试复制物体g1，命名为c1");
                    GameObject c1 = Object.Instantiate(g4);
                    c1.name = "c1";

                    Log.Print("现在测试赋值热更工程的Component，复制的物体为c1，命名为c2");
                    var c2 = Object.Instantiate<InstantiateDemo>(c1.GetComponent<InstantiateDemo>());
                    Log.Print(c2);
                });
                return;
            }
        }

        private void Start()
        {
            if(!IsFirst)
                Log.Print($"我被Instantiate了，我叫: {gameObject.name}");
        }
    }
}
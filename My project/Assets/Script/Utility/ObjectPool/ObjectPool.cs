using System;
using System.Collections.Generic;

namespace Script.Utility.ObjectPool
{
    public abstract class ObjectPoolBase
    {
        public string name { protected set;  get; }
        public abstract void Destory();
    }

    public interface IObjectPoolElement
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialization();
        /// <summary>
        /// 回收时执行
        /// </summary>
        public void Recycle();
    }
    
    public class ObjectPool<T> : ObjectPoolBase where T : IObjectPoolElement, new()
    {
        public T type { private set; get; }

        private Queue<T> _using = new();
        private Queue<T> _spare = new();

        public ObjectPool(string name)
        {
            this.name = name;
            this.type = type;
        }

        /// <summary>
        /// 对象池销毁
        /// 不要在外部直接调用，请使用ObjectPoolManager.Deregister
        /// </summary>
        public override void Destory()
        {
            while (_using.Count > 0)
            {
                _using.Dequeue().Recycle();
            }

            _using.Clear();
            _using = null;
            _spare.Clear();
            _spare = null;
        }

        /// <summary>
        /// 增加缓冲对象 
        /// 如果有预期的需要使用大量对象，且备用对象数量不够，可以提前使用该函数
        /// </summary>
        /// <param name="size"></param>
        public void AddBuffer(int size)
        {
            for (var i = 0; i < size; i++)
            {
                CreateElement();
            }
        }

        public T Apply()
        {
            _spare.TryDequeue(out var result);
            if (result == null)
            {
                result = CreateElement();
            }

            _using.Enqueue(result);
            return result;
        }

        private T CreateElement()
        {
            var result = new T();
            result.Initialization();
            _spare.Enqueue(result);
            return result;
        }
    }
}
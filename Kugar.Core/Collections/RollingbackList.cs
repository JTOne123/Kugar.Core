using System;
using System.ComponentModel;

namespace Kugar.Core.Collections
{
    /// <summary>
    ///     һ�������ڻ��������б�
    ///     �����ǣ�Ԥ��ָ���б��С��ʹ��Add�����������ݣ������ݵ���������֮�󣬻Ὣ��ǰ������ɾ��
    ///     ���ã�����ʵʱ���ߵ�ʱ�����ݵ�����ݴ洢
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RollingbackList<T> : BindingList<T>
    {
        private int _maxCount = 0;

        public RollingbackList(int maxCount)
        {
            if (maxCount <= 1)
            {
                throw new ArgumentOutOfRangeException(@"maxCount",@"�����������С�ڻ����1");
            }

            _maxCount = maxCount;
        }

        protected override void OnListChanged(ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                if (base.Count > _maxCount)
                {
                    lock (this)
                    {
                        var temp = base[0];

                        base.RemoveAt(0);

                        if (temp is IDisposable)
                        {
                            (temp as IDisposable).Dispose();
                        }
                    }
                }
            }

            base.OnListChanged(e);
        }

        public int MaxLength { get { return _maxCount; } }

        public T FirstNode
        {
            get
            {
                return base[0];
            }
        }

        public T LastNode
        {
            get
            {
                return base[base.Count - 1];
            }
        }

        public T[] ToArray()
        {
            var lst = new T[this.Count];

            lock (this)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    lst[i] = this[i];
                }
            }


            return lst;
        }

    }
}

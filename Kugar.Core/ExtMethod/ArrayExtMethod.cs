using System;

namespace Kugar.Core.ExtMethod
{
    public static class ArrayExtMethod
    {
        /// <summary>
        ///    �������ƶ� <br/>
        ///   �������е�ĳ�������sourceIndex�ƶ���desIndex��ʼ������
        /// </summary>
        /// <param name="array">Դ����</param>
        /// <param name="sourceIndex">Ҫ�ƶ����������ʼ����</param>
        /// <param name="moveCount">�ƶ���������</param>
        /// <param name="desIndex">�ƶ���Ŀ������</param>
        /// <returns></returns>
        
        public static int MoveItems(this Array array, int sourceIndex, int moveCount, int desIndex)
        {
            if (array==null || sourceIndex < 0 || desIndex < 0 || sourceIndex > array.Length || desIndex > array.Length || sourceIndex + moveCount > array.Length || desIndex + moveCount > array.Length || sourceIndex == desIndex)
            {
                throw new ArgumentOutOfRangeException("array");
            }

            var moveCountPerOne = 0; //ÿ���ƶ��������С

            moveCountPerOne = Math.Min(Math.Abs(sourceIndex - desIndex), moveCount);

            if (moveCountPerOne<=0)
            {
                return -1;
            }

            //���һ���Կɸ������,��ֱ�ӵ���Copy����
            if (moveCountPerOne >= moveCount)
            {
                lock (array)
                {
                    Array.Copy(array,sourceIndex,array,desIndex,moveCountPerOne);
                }

                return desIndex;
            }

            var needCopyCount = moveCount;  //�ܹ���Ҫ���Ƶ�������
            var tempsrcIndex = sourceIndex;   //��ʱ����ʼλ��ָ��
            var tempdesIndex = desIndex;        //��ʱ�Ľ���λ��ָ��
            var posDirection = 1;                       //ָ����ƶ�����,1Ϊ��ǰ,-1Ϊ���

            if (sourceIndex < desIndex)   //���sourceIndex��desIndexС.��Ӻ���ǰ����
            {
                tempsrcIndex = sourceIndex + moveCount - moveCountPerOne;   
                tempdesIndex = desIndex + moveCount - moveCountPerOne;
                posDirection = -1;  //ָ������ƶ�
            }

            lock (array)
            {
                while (needCopyCount > 0)
                {
                    Array.Copy(array, tempsrcIndex, array, tempdesIndex, moveCountPerOne);

                    needCopyCount -= moveCountPerOne;
                    tempsrcIndex += (posDirection * moveCountPerOne);
                    tempdesIndex += (posDirection * moveCountPerOne);
                }
            }

            return desIndex;
        }
    }

    public static class ArraySegmentExtMethod
    {
        /// <summary>
        ///     ����Ƭ���Ƿ�Ϊ��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool HasData<T>(this ArraySegment<T> data)
        {
            if (data.Array==null)
            {
                return false;
            }

            if (data.Array.IsInEnableRange(data.Offset,data.Count))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
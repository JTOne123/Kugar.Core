using System;

namespace Kugar.Core.Exceptions
{
    public class ObjectNotNullEnableException:System.Exception
    {
        private const string errorMsg = "{0}����ֵ����Ϊ��";
        public ObjectNotNullEnableException(string paramName) : base(string.Format(errorMsg,paramName)){}
    }
}
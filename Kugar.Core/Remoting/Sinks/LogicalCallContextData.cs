using System;
using System.Runtime.Remoting.Messaging;

namespace Kugar.Core.Remoting.Sinks
{
	/// <summary>
	/// LogicalCallContextData ��ժҪ˵����
	/// </summary>
	[Serializable]
	public class LogicalCallContextData:ILogicalThreadAffinative
	{
		private string m_Data;

		public LogicalCallContextData(string data)
		{
			m_Data=data;
		}

		public string Data
		{
			get
			{
				return m_Data;
			}
		}
	}
}

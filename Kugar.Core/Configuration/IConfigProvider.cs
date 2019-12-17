using System.Collections.Generic;

namespace Kugar.Core.Configuration
{
    /// <summary>
    ///     ��������ؼ������ṩ��
    /// </summary>
    /// <remarks>
    ///     XML��д������ʹ�ã� <br/>
    ///     ���ݿ��д���������Kugar.Core.CustomConfig.DatabaseProvider��
    /// </remarks>
    public interface ICustomConfigProvider
    {
        IEnumerable<CustomConfigItem> Load();
        bool Write(IEnumerable<CustomConfigItem> configList);

    }

    /// <summary>
    ///     �������ýڵ�֧����,��֧������ȡ�������������л�
    /// </summary>
    public interface ILocalCustomConfigProvider : ICustomConfigProvider { }
}

using System.Collections.Generic;

namespace Kugar.Core.Printer
{
    public interface IDetailWithDynamicColumnsCollection
    {
        Dictionary<string, ColumnConfig> DynamicColumns { get; }

        /// <summary>
        ///     ����̬���е������б仯ʱ����
        /// </summary>
        event DynamicColumnValueChanged DynamicColumnValueChanged;

        /// <summary>
        ///     ������������,��δ��ӵĶ�̬����,���������¼�
        /// </summary>
        event DynamicColumnChanged<DynamicColumn> DynamicColumnChanged;
    }
}
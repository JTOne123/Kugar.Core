using System;

namespace Kugar.Core.Printer
{
    /// <summary>
    ///     ��ӡ�ֶζ�����
    /// </summary>
    public class PrintElementItem
    {
        /// <summary>
        ///     ��ӡ�ֶε�Ψһ����
        /// </summary>
        public virtual string Name { set; get; }

        /// <summary>
        ///     ��ӡ�ֶα�ͷ
        /// </summary>
        public virtual string HeaderText { set; get; }

        /// <summary>
        ///     ��ӡ�ֶ���������
        /// </summary>
        public virtual Type DataType { set; get; }

        /// <summary>
        ///     ��ӡ�ֶε�Դ�ֶ�
        /// </summary>
        public virtual string BindingColumn { set; get; }

        /// <summary>
        ///     ���BindingColumnΪ��,����ʾ������ֵ
        /// </summary>
        public virtual string Text { set; get; }

        /// <summary>
        ///     ��ӡ�ֶ���ʾ������
        /// </summary>
        public virtual PrintElementDisplayType DisplayType { set; get; }

        public bool IsDynamicColumn { set; get; }

        public override bool Equals(object obj)
        {
            return Name == ((PrintElementItem)obj).Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public PrintElementItem Copy()
        {
            var pe = new PrintElementItem();

            pe.BindingColumn = this.BindingColumn;
            pe.HeaderText = this.HeaderText;
            pe.Name = this.Name;
            pe.DataType = this.DataType;
            pe.Text = this.Text;
            pe.DisplayType = this.DisplayType;

            return pe;
        }

    }
}
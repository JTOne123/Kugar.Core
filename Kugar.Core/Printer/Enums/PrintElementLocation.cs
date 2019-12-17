namespace Kugar.Core.Printer
{
    /// <summary>
    ///     �ֶ�����λ��<br/>
    /// </summary>
    /// <remarks>
    ///     �ñ�ʶλ��,�ڱ�ʶ������Ϊһ��Class����ʱ,,Class�µ������ֶ������λ�ö���
    /// </remarks>
    public enum PrintElementLocation
    {
        /// <summary>
        ///     ָ����Ԫ�ص�λ���ڱ���ͷ
        /// </summary>
        ReportHead,

        /// <summary>
        ///     ָ����Ԫ�ص�λ����ÿҳ��ҳͷ
        /// </summary>
        PageHead,

        /// <summary>
        ///     ָ����Ԫ�ص�λ����ÿҳ��ҳ��
        /// </summary>
        PageFooter,

        /// <summary>
        ///     ָ����Ԫ�ص�λ���ڱ����
        /// </summary>
        ReportFooter,

        /// <summary>
        ///     ָ����Ԫ������ϸ
        /// </summary>
        DetailInside,
    }
}
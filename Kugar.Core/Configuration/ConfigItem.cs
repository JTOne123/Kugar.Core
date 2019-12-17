using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Kugar.Core.ExtMethod;

namespace Kugar.Core.Configuration
{

    public enum CustomConfigItemStatus
    { 
        Normal,
        Modify
    }

    /// <summary>
    ///     ������
    /// </summary>
    [Serializable]
    public class CustomConfigItem : INotifyPropertyChanged
    {
        private object _value = null;

        [NonSerialized]
        private CustomConfigItemStatus _status = CustomConfigItemStatus.Normal;

        public CustomConfigItem()
        {
            ConfigType= CustomConfigLevel.System;
        }

        public CustomConfigItem(string name,ConfigItemDataType dataType,object value=null)
        {
            this.Name = name;
            this.DataType = dataType;
            _value =value.Cast(GetDataType());
        }

        /// <summary>
        ///     ����������
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        ///     �������ֵ,��������ֵ�������ͱȽ�,<br/>
        ///    ��������Ϊ��DataType���͵�ֵ,��᳢��ת������,��ϸ��ת�����Ͳο�ObjectAbout.Cast����
        /// </summary>
        /// <see cref="ObjectAbout.Cast"/>
        public object Value
        {
            get { return _value; }

            set
            {

                if (!_value.SafeEquals(value))
                {
                    _value = value;
                    Status = CustomConfigItemStatus.Modify;
                    OnPropertyChanged(Name);
                }

            }

        }

        /// <summary>
        ///     �����������
        /// </summary>
        public ConfigItemDataType DataType { set; get; }

        public CustomConfigLevel ConfigType { private set; get; }

        /// <summary>
        ///     ����������ֵ��Ӧ��Type
        /// </summary>
        /// <returns></returns>
        public Type GetDataType()
        {
            Type type = null;

            switch (DataType)
            {
                case ConfigItemDataType.Int:
                    type = typeof(int);
                    break;
                case ConfigItemDataType.Decimal:
                    type = typeof(decimal);
                    break;
                case ConfigItemDataType.String:
                    type = typeof(string);
                    break;
                case ConfigItemDataType.Boolean:
                    type = typeof(bool);
                    break;
                default:
                    type = typeof(object);
                    break;

            }

            return type;
        }


        /// <summary>
        ///     ��ǰ���õ�״̬
        /// </summary>
        public CustomConfigItemStatus Status { get; private set; }

        public void SetStatusToNormal()
        {
            Status = CustomConfigItemStatus.Normal;
        }

        [NonSerialized]
        private PropertyChangedEventHandler _propertyChanged=null;

        
        public event PropertyChangedEventHandler PropertyChanged
        {
            add{
                if (_propertyChanged==null)
                {
                    _propertyChanged = value;
                }
                else
                {
                    lock (_propertyChanged)
                    {
                        _propertyChanged += value;
                    }
                }
                
                
            }
            remove{
                if (_propertyChanged==null)
                {
                    return;
                }
                lock (_propertyChanged)
                {
                    _propertyChanged -= value;
                }                
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            //PropertyChangedEventHandler handler = PropertyChanged;
            if (_propertyChanged != null) _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public enum CustomConfigLevel
    {
        System,
        User
    }
}
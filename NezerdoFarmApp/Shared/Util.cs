namespace NezerdoFarmApp.Shared;

public class Util
{
    public class EnumStringConverter<TEnum> where TEnum : struct, Enum
    {
        private string _stringValue;

        public string Converter
        {
            get => _stringValue;
            set
            {
                if (Enum.TryParse(typeof(TEnum), value, out var enumValue))
                {
                    _stringValue = value;
                    EnumVal = (TEnum)enumValue;
                }
                else
                {
                    throw new ArgumentException($"Invalid value for enum type {typeof(TEnum).Name}");
                }
            }
        }

        public TEnum EnumVal
        {
            get => Enum.Parse<TEnum>(_stringValue);
            set => _stringValue = value.ToString();
        }
    }

}
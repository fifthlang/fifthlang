namespace Fifth.TypeSystem.PrimitiveTypes
{
    using System.Collections.Generic;
    using Fifth.PrimitiveTypes;

    public class PrimitiveList : PrimitiveGeneric
    {
        public PrimitiveList(TypeId typeParameter)
            : base(false, false, "list", typeParameter)
        {
        }

        public List<object> List { get; private set; }

        public IValueObject Head() => GetItemAt(0);

        public PrimitiveList Tail()
            => new(GenericTypeParameters[0]) {List = List.GetRange(1, List.Count - 1), TypeId = TypeId};

        private IValueObject GetItemAt(int i)
        {
            if (List.Count > i)
            {
                return new ValueObject(GenericTypeParameters[0], string.Empty, List[i]);
            }

            return default;
        }
    }
}

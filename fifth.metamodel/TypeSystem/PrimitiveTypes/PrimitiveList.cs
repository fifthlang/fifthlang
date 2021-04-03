namespace Fifth.TypeSystem.PrimitiveTypes
{
    using System;
    using System.Collections.Generic;
    using Fifth.PrimitiveTypes;
    using TypeSystem;

    public class PrimitiveList : PrimitiveGeneric
    {
        public PrimitiveList(TypeId typeParameter)
            : base(false, false, "list", typeParameter)
        {
        }

        public List<object> List { get; private set; }

        public IValueObject Head() => GetItemAt(0);

        public PrimitiveList Tail()
            => new PrimitiveList(TypeParameters[0]) { List = List.GetRange(1, List.Count - 1), TypeId = TypeId };

        private IValueObject GetItemAt(int i)
        {
            if (List.Count > i)
            {
                return new ValueObject(TypeParameters[0], String.Empty, List[i]);
            }

            return default;
        }
    }
}

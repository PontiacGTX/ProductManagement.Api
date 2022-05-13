using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class CollectionValidationAttribute:ValidationAttribute
    {
        public  int MinValueCount { get; set; } 

        public CollectionValidationAttribute()
        {
            ErrorMessage = $"Collection should contain at least {(MinValueCount !=0 ? $"{MinValueCount}":"1")}  elements";
        }
        public override bool IsValid(object value)
        {
            bool isValid = false;
            switch(value)
            {
                case IList<int> i:
                    isValid = i.Count >= MinValueCount;
                    break;
                case IEnumerable<int> iEnum:
                    isValid = iEnum.Count() >=MinValueCount;
                    break;
            }
            return base.IsValid(value);
        }
    }
}

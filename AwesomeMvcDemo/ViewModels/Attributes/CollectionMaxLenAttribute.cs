﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AwesomeMvcDemo.ViewModels.Attributes
{
    public class CollectionMaxLenAttribute : ValidationAttribute
    {
        private readonly int maxLen;

        public CollectionMaxLenAttribute(int maxLen)
        {
            this.maxLen = maxLen;
        }

        public override string FormatErrorMessage(string name)
        {
            return "max " + maxLen + " items";
        }

        public override bool IsValid(object value)
        {
            var list = value as IEnumerable<int>;

            return list == null || (list.Count() <= maxLen);
        }
    }
}
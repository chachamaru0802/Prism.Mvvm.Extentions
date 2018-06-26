using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Prism.Mvvm.Extentions
{
    public static class ValidatablePropertyExtentions
    {
        public static ValidatableProperty<T> InitValidateAttributes<T>(this ValidatableProperty<T> property, Expression<Func<ValidatableProperty<T>>> expression)
        {
            var memberExpression = (MemberExpression)expression.Body;
            var propertyInfo = (PropertyInfo)memberExpression.Member;
            var attrs = propertyInfo.GetCustomAttributes<ValidationAttribute>().ToArray();

            if (attrs.Length != 0)
            {
                property.SetValidateAttributes(attrs);
            }

            return property;
        }
    }
}

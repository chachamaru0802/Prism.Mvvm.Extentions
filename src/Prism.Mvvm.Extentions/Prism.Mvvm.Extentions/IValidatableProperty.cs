using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace Prism.Mvvm.Extentions
{
    public interface IValidatableProperty<T> : INotifyDataErrorInfo
    {
        T Value { get; set; }
        string ErrorMessage { get; }

        IReadOnlyList<string> Errors { get; }

        void ValidateProperty(T value, [CallerMemberName] string propertyName = null);

        void SetValidateAttributes(ValidationAttribute[] attrs);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DataValidator = System.ComponentModel.DataAnnotations.Validator;

namespace Prism.Mvvm.Extentions
{
    public class ValidatableProperty<T> : BindableBase, IValidatableProperty<T>
    {
        #region プロパティ

        public string ErrorMessage => _erros?.FirstOrDefault();

        private T _value;
        public T Value
        {
            get { return _value; }
            set
            {
                SetProperty(ref _value, value);
                ValidateProperty(value);
            }
        }

        List<string> _erros;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IReadOnlyList<string> Errors => _erros;

        public bool HasErrors => _erros.Count != 0;

        #endregion

        #region 変数

        ValidationAttribute[] _validationAttributes;

        #endregion

        #region コンストラクタ

        public ValidatableProperty()
        {
            _erros = new List<string>();
        }

        #endregion

        #region メソッド

        public void ValidateProperty(T value, [CallerMemberName] string propertyName = null)
        {
            var context = new ValidationContext(this) { MemberName = propertyName };
            var validationErrors = new List<ValidationResult>();

            if (!DataValidator.TryValidateValue(value, context, validationErrors, _validationAttributes))
            {
                var errors = validationErrors.Select(error => error.ErrorMessage);
                _erros = errors.ToList();
            }
            else
            {
                _erros.Clear();
            }


            RaisePropertyChanged(nameof(ErrorMessage));
            RaisePropertyChanged(nameof(Errors));
            RaisePropertyChanged(nameof(HasErrors));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _erros;
        }

        public void SetValidateAttributes(ValidationAttribute[] attrs)
        {
            _validationAttributes = attrs;
        }

        #endregion

    }
}

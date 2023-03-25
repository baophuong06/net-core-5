using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Web.ViewModel.Common
{
  public  class ApiErrorResult<T> : ApiResult<T>
    {
        public string[] ValidationErrors { get; set; }

        public ApiErrorResult()
        {
            IsSuccessed = false;
           // Message = "";
           // ValidationErrors = "";
        }

        public ApiErrorResult(string message)
        {
            IsSuccessed = false;
            Message = message;
        }

        public ApiErrorResult(string[] validationErrors)
        {
            IsSuccessed = false;
            ValidationErrors = validationErrors;
        }
    }
}

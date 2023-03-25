using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Web.ViewModel.Common
{
   public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult(T resultObj)
        {
            IsSuccessed = true;
            ResultObj = resultObj;
            //Message = message;
        }

        public ApiSuccessResult()
        {
            IsSuccessed = true;
        }
    }
}

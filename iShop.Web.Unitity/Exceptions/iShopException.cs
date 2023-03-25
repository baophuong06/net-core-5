using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Web.Unitity.Exceptions
{
  public  class IShopException:Exception
    {
        public IShopException()
        {
        }

        public IShopException(string message)
            : base(message)
        {
        }

        public IShopException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

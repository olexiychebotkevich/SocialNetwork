using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetworkMvc.Util
{
    public class NinjectRegistrations
    {
        public override void Load()
        {
            Bind<IServiceCreator>().To<BookRepository>();
        }
    }
}